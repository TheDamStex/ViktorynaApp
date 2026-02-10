using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ViktorynaApp.Services
{
    // Відповідає тільки за роботу з JSON-файлом: читання, запис, базові операції.
    public class JsonStorage<T> : IDataStorage<T>
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _options;
        private static readonly Dictionary<string, string> LegacyFileMap = new(StringComparer.OrdinalIgnoreCase)
        {
            ["korystuvachi.json"] = "data_korysnyky.json",
            ["pytannia.json"] = "data_pytannia.json",
            ["rezultaty.json"] = "data_rezultaty.json"
        };

        public JsonStorage(string fileName)
        {
            string dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            Directory.CreateDirectory(dataFolder);

            _filePath = Path.Combine(dataFolder, fileName);
            _options = new JsonSerializerOptions { WriteIndented = true };
            MigrateLegacyFile(fileName, dataFolder);
        }

        public List<T> Load()
        {
            if (!File.Exists(_filePath))
            {
                Save(new List<T>());
                return new List<T>();
            }

            try
            {
                string json = File.ReadAllText(_filePath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<T>();
                }

                return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new InvalidOperationException($"Доступ до файлу '{_filePath}' заборонено.", ex);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Файл '{_filePath}' містить пошкоджений JSON.", ex);
            }
            catch (IOException ex)
            {
                throw new InvalidOperationException($"Не вдалося прочитати файл '{_filePath}'.", ex);
            }
        }

        public void Save(List<T> data)
        {
            try
            {
                string json = JsonSerializer.Serialize(data, _options);
                ZapysatyAtomarno(_filePath, json);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new InvalidOperationException($"Доступ до файлу '{_filePath}' заборонено.", ex);
            }
            catch (IOException ex)
            {
                throw new InvalidOperationException($"Не вдалося записати файл '{_filePath}'.", ex);
            }
        }

        public void Add(T item)
        {
            var data = Load();
            data.Add(item);
            Save(data);
        }

        public bool Update(Func<T, bool> predicate, Func<T, T> updateFunc)
        {
            var data = Load();
            int index = data.FindIndex(item => predicate(item));
            if (index < 0)
            {
                return false;
            }

            data[index] = updateFunc(data[index]);
            Save(data);
            return true;
        }

        public bool Delete(Func<T, bool> predicate)
        {
            var data = Load();
            int removed = data.RemoveAll(item => predicate(item));
            if (removed == 0)
            {
                return false;
            }

            Save(data);
            return true;
        }

        // Запис через тимчасовий файл захищає дані від пошкодження при збоях.
        private void MigrateLegacyFile(string fileName, string dataFolder)
        {
            if (!LegacyFileMap.TryGetValue(fileName, out string? legacyName))
            {
                return;
            }

            string legacyPath = Path.Combine(dataFolder, legacyName);
            if (!File.Exists(legacyPath))
            {
                return;
            }

            if (!File.Exists(_filePath))
            {
                File.Move(legacyPath, _filePath);
                return;
            }

            string legacyJson = File.ReadAllText(legacyPath);
            string targetJson = File.ReadAllText(_filePath);

            if (string.IsNullOrWhiteSpace(legacyJson) || legacyJson.Trim() == "[]")
            {
                File.Delete(legacyPath);
                return;
            }

            if (string.IsNullOrWhiteSpace(targetJson) || targetJson.Trim() == "[]")
            {
                File.Copy(legacyPath, _filePath, true);
                File.Delete(legacyPath);
                return;
            }

            try
            {
                var legacyItems = JsonSerializer.Deserialize<List<JsonElement>>(legacyJson) ?? new List<JsonElement>();
                var targetItems = JsonSerializer.Deserialize<List<JsonElement>>(targetJson) ?? new List<JsonElement>();

                if (legacyItems.Count == 0)
                {
                    File.Delete(legacyPath);
                    return;
                }

                var combined = new List<JsonElement>(targetItems.Count + legacyItems.Count);
                combined.AddRange(targetItems);
                combined.AddRange(legacyItems);

                string mergedJson = JsonSerializer.Serialize(combined, _options);
                ZapysatyAtomarno(_filePath, mergedJson);
                File.Delete(legacyPath);
            }
            catch (JsonException)
            {
                string backupPath = legacyPath + ".legacy";
                File.Move(legacyPath, backupPath, true);
            }
        }

        private static void ZapysatyAtomarno(string filePath, string json)
        {
            string tempPath = filePath + ".tmp";
            File.WriteAllText(tempPath, json);
            File.Move(tempPath, filePath, true);
        }
    }
}

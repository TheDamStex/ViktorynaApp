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

        public JsonStorage(string fileName)
        {
            string dataFolder = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Data");
            Directory.CreateDirectory(dataFolder);

            _filePath = Path.Combine(dataFolder, fileName);
            _options = new JsonSerializerOptions { WriteIndented = true };
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
                ZapysatyAtomarno(json);
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
        private void ZapysatyAtomarno(string json)
        {
            string tempPath = _filePath + ".tmp";

            File.WriteAllText(tempPath, json);
            File.Move(tempPath, _filePath, true);
        }
    }
}

// Services/JsonDaniService.cs (обновленная версия)
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ViktorynaApp.Services
{
    public class JsonDaniService<T> : IDaniService<T> where T : class
    {
        private string shliakhDoFailu;
        private List<T>? _cachedData;
        private DateTime _lastCacheTime;

        public JsonDaniService(string imiaFailu)
        {
            string dataFolder = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Data");
            Directory.CreateDirectory(dataFolder);

            shliakhDoFailu = Path.Combine(dataFolder, imiaFailu);

            if (!File.Exists(shliakhDoFailu))
                File.WriteAllText(shliakhDoFailu, "[]");
        }

        public List<T> Zavantazhyty()
        {
            // Кешування на 5 секунд
            if (_cachedData != null && (DateTime.Now - _lastCacheTime).TotalSeconds < 5)
                return _cachedData;

            string json = File.ReadAllText(shliakhDoFailu);
            _cachedData = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            _lastCacheTime = DateTime.Now;

            return _cachedData;
        }

        public void Zberehty(List<T> dani)
        {
            string json = JsonSerializer.Serialize(dani, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(shliakhDoFailu, json);

            // Оновлюємо кеш
            _cachedData = dani;
            _lastCacheTime = DateTime.Now;
        }

        // Новий метод для асинхронної роботи
        public async System.Threading.Tasks.Task<List<T>> ZavantazhytyAsync()
        {
            string json = await File.ReadAllTextAsync(shliakhDoFailu);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        public async System.Threading.Tasks.Task ZberehtyAsync(List<T> dani)
        {
            string json = JsonSerializer.Serialize(dani, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            await File.WriteAllTextAsync(shliakhDoFailu, json);
        }
    }
}
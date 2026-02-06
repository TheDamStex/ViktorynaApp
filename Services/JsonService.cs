using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ViktorynaApp.Services
{
    public class JsonService<T>
    {
        private string filePath;

        public JsonService(string relativePath)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            string projectRoot = Path.Combine(baseDir, "..", "..", "..");

            string dataDir = Path.Combine(projectRoot, "Data");
            Directory.CreateDirectory(dataDir);

            filePath = Path.Combine(dataDir, relativePath);

            if (!File.Exists(filePath))
                File.WriteAllText(filePath, "[]");
        }

        public List<T> Load()
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        public void Save(List<T> data)
        {
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}

using System.Collections.Generic;

namespace ViktorynaApp.Services
{
    // Невелика перевірка для ручного запуску під час налагодження.
    public static class JsonStorageCheck
    {
        public static bool ProstaPerevirka()
        {
            var storage = new JsonStorage<string>("demo_storage_check.json");

            // Записуємо тестові дані і одразу читаємо їх назад.
            storage.Save(new List<string> { "перевірка" });
            var loaded = storage.Load();

            return loaded.Count == 1 && loaded[0] == "перевірка";
        }
    }
}

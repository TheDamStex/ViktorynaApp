// Services/JsonDaniService.cs
using System;
using System.Collections.Generic;

namespace ViktorynaApp.Services
{
    public class JsonDaniService<T> : IDaniService<T> where T : class
    {
        private readonly IDataStorage<T> _storage;
        private List<T>? _cachedData;
        private DateTime _lastCacheTime;

        public JsonDaniService(string imiaFailu)
        {
            _storage = new JsonStorage<T>(imiaFailu);
        }

        public JsonDaniService(IDataStorage<T> storage)
        {
            _storage = storage;
        }

        public List<T> Zavantazhyty()
        {
            // Кешування на 5 секунд, щоб не читати файл занадто часто.
            if (_cachedData != null && (DateTime.Now - _lastCacheTime).TotalSeconds < 5)
                return _cachedData;

            _cachedData = _storage.Load();
            _lastCacheTime = DateTime.Now;

            return _cachedData;
        }

        public void Zberehty(List<T> dani)
        {
            _storage.Save(dani);

            // Оновлюємо кеш після успішного запису.
            _cachedData = dani;
            _lastCacheTime = DateTime.Now;
        }
    }
}

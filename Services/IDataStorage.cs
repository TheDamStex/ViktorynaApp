using System;
using System.Collections.Generic;

namespace ViktorynaApp.Services
{
    // Простий контракт для зберігання колекції у файлі.
    public interface IDataStorage<T>
    {
        List<T> Load();
        void Save(List<T> data);
        void Add(T item);
        bool Update(Func<T, bool> predicate, Func<T, T> updateFunc);
        bool Delete(Func<T, bool> predicate);
    }
}

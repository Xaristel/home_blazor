using System;
using System.Collections.Generic;

namespace home_blazor.Data
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        IEnumerable<T> GetAll();
        T GetByID(int id);
        bool Add(T item);
        void AddRange(List<T> list);
        void Delete(T item);
        void Update(T item);
        void Save();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.DataBaseContext
{
    public interface IDatarepository<T> : IDisposable
        where T : class
    {
        void Insert(T item);
        void Update(T item);
        IQueryable<T> Select(int id);
        void Delete(int id);
    }
}

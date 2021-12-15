using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.Classes;

namespace ControlDantist.DataBaseContext
{

    public class RepositoryНаселенныйПункт : IDatarepository<ТНаселённыйПункт>, IFind<ТНаселённыйПункт>
    {
        private DContext dContext;
        public RepositoryНаселенныйПункт()
        {
            dContext = new DContext(ConnectDB.ConnectionString());
        }

        public RepositoryНаселенныйПункт(DContext dContext)
        {
            this.dContext = dContext;
        }


        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Добавляет новый населенный пункт в БД.
        /// </summary>
        /// <param name="item"></param>
        public void Insert(ТНаселённыйПункт item)
        {
            dContext.ТабНаселенныйПункт.Add(item);

            dContext.SaveChanges();
        }

        public IQueryable<ТНаселённыйПункт> Select(int id)
        {
            return dContext.ТабНаселенныйПункт.Where(w => w.id_насПункт == id);
        }

        public IQueryable<ТНаселённыйПункт> Select(string name)
        {
            return dContext.ТабНаселенныйПункт.Where(w => w.Наименование == name);
        }

        public void Update(ТНаселённыйПункт item)
        {
            throw new NotImplementedException();
        }
    }
}

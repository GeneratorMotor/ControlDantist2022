using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace ControlDantist.DataBaseContext
{
    public class RepositoryТипДокумента : IDatarepository<ТТипДокумент>,IFind<ТТипДокумент>
    {
        private DContext dc;
        private DataRow row;

        public RepositoryТипДокумента(DContext dc)
        {
            this.dc = dc;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Insert(ТТипДокумент item)
        {
            throw new NotImplementedException();
        }


        public IQueryable<ТТипДокумент> Select(string name)
        {
            return dc.ТабТипДокумент.Where(w => w.НаименованиеТипаДокумента.Trim().ToLower() == name.Trim().ToLower());
        }

        public IQueryable<ТТипДокумент> Select(int id)
        {
            throw new NotImplementedException();
        }


        public void Update(ТТипДокумент item)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.DataBaseContext
{
    class RepositoryУслугиПоДоговору : IDatarepository<ТУслугиПоДоговору>
    {
        private DContext dc;

        public RepositoryУслугиПоДоговору(DContext dc)
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

        public void Insert(ТУслугиПоДоговору item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ТУслугиПоДоговору> Select(int id)
        {
            return dc.ТУслугиПоДоговору.Where(w => w.id_договор == id);
        }

        public void Update(ТУслугиПоДоговору item)
        {
            throw new NotImplementedException();
        }
    }
}

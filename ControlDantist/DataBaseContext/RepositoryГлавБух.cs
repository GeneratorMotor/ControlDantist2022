using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.DataBaseContext
{
    public class RepositoryГлавБух : IDatarepository<ТГлавБух>
    {
        private DContext dc;

        public RepositoryГлавБух(DContext dc)
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

        public void Insert(ТГлавБух item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ТГлавБух> Select(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ТГлавБух item)
        {
            throw new NotImplementedException();
        }
    }
}

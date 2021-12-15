using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.DataBaseContext
{
    public class RepositoryФиоГлавВрач : IDatarepository<ТФиоГлавВрач>, IFind<ТФиоГлавВрач>
    {
        private DContext dc;

        public RepositoryФиоГлавВрач(DContext dc)
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

        public void Insert(ТФиоГлавВрач item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ТФиоГлавВрач> Select(int id)
        {
            throw new NotImplementedException();
        }

        // Поиск глав врача по ИНН поликлиннике.
        public IQueryable<ТФиоГлавВрач> Select(string name)
        {
            return this.dc.ТФиоГлавВрач.Where(w => w.ИНН_поликлинники.Trim() == name.Trim());
        }

        public void Update(ТФиоГлавВрач item)
        {
            throw new NotImplementedException();
        }
    }
}

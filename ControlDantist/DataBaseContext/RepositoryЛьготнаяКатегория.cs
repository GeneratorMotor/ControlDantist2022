using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.DataBaseContext
{
    public class RepositoryЛьготнаяКатегория : IDatarepository<ТЛьготнаяКатегория>, IFind<ТЛьготнаяКатегория>
    {
        private DContext dc;
        private string льготнаяКатегория = string.Empty;

        public RepositoryЛьготнаяКатегория(DContext dc, string льготнаяКатегория)
        {
            this.dc = dc;
            this.льготнаяКатегория = льготнаяКатегория;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Insert(ТЛьготнаяКатегория item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ТЛьготнаяКатегория> Select(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Возвращает список льгоотных категорий.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<ТЛьготнаяКатегория> Select(string name)
        {
            return dc.ТабЛьготнаяКатегория.Where(w => w.ЛьготнаяКатегория == name);
        }

        public void Update(ТЛьготнаяКатегория item)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.DataBaseContext
{
    public class RepozitoryЛьготник : IDatarepository<ТЛЬготник>
    {
        private DContext dc;
        private string firstName = string.Empty;
        private string name = string.Empty;
        private string surName = string.Empty;
        private DateTime dateBirth;

        public RepozitoryЛьготник(DContext dc, string firstName, string name, string surName, DateTime dateBirth)
        {
            this.dc = dc;
            this.firstName = firstName;
            this.name = name;
            this.surName = surName;
            this.dateBirth = dateBirth;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Insert(ТЛЬготник item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ТЛЬготник> Select(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ТЛЬготник> Select(string firstName, string name, string surName, DateTime dateBerd)
        {
            return dc.ТабЛьгоготник.Where(w => w.Фамилия == firstName && w.Имя == name && w.Отчество == surName && w.ДатаРождения == dateBerd);
        }

        public void Update(ТЛЬготник item)
        {
            throw new NotImplementedException();
        }
    }
}

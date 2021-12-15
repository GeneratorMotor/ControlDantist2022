using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ControlDantist.DataBaseContext;


namespace ControlDantist.ReadRegistrProject
{
    public class ReadЛьготнаяКатегория : IReadRegistr<ТЛьготнаяКатегория>
    {
        private DContext dc;
        private string tabLK = string.Empty;

        public ReadЛьготнаяКатегория(DContext dc, string tabLK)
        {
            this.dc = dc;
            this.tabLK = tabLK;
        }

        public ТЛьготнаяКатегория Get()
        {
            // Экземпляр типа данных Льготная категория.
            ТЛьготнаяКатегория тЛьготнаяКатегория = new ТЛьготнаяКатегория();

            if (tabLK != "")
            {
                var tlk = dc.ТабЛьготнаяКатегория.Where(w => w.ЛьготнаяКатегория.Trim().ToLower() == tabLK.Trim().ToLower()).FirstOrDefault();

                if(tlk != null)
                {
                    тЛьготнаяКатегория.id_льготнойКатегории = tlk.id_льготнойКатегории;
                    тЛьготнаяКатегория.ЛьготнаяКатегория = tlk.ЛьготнаяКатегория.Trim();
                }
            }

            return тЛьготнаяКатегория;
        }
    }
}

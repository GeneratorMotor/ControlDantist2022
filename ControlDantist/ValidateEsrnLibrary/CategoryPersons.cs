using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.DataBaseContext;

namespace ControlDantist.ValidateEsrnLibrary
{

    /// <summary>
    /// Возвращает льготную категорию из реестар выгрузки.
    /// </summary>
    public class CategoryPersons
    {
        static CategoryPersons categoryPersons;

        private List<ItemLibrary> list;

        public CategoryPersons(List<ItemLibrary> list)
        {
            this.list = list;
        }

        // Возвращает наименование льготной категории.
        public string GetPreferentCategory()
        {
            return list?[0].Packecge?.тЛьготнаяКатегория?.ЛьготнаяКатегория ?? "Категория_не_установлена";
        }
    }
}

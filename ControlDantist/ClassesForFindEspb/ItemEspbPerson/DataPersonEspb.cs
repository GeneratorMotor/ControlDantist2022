using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.ClassesForFindEspb.ItemEspbPerson
{
    /// <summary>
    /// Вспомогательный класс описывающий документы в ЕСПБ.
    /// </summary>
    public class DataPersonEspb 
    {
        /// <summary>
        /// GUID персонального дела льготника.
        /// </summary>
        public string PC_Guid { get; set; }

        public string СерияДокумента { get; set; }
        public string НомерДокумента { get; set; }
        public DateTime ДатаДокумента { get; set; }
        public string НазваниеДокумента { get; set; }
        public string Категория { get; set; }
    }
}

using ControlDantist.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.ClassessForDB
{
    /// <summary>
    /// Вспомогательный класс содержащий данные о льготникае из реестра.
    /// </summary>
    public class PersonContract : IЛьготник, INumContract
    {
        public string Фамилия { get; set ; }
        public string Имя { get ; set ; }
        public string Отчество { get ; set ; }
        public DateTime ДатаРождения { get ; set ; }
        public string NumContract { get; set ; }

        public bool FlagFoundLetter { get; set; }
    }
}

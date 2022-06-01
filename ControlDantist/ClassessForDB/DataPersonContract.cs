using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.ClassessForDB
{
    /// <summary>
    /// Вспомогательный класс описывающий списко ранее заключенных договоров.
    /// </summary>
    public class DataPersonContract 
    {
        public string NumContract { get; set; }
        public string DateContract { get; set; }
        public string NumAct { get; set; }
        public string DateAct { get; set; }
        public bool FlagAnulirovan { get; set; }
        public bool ФлагАнулирован { get; set; }
        public bool ФлагПроверки { get; set; }
        public bool ФлагНаличияАкта { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.ReportCountYear
{
    public class CountContractForYear
    {
        public int КоличествоДоговоровБезАкта { get; set; }
        public int КоличествоДоговоровАкт { get; set; }
        public decimal СуммаДоговоровБезАкт { get; set; }
        public decimal СуммаДоговоровАкт { get; set; }
        public int CountДоговоров { get; set; }
        public decimal СуммаДоговоров { get; set; }
        public string ЛьготнаяКатегория { get; set; }

        public decimal ЛимитГод { get; set; }

    }
}

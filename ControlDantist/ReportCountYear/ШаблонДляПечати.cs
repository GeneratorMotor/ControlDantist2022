using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.ReportCountYear
{
    public class ШаблонДляПечати
    {
        public string КоличествоДоговоровБезАкта { get; set; }
        public string КоличествоДоговоровАкт { get; set; }
        public string СуммаДоговоровБезАкт { get; set; }
        public string СуммаДоговоровАкт { get; set; }
        public string CountДоговоров { get; set; }
        public string СуммаДоговоров { get; set; }
        public string ЛьготнаяКатегория { get; set; }

        public string ЛимитГод { get; set; }
    }
}

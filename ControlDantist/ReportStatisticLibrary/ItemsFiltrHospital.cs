using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using ControlDantist.Repozirories;

namespace ControlDantist.ReportStatisticLibrary
{
    public class ItemsFiltrHospital
    {
        private List<ReportYear> list;

        public ItemsFiltrHospital()
        {
            this.list = new List<ReportYear>();
        }

        public List<ReportYear> Add(List<ReportYear> listFiltr)
        {
            this.list.AddRange(listFiltr);

            return this.list;
        }
    }
}

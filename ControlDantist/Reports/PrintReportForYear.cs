using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.Reports;
using ControlDantist.Repozirories;

namespace ControlDantist.Reports
{
    public class PrintReportForYear : PrintReport
    {
        private decimal limitYear = 0;

        public PrintReportForYear(decimal limitYear)
        {
            this.limitYear = limitYear;
        }

        public override void Print(List<ReportYear> listDate)
        {
            // Передадим данные на печать.
            ReportInformToYear report = new ReportInformToYear(this.limitYear);

            report.Print(listDate);
        }
    }
}

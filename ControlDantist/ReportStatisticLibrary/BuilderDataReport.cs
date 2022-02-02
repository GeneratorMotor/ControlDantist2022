using ControlDantist.Repozirories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.ReportStatisticLibrary
{
    public class BuilderDataReport : IBuilderReport
    {
        private List<ReportYear> listDateSaratov;
        private List<ReportYear> listDateCantry;

        public BuilderDataReport()
        {
            listDateSaratov = new List<ReportYear>();
            listDateCantry = new List<ReportYear>();
        }

        public void AddReportCantry(List<ReportYear> list)
        {
            listDateCantry.AddRange(list);
        }

        public void AddReportSaratov(List<ReportYear> list)
        {
            listDateSaratov.AddRange(list);
        }

        public List<ReportYear> GetDataReport()
        {
            List<ReportYear> reportResult = new List<ReportYear>();

            reportResult.AddRange(listDateSaratov);
            reportResult.AddRange(listDateCantry);

            return reportResult;
        }

        public List<ReportYear> GetDataReportSaratov()
        {
            List<ReportYear> reportResult = new List<ReportYear>();

            reportResult.AddRange(listDateSaratov);

            return reportResult;
        }

        //public List<ReportYear> ListItemsReportCantry()
        //{
        //    return listDateSaratov
        //}

        //public List<ReportYear> ListItemsReportSaratov()
        //{
        //    throw new NotImplementedException();
        //}
    }
}

using System.Collections.Generic;
using ControlDantist.Repozirories;

namespace ControlDantist.ReportStatisticLibrary
{
    public interface IBuilderReport
    {
        ///// <summary>
        ///// Список пунктов в отчет Саратов.
        ///// </summary>
        ///// <returns></returns>
        //List<ReportYear> ListItemsReportSaratov();


        List<ReportYear> GetDataReport();

        List<ReportYear> GetDataReportSaratov();
        

        /// <summary>
        /// Добавить список в город саратов.
        /// </summary>
        /// <param name="list"></param>
        void AddReportSaratov(List<ReportYear> list);

        /// <summary>
        /// Добавить в список в область.
        /// </summary>
        /// <param name="list"></param>
        void AddReportCantry(List<ReportYear> list);
    }
}

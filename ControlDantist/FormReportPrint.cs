using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using ControlDantist.Querys;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ControlDantist.Repozirories;
using ControlDantist.Reports;
using ControlDantist.Classes;
using ControlDantist.FactorySqlQuery;
using ControlDantist.ReportStatisticLibrary;
using ControlDantist.ClassessLimitYear;
using ControlDantist.DataTableClassess;

namespace ControlDantist
{
    public partial class FormReportPrint : Form
    {
        private IQueryFactory queryCreatorInformStomatolog;

        private CreateLimitYear cly;

        // Фабрика DataTable.
        private GetTableSQL getTableSql;
        public FormReportPrint(IQueryFactory queryCreatorInformStomatolog)
        {
            InitializeComponent();

            this.queryCreatorInformStomatolog = queryCreatorInformStomatolog;

            cly = new CreateLimitYear();

            // Получим строку подключения к БД.
            getTableSql = new GetTableSQL(ConnectDB.ConnectionString());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

           // Получим SQL строку для получения исходных данных для отчета.
            ICreateSqlQuery qf = queryCreatorInformStomatolog.GetSqlQuery();
            var sqlString = qf.SqlQuery();

            // Получим DataTable данных для отчета.
            DataTable dataReport = ТаблицаБД.GetTableSQL(qf.SqlQuery(), "Report");

            // Конвертор для данных.
            ReportDataToList reportToList = new ReportDataToList();

            // Сконвертируем DataTable в DataList с данными для отчета.
            List<ReportYear>listDataReport = reportToList.ConvertToList(dataReport);

            // Построитель данных для отчета.
            IBuilderReport builderReport = new BuilderDataReport();

            // Для сортировки применим паттерн строитель.
            ItemsFiltrHospital itemsSaratorГаузСМСП = new ItemsFiltrHospital();
            var listГаусСМП = itemsSaratorГаузСМСП.Add(listDataReport.Where(w => w.SerialNumber == 48).ToList());

            builderReport.AddReportSaratov(listГаусСМП);

            // Добавим 2 поликлиннику.
            ItemsFiltrHospital itemsSaratorHosp2 = new ItemsFiltrHospital();
            var listHosp2 = itemsSaratorHosp2.Add(listDataReport.Where(w => w.SerialNumber == 2).ToList());

            builderReport.AddReportSaratov(listHosp2);

            // Добавим 6 поликлиннику.
            ItemsFiltrHospital itemsSaratorHosp6 = new ItemsFiltrHospital();
            var listHosp6 = itemsSaratorHosp6.Add(listDataReport.Where(w => w.SerialNumber == 5).ToList());

            builderReport.AddReportSaratov(listHosp6);

            // Военный госпиталь.
            ItemsFiltrHospital itemsSaratorVHosp = new ItemsFiltrHospital();
            var listVHosp = itemsSaratorVHosp.Add(listDataReport.Where(w => w.SerialNumber == 7).ToList());

            builderReport.AddReportSaratov(listVHosp);

            // Военный госпиталь.
            ItemsFiltrHospital itemsSaratorGord = new ItemsFiltrHospital();
            var listGord = itemsSaratorGord.Add(listDataReport.Where(w => w.SerialNumber == 8).ToList());

            builderReport.AddReportSaratov(listGord);

            // Данные для города Саратова.
            var listDateForSaratov = builderReport.GetDataReport();

            // Данные для облости. Найдем разницу между данными для отчета и данными для Саратова.
            var dateCantry = listDataReport.Except(listDateForSaratov).OrderBy(w => w.SerialNumber).ToList();

            builderReport.AddReportCantry(dateCantry);

            var listDateForReport = builderReport.GetDataReport();

            if (listDataReport != null)
            {

                // Получим текущий год.
                DateTime dt = DateTime.Now;
                int year = dt.Year;

                // Получим запрос на получение года.
                IQuery queryGetYear = cly.GetYear(year);

                // Получим таблицу с данными по лимитам.
                ТаблицаSqlBd tabSqlLimit = getTableSql.GetTable(queryGetYear.Query(), "LimitYear");

                // Получим табличное представление лимитов за год.
                DataTable tabLimit = tabSqlLimit.GetTableSQL();

                if (tabLimit != null && tabLimit.Rows != null && tabLimit.Rows.Count > 0)
                {
                    decimal limit = Convert.ToDecimal(tabLimit.Rows[0]["LimitMoneyYear"]);

                   // Передадим данные для отчета в фабричный метод.
                    PrintReportForYear reportForYear = new PrintReportForYear(limit);
                    reportForYear.Print(listDateForReport);
                }

            }
            else
            {
                MessageBox.Show("Данных для печати нет, или возникла ошибка");
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ControlDantist.Repozirories;
using ControlDantist.Reports;
using ControlDantist.Classes;
using ControlDantist.FactorySqlQuery;

namespace ControlDantist
{
    public partial class FormReportPrint : Form
    {
        private IQueryFactory queryCreatorInformStomatolog;

        public FormReportPrint(IQueryFactory queryCreatorInformStomatolog)
        {
            InitializeComponent();

            this.queryCreatorInformStomatolog = queryCreatorInformStomatolog;
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

            // Коллекция классов описывающая отчет.
            ReportDataToList reportToList = new ReportDataToList();
            List<ReportYear>listDataReport = reportToList.ConvertToList(dataReport);

            if (listDataReport != null)
            {

                // Передадим данные для отчета в фабричный метод.
                PrintReportForYear reportForYear = new PrintReportForYear();
                reportForYear.Print(listDataReport);
               
            }
            else
            {
                MessageBox.Show("Данных для печати нет, или возникла ошибка");
            }

        }
    }
}

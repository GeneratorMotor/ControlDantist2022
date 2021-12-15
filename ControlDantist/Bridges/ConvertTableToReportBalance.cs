using ControlDantist.BalanceContract;
using System;
using System.Collections.Generic;
using System.Data;
using System;

namespace ControlDantist.Bridges
{
    public class ConvertTableToReportBalance : IBridge<ReportBalance>
    {
        private DataTable dataTable;

        public ConvertTableToReportBalance(DataTable dataTable)
        {
            this.dataTable = dataTable ?? throw new ArgumentNullException(nameof(dataTable));
        }

        public List<ReportBalance> ConvertTo()
        {
            List<ReportBalance> list = new List<ReportBalance>();

            foreach (DataRow row in dataTable.Rows)
            {
                ReportBalance it = new ReportBalance();

                // Проверим на null 
                if (row.IsNull("ЛьготнаяКатегория") == false)
                {
                    it.ЛьготнаяКатегория = row["ЛьготнаяКатегория"].ToString().Trim();
                }

                if (row.IsNull("КоличествоДоговоров") == false)
                {
                    it.КоличествоДоговоров = Convert.ToInt32(row["КоличествоДоговоров"]);
                }

                if (row.IsNull("Сумма") == false)
                {
                    it.СуммаДоговоров = Convert.ToDecimal(row["Сумма"]);
                }

                list.Add(it);
            }

            return list;
        }
    }
}

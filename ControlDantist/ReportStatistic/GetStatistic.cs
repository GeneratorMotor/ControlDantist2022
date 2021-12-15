using System;
using System.Collections.Generic;
using ControlDantist.UnpaidStatistic;
using System.Data;
using ControlDantist.Classes;

namespace ControlDantist.ReportStatistic
{
    public static class GetStatistic
    {
        public static IEnumerable<ReportBalanceStatistic> GenerateList(string query)
        {
            DataTable table = ТаблицаБД.GetTableSQL(query, "Balance");

            List<ReportBalanceStatistic> list = new List<ReportBalanceStatistic>();

            if (table != null && table.Rows != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    ReportBalanceStatistic it = new ReportBalanceStatistic();
                    it.ЛьготнаяКатегория = row["ЛьготнаяКатегория"].ToString();
                    it.КоличествоДоговоров = Convert.ToInt32(row["КоличествоДоговоров"]);
                    it.СуммаДоговоров = Convert.ToDecimal(row["Сумма"]);
                    it.НаименованиеПоликлинники = row["НаименованиеПоликлинники"].ToString();

                    list.Add(it);
                }
            }

            return list;
        }
    }
}

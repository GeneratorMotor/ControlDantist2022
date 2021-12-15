using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ControlDantist.Classes;

namespace ControlDantist.ReceptionDocuments
{
    public class ReisedueMoney : IPrintBalance
    {
        // Переменная для хранения льготной категории.
        private string lgotCategory = string.Empty;

        // Переменная для хранения года.
        private int year = 0;

        public ReisedueMoney(string lgotCategory, int year)
        {
            this.lgotCategory = lgotCategory;
            this.year = year;
        }

        public decimal PrintBalance()
        {
            decimal sum = 0.0m;

            string query = @"select Limit from LimittMoney
                            inner join LimitPreferenceCategory
                            on LimittMoney.[idLimitMoney] = LimitPreferenceCategory.idLimitMoney
                            inner join ЛьготнаяКатегория
                            on ЛьготнаяКатегория.[id_льготнойКатегории] = LimitPreferenceCategory.id_льготнойКатегории
                            inner join[Year]
                            on[Year].[intYear] = LimittMoney.idYear
                            where[Year] = " + this.year + " and LOWER(LTRIM(RTRIM(ЛьготнаяКатегория.ЛьготнаяКатегория))) = LOWER(LTRIM(RTRIM('" + lgotCategory + "')))";

            
            DataTable tabLimit = ТаблицаБД.GetTableSQL(query, "Лимит");

            if(tabLimit != null && tabLimit.Rows != null && tabLimit.Rows.Count > 0)
            {
                sum = Convert.ToDecimal(tabLimit.Rows[0]["Limit"]);
            }

            return sum;


        }
    }
}

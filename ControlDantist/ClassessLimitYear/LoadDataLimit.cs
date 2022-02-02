using System;
using System.Collections.Generic;
using System.Data;

namespace ControlDantist.ClassessLimitYear
{
    /// <summary>
    /// Адаптер для DataTable.
    /// </summary>
    public class LoadDataLimit : ILoadData<ItemLimit>
    {
        private DataTable dateTable;

        public LoadDataLimit(DataTable dateTable)
        {
            this.dateTable = dateTable ?? throw new ArgumentNullException(nameof(dateTable));
        }

        /// <summary>
        /// Возвращает список с лимитами за текущий год.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ItemLimit> Load()
        {
            List<ItemLimit> list = new List<ItemLimit>();

            foreach (DataRow row in this.dateTable.Rows)
            {
                ItemLimit item = new ItemLimit();
                item.idLimitYear = Convert.ToInt16(row["idLimitYear"]);
                item.Year = Convert.ToInt32(row["Year"]);
                item.LimitMoneyYear = Convert.ToDecimal(row["LimitMoneyYear"]);

                list.Add(item);
            }

            return list;
        }
    }
}

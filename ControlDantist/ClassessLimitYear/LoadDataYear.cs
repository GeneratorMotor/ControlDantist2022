using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ControlDantist.ClassessLimitYear
{
    public class LoadDataYear : ILoadData<ItemYear>
    {

        private DataTable dateTable;

        public LoadDataYear(DataTable dateTable)
        {
            this.dateTable = dateTable ?? throw new ArgumentNullException(nameof(dateTable));
        }

        public IEnumerable<ItemYear> Load()
        {
            List<ItemYear> list = new List<ItemYear>();

            foreach(DataRow row in this.dateTable.Rows)
            {
                ItemYear item = new ItemYear();
                item.intYear = Convert.ToInt16(row["intYear"]);
                item.Year = Convert.ToInt32(row["Year"]);

                list.Add(item);
            }

            return list;
        }
    }
}

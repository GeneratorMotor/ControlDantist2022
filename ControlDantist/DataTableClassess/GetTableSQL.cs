using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ControlDantist.DataTableClassess
{
    /// <summary>
    /// Фабрика DataTable/
    /// </summary>
    public class GetTableSQL
    {
        private string stringConnection = string.Empty;

        public GetTableSQL(string stringConnection)
        {
            this.stringConnection = stringConnection ?? throw new ArgumentNullException(nameof(stringConnection));
        }

        public ТаблицаSqlBd GetTable(string sqlQuery, string nameTable)
        {
            return new ТаблицаSqlBd(this.stringConnection, sqlQuery, nameTable);
        }
    }
}

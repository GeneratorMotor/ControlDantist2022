using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlDantist.Querys;

namespace ControlDantist.ClassesForFindEspb.QuerysForFindEsrn
{
    public class QueryInsertTempTable : IQuery
    {
        private List<ItemReportEspb> list;
        private string nameTable = string.Empty;

        public QueryInsertTempTable(List<ItemReportEspb> list, string nameTable)
        {
            this.list = list ?? throw new ArgumentNullException(nameof(list));
            this.nameTable = nameTable ?? throw new ArgumentNullException(nameof(nameTable));
        }

        /// <summary>
        /// Заполнение временной таблицы.
        /// </summary>
        /// <returns></returns>
        public string Query()
        {
            // Переменная для хранения строки запроса на заполнение временной таблицы.
            StringBuilder builderQuery = new StringBuilder();

            foreach (var itm in this.list)
            {
                // Guid льготника.
                var guid = itm?.PC_GUID ?? "";

                // Серия документа.
                string snils = itm?.СНИЛС ?? "";

                // Номер документа.
                string numberDoc = itm?.НазваниеДокумента ?? "";

                string queryInsert = " insert into " + this.nameTable + " (PC_GUID,СНИЛС,НазваниеДокумента) " +
                                             " values('" + guid.Trim() + "','" + snils.Trim() + "','" + numberDoc.Trim() + "' ) ";

                builderQuery.Append(queryInsert);
            }

            return builderQuery.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.Querys;

namespace ControlDantist.ValididtyTicket
{
    public class InsertTableЭсрн : IQuery
    {
        private List<string> listBarCod;
        private string nameTable = string.Empty;

        public InsertTableЭсрн(List<string> listBarCod, string nameTable)
        {
            this.listBarCod = listBarCod;
            this.nameTable = nameTable;
        }

        public string Query()
        {
            // Переменная для хранения строки запроса на заполнение временной таблицы.
            StringBuilder builderQuery = new StringBuilder();

            foreach (var itm in this.listBarCod)
            {
                string queryInsert = " insert into " + this.nameTable + " (PC_GUID) " +
                                             " values('" + itm + "' ) ";

                builderQuery.Append(queryInsert);
            }

            return builderQuery.ToString();
        }
    }
}

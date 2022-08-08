using System;
using System.Collections.Generic;
using System.Text;
using ControlDantist.Querys;
using ControlDantist.ClassesForFindEspb.ItemEspbPerson;
using ControlDantist.Classes;

namespace ControlDantist.ClassesForFindEspb
{
    /// <summary>
    /// SQL скрипт заполнения временной таблицы.
    /// </summary>
    public class QueryInsertTempTable : IQuery
    {
        private List<DataPersonEspb> list;
        private string nameTable = string.Empty;

        public QueryInsertTempTable(List<DataPersonEspb> list, string nameTable)
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
                var guid = itm?.PC_Guid?? "";

                // Серия документа.
                string seriesDoc = itm?.СерияДокумента ?? "";

                // Номер документа.
                string numberDoc = itm?.НомерДокумента ?? "";

                // Тип документа.
                string typeDoc = itm?.НазваниеДокумента ?? "";

                // Дата выдачи документа.
                string dateDoc = Время.Дата(itm.ДатаДокумента.ToShortDateString());

                // Льготная категория.
                string lk = itm?.Категория ?? "";

                string queryInsert = " insert into " + this.nameTable + " (PC_GUID,СерияДокумента,НомерДокумента,НазваниеДокумента,ДатаДокумента,Категория) " +
                                             " values('" + guid.Trim() + "','" + seriesDoc.Trim() + "','" + numberDoc.Trim() + "','" + typeDoc.Trim() + "','" + dateDoc.ToLower().Trim() + "','" + lk.Trim() + "' ) ";

                 builderQuery.Append(queryInsert);
            }

            return builderQuery.ToString();
        }
    }
}

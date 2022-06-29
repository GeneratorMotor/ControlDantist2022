using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace ControlDantist.ValidateEsrnLibrary
{
    public class ConvertDTableToList : IConvertor<DatePerson>
    {

        DataTable dataTable;

        public ConvertDTableToList(DataTable dataTable)
        {
            this.dataTable = dataTable ?? throw new ArgumentNullException(nameof(dataTable));
        }

        public List<DatePerson> ConvertDate()
        {
            // Переменная для хранения списка данных о льготнике
            // полученного из ЭСРН.
            List<DatePerson> list = new List<DatePerson>();

            foreach(DataRow row in this.dataTable.Rows)
            {
                DatePerson date = new DatePerson();

                if(!DBNull.Value.Equals(row["Фамилия"]))
                {
                    date.Фамилия = row["Фамилия"].ToString().Trim();
                }
               
                if(!DBNull.Value.Equals(row["Имя"]))
                {
                    date.Имя = row["Имя"].ToString().Trim();
                }

                if (!DBNull.Value.Equals(row["Отчество"]))
                {
                    date.Отчество = row["Отчество"].ToString().Trim();
                }
                else
                {
                    date.Отчество = "";
                }

                if (!DBNull.Value.Equals(row["ДатаРождения"]))
                {
                    date.ДатаРождения = Convert.ToDateTime(row["ДатаРождения"]);
                }

                if (!DBNull.Value.Equals(row["СерияДокумента"]))
                {
                    date.СерияДокумента = row["СерияДокумента"].ToString().Trim();
                }

                if (!DBNull.Value.Equals(row["НомерДокумента"]))
                {
                    date.НомерДокумента = row["НомерДокумента"].ToString().Trim();
                }

                if (!DBNull.Value.Equals(row["ДатаВыдачи"]))
                {
                    date.ДатаВыдачи = Convert.ToDateTime(row["ДатаВыдачи"]);
                }

                if (!DBNull.Value.Equals(row["НаименованиеДокумента"]))
                {
                    date.НаименованиеДокумента = row["НаименованиеДокумента"].ToString().Trim();
                }

                if (!DBNull.Value.Equals(row["Адрес"]))
                {
                    date.Адрес = row["Адрес"].ToString().Trim();
                }

                list.Add(date);

            }

            return list;
        }
    }
}

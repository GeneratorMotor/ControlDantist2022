using System;
using System.Data;
using System.Text;
using ControlDantist.Querys;


namespace ControlDantist.ClassesForFindEspb.Rezult
{
    public class QueryWriteTableResult : IQuery
    {

        private DataTable tabEsrn;
        private string region = string.Empty;

        public QueryWriteTableResult(DataTable tabEsrn, string region)
        {
            this.tabEsrn = tabEsrn ?? throw new ArgumentNullException(nameof(tabEsrn));
            this.region = region ?? throw new ArgumentNullException(nameof(region));
        }

        public string Query()
        {
            // Переменная для хранения скрипта на запись в БД.
            StringBuilder stringBuilder = new StringBuilder();

            foreach (DataRow row in tabEsrn.Rows)
            {

                // НАРУШИЛ SOLID.
                string psGuid = string.Empty;
                string НазваниеДокумента = string.Empty;
                string Фамилия = string.Empty;
                string Имя = string.Empty;
                string Отчество = string.Empty;
                DateTime ДатаРождения;
                string Адрес = string.Empty;
                string Район = string.Empty;
                string Снилс = string.Empty;

                if (!DBNull.Value.Equals(row["GUID"]))
                {
                    psGuid = row["GUID"].ToString();
                }
                else
                {
                    psGuid = "";
                }

                // Проверка название документа.
                if (!DBNull.Value.Equals(row["НазваниеДокумента"]))
                {
                    НазваниеДокумента = row["НазваниеДокумента"].ToString();
                }
                else
                {
                    НазваниеДокумента = "";
                }


                // Фамилия.
                if (!DBNull.Value.Equals(row["Фамилия"]))
                {
                    Фамилия = row["Фамилия"].ToString();
                }
                else
                {
                    Фамилия = "";
                }

                // Имя.
                if (!DBNull.Value.Equals(row["Имя"]))
                {
                    Имя = row["Имя"].ToString();
                }
                else
                {
                    Имя = "";
                }

                // Отчество.
                if (!DBNull.Value.Equals(row["Отчество"]))
                {
                    Отчество = row["Отчество"].ToString();
                }
                else
                {
                    Отчество = "";
                }

                // Дата рождения.
                if (!DBNull.Value.Equals(row["ДатаРождения"]))
                {
                    ДатаРождения = Convert.ToDateTime(row["ДатаРождения"]);
                }
                else
                {
                    DateTime dt = new DateTime(1900, 1, 1);
                    ДатаРождения = dt;
                }

                // Адрес.
                if (!DBNull.Value.Equals(row["Адрес"]))
                {
                    Адрес = row["Адрес"].ToString();
                }
                else
                {
                    Адрес = "";
                }

                // СНИЛС.
                if (!DBNull.Value.Equals(row["СНИЛС"]))
                {
                    Снилс = row["СНИЛС"].ToString();
                }
                else
                {
                    Снилс = "";
                }

                IQuery queryInsert = new QueryInsertRezultDoc(psGuid, Снилс,НазваниеДокумента, Фамилия, Имя, Отчество, ДатаРождения, Адрес, region);
                stringBuilder.Append(queryInsert.Query());
            }

            return stringBuilder.ToString();
        }

        //string WriteTabEspbEsrn(DataTable tabEsrn, string region);
    }
}

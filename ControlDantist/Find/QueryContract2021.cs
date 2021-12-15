using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.Find
{
    public class QueryContract2021 : IQueryFind
    {
        private int id = 0;

        public QueryContract2021(int id)
        {
            this.id = id;
        }

        public string Query()
        {
            // Получим данные по услугам к договору.
            return "SELECT УслугиПоДоговору.НаименованиеУслуги, УслугиПоДоговору.цена, " +
                                   "УслугиПоДоговору.Количество, УслугиПоДоговору.Сумма " +
                                   "FROM Договор INNER JOIN " +
                                   "УслугиПоДоговору ON Договор.id_договор = УслугиПоДоговору.id_договор " +
                                   "where Договор.id_договор = " + this.id + " ";
        }
    }
}

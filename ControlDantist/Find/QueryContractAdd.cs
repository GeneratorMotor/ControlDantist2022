using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.Find
{
    public class QueryContractAdd : IQueryFind
    {
        private int id = 0;

        public QueryContractAdd(int id)
        {
            this.id = id;
        }

        public string Query()
        {
            return "SELECT УслугиПоДоговоруAdd.НаименованиеУслуги, УслугиПоДоговоруAdd.цена, " +
                                           "УслугиПоДоговоруAdd.Количество, УслугиПоДоговоруAdd.Сумма " +
                                           "FROM ДоговорAdd INNER JOIN " +
                                           "УслугиПоДоговоруAdd ON ДоговорAdd.id_договор = УслугиПоДоговоруAdd.id_договор " +
                                           "where ДоговорAdd.id_договор = " + this.id + " ";
        }
    }
}

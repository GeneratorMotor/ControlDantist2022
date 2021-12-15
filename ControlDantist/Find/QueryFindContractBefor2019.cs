using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.Find
{
    public class QueryFindContractBefor2019 : IQueryFind
    {
        private int id = 0;

        public QueryFindContractBefor2019(int id)
        {
            this.id = id;
        }

        public string Query()
        {
            return "SELECT УслугиПоДоговоруАрхив.НаименованиеУслуги, УслугиПоДоговоруАрхив.цена, " +
                                           "УслугиПоДоговоруАрхив.Количество, УслугиПоДоговоруАрхив.Сумма " +
                                           "FROM ДоговорАрхив INNER JOIN " +
                                           "УслугиПоДоговоруАрхив ON ДоговорАрхив.id_договор = УслугиПоДоговоруАрхив.id_договор " +
                                           "where ДоговорАрхив.id_договор = " + this.id + " ";
        }
    }
}

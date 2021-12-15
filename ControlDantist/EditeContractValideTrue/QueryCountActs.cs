using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.Querys;

namespace ControlDantist.EditeContractValideTrue
{
    public class QueryCountActs : IQuery
    {
        private int id_contract;

        public QueryCountActs(int id_contract)
        {
            this.id_contract = id_contract;
        }

        public string Query()
        {
            return @"select COUNT(id_акт),НомерАкта, ДатаАктаВыполненныхРабот from АктВыполненныхРабот
                    inner join Договор
                    on Договор.id_договор = АктВыполненныхРабот.id_договор
                    where Договор.id_договор = "+ this.id_contract +" " +
                    " group by id_акт,НомерАкта,ДатаАктаВыполненныхРабот ";
        }
    }
}

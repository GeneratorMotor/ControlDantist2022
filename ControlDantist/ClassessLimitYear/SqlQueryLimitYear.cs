using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.Querys;

namespace ControlDantist.ClassessLimitYear
{
    public class SqlQueryLimitYear : IQuery
    {
        private int year = 0;

        public SqlQueryLimitYear(int year)
        {
            this.year = year;
        }

        /// <summary>
        /// SQL запрос на получение лимита за год.
        /// </summary>
        /// <returns></returns>
        public string Query()
        {
            return @"SELECT[idLimitYear]
                  ,[Year]
                  ,[LimitMoneyYear]
                  FROM[LimitMoneyYear] where Year = "+ year +" ";
        }
    }
}

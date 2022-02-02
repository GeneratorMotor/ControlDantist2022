using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.Querys;

namespace ControlDantist.ClassessLimitYear
{
    public class GetQueryStringForLimit
    {
        /// <summary>
        /// Sql запрос на получение года.
        /// </summary>
        /// <returns></returns>
        public IQuery GetSqlYear()
        {
            return new SqlQueryYear();
        }

        /// <summary>
        /// SQL Запрос на получение лимита за год.
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public IQuery GetSqlLimitYear(int year)
        {
            return new SqlQueryLimitYear(year);
        }
    }
}

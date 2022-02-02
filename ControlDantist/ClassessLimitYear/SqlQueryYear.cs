using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.Querys;

namespace ControlDantist.ClassessLimitYear
{
    public class SqlQueryYear : IQuery
    {
        /// <summary>
        /// Запрос на получение года.
        /// </summary>
        /// <returns></returns>
        public string Query()
        {
            return "select intYear,Year from Year";
        }
    }
}

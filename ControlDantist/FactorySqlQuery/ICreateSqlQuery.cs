using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.FactorySqlQuery
{
    /// <summary>
    /// Интерфейс описывающий SQL запрос к БД.
    /// </summary>
    public interface ICreateSqlQuery
    {
        /// <summary>
        /// Строка запроса.
        /// </summary>
        /// <returns></returns>
        string SqlQuery();
    }
}

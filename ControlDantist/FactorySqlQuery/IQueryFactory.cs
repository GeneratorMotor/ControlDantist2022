using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.FactorySqlQuery
{
    /// <summary>
    /// Интерфейс описывающий сущьность возвращающий
    /// строку запроса.
    /// </summary>
    public interface IQueryFactory
    {
        ICreateSqlQuery GetSqlQuery();
    }
}

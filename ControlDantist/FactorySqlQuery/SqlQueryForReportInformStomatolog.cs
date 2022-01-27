using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.FactorySqlQuery
{
    /// <summary>
    /// Формирует sql запрос для формирования отчета по стоматлогиям.
    /// </summary>
    public class SqlQueryForReportInformStomatolog : IQueryFactory
    {

        private string strDateStart = string.Empty;
        private string strDateEnd = string.Empty;

        /// <summary>
        /// Инкапсулирует запрос к БД для формирования данных для информации по зубопротезированию.
        /// </summary>
        /// <param name="strDateStart"></param>
        /// <param name="strDateEnd"></param>
        public SqlQueryForReportInformStomatolog(string strDateStart, string strDateEnd)
        {
            this.strDateStart = strDateStart ?? throw new ArgumentNullException(nameof(strDateStart));
            this.strDateEnd = strDateEnd ?? throw new ArgumentNullException(nameof(strDateEnd));
        }

        /// <summary>
        /// Фабричный метод для формирования SQL запроса.
        /// </summary>
        /// <returns></returns>
        public ICreateSqlQuery GetSqlQuery()
        {
            return new QuerySqlReportInformStomatolog(this.strDateStart, this.strDateEnd);
        }
       
    }
}

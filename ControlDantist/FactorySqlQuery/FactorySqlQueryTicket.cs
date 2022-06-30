using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.Querys;
using ControlDantist.ValididtyTicket;    

namespace ControlDantist.FactorySqlQuery
{
    public class FactorySqlQueryTicket
    {
        /// <summary>
        /// Sql запрос на создание временной таблицы на удаленном сервере.
        /// </summary>
        /// <param name="nameTempTable">Название временной таблицы.</param>
        /// <returns>SQL запрос на создание временной таблицы.</returns>
        public virtual IQuery CreateTempTableTicketSqlQuery(string nameTempTable)
        {
            return new CreateTempTableTicket(nameTempTable);
        }

        /// <summary>
        /// Sql запрос на добавление данных во временную таблицу.
        /// </summary>
        /// <param name="list">Список с данными по льготникам.</param>
        /// <returns>Sql запрос на добавление данных во временную таблицу.</returns>
        public virtual IQuery InsertDateTempTicketTableSqlQuery(List<string> list, string nameTable)
        {
            return new InsertTableTicket(list, nameTable);
        }

        /// <summary>
        /// Sql запрос на поиск льготников в БД.
        /// </summary>
        /// <returns>Sql запрос на поиск льготников по удаленной базе данных.</returns>
        public virtual IQuery FindPersonTiketSqlQuery(string tempNameTable)
        {
            return new FIndTicket(tempNameTable);
        }
    }
}

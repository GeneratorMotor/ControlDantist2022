using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.FactorySqlQuery;
using ControlDantist.Querys;

namespace ControlDantist.ValididtyTicket
{
    public class ФабрикаSQLЭсрн 
    {
        /// <summary>
        /// Sql запрос на создание временной таблицы на удаленном сервере.
        /// </summary>
        /// <param name="nameTempTable">Название временной таблицы.</param>
        /// <returns>SQL запрос на создание временной таблицы.</returns>
        public IQuery CreateTempTableЭсрнSqlQuery(string nameTempTable)
        {
            return new CreateTempTableЭСРН(nameTempTable);
        }

        /// <summary>
        /// Sql запрос на добавление данных во временную таблицу.
        /// </summary>
        /// <param name="list">Список с данными по льготникам.</param>
        /// <returns>Sql запрос на добавление данных во временную таблицу.</returns>
        public IQuery InsertDateTempЭсрнTableSqlQuery(List<string> list, string nameTable)
        {
            return new InsertTableЭсрн(list, nameTable);
        }

        /// <summary>
        /// Sql запрос на поиск льготников в БД.
        /// </summary>
        /// <returns>Sql запрос на поиск льготников по удаленной базе данных.</returns>
        public IQuery FindPersonЭсрнSqlQuery(string tempNameTable)
        {
            return new FindЭсрн(tempNameTable);
        }
    }
}

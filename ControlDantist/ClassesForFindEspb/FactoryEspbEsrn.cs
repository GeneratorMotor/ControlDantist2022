using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.Querys;
using ControlDantist.ClassesForFindEspb.ItemEspbPerson;

namespace ControlDantist.ClassesForFindEspb
{
    /// <summary>
    /// Фабрика поиска льготников.
    /// </summary>
    public class FactoryEspbEsrn
    {
        /// <summary>
        /// Sql запрос на создание временной таблицы на удаленном сервере.
        /// </summary>
        /// <param name="nameTempTable">Название временной таблицы.</param>
        /// <returns>SQL запрос на создание временной таблицы.</returns>
        public IQuery CreateTempTableSqlQuery(string nameTempTable)
        {
            return new QueryCreateTempTable(nameTempTable);
        }


        /// <summary>
        /// Sql запрос на добавление данных во временную таблицу.
        /// </summary>
        /// <param name="list">Список с данными по льготникам.</param>
        /// <returns>Sql запрос на добавление данных во временную таблицу.</returns>
        public IQuery InsertDateTempTableSqlQuery(List<DataPersonEspb> list, string nameTable)
        {
            return new QueryInsertTempTable(list, nameTable);
        }

        /// <summary>
        /// Sql запрос на поиск льготников в БД.
        /// </summary>
        /// <returns>Sql запрос на поиск льготников по удаленной базе данных.</returns>
        public virtual IQuery FindPersonSqlQuery(string tempNameTable)
        {
            return new QueryFindPersonEspEsrn(tempNameTable);
        }
    }
}

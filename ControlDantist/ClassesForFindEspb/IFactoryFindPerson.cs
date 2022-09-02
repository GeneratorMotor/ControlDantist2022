using System;
using System.Collections.Generic;
using ControlDantist.Querys;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDantist.ClassesForFindEspb
{
    public interface IFactoryFindPerson<T>
    {
        /// Sql запрос на создание временной таблицы на удаленном сервере.
        /// </summary>
        /// <param name="nameTempTable">Название временной таблицы.</param>
        /// <returns>SQL запрос на создание временной таблицы.</returns>
        IQuery CreateTempTableSqlQuery(string nameTempTable);



        /// <summary>
        /// Sql запрос на добавление данных во временную таблицу.
        /// </summary>
        /// <param name="list">Список с данными по льготникам.</param>
        /// <returns>Sql запрос на добавление данных во временную таблицу.</returns>
        IQuery InsertDateTempTableSqlQuery(List<T> list, string nameTable);


        /// <summary>
        /// Sql запрос на поиск льготников в БД.
        /// </summary>
        /// <returns>Sql запрос на поиск льготников по удаленной базе данных.</returns>
        IQuery FindPersonSqlQuery(string tempNameTable);
        
    }
}

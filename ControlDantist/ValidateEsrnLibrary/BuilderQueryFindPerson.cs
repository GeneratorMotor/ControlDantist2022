﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.FactoryClass;
using ControlDantist.DataBaseContext;
using ControlDantist.Querys;

namespace ControlDantist.ValidateEsrnLibrary
{
    public class BuilderQueryFindPerson : IBuilderTempTable
    {
        // Фабрика для формирования SQL запросов.
        private FactorySqlQueryEsrnValidate factory;

        // Переменная для хранеия имени временной таблицы.
        private string nameTempTable = string.Empty;

        private List<ItemLibrary> list;

        // Строка для хранения SQl запрроса.
        private StringBuilder buiderSql = new StringBuilder();

        public BuilderQueryFindPerson(FactorySqlQueryEsrnValidate factory, string nameTempTable, List<ItemLibrary> list)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.nameTempTable = nameTempTable ?? throw new ArgumentNullException(nameof(nameTempTable));
            this.list = list ?? throw new ArgumentNullException(nameof(list));
        }

        /// <summary>
        /// Создание временной таблицы.
        /// </summary>
        public void CreateTempTable()
        {
            // Создаем SQL запрос на создание временной таблицы.
            IQuery createTempTable = this.factory.CreateTempTableSqlQuery(this.nameTempTable);
            buiderSql.Append(createTempTable.Query());
        }

        /// <summary>
        /// Поиск льготников на удаленном вервере.
        /// </summary>
        public void FindPerson()
        {
            // Джойн временной таблицы с данными на удаленном сервере.
            IQuery queryFindPerson = this.factory.FindPersonSqlQuery(this.nameTempTable);
            buiderSql.Append(queryFindPerson.Query());
        }

        /// <summary>
        /// Возвращает SQL запрос на выполнение поиска льготников на удаленном серврере.
        /// </summary>
        /// <returns></returns>
        public string GetQuery()
        {
            return buiderSql.ToString();
        }

        /// <summary>
        /// Заполнение данными временной таблицы.
        /// </summary>
        public void InsertDateTempTable()
        {
            // Заполняем данными времнную таблицу.
            IQuery queryInsertDate = this.factory.InsertDateTempTableSqlQuery(this.list, this.nameTempTable);
            buiderSql.Append(queryInsertDate.Query());
        }
    }
}

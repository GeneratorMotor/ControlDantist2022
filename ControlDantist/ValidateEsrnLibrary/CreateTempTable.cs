using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.Querys;

namespace ControlDantist.ValidateEsrnLibrary
{
    /// <summary>
    /// Создание временной таблицы на сервере района области.
    /// </summary>
    public class CreateTempTable : IQuery
    {
        // Переменная для хранения названия временной таблицы.
        private string nameTempTable = string.Empty;

        /// <summary>
        /// Создание экземпляра SQL запроса на создание временной таблицы.
        /// </summary>
        /// <param name="nameTempTable">Название временной таблицы.</param>
        public CreateTempTable(string nameTempTable)
        {
            this.nameTempTable = nameTempTable ?? throw new ArgumentNullException(nameof(nameTempTable));
        }

        /// <summary>
        /// Возвращает SQL скрипт на создание вроеменной таблицы на SQL Server  в районе области.
        /// </summary>
        /// <returns>SQL запрос на создание временной таблицы.</returns>
        public string Query()
        {
            string createTable = "  create table " + nameTempTable + " ([id_карточки] [int] IDENTITY(1,1) NOT NULL, " +
                                "id_договор int NULL, " +
                                "[Фамилия] [nvarchar](50) NULL, " +
                                "[Имя] [nvarchar](50) NULL, " +
                                "[Отчество] [nvarchar](50) NULL, " +
                                "[ДатаРождения] DateTime, " +
                                "[ВидЛьготногоУдостоверения] [nvarchar](50) NULL, " +
                                "[СерияДокумента] [nvarchar](50) NULL, " +
                                "[НомерДокумента] [nvarchar](50) NULL, " +
                                 "[ДатаВыдачиДокумента] DateTime, " +
                                 "СерияПаспорта nvarchar (10) NULL, " +
                                 "НомерПаспорта nvarchar (10) NULL, " +
                                 "ДатаВыдачиПаспорта DateTime )";

            return createTable;
        }
    }
}

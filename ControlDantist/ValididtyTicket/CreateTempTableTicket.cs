using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.Querys;

namespace ControlDantist.ValididtyTicket
{
    public class CreateTempTableTicket : IQuery
    {
        // Переменная для хранения названия временной таблицы.
        private string nameTempTable = string.Empty;

        /// <summary>
        /// Создание экземпляра SQL запроса на создание временной таблицы.
        /// </summary>
        /// <param name="nameTempTable">Название временной таблицы.</param>
        public CreateTempTableTicket(string nameTempTable)
        {
            this.nameTempTable = nameTempTable ?? throw new ArgumentNullException(nameof(nameTempTable));
        }

        /// <summary>
        /// Возвращает SQL скрипт на создание вроеменной таблицы на SQL Server  в районе области.
        /// </summary>
        /// <returns>SQL запрос на создание временной таблицы.</returns>
        public string Query()
        {
            string createTable = " create table " + nameTempTable + " ([id_карточки] [int] IDENTITY(1,1) NOT NULL, " +
                                "[BAR_CODE] [nvarchar](50) NULL ) ";

            return createTable;
        }
    }
}

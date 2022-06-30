using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.FactoryClass;
using ControlDantist.DataBaseContext;
using ControlDantist.Querys;
using ControlDantist.ValidateEsrnLibrary;
using ControlDantist.FactorySqlQuery;

namespace ControlDantist.ValididtyTicket
{
    class BuilderQueryFindTicketEspb : IBuilderTempTable
    {
        private FactorySqlQueryTicket factorySqlQueryTicket;

        // Переменная для хранеия имени временной таблицы.
        private string nameTempTable = string.Empty;

        private List<string> listBarCode;

        // Строка для хранения SQl запрроса.
        private StringBuilder buiderSql = new StringBuilder();

        public BuilderQueryFindTicketEspb(FactorySqlQueryTicket factorySqlQueryTicket, string nameTempTable, List<string> listBarCode)
        {
            this.factorySqlQueryTicket = factorySqlQueryTicket;
            this.nameTempTable = nameTempTable;
            this.listBarCode = listBarCode;
        }

        public void CreateTempTable()
        {
            // Создаем SQL запрос на создание временной таблицы.
            IQuery createTempTable = this.factorySqlQueryTicket.CreateTempTableTicketSqlQuery(this.nameTempTable);
            buiderSql.Append(createTempTable.Query());
        }

        public void FindPerson()
        {
            // Джойн временной таблицы с данными на удаленном сервере.
            IQuery queryFindPerson = this.factorySqlQueryTicket.FindPersonTiketSqlQuery(this.nameTempTable);
            buiderSql.Append(queryFindPerson.Query());
        }

        public string GetQuery()
        {
            return buiderSql.ToString();
        }

        public void InsertDateTempTable()
        {
            // Заполняем данными времнную таблицу.
            IQuery queryInsertDate = this.factorySqlQueryTicket.InsertDateTempTicketTableSqlQuery(this.listBarCode, this.nameTempTable);
            buiderSql.Append(queryInsertDate.Query());
        }
    }
}

using ControlDantist.ConvertDataTableToList;
using ControlDantist.ClassessForDB;
using ControlDantist.PatternSql;
using System.Collections.Generic;
using ControlDantist.Querys;
using ControlDantist.Classes;
using System;

namespace ControlDantist.DisplayLetter
{
    public class ValidContractsNotDateOfBerts : ValidContracts
    {
        private IConvertRegistr<PersonContract> convertRegistr;
        private ControlDantist.PatternSql.FactoryQuery factorySqlQuery;
        private string strConnection = string.Empty;

        public ValidContractsNotDateOfBerts(IConvertRegistr<PersonContract> convertRegistr, FactoryQuery factorySqlQuery, string strConnection) : base(convertRegistr,factorySqlQuery,strConnection)
        {
            this.convertRegistr = convertRegistr;
            this.factorySqlQuery = factorySqlQuery;
            this.strConnection = strConnection;
        }

        public new IEnumerable<DataPerson> Validate()
        {
            var persons = this.convertRegistr.GetPersons();

            // Список договоров.
            List<DataPerson> list = new List<DataPerson>();

            foreach (var person in persons)
            {
                DataPerson dataPerson = new DataPerson();
                dataPerson.PC = person;

                // Проверим есть ли отчество.
                if (person.Отчество != null && person.Отчество.Trim().ToLower() != "".ToLower().Trim())
                {
                    // Полкучим SQl запрос на поиск данных по текущему льготнику.
                    IQuery query = this.factorySqlQuery.QueryFindContractFioNotDr(person.Фамилия, person.Имя, person.Отчество, TodayDate());

                    // Заполним данными о писбмах.
                    GetDate(query.Query(), this.strConnection, dataPerson);

                }
                else
                {
                    IQuery query = this.factorySqlQuery.QueryFindFiNotDr(person.Фамилия, person.Имя, TodayDate());

                    // Заполним данными о писбмах.
                    GetDate(query.Query(), this.strConnection, dataPerson);

                }

            }

                return null;
        }
    }
}

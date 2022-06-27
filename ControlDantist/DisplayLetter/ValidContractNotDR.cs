using ControlDantist.ClassessForDB;
using ControlDantist.ConvertDataTableToList;
using ControlDantist.PatternSql;
using System;
using System.Collections.Generic;
using ControlDantist.Querys;
using System.Data;


namespace ControlDantist.DisplayLetter
{
    public class ValidContractNotDR : ValidContracts
    {
        private IConvertRegistr<PersonContract> convertRegistr;
        private ControlDantist.PatternSql.FactoryQuery factorySqlQuery;
        private string strConnection = string.Empty;

        public ValidContractNotDR(IConvertRegistr<PersonContract> convertRegistr, FactoryQuery factorySqlQuery, string strConnection) : base(convertRegistr,factorySqlQuery,strConnection)
        {
            this.convertRegistr = convertRegistr;
            this.factorySqlQuery = factorySqlQuery;
            this.strConnection = strConnection;
        }



        //public ValidContractNotDR(IConvertRegistr<PersonContract> convertRegistr, FactoryQuery factorySqlQuery, string strConnection) : base()
        //{
        //    this.convertRegistr = convertRegistr;
        //    this.factorySqlQuery = factorySqlQuery;
        //    this.strConnection = strConnection;
        //}

        /// <summary>
        ///  Переопределим метод.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<DataPerson> Validate()
        {
            // Получим список льготников у которых не найдены задвоенные договора.
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
                    IQuery query = this.factorySqlQuery.QueryFindContractFioNotDr(person.Фамилия,person.Имя,person.Отчество, this.TodayDate());

                    // Заполним данными о писбмах.
                    GetDate(query.Query(), this.strConnection, dataPerson);

                    // Полкучим SQl запрос на поиск данных по текущему льготнику.
                    IQuery query21 = this.factorySqlQuery.QueryFindFioNotDr21(person.Фамилия, person.Имя, person.Отчество, this.TodayDate());

                    var test21 = query21.Query();

                    // Заполним данными о писбмах.
                    GetDate(query21.Query(), this.strConnection, dataPerson);
                }
                else
                {
                    // Полкучим SQl запрос на поиск данных по текущему льготнику.
                    IQuery query = this.factorySqlQuery.QueryFindFiNotDr(person.Фамилия, person.Имя, this.TodayDate());

                    // Заполним данными о писбмах.
                    GetDate(query.Query(), this.strConnection, dataPerson);

                    // Полкучим SQl запрос на поиск данных по текущему льготнику.
                    IQuery query21 = this.factorySqlQuery.QueryFindFiNotDr21(person.Фамилия, person.Имя, this.TodayDate());

                    var test21 = query21.Query();

                    // Заполним данными о писбмах.
                    GetDate(query21.Query(), this.strConnection, dataPerson);

                }

                list.Add(dataPerson);

            }

            return list;
        }

        //public void GetDate(string query, string strConnection, DataPerson dataPerson)
        //{
        //    // Получим ранее заключенные договора.
        //    IGetDataTableSQL tableSQL = new TableBD(query, "TabPerson", this.strConnection);

        //    DataTable table = tableSQL.GetTableSQL();

        //    IConvertDataLetter convertData = new ConvertDataLetter(dataPerson);
        //    convertData.ConvertDate(table);
        //}

        //public int TodayDate()
        //{
        //    // Получим текущий год.
        //    int currentYear = DateTime.Now.Year;

        //    // Получим год окончания проверки.
        //   // return currentYear - 2;
        //   return currentYear - 4;
        //}

    }
}

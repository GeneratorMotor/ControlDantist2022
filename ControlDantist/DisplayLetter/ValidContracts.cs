using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ControlDantist.ConvertDataTableToList;
using ControlDantist.ClassessForDB;
using ControlDantist.Classes;
using ControlDantist.Querys;
using System.Threading.Tasks;

namespace ControlDantist.DisplayLetter
{
    public class ValidContracts : IValidateContract
    {
        private IConvertRegistr<PersonContract> convertRegistr;
        private ControlDantist.PatternSql.FactoryQuery factorySqlQuery;
        private string strConnection = string.Empty;

        public ValidContracts(IConvertRegistr<PersonContract> convertRegistr, 
                                     PatternSql.FactoryQuery factorySqlQuery, 
                                     string strConnection)
        {
            this.convertRegistr = convertRegistr;
            this.factorySqlQuery = factorySqlQuery;
            this.strConnection = strConnection;
        }

        public IEnumerable<DataPerson> Validate()
        {
            var persons = this.convertRegistr.GetPersons();

            // Список договоров.
            List<DataPerson> list = new List<DataPerson>();

            // Получим текущий год.


            foreach(var person in persons)
            {
                DataPerson dataPerson = new DataPerson();
                dataPerson.PC = person;

                // Проверим есть ли отчество.
                if (person.Отчество != null && person.Отчество.Trim().ToLower() != "".ToLower().Trim())
                {
                    // Полкучим SQl запрос на поиск данных по текущему льготнику.
                    IQuery query = this.factorySqlQuery.QueryFindContractFio(person.Фамилия, person.Имя, person.Отчество, Время.Дата(person.ДатаРождения.ToShortDateString()), person.NumContract, this.TodayDate());

                    //var test1 = query.Query();

                    // Заполним данными о писбмах.
                    GetDate(query.Query(), this.strConnection, dataPerson);

                    // Полкучим SQl запрос на поиск данных по текущему льготнику.
                    IQuery query21 = this.factorySqlQuery.QueryFindContractFio21(person.Фамилия, person.Имя, person.Отчество, Время.Дата(person.ДатаРождения.ToShortDateString()), person.NumContract, this.TodayDate());

//                    var test21 = query21.Query();

                    // Заполним данными о писбмах.
                    GetDate(query21.Query(), this.strConnection, dataPerson);
                }
                else
                {
                    // Полкучим SQl запрос на поиск данных по текущему льготнику.
                    IQuery query = this.factorySqlQuery.QueryFindContractFi(person.Фамилия, person.Имя, Время.Дата(person.ДатаРождения.ToShortDateString()), person.NumContract, this.TodayDate());

                    var test1 = query.Query();

                    // Заполним данными о писбмах.
                    GetDate(query.Query(), this.strConnection, dataPerson);

                    // Полкучим SQl запрос на поиск данных по текущему льготнику.
                    IQuery query21 = this.factorySqlQuery.QueryFindContractFi21(person.Фамилия, person.Имя, Время.Дата(person.ДатаРождения.ToShortDateString()), person.NumContract, this.TodayDate());

                    var test21 = query21.Query();

                    // Заполним данными о писбмах.
                    GetDate(query21.Query(), this.strConnection, dataPerson);
                }

                // То же самое сделаем и с 2020 годом.

                list.Add(dataPerson);
            }

            return list;
        }


        private void GetDate(string query,string strConnection, DataPerson dataPerson)
        {
            // Получим ранее заключенные договора.
            IGetDataTableSQL tableSQL = new TableBD(query, "TabPerson", this.strConnection);

            DataTable table = tableSQL.GetTableSQL();

            IConvertDataLetter convertData = new ConvertDataLetter(dataPerson);
            convertData.ConvertDate(table);
        }

        private int TodayDate()
        {
            // Получим текущий год.
            int currentYear = DateTime.Now.Year;

            // Получим год окончания проверки.
            return currentYear - 2;
        }
    }
}

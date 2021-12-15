using System;
using System.Collections.Generic;
using ControlDantist.Classes;
using ControlDantist.FindPersonFullTrue;
using ControlDantist.ValidPersonContract;

namespace ControlDantist.Find
{
    public class FindPersonToSernameName
    {
        private string fiastName = string.Empty;
        private string secondName = string.Empty;
        private bool flagValidity = false;
        private bool flagSecondName = false;

        public FindPersonToSernameName(string fiastName, string secondName, bool flagValidity, bool flagSecondName)
        {
            this.fiastName = fiastName;
            this.secondName = secondName;
            this.flagValidity = flagValidity;
            this.flagSecondName = flagSecondName;
        }

        public List<ValideContract> GetPersonFnSn()
        {
            // Временный cписок содержащий все найденные договора.
            List<ValideContract> listTempDisplay = new List<ValideContract>();

            // Здесь я применяю паттерн фабричный метод. !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            FindPersonFactory findFullTo2019 = new FindPersonFSName2019To(fiastName, this.secondName, flagValidity, flagSecondName);
            IFindPerson findPerson2019To = findFullTo2019.Query();

            //var testSuka = findPerson2019To.Query();

            // Загрузим данные до 2019 года.
            listTempDisplay.AddRange(QueryExecute.ExecuteQuery(GetDate(findPerson2019To.Query())));

            // Поиск льготников по таблице ДоговорAdd - 2019 год.
            findFullTo2019 = new FindPersonFSName2019Add(fiastName, this.secondName, flagValidity, flagSecondName);
            IFindPerson findPerson2019Add = findFullTo2019.Query();

            // Сртока запроса на поиск льготников в таблице ЛьготникAdd.
            string query2019Add = findPerson2019Add.Query();

            // Поиск льготников в таблице ДоговорАрхив 2019 год.
            findFullTo2019 = new FindPersonFSName2019Архив(fiastName, this.secondName, flagValidity, flagSecondName);
            IFindPerson findPerson2019Архив = findFullTo2019.Query();

            // Строка содержащая SQL запрос.
            string query2019Архив = findPerson2019Архив.Query();

            // Сформируем union all запрос по 2019 году.
            string query2019UnionAll = query2019Add + " union all " + query2019Архив;

            listTempDisplay.AddRange(QueryExecute.ExecuteQuery(GetDate(query2019UnionAll)));

            // Поиск льготников ДоговорАрхив > 2019 года.
            findFullTo2019 = new FindPersonFS2019After(fiastName, this.secondName, flagValidity, flagSecondName);
            IFindPerson findPerson2019Aftar = findFullTo2019.Query();

            //var test = findPerson2019Aftar.Query();

            listTempDisplay.AddRange(QueryExecute.ExecuteQuery(GetDate(findPerson2019Aftar.Query())));

            // Посик льготников в таблице Договор, 2021 и дольше год.
            findFullTo2019 = new FindPersonFSName(fiastName, this.secondName, flagValidity, flagSecondName);
            IFindPerson findPersonSecondName = findFullTo2019.Query();



            listTempDisplay.AddRange(QueryExecute.ExecuteQuery(GetDate(findPersonSecondName.Query())));

            return listTempDisplay;
        }

        private StringParametr GetDate(string query)
        {
            StringParametr stringParametr = new StringParametr();
            stringParametr.Query = query;

            return stringParametr;
        }
    }
}

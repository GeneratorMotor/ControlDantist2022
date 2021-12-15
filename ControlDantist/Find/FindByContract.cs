using System;
using System.Collections.Generic;
using ControlDantist.Classes;
using ControlDantist.ValidPersonContract;

namespace ControlDantist.Find
{
    public class FindByContract
    {
        // Номер контракта.
        private string numContract = string.Empty;

        // Флаг прошел проверку или нет.
        private bool flagValidate = false;

        public FindByContract(string numContract, bool flagValid)
        {
            this.numContract = numContract ?? throw new ArgumentNullException(nameof(numContract));
            this.flagValidate = flagValid;
        }

        public List<ValideContract> GetNumber()
        {
            // Временный cписок содержащий все найденные договора.
            List<ValideContract> listTempDisplay = new List<ValideContract>();

            // Переменная для хранения номера договора который необходимо найти.
            //string numContract = this.textBox1.Text;

            // Поиск льготника прошедшего проверку по номеру договора.
            IFindPerson findPerson = new FindContractTo2019(numContract, flagValidate);
            string queryTo2019 = findPerson.Query();

            // Загрузим данные до 2019 года.
            listTempDisplay.AddRange(QueryExecute.ExecuteQuery(GetDate(queryTo2019)));

            ////Поиск номера договора за 2019 год по таблицам TableAdd.
            IFindPerson fintPerson2019Add = new FindContract2019Add(numContract, flagValidate);
            string query2019Add = fintPerson2019Add.Query();

            ////Пока скроем поиск льготников в основной таблице за 2019 год.
            ////  Поиск номера договора за 2019 год по обычным таблицам.
            IFindPerson findPerson2019 = new FindContract2019(numContract, flagValidate);
            string query2019 = findPerson2019.Query();

            string queryUnionAll = query2019Add + " union all " + query2019;

            listTempDisplay.AddRange(QueryExecute.ExecuteQuery(GetDate(queryUnionAll)));

            //// Поиск номера договора позже 2019 года.
            IFindPerson fintPersonAftar2019 = new FindPersonAftar2019(numContract, flagValidate);
            string query2019Aftar = fintPersonAftar2019.Query();

            listTempDisplay.AddRange(QueryExecute.ExecuteQuery(GetDate(query2019Aftar)));

            // Поиск договора по таблицам 2021 года.
            IFindPerson findPerson2021 = new FindPersonAftar(numContract, flagValidate);
            string query2021 = findPerson2021.Query();

            listTempDisplay.AddRange(QueryExecute.ExecuteQuery(GetDate(query2021)));

            // Загрузим данными.
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

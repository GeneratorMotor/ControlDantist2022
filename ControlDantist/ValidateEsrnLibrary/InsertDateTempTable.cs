using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.Querys;
using ControlDantist.DataBaseContext;
using ControlDantist.ExceptionClassess;
using ControlDantist.Classes;

namespace ControlDantist.ValidateEsrnLibrary
{
    public class InsertDateTempTable : IQuery
    {
        private List<ItemLibrary> list;
        private string nameTable = string.Empty;

        public InsertDateTempTable(List<ItemLibrary> list, string nameTable)
        {
            this.list = list ?? throw new ArgumentNullException(nameof(list));
            this.nameTable = nameTable ?? throw new ArgumentNullException(nameof(nameTable));
        }

        public string Query()
        {
            // Переменная для хранения строки запроса на заполнение временной таблицы.
            StringBuilder builderQuery = new StringBuilder();

            foreach (var itm in this.list)
            {
                // Получим данные о льготнике.
                var person = itm?.Packecge?.льготник ?? null;

                // Дата рождения льготников.
                string dateBirthPerson = itm?.DateBirdthPerson ?? "";

                // Дата выдачи документа.
                string dateDoc = itm?.DateDoc ?? "";

                // Дата выдачи паспорта.
                string datePassword = itm?.DatePassword ?? "";

                // Получим данные о договоре.
                var contract = itm?.Packecge?.тДоговор ?? null;

                if (person != null && contract != null)
                {
                    // Проверим заполненность полей льготника.
                    ValidErrorPerson vrp = new ValidErrorPerson(person);

                    //// Проверим заполненность полей льготника.
                    if (vrp.Validate() == true)
                    {
                        string queryInsert = " insert into "+ this.nameTable +" (id_договор,Фамилия,Имя,Отчество,ДатаРождения,СерияДокумента,НомерДокумента,ДатаВыдачиДокумента,СерияПаспорта,НомерПаспорта,ДатаВыдачиПаспорта) " +
                                             " values(" + contract.id_договор + ",'" + person.Фамилия.Trim().ToLower() + "','" + person.Имя.Trim().ToLower() + "','" + person.Отчество.Do(x => x, "").Trim().ToLower() + "','" + Время.Дата(person.ДатаРождения.Date.ToShortDateString().Trim()) + "','" + person.СерияДокумента.Do(x => x, "").ToLower().Trim() + "','" + person.НомерДокумента.Do(x => x, "").ToLower().Trim() + "','" + Время.Дата(person.ДатаВыдачиДокумента.Date.ToShortDateString().Trim()) + "',  " +
                                             " '" + person.СерияПаспорта.Do(x => x, "").Replace(" ", "").Trim().ToLower() + "','" + person.НомерПаспорта.Do(x => x, "").Trim().ToLower() + "','" + Время.Дата(person.ДатаВыдачиПаспорта.Date.ToShortDateString()) + "' ) ";
 
                        builderQuery.Append(queryInsert);
                    }
                }
            }

            return builderQuery.ToString();
        }
    }
}

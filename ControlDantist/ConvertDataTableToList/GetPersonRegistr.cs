using System;
using ControlDantist.ClassessForDB;
using ControlDantist.DataBaseContext;
using System.Collections.Generic;


namespace ControlDantist.ConvertDataTableToList
{
    public class GetPersonRegistr : IConvertRegistr<PersonContract>
    {
        List<ItemLibrary> list = new List<ItemLibrary>();

        public GetPersonRegistr(List<ItemLibrary> listRegistr)
        {
            if (listRegistr != null && listRegistr.Count > 0)
            {
                this.list.AddRange(listRegistr);
            }
            else
            {
                //throw new ArgumentNullException("Нет данных для ");
                return;
            }

        }

        public List<PersonContract> GetPersons()
        {

            List<PersonContract> listPerson = new List<PersonContract>();

            foreach (ItemLibrary it in this.list)
            {
                PersonContract person = new PersonContract();

                person.Фамилия = it.Packecge.льготник.Фамилия.Trim();
                person.Имя = it.Packecge.льготник.Имя.Trim();
                person.Отчество = it.Packecge.льготник.Отчество.Trim();
                person.ДатаРождения = it.Packecge.льготник.ДатаРождения;
                // Запишем данные по договору.
                person.NumContract = it.NumContract;

                //person.Фамилия = "Шапкина".Trim();
                //person.Имя = "Татьяна".Trim();
                //person.Отчество = "Васильевна".Trim();

                //DateTime dateTime = new DateTime(1957, 2, 16);

                //person.ДатаРождения = dateTime;
                //// Запишем данные по договору.
                //person.NumContract = "СПКМРСО/3906";

                //listPerson.Add(person);

                //PersonContract person2 = new PersonContract();
                //person2.Фамилия = "Шувалов".Trim();
                //person2.Имя = "Валерий".Trim();
                //person2.Отчество = "Владимирович".Trim();

                //DateTime dateTime2 = new DateTime(1944, 4, 9);

                //person2.ДатаРождения = dateTime2;

                //// Запишем данные по договору.
                //person2.NumContract = "БАКСП-2/4846";

                //listPerson.Add(person2);

                //PersonContract person3 = new PersonContract();
                //person3.Фамилия = "Антонов".Trim();
                //person3.Имя = "Павел".Trim();
                //person3.Отчество = "Геннадьевич".Trim();

                //DateTime dateTime3 = new DateTime(1958, 4, 11);

                //person2.ДатаРождения = dateTime3;

                //// Запишем данные по договору.
                //person3.NumContract = "2-1/7383";

                //listPerson.Add(person3);
                listPerson.Add(person);
            }

            return listPerson;

        }
    }
}

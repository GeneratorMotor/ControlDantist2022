using ControlDantist.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.ValidateEsrnLibrary
{
    public class ValidPersonEsrn : IValidPersonEsrn
    {

        //DiscriptionValidate discriptionError;

        //public ValidPersonEsrn(DiscriptionValidate discriptionError)
        public ValidPersonEsrn()
        {
            //this.discriptionError = discriptionError ?? throw new ArgumentNullException(nameof(discriptionError));
        }

        //public bool FlagValid()
        //{
        //    throw new NotImplementedException();
        //}

        //public void InstallFlagValid()
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Сверка докментов из реестра со списком документов
        /// </summary>
        /// <param name="itemLibrary"></param>
        /// <param name="datePerson"></param>
        public void ValidDocument(ItemLibrary itemLibrary, DatePerson datePerson)
        {

            // Серия документа из реестра.
            var серияДокР = itemLibrary.Packecge.льготник.СерияДокумента ?? "".Trim().ToLower();

            // Серия документа из ЭСРН.
            var серияДокумента = datePerson.СерияДокумента ?? "".Trim().ToLower();

            // Уберем все пробелы из серии документа и сравним оставшиеся строки.
            if(string.Compare(серияДокР.Replace(" ", string.Empty), серияДокумента.Replace(" ", string.Empty)) == 0)
            {

            }

        }

        public void ValidCategoryPerson(ItemLibrary itemLibrary, DatePerson datePerson)
        {
            //if(string.Compare(itemLibrary.Packecge.тЛьготнаяКатегория?.ЛьготнаяКатегория?.Replace(" ","")?.ToLower()?.Trim(),datePerson.)
        }

        public void ValidDr(ItemLibrary itemLibrary, DatePerson datePerson)
        {

            //// Сравним данные по Фамилии.
            if (DateTime.Compare(itemLibrary.Packecge.льготник.ДатаРождения.Date,datePerson.ДатаРождения.Date) == 0)
            {
                // Если данные совподают тогда
                itemLibrary.DiscriptionValidate.SetFlag(true, "");

            }
            else
            {
                itemLibrary.DiscriptionValidate.SetFlag(false, " Ошибка Дата рождения; ");
            }
        }


        public void ValidFirstName(ItemLibrary itemLibrary, DatePerson datePerson)
        {
            // Присвоим 
            itemLibrary.DiscriptionValidate = new DiscriptionValidate();

            //// Сравним данные по Фамилии.
            if (itemLibrary.Packecge.льготник.Фамилия.ToLower().Trim() == datePerson.Фамилия.ToLower().Trim())
            {
                // Если данные совподают тогда
                itemLibrary.DiscriptionValidate.SetFlag(true, "");
            }
            else
            {
                itemLibrary.DiscriptionValidate.SetFlag(false, " Ошибка Фамилия; ");
            }
        }

        public void ValidName(ItemLibrary itemLibrary, DatePerson datePerson)
        {
            //// Сравним данные по Фамилии.
            if (itemLibrary.Packecge.льготник.Имя.ToLower().Trim() == datePerson.Имя.ToLower().Trim())
            {
                // Если данные совподают тогда
                itemLibrary.DiscriptionValidate.SetFlag(true, "");
            }
            else
            {
                itemLibrary.DiscriptionValidate.SetFlag(false, " Ошибка Имя; ");
            }
        }

        /// <summary>
        /// Проверка паспортных данных льготника с данными из ЭСРН.
        /// </summary>
        /// <param name="itemLibrary">Данные по льготнику из реестра.</param>
        /// <param name="datePerson">Данные по льготнику из ЭСРН.</param>
        public void ValidPassword(ItemLibrary itemLibrary, DatePerson datePerson)
        {
            var серияПасспортаР = itemLibrary.Packecge.льготник.СерияПаспорта ?? "".Trim().ToLower();

            var серияДокумента = datePerson.СерияДокумента ?? "".Trim().ToLower();

            if (серияПасспортаР.ToLower().Trim() == серияДокумента.ToLower().Trim())
            {
                itemLibrary.DiscriptionValidate.SetFlagPassword(true, "");
            }
            else
            {
                itemLibrary.DiscriptionValidate.SetFlagPassword(false, " Ошибка СЕРИЯ ПАСПОРТА; ");
            }

            var номерПасспортаР = itemLibrary.Packecge.льготник.НомерПаспорта ?? "".Trim().ToLower();

            var номерДокумента = datePerson.НомерДокумента ?? "".Trim().ToLower();

            if (номерПасспортаР.ToLower().Trim() == номерДокумента.ToLower().Trim())
            {
                itemLibrary.DiscriptionValidate.SetFlagPassword(true, "");
            }
            else
            {
                itemLibrary.DiscriptionValidate.SetFlagPassword(false, " Ошибка НОМЕР ПАСПОРТА; ");
            }

            // Дата выдачи паспорта.
            string датаВыдачиПаспорта = string.Empty;

            // Дата выдачи документа.
            string датаВыдачиDoc = string.Empty;

            if(DateTime.Compare(itemLibrary.Packecge.льготник.ДатаВыдачиПаспорта.Date, datePerson.ДатаВыдачи.Date) == 0)
            {
                itemLibrary.DiscriptionValidate.SetFlagPassword(true, "");
            }
            else
            {
                itemLibrary.DiscriptionValidate.SetFlagPassword(false, " ДАТА ВЫДАЧИ ПАСПОРТА ");
            }

        }

        public void ValidSecondName(ItemLibrary itemLibrary, DatePerson datePerson)
        {
            //// Сравним данные по Фамилии.
            if (itemLibrary.Packecge.льготник?.Отчество.ToLower().Trim() == datePerson.Отчество.ToLower().Trim())
            {
                // Если данные совподают тогда
                itemLibrary.DiscriptionValidate.SetFlag(true, "");
            }
            else
            {
                itemLibrary.DiscriptionValidate.SetFlag(false, " Ошибка Отчество; ");
            }
        }
    }
}

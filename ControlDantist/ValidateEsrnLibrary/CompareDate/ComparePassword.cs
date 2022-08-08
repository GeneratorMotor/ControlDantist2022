using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.DataBaseContext;
using ControlDantist.Classes;

namespace ControlDantist.ValidateEsrnLibrary.CompareDate
{
    public class ComparePassword : ICompareRegistrPerson
    {
        // Переменная для хранения данных о льготнике полученных из ЭСРН.
        private List<DatePerson> listPerson;

        // Переменная для хранения данных о льготнике полученных из реестра.
        private ItemLibrary currentPerson;

        private string errorMessage = string.Empty;

        public ComparePassword(List<DatePerson> listPerson, ItemLibrary currentPerson) 
        {
            this.listPerson = listPerson ?? throw new ArgumentNullException(nameof(listPerson));
            this.currentPerson = currentPerson ?? throw new ArgumentNullException(nameof(currentPerson));
        }

        public string ComparePerson()
        {
            // Данные по текущему льготнику.
            var person = this.currentPerson.Packecge.льготник;

            // Переменная для хранения описания ошибки.
            string messageError = string.Empty;

            // Отчество.
            if (person.НомерПаспорта == null || person.СерияПаспорта == null || person.ДатаВыдачиПаспорта == null)
            {
                messageError = " Ошибка нет данных по паспорту ; ";

                return messageError;
            }


            // Сравним данные по льготнику из реестра с данными из ЭСРН.
            var rezultfio = this.listPerson.Any(w => w?.НомерДокумента?.ToLower().Trim() == person?.НомерПаспорта?.ToLower().Trim() && w?.СерияДокумента?.ToLower().Trim() == person?.СерияПаспорта?.ToLower().Trim() && Время.Дата(w.ДатаВыдачи.ToShortDateString()).Trim() == Время.Дата(person?.ДатаВыдачиПаспорта.ToShortDateString()));

            if (rezultfio == false)
            {
                messageError = " Ошибка паспорт номер, серия или дата выдачи ; ";
            }

            return messageError;
        }

       
    }
}

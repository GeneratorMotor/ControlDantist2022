using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.DataBaseContext;
using ControlDantist.ValidateEsrnLibrary.Interfaces;

namespace ControlDantist.ValidateEsrnLibrary.CompareDate
{
    public class CompareDateDocument : ICompareDate
    {

        // Переменная для хранения данных о льготнике полученных из ЭСРН.
        private DatePerson datePerson;

        // Переменная для хранения данных о льготнике полученных из реестра.
        private ItemLibrary person;

        /// <summary>
        /// Сверка данных из реестра с данными полученными из ЭСРН.
        /// </summary>
        /// <param name="listPerson">Данные из ЭСРН</param>
        /// <param name="list">Данные из реестра.</param>
        /// <param name="listBoolValidDocPerson"></param>
        public CompareDateDocument(DatePerson datePerson, ItemLibrary person)
        {
            // Получим данные из ЭСРН.
            this.datePerson = datePerson ?? throw new ArgumentNullException(nameof(datePerson));

            // Получим данные из реестра.
            this.person = person ?? throw new ArgumentNullException(nameof(person));
        }

        public bool Execue()
        {
            // Флаг указывающий прошла ли проверка по дате документа.
            bool flagValidDateDoc = false;

            // Серия документа из реестра.
            var датаДокументаРеестр = person.Packecge.льготник.ДатаВыдачиДокумента.ToLocalTime().Date;

            // Серия документа из ЭСРН.
            var датаДокументаЭсрн = datePerson.ДатаВыдачи.ToLocalTime().Date;

            //var timFile = TimeZoneInfo.ConvertTimeToUtc(датаДокументаРеестр);

            //var timeEsrn = TimeZoneInfo.ConvertTimeToUtc(датаДокументаЭсрн);

            if (DateTime.Compare(датаДокументаРеестр, датаДокументаЭсрн) == 0)
            {
                flagValidDateDoc = true;
            }

            return flagValidDateDoc;
        }
    }
}

using ControlDantist.DataBaseContext;
using ControlDantist.ValidateEsrnLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.ValidateEsrnLibrary.CompareDate;

namespace ControlDantist.ValidateEsrnLibrary
{

    public class CompareDocumentEsrn : ICompareDocEsrn
    {

        // Переменная для хранения данных о льготнике полученных из ЭСРН.
        private List<DatePerson> listPerson;

        // Переменная для хранения данных о льготнике полученных из реестра.
        private ItemLibrary person;

        // Список логических отметок 
        //private List<bool> listFlag;

        ICompareDate compareDate;

        /// <summary>
        /// Сверка данных из реестра с данными полученными из ЭСРН.
        /// </summary>
        /// <param name="listPerson">Данные из ЭСРН</param>
        /// <param name="list">Данные из реестра.</param>
        /// <param name="listBoolValidDocPerson"></param>
        public CompareDocumentEsrn(List<DatePerson> listPerson, ItemLibrary person)
        {
            // Получим данные из ЭСРН.
            this.listPerson = listPerson ?? throw new ArgumentNullException(nameof(listPerson));

            // Получим данные из реестра.
            this.person = person ?? throw new ArgumentNullException(nameof(person));

            this.compareDate = compareDate;

            // Присвоим экземпляр описания документа к свойству объекта об льготнике.
            this.person.DiscriptionValidate = new DiscriptionValidate();

            // Присвоим экземпляр списка ошибок с номерами документов к к свойству объекта об льготнике.
            this.person.ListFalagValidateDocuments = new List<bool>();
        }

        public bool ValidateDoc()
        {
            bool flagResult = false;

            // Получим список документов которые есть в ЭСРН у данного льготника.
            var docsPerson = this.listPerson.Where(w => w.НаименованиеДокумента.Trim().ToLower() != "Паспорт гражданина России".Trim().ToLower() && w.Фамилия.ToLower().Trim() == person.Packecge.льготник.Фамилия.ToLower().Trim());

            foreach (var docPerson in docsPerson)
            {
                var flagNumberSeries = CompareSeriesNumberDoc(docPerson, this.person);

                // Проверим дату документа из реестра с текцущй датой документа из ЭСРН.
                ICompareDate compareDate = new CompareDateDocument(docPerson, this.person);

                var flagDateDoc = compareDate.Execue();

                if((flagNumberSeries == true) && (flagDateDoc == true))
                {
                    flagResult = true;
                }
            }

            return flagResult;
        }

        /// <summary>
        /// Сравнение серии и номера документа реестра и ЭСРН.
        /// </summary>
        /// <param name="datePerson">Данные из ЭСРН.</param>
        /// <param name="person">Данные из реестра</param>
        /// <returns>Булевоее значение результата, true - прошёл проверку, false - проверку не прошел</returns>
        private bool CompareSeriesNumberDoc(DatePerson datePerson, ItemLibrary person)
        {
            // Номер документа из реестра.
            var номерДокументаРеестр = person.Packecge.льготник.НомерДокумента ?? "".Trim().ToLower();

            // Серия документа из реестра.
            var серияДокР = person.Packecge.льготник.СерияДокумента ?? "".Trim().ToLower();

            // Серия документа из ЭСРН.
            var номерДокументаЭсрн = datePerson.НомерДокумента ?? "".Trim().ToLower();

            // Серия документа из ЭСРН.
            var серияДокументаЭсрн = datePerson.СерияДокумента ?? "".Trim().ToLower();

            // Объединим серию и номер документа из реестра в одну строку.
            string СерияНомерДокументаРеестр = (серияДокР + номерДокументаРеестр).Replace(" ", string.Empty).Trim();

            // Объединим серию и номер документа из ЭСРН в одну строку.
            string СерияНомерДокументаЭсрн = (серияДокументаЭсрн + номерДокументаЭсрн).Replace(" ", string.Empty).Trim();

            // Уберем все пробелы из серии документа и сравним оставшиеся строки.
            //if (string.Compare(номерДокументаРеестр.Replace(" ", string.Empty), номерДокументаЭсрн.Replace(" ", string.Empty)) == 0)
            if(string.Compare(СерияНомерДокументаРеестр, СерияНомерДокументаЭсрн) == 0)
            {
                // Если прошла проверка то поместим флаг в 
                person.ListFalagValidateDocuments.Add(true);
            }
            else
            {
                // Если документ не прошёл проверку.
                person.ListFalagValidateDocuments.Add(false);
            }

            return GetValidDocumentNs();
           
        }

        /// <summary>
        /// Находим хотя одно соответсвие документа из реес тра с документами из ЭСРН.
        /// </summary>
        /// <returns></returns>
        private bool GetValidDocumentNs()
        {
            bool flagValid = false;

            // Проверим хотя бы 
            if (person.ListFalagValidateDocuments.Any(w => w == true) == true)
            {
                flagValid = true;
            }

            return flagValid;
        }

        /// <summary>
        /// /Сравнение даты выдачи документа (по хорошему проверку по дате вывести в отдельный документ наследующмй interface).
        /// </summary>
        /// <param name="datePerson">Данные из ЭСРН.</param>
        /// <param name="person">Данные из файла (реестра)</param>
        /// <returns>Логическое значение указывающее прошёл проверку документ - True или нет - False.</returns>
        //private bool CompareDateDoc(DatePerson datePerson, ItemLibrary person)
        //{
        //    // Флаг указывающий прошла ли проверка по дате документа.
        //    bool flagValidDateDoc = false;

        //    // Серия документа из реестра.
        //    var датаДокументаРеестр = person.Packecge.льготник.ДатаВыдачиДокумента.Date;

        //    // Серия документа из ЭСРН.
        //    var датаДокументаЭсрн = datePerson.ДатаВыдачи.Date;

        //    if(DateTime.Compare(датаДокументаРеестр, датаДокументаЭсрн) == 0)
        //    {
        //        flagValidDateDoc = true;
        //    }

        //    return flagValidDateDoc;
        //}


    }
}

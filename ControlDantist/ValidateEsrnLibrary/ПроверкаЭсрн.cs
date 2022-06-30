using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.DataBaseContext;

namespace ControlDantist.ValidateEsrnLibrary
{
    public class ПроверкаЭсрн : IValidateЭсрн
    {
        // Переменная для хранения данных о льготнике полученных из ЭСРН.
        private List<DatePerson> listPerson;

        // Переменная для хранения данных о льготнике полученных из реестра.
        private List<ItemLibrary> list;

        public ПроверкаЭсрн(List<DatePerson> listPerson, List<ItemLibrary> list)
        {
            this.listPerson = listPerson ?? throw new ArgumentNullException(nameof(listPerson));
            this.list = list ?? throw new ArgumentNullException(nameof(list));
        }

        /// <summary>
        /// Проверка льготника по данным полученным из ЭСРН.
        /// </summary>
        public void Validate()
        {
            // Откажемся пока от паттерна строитель.
            foreach (var person in this.list.OrderBy(w => w.Packecge.льготник.Фамилия))
            {
                // Класс
                DiscriptionValidate discriptionError = new DiscriptionValidate();

                // Шаблон строитель (частично реализованный) без прораба.
                ValidPersonEsrn validPersonEsrn = new ValidPersonEsrn(discriptionError);

                // Проверим ФИО и ДР.
                foreach (var itm in this.listPerson.OrderBy(w => w.Фамилия))
                {
                    validPersonEsrn.ValidFioDr(person, itm);

                    // Проверка паспорта.
                    var passwords = this.listPerson.Where(w => w.НаименованиеДокумента.Trim().ToLower() == "Паспорт гражданина России".Trim().ToLower()).OrderBy(w => w.Фамилия).ToList();

                    foreach (var pass in passwords)
                    {
                        validPersonEsrn.ValidPassword(person, pass);
                    }
                }

               


            }

            #region Старая реализация
            foreach (var person in this.list.OrderBy(w=>w.Packecge.льготник.Фамилия))
            {
                // Массив флагов проверки Фамилии.
                bool[] flagFirstName = new bool[10];
                //bool[] flagFirstName = new bool[7];

                if (person.Packecge.льготник.Фамилия.ToLower().Trim() == "Соколова".ToLower().Trim())
                {
                    string sTest = "";
                }

                // Проверим ФИО и ДР.
                foreach (var itm in this.listPerson.OrderBy(w=>w.Фамилия))
                {
                    if(person.Packecge.льготник.Фамилия.ToLower().Trim() == itm.Фамилия.ToLower().Trim())
                    {
                        flagFirstName[0] = true;
                    }

                    if (person.Packecge.льготник.Имя.ToLower().Trim() == itm.Имя.ToLower().Trim())
                    {
                        flagFirstName[1] = true;
                    }


                    // Отчество из реестра.
                    var secondNameR = person.Packecge.льготник.Отчество ?? "".Trim();

                    // Отчетство из ЭСРН.
                    var отчество = itm.Отчество ?? "".Trim();

                    if (secondNameR.ToLower().Trim() == отчество.ToLower().Trim())
                    {
                        flagFirstName[2] = true;
                    }

                    if(person.Packecge.льготник.ДатаРождения.ToShortDateString().Trim() == itm.ДатаРождения.ToShortDateString().Trim())
                    {
                        flagFirstName[3] = true;
                    }


                    // Проверка паспорта.
                    var passwords = this.listPerson.Where(w => w.НаименованиеДокумента.Trim().ToLower() == "Паспорт гражданина России".Trim().ToLower()).OrderBy(w => w.Фамилия).ToList();

                    foreach (var pass in passwords)
                    {
                        var серияПасспортаР = person.Packecge.льготник.СерияПаспорта ?? "".Trim().ToLower();

                        var серияДокумента = itm.СерияДокумента ?? "".Trim().ToLower();

                        if (серияПасспортаР.ToLower().Trim() == серияДокумента.ToLower().Trim())
                        {
                            flagFirstName[4] = true;
                        }

                        var номерПасспортаР = person.Packecge.льготник.НомерПаспорта ?? "".Trim().ToLower();

                        var номерДокумента = itm.НомерДокумента ?? "".Trim().ToLower();

                        if (номерПасспортаР.ToLower().Trim() == номерДокумента.ToLower().Trim())
                        {
                            flagFirstName[5] = true;
                        }

                        // Дата выдачи паспорта.
                        string датаВыдачиПаспорта = string.Empty;

                        // Дата выдачи документа.
                        string датаВыдачиDoc = string.Empty;

                        if (person.Packecge.льготник.ДатаВыдачиПаспорта != null)
                        {
                            датаВыдачиПаспорта = person.Packecge.льготник.ДатаВыдачиПаспорта.ToShortDateString().Trim();
                        }

                        if (itm.ДатаВыдачи != null)
                        {
                            датаВыдачиDoc = itm.ДатаВыдачи.ToShortDateString().Trim();
                        }

                        // Сверим дату выдачи паспартов.
                        if (датаВыдачиПаспорта.ToLower().Trim() == датаВыдачиDoc.ToLower().Trim())
                        {
                            flagFirstName[6] = true;
                        }
                    }

                    // Проверка документов.
                    var docs = this.listPerson.Where(w => w.НаименованиеДокумента.Trim().ToLower() != "Паспорт гражданина России".Trim().ToLower()).OrderBy(w => w.Фамилия).ToList();

                    foreach (var doc in docs)
                    {
                        var серияДокР = person.Packecge.льготник.СерияДокумента ?? "".Trim().ToLower();
                        var серияДокумента = itm.СерияДокумента ?? "".Trim().ToLower();

                        if (серияДокР.ToLower().Trim() == серияДокумента.ToLower().Trim())
                        {
                            flagFirstName[7] = true;
                        }

                        var номерДокументаР = person.Packecge.льготник.НомерДокумента ?? "".Trim().ToLower();

                        var номерДокумента = itm.НомерДокумента ?? "".Trim().ToLower();

                        if (номерДокументаР.ToLower().Trim() == номерДокумента.ToLower().Trim())
                        {
                            flagFirstName[8] = true;
                        }

                        var датаВыдачиДокументаР = string.Empty;
                        var датаВыдачиДокумента = string.Empty;

                        if (person.Packecge.льготник.ДатаВыдачиДокумента != null)
                        {
                            датаВыдачиДокументаР = person.Packecge.льготник.ДатаВыдачиДокумента.ToShortDateString().Trim().ToLower();
                        }

                        if (itm.ДатаВыдачи != null)
                        {
                            датаВыдачиДокумента = itm.ДатаВыдачи.ToShortDateString().Trim().ToLower();
                        }

                        if (датаВыдачиДокументаР.ToLower().Trim() == датаВыдачиДокумента.ToLower().Trim())
                        {
                            flagFirstName[9] = true;
                        }
                    }

                    // Сверяем данные и выставляем флаг проверки.
                    bool flagValidateEsrn = true;

                    foreach(var flag in flagFirstName)
                    {

                        if(flag == false)
                        {
                            flagValidateEsrn = false;
                        }
                    }

                    if (flagValidateEsrn == true)
                    {
                        person.FlagValidateEsrn = true;
                    }
                }
            }

            #endregion
        }
    }
}

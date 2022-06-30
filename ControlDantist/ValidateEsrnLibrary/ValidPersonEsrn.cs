using ControlDantist.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.ValidateEsrnLibrary
{
    public class ValidPersonEsrn : IValidPersonEsrn
    {

        DiscriptionValidate discriptionError;

        public ValidPersonEsrn(DiscriptionValidate discriptionError)
        {
            this.discriptionError = discriptionError ?? throw new ArgumentNullException(nameof(discriptionError));
        }

        public bool FlagValid()
        {
            throw new NotImplementedException();
        }

        public void InstallFlagValid()
        {
            throw new NotImplementedException();
        }

        public void ValidDocument(ItemLibrary itemLibrary, DatePerson datePerson)
        {
            var серияДокР = itemLibrary.Packecge.льготник.СерияДокумента ?? "".Trim().ToLower();
            var серияДокумента = datePerson.СерияДокумента ?? "".Trim().ToLower();

            if (серияДокР.ToLower().Trim() == серияДокумента.ToLower().Trim())
            {
                this.discriptionError.SetFlag(true, "");
            }
            else
            {
                this.discriptionError.SetFlag(false, " Ошибка СЕРИЯ ДОКУМЕНТА ");
            }

            var номерДокументаР = itemLibrary.Packecge.льготник.НомерДокумента ?? "".Trim().ToLower();

            var номерДокумента = datePerson.НомерДокумента ?? "".Trim().ToLower();

            if (номерДокументаР.ToLower().Trim() == номерДокумента.ToLower().Trim())
            {
                this.discriptionError.SetFlag(true, "");
            }
            else
            {
                this.discriptionError.SetFlag(false, " Ошибка НОМЕР ДОКУМЕНТА ");
            }

            var датаВыдачиДокументаР = string.Empty;
            var датаВыдачиДокумента = string.Empty;

            if (itemLibrary.Packecge.льготник.ДатаВыдачиДокумента != null)
            {
                датаВыдачиДокументаР = itemLibrary.Packecge.льготник.ДатаВыдачиДокумента.ToShortDateString().Trim().ToLower();
            }

            if (datePerson.ДатаВыдачи != null)
            {
                датаВыдачиДокумента = datePerson.ДатаВыдачи.ToShortDateString().Trim().ToLower();
            }

            if (датаВыдачиДокументаР.ToLower().Trim() == датаВыдачиДокумента.ToLower().Trim())
            {
                this.discriptionError.SetFlag(true, "");
            }
            else
            {
                this.discriptionError.SetFlag(false, " Дата выдачи ДОКУМЕНТА ");
            }
        }


        /// <summary>
        /// Проверка льготника по ФИО и дате рождения.
        /// </summary>
        /// <param name="itemLibrary">Данные по льготнику из реестра.</param>
        /// <param name="datePerson">Данные по льготнику из ЭСРН.</param>
        public void ValidFioDr(ItemLibrary itemLibrary, DatePerson datePerson)
        {

            if (itemLibrary.Packecge.льготник.Фамилия.ToLower().Trim() == datePerson.Фамилия.ToLower().Trim())
            {
                this.discriptionError.SetFlag(true, "");
            }
            else
            {
                this.discriptionError.SetFlag(false, " Ошибка Фамилия ");
            }

            if (itemLibrary.Packecge.льготник.Имя.ToLower().Trim() == datePerson.Имя.ToLower().Trim())
            {
                this.discriptionError.SetFlag(true, "");
            }
            else
            {
                this.discriptionError.SetFlag(false, " Ошибка Имя ");
            }

            // Отчество из реестра.
            var secondNameR = itemLibrary.Packecge.льготник.Отчество ?? "".Trim();

            // Отчетство из ЭСРН.
            var отчество = datePerson.Отчество ?? "".Trim();

            if (secondNameR.ToLower().Trim() == отчество.ToLower().Trim())
            {
                this.discriptionError.SetFlag(true, "");
            }
            else
            {
                this.discriptionError.SetFlag(false, " Ошибка Отчество ");
            }


            if (itemLibrary.Packecge.льготник.ДатаРождения.ToShortDateString().Trim() == datePerson.ДатаРождения.ToShortDateString().Trim())
            {
                this.discriptionError.SetFlag(true, "");
            }
            else
            {
                this.discriptionError.SetFlag(false, " Ошибка Дата рождения ");
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
                this.discriptionError.SetFlag(true, "");
            }
            else
            {
                this.discriptionError.SetFlag(false, " Ошибка СЕРИЯ ПАСПОРТА ");
            }

            var номерПасспортаР = itemLibrary.Packecge.льготник.НомерПаспорта ?? "".Trim().ToLower();

            var номерДокумента = datePerson.НомерДокумента ?? "".Trim().ToLower();

            if (номерПасспортаР.ToLower().Trim() == номерДокумента.ToLower().Trim())
            {
                this.discriptionError.SetFlag(true, "");
            }
            else
            {
                this.discriptionError.SetFlag(false, " Ошибка НОМЕР ПАСПОРТА ");
            }

            // Дата выдачи паспорта.
            string датаВыдачиПаспорта = string.Empty;

            // Дата выдачи документа.
            string датаВыдачиDoc = string.Empty;

            if (itemLibrary.Packecge.льготник.ДатаВыдачиПаспорта != null)
            {
                датаВыдачиПаспорта = itemLibrary.Packecge.льготник.ДатаВыдачиПаспорта.ToShortDateString().Trim();
            }

            if (datePerson.ДатаВыдачи != null)
            {
                датаВыдачиDoc = datePerson.ДатаВыдачи.ToShortDateString().Trim();
            }

            // Сверим дату выдачи паспартов.
            if (датаВыдачиПаспорта.ToLower().Trim() == датаВыдачиDoc.ToLower().Trim())
            {
                this.discriptionError.SetFlag(true, "");
            }
            else
            {
                this.discriptionError.SetFlag(false, " ДАТА ВЫДАЧИ ПАСПОРТА ");
            }

        }
    }
}

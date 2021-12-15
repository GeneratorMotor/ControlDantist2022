using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.DataBaseContext;

namespace ControlDantist.ExceptionClassess
{
    /// <summary>
    /// Вспомогательный класс проверки модели льготник.
    /// </summary>
    public class ValidErrorPerson
    {
        ТЛЬготник person;
        public ValidErrorPerson(ТЛЬготник person)
        {
            this.person = person;
        }

        public bool Validate()
        {
            bool flagTrue = false;

            if(this.ValidФамилия() == true && this.ValidИмя() == true && this.ValidДатаРождения() == true && this.ValidДатаВыдачиДокумента() == true)
            {
                flagTrue = true;
            }

            return flagTrue;
        }

        private bool ValidФамилия()
        {
            bool flagError = false;

            // Проверим класс.
            if(person.Фамилия != null && person.Фамилия != "")
            {
                flagError = true;
            }

            return flagError;
        }

        private bool ValidИмя()
        {
            bool flagError = false;

            // Проверим класс.
            if (person.Имя != null && person.Имя != "")
            {
                flagError = true;
            }

            return flagError;
        }

        private bool ValidДатаРождения()
        {
            bool flagError = false;

            DateTime dt = new DateTime(1900, 1, 1);

            // Проверим класс.
            if (person.ДатаРождения != null && person.ДатаРождения.Date > dt)
            {
                flagError = true;
            }

            return flagError;
        }

        private bool ValidДатаВыдачиДокумента()
        {
            bool flagError = false;

            DateTime dt = new DateTime(1900, 1, 1);

            // Проверим класс.
            if (person.ДатаВыдачиДокумента != null && person.ДатаВыдачиДокумента.Date > dt)
            {
                flagError = true;
            }

            return flagError;
        }

        private bool ValidДатаВыдачиПаспорта()
        {
            bool flagError = false;

            DateTime dt = new DateTime(1900, 1, 1);

            // Проверим класс.
            if (person.ДатаВыдачиПаспорта != null && person.ДатаВыдачиПаспорта.Date > dt)
            {
                flagError = true;
            }

            return flagError;
        }
    }
}

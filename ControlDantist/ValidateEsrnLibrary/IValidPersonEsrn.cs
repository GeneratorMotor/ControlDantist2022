using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.DataBaseContext;

namespace ControlDantist.ValidateEsrnLibrary
{
    public interface IValidPersonEsrn
    {
        /// <summary>
        /// Проверка по Фамилии.
        /// </summary>
        /// <param name="itemLibrary">Данные из ЭСРН</param>
        /// <param name="datePerson">Данные из файла</param>
        void ValidFirstName(ItemLibrary itemLibrary, DatePerson datePerson);
        void ValidName(ItemLibrary itemLibrary, DatePerson datePerson);
        void ValidSecondName(ItemLibrary itemLibrary, DatePerson datePerson);
        void ValidDr(ItemLibrary itemLibrary, DatePerson datePerson);
        void ValidPassword(ItemLibrary itemLibrary, DatePerson datePerson);
        //void ValidDocument(ItemLibrary itemLibrary, DatePerson datePerson);
        //void InstallFlagValid();
        //bool FlagValid();
    }
}

using ControlDantist.Find;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.FindPersonFullTrue
{
    /// <summary>
    /// Фабричный метод поиска льготников за 2019 год в таблице ДоговорАрхив.
    /// </summary>
    public class FindPersonFSName2019Архив : FindPersonFactory
    {
        private string firstName = string.Empty;
        private string secondName = string.Empty;

        private bool flagValidate = false;

        // Флаг указывает добавлять в поиск Имя.
        private bool flagNAme = false;


        public FindPersonFSName2019Архив(string firstName, string secondName, bool flagValidate, bool flagNAme) : base(firstName, secondName)
        {
            this.firstName = firstName;
            this.secondName = secondName;

            this.flagValidate = flagValidate;

            this.flagNAme = flagNAme;
        }

        public override IFindPerson Query()
        {
            IFindPerson findPerson = new FindPersonFirstNameSecondName2019Архив(this.firstName, this.secondName, this.flagValidate, this.flagNAme);
            return findPerson;
        }
    }
}

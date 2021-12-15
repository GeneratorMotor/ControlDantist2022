using ControlDantist.Find;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.FindPersonFullTrue
{
    /// <summary>
    /// Поиск льготников до 2019 года.
    /// </summary>
    public class FindPersonFSName2019To : FindPersonFactory
    {
        private string firstName = string.Empty;
        private string secondName = string.Empty;

        private bool flagValidate = false;

        // Флаг указывает добавлять в поиск Имя.
        private bool flagNAme = false;

        public FindPersonFSName2019To(string firstName, string secondName, bool flagValidate, bool flagNAme) : base(firstName, secondName)
        {
            this.firstName = firstName;
            this.secondName = secondName;

            this.flagValidate = flagValidate;

            this.flagNAme = flagNAme;
        }


        public override IFindPerson Query()
        {
            IFindPerson findPerson = new FindPersonFirstNameSecondName2019To(this.firstName, this.secondName, this.flagValidate, this.flagNAme);
            return findPerson;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.Find;

namespace ControlDantist.FindPersonFullTrue
{
    public class FindPersonFS2019After : FindPersonFactory
    {
        public string firstName = string.Empty;
        public string secondName = string.Empty;

        // Флаг указывает прошёл льготник проверку или нет.
        private bool flagValidate = false;

        private bool flagName = false;

        public FindPersonFS2019After(string firstName, string secondName, bool flagValidate, bool flagName) : base(firstName, secondName)
        {
            this.firstName = firstName;
            this.secondName = secondName;
            this.flagValidate = flagValidate;

            this.flagName = flagName;
        }

        public override IFindPerson Query()
        {
            IFindPerson findPerson = new FindPersonFirstNameSecondName2019Aftar(this.firstName, this.secondName, this.flagValidate, this.flagName);
            return findPerson;
        }
    }
}

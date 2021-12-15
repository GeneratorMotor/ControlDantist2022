using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.Find;

namespace ControlDantist.FindPersonFullTrue
{
    public class FindPersonFSName : FindPersonFactory
    {

        public string firstName = string.Empty;
        public string secondName = string.Empty;

        // Флаг указывает прошёл льготник проверку или нет.
        private bool flagValidate = false;

        private bool flagFindName = false;

        public FindPersonFSName(string firstName, string secondName, bool flagValidate, bool flagFindName) : base(firstName, secondName)
        {
            this.firstName = firstName;
            this.secondName = secondName;
            this.flagValidate = flagValidate;

            this.flagFindName = flagFindName;
        }

        public override IFindPerson Query()
        {
            IFindPerson findPerson = new FindPersonFirstNameSecondName(this.firstName, this.secondName, this.flagValidate, this.flagFindName);
            return findPerson;
        }
    }
}

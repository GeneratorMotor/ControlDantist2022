using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.Find;

namespace ControlDantist.FindPersonFullTrue
{
    public class FindPersonFSName2019Add : FindPersonFactory
    {
        private string firstName = string.Empty;
        private string secondName = string.Empty;

        private bool flagValidate = false;

        private bool flagNAme = false;

        public FindPersonFSName2019Add(string firstName, string secondName, bool flagValidate, bool flagNAme) : base(firstName, secondName)
        {
            this.firstName = firstName;
            this.secondName = secondName;

            this.flagValidate = flagValidate;

            this.flagNAme = flagNAme;
        }

        public override IFindPerson Query()
        {
            IFindPerson findPerson = new FindPersonFirstNameSecondName2019Add(this.firstName, this.secondName, this.flagValidate, this.flagNAme);
            return findPerson;
        }
    }
}

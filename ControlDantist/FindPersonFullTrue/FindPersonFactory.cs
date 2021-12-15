using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.Find;

namespace ControlDantist.FindPersonFullTrue
{
    public abstract class FindPersonFactory
    {
        string firstName = string.Empty;
        string secondName = string.Empty;

        protected FindPersonFactory(string firstName, string secondName)
        {
            this.firstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            this.secondName = secondName ?? throw new ArgumentNullException(nameof(secondName));
        }

        public abstract IFindPerson Query();
        
    }
}

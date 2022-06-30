using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.DataBaseContext;

namespace ControlDantist.ValidateEsrnLibrary
{
    public interface IValidPersonEsrn
    {
        void ValidFioDr(ItemLibrary itemLibrary, DatePerson datePerson);
        void ValidPassword(ItemLibrary itemLibrary, DatePerson datePerson);
        void ValidDocument(ItemLibrary itemLibrary, DatePerson datePerson);
        void InstallFlagValid();
        bool FlagValid();
    }
}

using System.Collections.Generic;
using ControlDantist.ClassessForDB;

namespace ControlDantist.DisplayLetter
{
    public interface IValidateContract
    {
        IEnumerable<DataPerson> Validate();
    }
}

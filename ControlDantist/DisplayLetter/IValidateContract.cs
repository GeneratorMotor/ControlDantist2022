using System.Collections.Generic;
using ControlDantist.ClassessForDB;

namespace ControlDantist.DisplayLetter
{
    public interface IValidateContract<in T>
    {
        IEnumerable<DataPerson> Validate();
    }
}

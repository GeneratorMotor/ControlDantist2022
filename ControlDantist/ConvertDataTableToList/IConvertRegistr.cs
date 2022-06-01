using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.ConvertDataTableToList
{
    public interface IConvertRegistr<T>
    {
        List<T> GetPersons();
    }
}

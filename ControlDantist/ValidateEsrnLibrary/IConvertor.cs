using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.ValidateEsrnLibrary
{
    public interface IConvertor<T>
    {
        List<T> ConvertDate();
    }
}

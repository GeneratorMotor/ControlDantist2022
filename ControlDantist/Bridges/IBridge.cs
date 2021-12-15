using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.Bridges
{
    public interface IBridge<T>
    {
        List<T> ConvertTo();
    }
}

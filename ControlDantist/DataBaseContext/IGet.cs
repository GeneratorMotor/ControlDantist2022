using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.DataBaseContext
{
    public interface IGet<T>
        where T : class
    {
        T Get();
    }
}

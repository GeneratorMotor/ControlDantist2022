using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.ReadRegistrProject
{
    public interface IReadRegistr<T>
        where T: class
    {
        T Get();
    }
}

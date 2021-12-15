using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.WriteDB
{
    public interface IValidBD<T> where T :class
    {
        //bool Validate(Func<T,bool> predicate);
        bool Validate();

        T Get();
    }
}

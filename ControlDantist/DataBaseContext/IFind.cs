using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.DataBaseContext
{
    public interface IFind<T>
        where T:class
    {
        IQueryable<T> Select(string name);
    }
}

using System;
using System.Collections.Generic;
using System.Data;

namespace ControlDantist.ClassessLimitYear
{
    public interface ILoadDateFactory<T,C>
        where T : class
        where C : class
    {
        ILoadData<T> GetYears();
        ILoadData<C> GetLimit();
    }
}

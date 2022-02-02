using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.ClassessLimitYear
{
    /// <summary>
    /// Интерфейс описывающий загрузку данных в элементы интерфейса.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILoadData<T>
        where T: class
    {
        IEnumerable<T> Load();
    }
}

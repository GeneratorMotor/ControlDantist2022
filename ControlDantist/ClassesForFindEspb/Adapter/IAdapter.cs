using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDantist.ClassesForFindEspb.Adapter
{
    /// <summary>
    /// Адаптер типов.
    /// </summary>
    /// <typeparam name="T">Тип данных получаемый в результате конвертации</typeparam>
    public interface IAdapter<T>
    {
        /// <summary>
        /// Преобразует исходный тип данных в тип T
        /// </summary>
        /// <returns></returns>
        T Convertor();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.ValidateEsrnLibrary;

namespace ControlDantist.ValidateEsrnLibrary.CompareDate
{
    /// <summary>
    /// Сравнение данных из реестра проектов договорв с данными из ЭСРН.
    /// </summary>
    public interface ICompareRegistrPerson
    {
        /// <summary>
        /// Проверка льготника с данными из ЭСРН.
        /// </summary>
        /// <returns>Результат проверки. True проверку на прошел.</returns>
        string ComparePerson();

    }
}

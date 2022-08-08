using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.FactoryConnectionStringBD
{
    /// <summary>
    /// Строки подключения к БД по области.
    /// </summary>
    public interface IConfigConnectionString
    {
        /// <summary>
        /// Словарь со строками подключения к БД ЭСРН в обалсти.
        /// </summary>
        /// <returns>Словарь в качестве ключа название района области, в качестве значения строка подключения к БД ЭСРН.</returns>
        Dictionary<string, string> Select();
    }
}

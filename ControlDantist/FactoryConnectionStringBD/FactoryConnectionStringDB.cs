using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigLibrary;

namespace ControlDantist.FactoryConnectionStringBD
{
    public class FactoryConnectionStringDB
    {
        /// <summary>
        /// Возвращает строки подключения к БД в районах бласти.
        /// </summary>
        /// <returns></returns>
        public IConfig ConnectionStringDB()
        {
            return new Config();
        }
    }
}

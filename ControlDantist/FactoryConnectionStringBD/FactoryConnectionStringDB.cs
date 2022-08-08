using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ControlDantist.FactoryConnectionStringBD
{
    public class FactoryConnectionStringDB
    {
        /// <summary>
        /// Возвращает строки подключения к БД в районах бласти.
        /// </summary>
        /// <returns></returns>
        public IConfigConnectionString ConnectionStringDB()
        {
            return new Config();
        }
    }
}

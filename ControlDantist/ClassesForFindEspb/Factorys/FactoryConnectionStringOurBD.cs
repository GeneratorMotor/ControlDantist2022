using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlDantist.Querys;
using ControlDantist.ClassesForFindEspb.ConnectionStringDb;

namespace ControlDantist.ClassesForFindEspb.Factorys
{
    /// <summary>
    /// Строки подключения к нашему серевреу БД.
    /// </summary>
    public class FactoryConnectionStringOurBD
    {
        /// <summary>
        /// Строка подключения к БД ЕСПБ.
        /// </summary>
        /// <returns></returns>
        public IQuery ConnectionStringEspb()
        {
            return new ConnectionStringBdEspb();
        }

        /// <summary>
        /// Строка подключения к БД с результатами выполнения поиска.
        /// </summary>
        /// <returns></returns>
        public IQuery ConnectionStringDbResult()
        {
            return new ConnectionStringDBTemp();
        }

    }
}

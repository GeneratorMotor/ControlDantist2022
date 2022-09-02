using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlDantist.Querys;

namespace ControlDantist.ClassesForFindEspb.ConnectionStringDb
{
    /// <summary>
    /// Строка подключения к БД ConnectionStringsDirectory которая содержит результат проверки.
    /// </summary>
    public class ConnectionStringDBTemp : IQuery
    {
        /// <summary>
        /// Возвращает строку подключения к БД ConnectionStringsDirectory содержащий результат проверки из ЭСРН.
        /// </summary>
        /// <returns></returns>
        public string Query()
        {
            return " Data Source=10.159.102.21;Initial Catalog=ConnectionStringsDirectory;User ID=sa;Password=sitex ";
        }
    }
}

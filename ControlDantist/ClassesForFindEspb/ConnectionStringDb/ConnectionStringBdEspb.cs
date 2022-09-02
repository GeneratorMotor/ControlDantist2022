using ControlDantist.Querys;

namespace ControlDantist.ClassesForFindEspb.ConnectionStringDb
{
    /// <summary>
    /// Строка подключения к БД ЕСПБ.
    /// </summary>
    public class ConnectionStringBdEspb : IQuery
    {
        /// <summary>
        /// Возвращает строка подключения к БД ЕСПБ.
        /// </summary>
        /// <returns></returns>
        public string Query()
        {
            return " Data Source=10.159.102.21;Initial Catalog=espb;User ID=sa;Password=sitex ";
        }
    }
}

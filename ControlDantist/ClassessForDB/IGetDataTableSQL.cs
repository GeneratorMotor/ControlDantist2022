using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ControlDantist.ClassessForDB
{
    /// <summary>
    /// Контракт на получение информации из БД
    /// </summary>
    public interface  IGetDataTableSQL
    {
        DataTable GetTableSQL();
    }
}

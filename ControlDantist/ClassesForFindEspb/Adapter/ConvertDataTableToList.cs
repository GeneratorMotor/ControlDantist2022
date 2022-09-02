using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDantist.ClassesForFindEspb.Adapter
{
    public class ConvertDataTableToList : IAdapter<IEnumerable<ItemReportEspb>>
    {
        // Преременная для хранения конвертируемых данных.
        private DataTable table;

        /// <summary>
        /// Конвертор DataTab в List
        /// </summary>
        /// <param name="table">Входной параметр типа DataTable</param>
        public ConvertDataTableToList(DataTable table)
        {
            this.table = table ?? throw new ArgumentNullException(nameof(table));
        }

        /// <summary>
        /// Конвертор.
        /// </summary>
        /// <returns>Перечисление</returns>
        public IEnumerable<ItemReportEspb> Convertor()
        {
            return this.table.Rows.OfType<DataRow>().Select(w => new ItemReportEspb
            {
                PC_GUID = w.Field<string>("PC_GUID") ?? "",
                СНИЛС = w.Field<string>("СНИЛС") ?? "",
                НазваниеДокумента = w.Field<string>("НазваниеДокумента")
            }).ToList();
        }
    }
}

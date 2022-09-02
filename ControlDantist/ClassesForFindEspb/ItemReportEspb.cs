using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDantist.ClassesForFindEspb
{
    /// <summary>
    /// Вспомогательный класс представлящий пункт отчтёта.
    /// </summary>
    public class ItemReportEspb
    {
        /// <summary>
        /// GUID персонального дела льготника в ЭСРН.
        /// </summary>
        public string PC_GUID { get; set; }

        /// <summary>
        /// СНИЛС льготника.
        /// </summary>
        public string СНИЛС { get; set; }

        /// <summary>
        /// Название документа.
        /// </summary>
        public string НазваниеДокумента { get; set; }

    }
}

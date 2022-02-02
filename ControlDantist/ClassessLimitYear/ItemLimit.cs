using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.ClassessLimitYear
{
    public class ItemLimit
    {
        /// <summary>
        /// id записи.
        /// </summary>
        public int idLimitYear { get; set; }

        /// <summary>
        /// Год.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Лимит за год.
        /// </summary>
        public decimal LimitMoneyYear { get; set; }
    }
}

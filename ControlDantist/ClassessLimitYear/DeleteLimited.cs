using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.Querys;

namespace ControlDantist.ClassessLimitYear
{
    /// <summary>
    /// Удаление лимита.
    /// </summary>
    public class DeleteLimited : IQuery
    {
        private int idLimitYear = 0;

        public DeleteLimited(int idLimitYear)
        {
            this.idLimitYear = idLimitYear;
        }

        public string Query()
        {
            return @" DELETE FROM LimitMoneyYear
                      WHERE idLimitYear = "+ this.idLimitYear +" ";
        }
    }
}

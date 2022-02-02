using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.Querys;

namespace ControlDantist.ClassessLimitYear
{
    /// <summary>
    /// Обновление лимита на год.
    /// </summary>
    public class UpdateLimited : IQuery
    {
        private int Year = 0;
        private string summ = string.Empty;
        private int idLimitYear = 0;

        public UpdateLimited(int year, string summ, int idLimitYear)
        {
            Year = year;
            this.summ = summ;
            this.idLimitYear = idLimitYear;
        }

        public string Query()
        {
            return @" UPDATE[LimitMoneyYear]
                    SET[Year] = " + this.Year + " " +
                    " ,[LimitMoneyYear] = CAST('" +  this.summ + "' AS money) " +
                    " WHERE idLimitYear = " + idLimitYear +" ";
        }
    }
}

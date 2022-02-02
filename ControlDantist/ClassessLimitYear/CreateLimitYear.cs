using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.Querys;

namespace ControlDantist.ClassessLimitYear
{
    /// <summary>
    /// Создание строк запроса на выполнение GRUID
    /// </summary>
    public class CreateLimitYear
    {
        public IQuery CreateLimited(string money,int year)
        {
            return new InsertLimit(money, year);
        }

        public IQuery UpdateLimited(int year, string money, int idLimitYear)
        {
            return new UpdateLimited(year, money, idLimitYear);
        }

        public IQuery DeleteLimited(int idLimitYear)
        {
            return new DeleteLimited(idLimitYear);
        }

        public IQuery GetYear(int year)
        {
            return new SqlGetLimit(year);
        }


    }
}

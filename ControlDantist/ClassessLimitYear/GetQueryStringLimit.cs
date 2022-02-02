using ControlDantist.Querys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.ClassessLimitYear
{
    public class GetQueryStringLimit : IGetSqlQuery
    {
        private int year = 0;

        public GetQueryStringLimit(int year)
        {
            this.year = year;
        }

        public IQuery GetSqlString()
        {
            return new SqlQueryLimitYear(this.year);
        }
    }
}

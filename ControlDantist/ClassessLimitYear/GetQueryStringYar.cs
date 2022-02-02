using ControlDantist.Querys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.ClassessLimitYear
{
    public class GetQueryStringYar : IGetSqlQuery
    {
        public IQuery GetSqlString()
        {
            return new SqlQueryYear();
        }
    }
}

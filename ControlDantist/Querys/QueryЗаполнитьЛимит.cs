using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.Querys
{
    public class QueryЗаполнитьЛимит : IQuery
    {
        private int year = 0;

        public QueryЗаполнитьЛимит(int year)
        {
            this.year = year;
        }

        public string Query()
        {
            return @"SELECT[id_льготнойКатегории]
                  ,[ЛьготнаяКатегория]
                  ,[Limit]
                  ,[idLimitMoney]
                  ,[Year]
                        FROM[Dentists].[dbo].[ViewDisplayLimit]
                  where[Year] = "+ this.year +" ";
        }
    }
}

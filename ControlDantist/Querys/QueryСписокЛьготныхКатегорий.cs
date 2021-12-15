using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.Querys
{
    public class QueryСписокЛьготныхКатегорий : IQuery
    {
        public string Query()
        {
            return @" select ЛьготнаяКатегория from ЛьготнаяКатегория
                    where id_льготнойКатегории<> 42 ";
        }
    }
}

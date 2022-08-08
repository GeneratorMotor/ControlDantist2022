using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.Querys;

namespace ControlDantist.ValididtyTicket
{
    public class QueryPC_GUID : IQuery
    {
        public string Query()
        {
            return " select distinct PC_GUID from [DateEspb] ";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.Querys;

namespace ControlDantist.ValididtyTicket
{
    public class QueryBarCode : IQuery
    {
        public string Query()
        {
            return " select BAR_CODE from TableBarCode ";
        }
    }
}

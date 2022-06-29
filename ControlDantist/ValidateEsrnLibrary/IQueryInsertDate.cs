using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.Querys;

namespace ControlDantist.ValidateEsrnLibrary
{
    public interface IQueryInsertDate : IQuery
    {
        string Query(string nameTable);
    }
}

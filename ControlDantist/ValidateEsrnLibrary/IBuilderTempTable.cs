using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.ValidateEsrnLibrary
{
    public interface IBuilderTempTable
    {
        void CreateTempTable();
        void InsertDateTempTable();
        void FindPerson();
        string GetQuery();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ControlDantist.ClassessForDB
{
    public interface IConvertDataLetter
    {
        void ConvertDate(DataTable table);
    }
}

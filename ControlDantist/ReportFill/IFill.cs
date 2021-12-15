using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace ControlDantist.ReportFill
{
    public interface IFill
    {
        void Fill(DataTable dt);
    }
}

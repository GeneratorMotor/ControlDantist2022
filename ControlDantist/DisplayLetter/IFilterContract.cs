using System;
using System.Collections.Generic;
using ControlDantist.Classes;
using System.Text;

namespace ControlDantist.DisplayLetter
{
    public interface IFilterContract
    {
        IEnumerable<PrintContractsValidate> GetContracts();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.ClassessForDB
{
    public class DataPerson
    {
        public PersonContract PC { get; set; }

        public List<DataPersonContract> DataContracts { get; set; }
    }
}

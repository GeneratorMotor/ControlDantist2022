using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.ClassessForDB
{
    public class DataPerson
    {
        public DataPerson()
        {
            this.DataContracts = new List<DataPersonContract>();
        }

        public PersonContract PC { get; set; }

        public List<DataPersonContract> DataContracts { get; set; }

        public bool FlagValidate { get; set; }

        /// <summary>
        /// Указывает ято у данного льготника найдены ранее заключенные договора.
        /// </summary>
        public bool FlagDatatLetter { get; set; }
    }
}

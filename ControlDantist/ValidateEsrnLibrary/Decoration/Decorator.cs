using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.ValidateEsrnLibrary.Decoration
{
    public abstract class Decorator : Component
    {
        public Component GetComponent { get; set; }

        public override IEnumerable<DiscriptionValidate> ComparePerson()
        {
            List<DiscriptionValidate> list = new List<DiscriptionValidate>();

            if(GetComponent != null)
            {
                list.AddRange(GetComponent.ComparePerson());
            }

            return list;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.DataBaseContext;

namespace ControlDantist.ClassErrors
{
    public class ConvertДоговор
    {
        Т2Договор tdoc;
        ТДоговор doc;

        public ConvertДоговор(ТДоговор doc)
        {
            this.doc = doc ?? throw new ArgumentNullException(nameof(doc));
            tdoc = new Т2Договор();
        }

        public Т2Договор Convert()
        {
            tdoc.НомерДоговора = doc.НомерДоговора;

            return tdoc;
        }

    }
}

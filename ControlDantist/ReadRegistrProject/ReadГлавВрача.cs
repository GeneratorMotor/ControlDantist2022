using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.DataBaseContext;

namespace ControlDantist.ReadRegistrProject
{
    public class ReadГлавВрача : IReadRegistr<ТФиоГлавВрач>
    {
        private DContext dc;
        private string инн = string.Empty;

        public ReadГлавВрача(DContext dc, string инн)
        {
            this.dc = dc;
            this.инн = инн;
        }

        public ТФиоГлавВрач Get()
        {
            ТФиоГлавВрач тФиоГлавВрач = new ТФиоГлавВрач();

            ТФиоГлавВрач тФио = dc.ТФиоГлавВрач.Where(w => w.ИНН_поликлинники == this.инн).FirstOrDefault();

            if (тФио != null)
                тФиоГлавВрач = тФио;

            return тФиоГлавВрач;
        }
    }
}

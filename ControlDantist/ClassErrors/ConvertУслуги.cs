using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.DataBaseContext;


namespace ControlDantist.ClassErrors
{
    public class ConvertУслуги
    {
        private List<ТУслугиПоДоговору> listUc;

        private List<Т2УслугиПоДоговору> list;

        public ConvertУслуги(List<ТУслугиПоДоговору> listUc)
        {
            this.listUc = listUc ?? throw new ArgumentNullException(nameof(listUc));

            list = new List<Т2УслугиПоДоговору>();
        }

        public List<Т2УслугиПоДоговору> Convert()
        {
            foreach(var it in listUc)
            {
                Т2УслугиПоДоговору i = new Т2УслугиПоДоговору();
                i.id_договор = it.id_договор;
                i.id_услугиДоговор = it.id_услугиДоговор;
                i.Количество = it.Количество;
                i.НаименованиеУслуги = it.НаименованиеУслуги;
                i.НомерПоПеречню = it.НомерПоПеречню;
                i.Сумма = it.Сумма;
                i.цена = it.цена;

                list.Add(i);
            }

            return list;
        }

    }
}

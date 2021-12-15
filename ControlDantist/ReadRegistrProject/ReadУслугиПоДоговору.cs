using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ControlDantist.DataBaseContext;

namespace ControlDantist.ReadRegistrProject
{
    public class ReadУслугиПоДоговору 
    {
        private DataTable tab;

        public ReadУслугиПоДоговору(DataTable tab)
        {
            this.tab = tab;
        }

        public List<ТУслугиПоДоговору> Get()
        {
            List<ТУслугиПоДоговору> list = new List<ТУслугиПоДоговору>();

            foreach (DataRow row in tab.Rows)
            {
                ТУслугиПоДоговору tu = new ТУслугиПоДоговору();

                tu.НаименованиеУслуги = row["НаименованиеУслуги"].ToString();
                tu.Количество = Convert.ToInt32(row["Количество"]);
                tu.НомерПоПеречню = row["НомерПоПеречню"].ToString();
                tu.Сумма = Convert.ToDecimal(row["Сумма"]);
                tu.ТехЛист = Convert.ToInt16(row["ТехЛист"]);
                tu.цена = Convert.ToDecimal(row["цена"]);
                tu.id_договор = 0;

                list.Add(tu);
            }

            return list;
        }
    }
}

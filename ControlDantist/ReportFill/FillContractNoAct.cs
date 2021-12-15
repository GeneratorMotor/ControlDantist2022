using System;
using System.Collections.Generic;
using System.Data;
using ControlDantist.ReportCountYear;
using System.Linq;

namespace ControlDantist.ReportFill
{
    /// <summary>
    /// Заполняет договора без акта.
    /// </summary>
    public class FillContractNoAct : IFill
    {
        private List<CountContractForYear> list;

        public FillContractNoAct(List<CountContractForYear> list)
        {
            this.list = list ?? throw new ArgumentNullException(nameof(list));
        }

        public void Fill(DataTable dt)
        {
            if (dt != null && dt.Rows != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string lk = row["ЛьготнаяКатегория"].ToString().Trim();

                    var item = list.Where(w => w.ЛьготнаяКатегория.ToLower().Trim() == lk.ToLower().Trim()).FirstOrDefault();

                    if (item != null)
                    {
                        item.КоличествоДоговоровБезАкта = Convert.ToInt32(row["id"]);
                        decimal v = Convert.ToDecimal(row["Сумма"]);
                        item.СуммаДоговоровБезАкт = v;
                    }
                }

            }
            
        }
    }
}

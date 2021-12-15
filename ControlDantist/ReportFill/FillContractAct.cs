using System;
using System.Collections.Generic;
using System.Data;
using ControlDantist.ReportCountYear;
using System.Linq;

namespace ControlDantist.ReportFill
{
    public class FillContractAct : IFill
    {

        private List<CountContractForYear> list;

        public FillContractAct(List<CountContractForYear> list)
        {
            this.list = list ?? throw new ArgumentNullException(nameof(list));
        }

        public void Fill(DataTable dt)
        {
            if (dt != null && dt.Rows != null)
            {
                foreach (DataRow row1 in dt.Rows)
                {
                    string lk = row1["ЛьготнаяКатегория"].ToString().Trim();

                    var item = list.Where(w => w.ЛьготнаяКатегория.ToLower().Trim() == lk.ToLower().Trim()).FirstOrDefault();

                    if (item != null)
                    {
                        item.КоличествоДоговоровАкт = Convert.ToInt32(row1["id"]);
                        decimal v = Convert.ToDecimal(row1["Сумма"]);
                        item.СуммаДоговоровАкт = v;
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ControlDantist.ReportCountYear;

namespace ControlDantist.ReportFill
{
    public class FillLimit : IFill
    {
        private List<CountContractForYear> list;

        public FillLimit(List<CountContractForYear> list)
        {
            this.list = list ?? throw new ArgumentNullException(nameof(list));
        }

        /// <summary>
        /// Заполняет список лимитами на год.
        /// </summary>
        /// <param name="dt"></param>
        void IFill.Fill(DataTable dt)
        {
            if (dt != null && dt.Rows != null)
            {
                foreach (DataRow row1 in dt.Rows)
                {
                    string lk = row1["ЛьготнаяКатегория"].ToString().Trim();

                    var item = list.Where(w => w.ЛьготнаяКатегория.ToLower().Trim() == lk.ToLower().Trim()).FirstOrDefault();

                    if (item != null)
                    {
                        decimal v = Convert.ToDecimal(row1["Limit"]);
                        item.ЛимитГод = v;
                    }
                }
            }
        }
    }
}

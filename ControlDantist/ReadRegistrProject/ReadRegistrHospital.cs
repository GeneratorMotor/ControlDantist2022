using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.DataBaseContext;
using System.Data;

namespace ControlDantist.ReadRegistrProject
{
    public class ReadRegistrHospital : IReadRegistr<ТПоликлинника>
    {
        private DContext dc;
        private string инн = string.Empty;
        private DataRow row;

        public ReadRegistrHospital(DContext dc, string инн, DataRow row)
        {
            this.dc = dc;
            this.инн = инн;
            this.row = row;
        }

        public ТПоликлинника Get()
        {
            ТПоликлинника hosp = new ТПоликлинника();
            
            var rez = dc.ТПоликлинника?.Where(w => w.ИНН == this.инн)?.OrderByDescending(w=>w.id_поликлинника)?.FirstOrDefault() ?? null; 
            if(rez != null)
            {
                hosp = rez;
            }
            else
            {
                hosp.НаименованиеПоликлинники = this.row["НаименованиеПоликлинники"].ToString();
                hosp.id_главБух = 0;
                hosp.id_главВрач = 0;
                hosp.ИНН = this.row["ИНН"].ToString();
                hosp.ДатаРегистрацииЛицензии = Convert.ToDateTime(this.row["ИНН"]);
                hosp.КодПоликлинники = "";
                hosp.ЮридическийАдрес = "";
                hosp.ФактическийАдрес = "";
                hosp.СвидетельствоРегистрации = "";
                hosp.КПП = "";
                hosp.БИК = "";
                hosp.НаименованиеБанка = "";
                hosp.РасчётныйСчёт = "";
                hosp.ЛицевойСчёт = "";
                hosp.НомерЛицензии = "";
                hosp.ОГРН = "";
                hosp.СвидетельствоРегистрации = "";
                hosp.СвидетельствоРегистрацииЕГРЮЛ = "";
                hosp.ОрганВыдавшийЛицензию = "";
                hosp.Постановление = "";
                hosp.ОКПО = "";
                hosp.ОКАТО = "";
                hosp.НачальныйНомерДоговора = 0;
                hosp.Flag = 0;

                dc.ТПоликлинника.Add(hosp);
                dc.SaveChanges();
            }

            return hosp;
        }
    }
}

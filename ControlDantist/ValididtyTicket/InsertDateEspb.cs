using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.Querys;
using System.Data;

namespace ControlDantist.ValididtyTicket
{
    public class InsertDateEspb : IQuery
    {
        DataTable tab;

        public InsertDateEspb(DataTable tab)
        {
            this.tab = tab;
        }

        public string Query()
        {
            StringBuilder builder = new StringBuilder();

            foreach(DataRow row in this.tab.Rows)
            {
                string insert = " insert into DateEsrn (BAR_CODE,Год,Месяц,МесяцГод,Login,Реализатор,НазваниеПеревозчика,ОткудаПеревозчик,PC_GUID,СерияДокумента,НомерДокумента,НазваниеДокумента,ДатаДокумента) " +
                                " values('"+row["BAR_CODE"].ToString()+"','"+ row["Год"].ToString() + "','"+ row["Месяц"].ToString() + "','" + row["МесяцГод"].ToString() + "','" + row["Login"].ToString() + "', " + 
                                " '" + row["Реализатор"].ToString() + "','"+ row["НазваниеПеревозчика"].ToString() + "', " + 
                                " '"+ row["ОткудаПеревозчик"].ToString() + "','" + row["PC_GUID"].ToString() + "','" + row["СерияДокумента"].ToString() + "', " + 
                                " '"+ row["НомерДокумента"].ToString() + "','"+ row["НазваниеДокумента"].ToString() + "','"+ row["ДатаДокумента"].ToString() + "') ";


                builder.Append(insert);

                //select ESPB_IMPLEMENTED.BAR_CODE, [PERIOD_YEAR] as 'Год',PERIOD_MONTH as 'Месяц', PERIOD_NAME as 'Месяц',ESPB_EXT_USER.[LOGIN] as 'Login',
                //    TabРеализатор.TYPE as 'Реализатор',TabПеревозчик.NAME as 'НазваниеПеревозчика',
                //        TabПеревозчик.DATA as 'ОткудаПеревозчик', 
                //    PC_GUID,ESPB_ENTITLING_DOCUMENT.DOCUMENT_SERIES as 'СерияДокумента',
                //        ESPB_ENTITLING_DOCUMENT.DOCUMENT_NUMBER as 'НомерДокумента'
                //    ,SPR_DOC_TYPE.A_NAME as 'НазваниеДокумента',
                //    ESPB_ENTITLING_DOCUMENT.DATE_ISSUE as 'ДатаДокумента' from ESPB_IMPLEMENTED

            }

            return builder.ToString().Trim();
        }
    }
}

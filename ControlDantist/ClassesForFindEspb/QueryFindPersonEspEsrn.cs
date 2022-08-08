using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.Querys;

namespace ControlDantist.ClassesForFindEspb
{
    /// <summary>
    /// Поиск льготника в ЭСРН.
    /// </summary>
    public class QueryFindPersonEspEsrn : IQuery
    {

        string nameTemptable = string.Empty;

        public QueryFindPersonEspEsrn(string nameTemptable)
        {
            this.nameTemptable = nameTemptable ?? throw new ArgumentNullException(nameof(nameTemptable));
        }

        /// <summary>
        /// SQL запрос поиск льготника по GUID в базах ЭСРН.
        /// </summary>
        /// <returns></returns>
        public string Query()
        {
            string query =  @" select  GUID,Tab1.Фамилия,Tab1.Имя,Tab1.Отчество,A_ADRTITLE as 'Адрес',BIRTHDATE as 'ДатаРождения', "+ this.nameTemptable + ".СерияДокумента, "+ this.nameTemptable + ".НомерДокумента , " +
                            " "+ this.nameTemptable + ".НазваниеДокумента , "+ this.nameTemptable + ".ДатаДокумента , "+ this.nameTemptable + ".Категория from( " +
                               @" select WM_PERSONAL_CARD.GUID, SPR_FIO_SURNAME.A_NAME as Фамилия,dbo.SPR_FIO_NAME.A_NAME as 'Имя',SPR_FIO_SECONDNAME.A_NAME as 'Отчество',WM_ACTDOCUMENTS.DOCUMENTSTYPE, 
                               WM_ACTDOCUMENTS.DOCUMENTSERIES ,WM_ACTDOCUMENTS.DOCUMENTSNUMBER ,WM_ACTDOCUMENTS.ISSUEEXTENSIONSDATE ,PPR_DOC.A_NAME,WM_ADDRESS.A_ADRTITLE, 
                                WM_PERSONAL_CARD.BIRTHDATE ,dbo.WM_PERSONAL_CARD.A_SNILS, dbo.REGISTER_CONFIG.A_REGREGIONCODE from dbo.WM_PERSONAL_CARD
                                join  SPR_FIO_SURNAME
                                 on WM_PERSONAL_CARD.SURNAME = SPR_FIO_SURNAME.OUID
                                  join SPR_FIO_NAME
                                  on SPR_FIO_NAME.OUID = WM_PERSONAL_CARD.A_NAME
                                  join SPR_FIO_SECONDNAME
                                  on SPR_FIO_SECONDNAME.OUID = WM_PERSONAL_CARD.A_SECONDNAME
                                  join dbo.WM_ACTDOCUMENTS
                                  on WM_PERSONAL_CARD.OUID = dbo.WM_ACTDOCUMENTS.PERSONOUID
                                 join PPR_DOC
                                 on WM_ACTDOCUMENTS.DOCUMENTSTYPE = PPR_DOC.A_ID
                                  join WM_ADDRESS
                                  on WM_ADDRESS.OUID = WM_PERSONAL_CARD.A_REGFLAT
                                  inner join WM_CATEGORY
								  on WM_PERSONAL_CARD.OUID = WM_CATEGORY.PERSONOUID
                                 CROSS JOIN dbo.REGISTER_CONFIG
                                  where  WM_PERSONAL_CARD.A_PCSTATUS = 1 and (A_DATELAST is null or A_DATELAST >= GETDATE()) and (WM_ACTDOCUMENTS.completionsactiondate is null or WM_ACTDOCUMENTS.completionsactiondate >= getdate())
								  and( WM_PERSONAL_CARD.A_STATUS = 10) and (WM_ACTDOCUMENTS.A_STATUS = 10) ) as Tab1 
                                  inner join " + this.nameTemptable + " " +
                                  " on Tab1.GUID = "+ this.nameTemptable + ".PC_GUID ";

            return query;
        }
    }
}

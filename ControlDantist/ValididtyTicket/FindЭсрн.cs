using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.Querys;

namespace ControlDantist.ValididtyTicket
{
    public class FindЭсрн : IQuery
    {
        string nameTemptable = string.Empty;
        public FindЭсрн(string nameTemptable)
        {
            this.nameTemptable = nameTemptable ?? throw new ArgumentNullException(nameof(nameTemptable));
        }
        public string Query()
        {
            return @" select WM_PERSONAL_CARD.[GUID], WM_PERSONAL_CARD.OUID, SPR_FIO_SURNAME.A_NAME as Фамилия,
                        dbo.SPR_FIO_NAME.A_NAME as 'Имя',
                        SPR_FIO_SECONDNAME.A_NAME as 'Отчество',
                        WM_ACTDOCUMENTS.DOCUMENTSTYPE,WM_ACTDOCUMENTS.[GUID] as 'DocGuid',  WM_ACTDOCUMENTS.DOCUMENTSERIES as 'СерияДокумента' ,WM_ACTDOCUMENTS.DOCUMENTSNUMBER as 'НомерДокумента' ,
                        WM_ACTDOCUMENTS.ISSUEEXTENSIONSDATE ,PPR_DOC.A_NAME,WM_ADDRESS.A_ADRTITLE 'АдресЖительства',  WM_PERSONAL_CARD.BIRTHDATE as 'ДатаРождения' ,
                        dbo.WM_PERSONAL_CARD.A_SNILS, dbo.REGISTER_CONFIG.A_REGREGIONCODE from dbo.WM_PERSONAL_CARD  
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
                        CROSS JOIN dbo.REGISTER_CONFIG 
                        inner join " + this.nameTemptable + " " +
                        " on " + this.nameTemptable + ".PC_GUID = WM_PERSONAL_CARD.[GUID] ";
        }
    }
}

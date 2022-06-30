using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.Querys;

namespace ControlDantist.ValididtyTicket
{
    public class FIndTicket : IQuery
    {

        string nameTemptable = string.Empty;
        public FIndTicket(string nameTemptable)
        {
            this.nameTemptable = nameTemptable ?? throw new ArgumentNullException(nameof(nameTemptable));
        }
        public string Query()
        {
            return @" select ESPB_IMPLEMENTED.BAR_CODE, [PERIOD_YEAR] as 'Год',PERIOD_MONTH as 'Месяц', PERIOD_NAME as 'МесяцГод',ESPB_EXT_USER.[LOGIN] as 'Login',TabРеализатор.TYPE as 'Реализатор',TabПеревозчик.NAME as 'НазваниеПеревозчика',
                        TabПеревозчик.DATA as 'ОткудаПеревозчик', PC_GUID,ESPB_ENTITLING_DOCUMENT.DOCUMENT_SERIES as 'СерияДокумента',
                        ESPB_ENTITLING_DOCUMENT.DOCUMENT_NUMBER as 'НомерДокумента',SPR_DOC_TYPE.A_NAME as 'НазваниеДокумента',ESPB_ENTITLING_DOCUMENT.DATE_ISSUE as 'ДатаДокумента' from ESPB_IMPLEMENTED
                        inner join ESPB_VALIDITY
                        on ESPB_IMPLEMENTED.VALIDITY = ESPB_VALIDITY.OUID
                        inner join ESPB_EXT_USER
                        on ESPB_EXT_USER.OUID = ESPB_IMPLEMENTED.[USER]
                        left outer join (select ESPB_EXT_USER.OUID,ESPB_IMPLEMENTER.TYPE from ESPB_EXT_USER 
                        inner join ESPB_IMPLEMENTER
                        on ESPB_IMPLEMENTER.OUID = ESPB_EXT_USER.ORGANIZATION) as TabРеализатор
                        on TabРеализатор.OUID = ESPB_IMPLEMENTED.[USER]
                        inner join (select ESPB_IMPLEMENTED, OSZN_USER,ESPB_CARRIER.NAME,ESPB_CARRIER.DATA, ESPB_BARCODE from ESPB_RECEIVED
                        inner join ESPB_CARRIER
                        on ESPB_RECEIVED.CARRIER = ESPB_CARRIER.OUID) as TabПеревозчик
                        on ESPB_IMPLEMENTED.OUID = TabПеревозчик.ESPB_IMPLEMENTED
                        inner join ESPB_PERSONAL_CARD
                        ON ESPB_PERSONAL_CARD.OUID = ESPB_IMPLEMENTED.PERSONAL_CARD
                        inner join ESPB_ENTITLING_DOCUMENT
                        on ESPB_IMPLEMENTED.BASE_DOCUMENT = ESPB_ENTITLING_DOCUMENT.OUID
                        inner join SPR_DOC_TYPE
                        ON SPR_DOC_TYPE.OUID = ESPB_ENTITLING_DOCUMENT.DOCUMENT_TYPE 
                        inner join " + this.nameTemptable + " " +
                        " on " + this.nameTemptable + ".BAR_CODE = ESPB_IMPLEMENTED.BAR_CODE ";
        }
    }
}

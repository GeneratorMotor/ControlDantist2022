using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.Querys;

namespace ControlDantist.ClassesForFindEspb
{
    /// <summary>
    /// SQL запрос на получение данных о льготниках без СНИЛС.
    /// </summary>
    public class SqlQueryDatePersonEspb : IQuery
    {
        public string Query()
        {
            //return @" select ESPB_PERSONAL_CARD.PC_GUID, ESPB_PERSONAL_CARD.SNILS as 'СНИЛС',ESPB_ENTITLING_DOCUMENT.DOCUMENT_SERIES as 'СерияДокумента',
            //        ESPB_ENTITLING_DOCUMENT.DOCUMENT_NUMBER as 'НомерДокумента',[ESPB_DOCUMENT_TYPE].TYPE_NAME as 'НазваниеДокумента',
            //        CAST(ESPB_ENTITLING_DOCUMENT.CREATEDATE as DATE) 'ДатаДокумента', 
            //        CAST(ESPB_ENTITLING_DOCUMENT.DATE_EXPIRATION as DATE) 'ДатаОкончанияДействияДокумента'
            //        ,ESPB_BENEFITS_CATEGORY.NAME as 'Категория'
            //         from ESPB_PERSONAL_CARD
            //        inner join ESPB_ENTITLING_DOCUMENT
            //        on ESPB_PERSONAL_CARD.OUID = ESPB_ENTITLING_DOCUMENT.PERSONAL_CARD
            //        inner
            //         join [dbo].[ESPB_DOCUMENT_TYPE]
            //        --inner join SPR_DOC_TYPE
            //        ON [ESPB_DOCUMENT_TYPE].OUID = ESPB_ENTITLING_DOCUMENT.DOCUMENT_TYPE
            //        INNER JOIN
            //                ESPB_BENEFITS_CATEGORY ON
            //                ESPB_ENTITLING_DOCUMENT.BENEFITS_CATEGORY = ESPB_BENEFITS_CATEGORY.OUID
            //        where ESPB_ENTITLING_DOCUMENT.DATE_EXPIRATION is null and
            //        ESPB_ENTITLING_DOCUMENT.CREATEDATE < GETDATE() and ESPB_PERSONAL_CARD.SNILS is null ";


            return @" select TabPersonalCard.PC_GUID,СНИЛС,НазваниеДокумента,COUNT(СНИЛС) from (
select ESPB_PERSONAL_CARD.PC_GUID from(
select PERSONAL_CARD, COUNT(PERSONAL_CARD) as 'КоличествоКарт' from ESPB_IMPLEMENTED
where VALIDITY = 85
GROUP BY PERSONAL_CARD
) as EspbImplemented
inner join ESPB_PERSONAL_CARD
on ESPB_PERSONAL_CARD.OUID = EspbImplemented.PERSONAL_CARD) as TabPersonalCard
inner join(
select ESPB_PERSONAL_CARD.PC_GUID, ESPB_PERSONAL_CARD.SNILS as 'СНИЛС',ESPB_ENTITLING_DOCUMENT.DOCUMENT_SERIES as 'СерияДокумента',
ESPB_ENTITLING_DOCUMENT.DOCUMENT_NUMBER as 'НомерДокумента',[ESPB_DOCUMENT_TYPE].TYPE_NAME as 'НазваниеДокумента',
CAST(ESPB_ENTITLING_DOCUMENT.DATE_ISSUE as DATE) 'ДатаДокумента', 
CAST(ESPB_ENTITLING_DOCUMENT.DATE_EXPIRATION as DATE) 'ДатаОкончанияДействияДокумента'
,ESPB_BENEFITS_CATEGORY.NAME as 'Категория'--, ESPB_ENTITLING_DOCUMENT.CANCELLATION_DATE
from ESPB_PERSONAL_CARD
inner join ESPB_ENTITLING_DOCUMENT
on ESPB_PERSONAL_CARD.OUID = ESPB_ENTITLING_DOCUMENT.PERSONAL_CARD
inner join[dbo].[ESPB_DOCUMENT_TYPE]
            ON[ESPB_DOCUMENT_TYPE].OUID = ESPB_ENTITLING_DOCUMENT.DOCUMENT_TYPE
INNER JOIN
        ESPB_BENEFITS_CATEGORY ON
        ESPB_ENTITLING_DOCUMENT.BENEFITS_CATEGORY = ESPB_BENEFITS_CATEGORY.OUID
where ESPB_ENTITLING_DOCUMENT.DATE_EXPIRATION is null-- and ESPB_ENTITLING_DOCUMENT.CANCELLATION_DATE is null
-- and ESPB_ENTITLING_DOCUMENT.DATE_ISSUE <= GETDATE()) as Tab
and ESPB_ENTITLING_DOCUMENT.DATE_ISSUE is null) as Tab
on TabPersonalCard.PC_GUID = Tab.PC_GUID
group by СНИЛС,TabPersonalCard.PC_GUID,СНИЛС,НазваниеДокумента
order by COUNT(СНИЛС) desc ";
        }
    }
}

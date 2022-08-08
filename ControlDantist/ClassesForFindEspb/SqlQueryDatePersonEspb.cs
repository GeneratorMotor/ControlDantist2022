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
            return @" select ESPB_PERSONAL_CARD.PC_GUID, ESPB_PERSONAL_CARD.SNILS as 'СНИЛС',ESPB_ENTITLING_DOCUMENT.DOCUMENT_SERIES as 'СерияДокумента',
                    ESPB_ENTITLING_DOCUMENT.DOCUMENT_NUMBER as 'НомерДокумента',[ESPB_DOCUMENT_TYPE].TYPE_NAME as 'НазваниеДокумента',
                    CAST(ESPB_ENTITLING_DOCUMENT.CREATEDATE as DATE) 'ДатаДокумента', 
                    CAST(ESPB_ENTITLING_DOCUMENT.DATE_EXPIRATION as DATE) 'ДатаОкончанияДействияДокумента'
                    ,ESPB_BENEFITS_CATEGORY.NAME as 'Категория'
                     from ESPB_PERSONAL_CARD
                    inner join ESPB_ENTITLING_DOCUMENT
                    on ESPB_PERSONAL_CARD.OUID = ESPB_ENTITLING_DOCUMENT.PERSONAL_CARD
                    inner
                     join [dbo].[ESPB_DOCUMENT_TYPE]
                    --inner join SPR_DOC_TYPE
                    ON [ESPB_DOCUMENT_TYPE].OUID = ESPB_ENTITLING_DOCUMENT.DOCUMENT_TYPE
                    INNER JOIN
                            ESPB_BENEFITS_CATEGORY ON
                            ESPB_ENTITLING_DOCUMENT.BENEFITS_CATEGORY = ESPB_BENEFITS_CATEGORY.OUID
                    where ESPB_ENTITLING_DOCUMENT.DATE_EXPIRATION is null and
                    ESPB_ENTITLING_DOCUMENT.CREATEDATE < GETDATE() and ESPB_PERSONAL_CARD.SNILS is null ";
        }
    }
}

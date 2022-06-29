using System;
using ControlDantist.Querys;

namespace ControlDantist.ValidateEsrnLibrary
{
    public class FindPersonPasswordQuery : IQuery
    {
        string nameTemptable = string.Empty;

        public FindPersonPasswordQuery(string nameTemptable)
        {
            this.nameTemptable = nameTemptable ?? throw new ArgumentNullException(nameof(nameTemptable));
        }

        public string Query()
        {
            string queryJoin = @"select  " + this.nameTemptable + ".id_договор, OUID,Tab1.Фамилия,Tab1.Имя,Tab1.Отчество,DOCUMENTSTYPE,DOCUMENTSERIES as 'Серия документа',DOCUMENTSNUMBER as 'Номер документа',ISSUEEXTENSIONSDATE as 'дата выдачи',A_NAME,A_ADRTITLE as 'Адрес',BIRTHDATE as 'ДатаРождения',A_SNILS,A_REGREGIONCODE from( " +
                               " select WM_PERSONAL_CARD.OUID, SPR_FIO_SURNAME.A_NAME as Фамилия,dbo.SPR_FIO_NAME.A_NAME as 'Имя',SPR_FIO_SECONDNAME.A_NAME as 'Отчество',WM_ACTDOCUMENTS.DOCUMENTSTYPE, " +
                               " WM_ACTDOCUMENTS.DOCUMENTSERIES ,WM_ACTDOCUMENTS.DOCUMENTSNUMBER ,WM_ACTDOCUMENTS.ISSUEEXTENSIONSDATE ,PPR_DOC.A_NAME,WM_ADDRESS.A_ADRTITLE, " +
                               " WM_PERSONAL_CARD.BIRTHDATE ,dbo.WM_PERSONAL_CARD.A_SNILS, dbo.REGISTER_CONFIG.A_REGREGIONCODE from dbo.WM_PERSONAL_CARD " +
                                " join  SPR_FIO_SURNAME " +
                                " on WM_PERSONAL_CARD.SURNAME = SPR_FIO_SURNAME.OUID " +
                                 " join SPR_FIO_NAME " +
                                 " on SPR_FIO_NAME.OUID = WM_PERSONAL_CARD.A_NAME " +
                                 " join SPR_FIO_SECONDNAME " +
                                 " on SPR_FIO_SECONDNAME.OUID = WM_PERSONAL_CARD.A_SECONDNAME " +
                                 " join dbo.WM_ACTDOCUMENTS " +
                                 " on WM_PERSONAL_CARD.OUID = dbo.WM_ACTDOCUMENTS.PERSONOUID " +
                                 " join PPR_DOC " +
                                 " on WM_ACTDOCUMENTS.DOCUMENTSTYPE = PPR_DOC.A_ID " +
                                 " join WM_ADDRESS " +
                                 " on WM_ADDRESS.OUID = WM_PERSONAL_CARD.A_REGFLAT " +
                                 " CROSS JOIN dbo.REGISTER_CONFIG  " +
                                  " where  WM_PERSONAL_CARD.A_PCSTATUS = 1 " +
                                  //" and " + nameDocPreferentCategory + " " +
                                  //" and PPR_DOC.A_NAME in ('Удостоверение ветерана труда','Удостоверение ветерана труда Саратовской области', " +
                                  " and PPR_DOC.A_NAME in ('Удостоверение ветерана труда','Удостоверение ветерана труда Саратовской области', " +
                               " 'Удостоверение о праве на льготы (отметка - ст.20)','Свидетельство о праве на льготы для реабилитированных лиц', " +
                               " 'Справка о реабилитации','Свидетельство о праве на льготы для лиц, признанных пострадавшими от политических репрессий', " +
                               " 'Справка о признании пострадавшим от политических репрессий','Удостоверение ветерана военной службы','Паспорт гражданина России') ) as Tab1 " +
                                 " inner join " + this.nameTemptable + " " +
                                   " on LOWER(RTRIM(LTRIM(Tab1.Фамилия))) = LOWER(RTRIM(LTRIM(" + this.nameTemptable + ".Фамилия))) " +
                                   " and LOWER(RTRIM(LTRIM(Tab1.Имя))) = LOWER(RTRIM(LTRIM(" + this.nameTemptable + ".Имя))) " +
                                   " and((LOWER(RTRIM(LTRIM(Tab1.Отчество))) = LOWER(RTRIM(LTRIM(" + this.nameTemptable + ".Отчество))) or " + this.nameTemptable + ".Отчество is NULL)) " +
                                   //" and CONVERT(char(10), Tab1.BIRTHDATE, 112) = CONVERT(char(10), LOWER(RTRIM(LTRIM(" + this.nameTemptable + ".ДатаРождения))), 112) " + 
                                   " and CONVERT(char(10), CAST(LOWER(RTRIM(LTRIM(Tab1.BIRTHDATE))) as DATE), 104) = CONVERT(char(10), CAST(LOWER(RTRIM(LTRIM(" + this.nameTemptable + ".ДатаРождения))) as DATE), 104)  " +
                                   " and LOWER(RTRIM(LTRIM(Tab1.DOCUMENTSERIES))) = LOWER(RTRIM(LTRIM(" + this.nameTemptable + ".СерияПаспорта))) " +
                                   " and LOWER(RTRIM(LTRIM(Tab1.DOCUMENTSNUMBER))) = LOWER(RTRIM(LTRIM(" + this.nameTemptable + ".НомерПаспорта)))   " +
            //and CONVERT(char(10), LOWER(RTRIM(LTRIM(Tab1.ISSUEEXTENSIONSDATE))), 104) = CONVERT(char(10), LOWER(RTRIM(LTRIM(" + nameTempTable + ".ДатаВыдачиПаспорта))), 104) ";
                                   "and CONVERT(char(10), CAST(LOWER(RTRIM(LTRIM(Tab1.ISSUEEXTENSIONSDATE))) as DATE), 104) = CONVERT(char(10), CAST(LOWER(RTRIM(LTRIM(" + this.nameTemptable + ".ДатаВыдачиПаспорта))) as DATE), 104)  ";
            //" --and Tab1.A_NAME = 'Удостоверение ветерана труда' ";
            //+
            //"and CONVERT(char(10), Tab1.ISSUEEXTENSIONSDATE, 104) = CONVERT(char(10), " + nameTempTable + ".ДатаВыдачиДокумента, 104) ";
            // " and CONVERT(char(10), LOWER(RTRIM(LTRIM(Tab1.ISSUEEXTENSIONSDATE))), 104) = CONVERT(char(10), LOWER(RTRIM(LTRIM(" + nameTempTable + ".ДатаВыдачиДокумента))), 104) ";"

            //string queryJoin = @"select  " + nameTempTable + ".id_договор, OUID,Tab1.Фамилия,Tab1.Имя,Tab1.Отчество,DOCUMENTSTYPE,DOCUMENTSERIES,DOCUMENTSNUMBER,ISSUEEXTENSIONSDATE,A_NAME,A_ADRTITLE,BIRTHDATE,A_SNILS from ( " +
            //                   "  select WM_PERSONAL_CARD.OUID, SPR_FIO_SURNAME.A_NAME as Фамилия,dbo.SPR_FIO_NAME.A_NAME as 'Имя',SPR_FIO_SECONDNAME.A_NAME as 'Отчество',WM_ACTDOCUMENTS.DOCUMENTSTYPE, " +
            //                     " WM_ACTDOCUMENTS.DOCUMENTSERIES ,WM_ACTDOCUMENTS.DOCUMENTSNUMBER ,WM_ACTDOCUMENTS.ISSUEEXTENSIONSDATE ,PPR_DOC.A_NAME,WM_ADDRESS.A_ADRTITLE, " +
            //                    " WM_PERSONAL_CARD.BIRTHDATE ,dbo.WM_PERSONAL_CARD.A_SNILS, dbo.REGISTER_CONFIG.A_REGREGIONCODE from dbo.WM_PERSONAL_CARD " +
            //                     " join  SPR_FIO_SURNAME " +
            //                     " on WM_PERSONAL_CARD.SURNAME = SPR_FIO_SURNAME.OUID " +
            //                      " join SPR_FIO_NAME " +
            //                      " on SPR_FIO_NAME.OUID = WM_PERSONAL_CARD.A_NAME " +
            //                      " join SPR_FIO_SECONDNAME " +
            //                      " on SPR_FIO_SECONDNAME.OUID = WM_PERSONAL_CARD.A_SECONDNAME " +
            //                      " join dbo.WM_ACTDOCUMENTS " +
            //                      " on WM_PERSONAL_CARD.OUID = dbo.WM_ACTDOCUMENTS.PERSONOUID " +
            //                      " join PPR_DOC " +
            //                      " on WM_ACTDOCUMENTS.DOCUMENTSTYPE = PPR_DOC.A_ID " +
            //                      " join WM_ADDRESS " +
            //                      " on WM_ADDRESS.OUID = WM_PERSONAL_CARD.A_REGFLAT " +
            //                      " CROSS JOIN dbo.REGISTER_CONFIG  " +
            //                      " where  WM_PERSONAL_CARD.A_PCSTATUS = 1  " +
            //                      " and PPR_DOC.A_NAME in ('Удостоверение ветерана труда','Удостоверение ветерана труда Саратовской области', " +
            //                    " 'Удостоверение о праве на льготы (отметка - ст.20)','Свидетельство о праве на льготы для реабилитированных лиц', " +
            //                    " 'Справка о реабилитации','Свидетельство о праве на льготы для лиц, признанных пострадавшими от политических репрессий', " +
            //                    " 'Справка о признании пострадавшим от политических репрессий','Удостоверение ветерана военной службы','Паспорт гражданина России') ) as Tab1  " +
            //                     " inner join " + nameTempTable + " " +
            //                    " on LOWER(RTRIM(LTRIM(Tab1.Фамилия))) = LOWER(RTRIM(LTRIM(" + nameTempTable + ".Фамилия))) " +
            //                     " and LOWER(RTRIM(LTRIM(Tab1.Имя))) = LOWER(RTRIM(LTRIM(" + nameTempTable + ".Имя))) " +
            //                     " and((LOWER(RTRIM(LTRIM(Tab1.Отчество))) = LOWER(RTRIM(LTRIM(" + nameTempTable + ".Отчество))) or  " + nameTempTable + ".Отчество is NULL)) " +
            //                     " and REPLACE(CONVERT(char(10), LOWER(RTRIM(LTRIM(Tab1.BIRTHDATE))), 104),' ','') = REPLACE(CONVERT(char(10), LOWER(RTRIM(LTRIM( " + nameTempTable + ".ДатаРождения))), 104), ' ','') " +
            //                     " and REPLACE(LOWER(RTRIM(LTRIM(Tab1.DOCUMENTSERIES))),' ','') = REPLACE(LOWER(RTRIM(LTRIM( " + nameTempTable + ".СерияПаспорта))),' ','') " +
            //                     " and LOWER(RTRIM(LTRIM(Tab1.DOCUMENTSNUMBER))) = LOWER(RTRIM(LTRIM( " + nameTempTable + ".НомерПаспорта))) " +
            //                     " and CONVERT(char(10), LOWER(RTRIM(LTRIM(Tab1.ISSUEEXTENSIONSDATE))), 104) = CONVERT(char(10), LOWER(RTRIM(LTRIM( " + nameTempTable + ".ДатаВыдачиПаспорта))), 104) ";

            return queryJoin;

        }
    }
}

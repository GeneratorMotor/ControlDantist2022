using System;
using System.Collections.Generic;
using ControlDantist.DataBaseContext;
using System.Text;
using ControlDantist.Classes;
using ControlDantist.ExceptionClassess;

namespace ControlDantist.ValidateRegistrProject
{
    public class ValidatePersonFioPassword : IEsrnValidate
    {

        private List<ItemLibrary> list;

        // Переменная для хранения названия временной таблицы.
        private string nameTempTable = string.Empty;

        public ValidatePersonFioPassword(List<ItemLibrary> list)
        {
            // Здесь что то не так.
            this.list = list;
        }

        /// <summary>
        /// Возвращает льготтную категорию реестра.
        /// </summary>
        /// <returns></returns>
        public string GetPreferentCategory()
        {
            return this.list?[0].Packecge?.тЛьготнаяКатегория?.ЛьготнаяКатегория ?? "Категория_не_установлена";
        }

        /// <summary>
        /// Создание временной таблицы.
        /// </summary>
        /// <returns></returns>
        private string CreateTempTable(string nameTempTable)
        {
            // Здесь что то не так.
            this.nameTempTable = nameTempTable;

            string createTable = "create table " + nameTempTable + " ([id_карточки] [int] IDENTITY(1,1) NOT NULL, " +
                                "id_договор int NULL, " +
                                "[Фамилия] [nvarchar](50) NULL, " +
                                "[Имя] [nvarchar](50) NULL, " +
                                "[Отчество] [nvarchar](50) NULL, " +
                                "[ДатаРождения] DateTime, " +
                                "[ВидЛьготногоУдостоверения] [nvarchar](50) NULL, " +
                                "[СерияДокумента] [nvarchar](50) NULL, " +
                                "[НомерДокумента] [nvarchar](50) NULL, " +
                                 "[ДатаВыдачиДокумента] DateTime, " +
                                 "СерияПаспорта nvarchar (10) NULL, " +
                                 "НомерПаспорта nvarchar (10) NULL, " +
                                 "ДатаВыдачиПаспорта DateTime )";

            return createTable;
        }

        /// <summary>
        /// Заполнение временной таблицы данными.
        /// </summary>
        /// <param name="builderQuery"></param>
        private void InsertDateTempTable(StringBuilder builderQuery)
        {
            foreach (var itm in this.list)
            {
                // Получим данные о льготнике.
                var person = itm?.Packecge?.льготник ?? null;

                // Получим данные о договоре.
                var contract = itm?.Packecge?.тДоговор ?? null;

                if (person != null && contract != null)
                {
                    // Проверим заполненность полей льготника.
                    ValidErrorPerson vrp = new ValidErrorPerson(person);

                    // Строка с датой рождения льготника.
                    string stringDateBirthPerson = itm?.DateBirdthPerson ?? "";

                    // Строка с датой выдачи документа.
                    string strDateDoc = itm?.DateDoc ?? "";

                    // Строка с датой выдачи паспорта.
                    string strDatePassword = itm?.DatePassword ?? "";

                    //// Проверим заполненность полей льготника.
                    if (vrp.Validate() == true)
                    {
                        //string queryInsert = " insert into " + nameTempTable + " (id_договор,Фамилия,Имя,Отчество,ДатаРождения,СерияДокумента,НомерДокумента,ДатаВыдачиДокумента,СерияПаспорта,НомерПаспорта,ДатаВыдачиПаспорта) " +
                        //                     " values(" + contract.id_договор + ",'" + person.Фамилия.Trim().ToLower() + "','" + person.Имя.Trim().ToLower() + "','" + person.Отчество.Do(x => x, "").Trim().ToLower() + "','" + Время.Дата(stringDateBirthPerson.Trim()) + "','" + person.СерияДокумента.Do(x => x, "").ToLower().Trim() + "','" + person.НомерДокумента.Do(x => x, "").ToLower().Trim() + "','" + Время.Дата(strDateDoc.Trim()) + "',  " +
                        //                     " '" + person.СерияПаспорта.Do(x => x, "").Trim().ToLower() + "','" + person.НомерПаспорта.Do(x => x, "").Trim().ToLower() + "','" + Время.Дата(strDatePassword.Trim()) + "' ) ";


                        string queryInsert = " insert into " + nameTempTable + " (id_договор,Фамилия,Имя,Отчество,ДатаРождения,СерияДокумента,НомерДокумента,ДатаВыдачиДокумента,СерияПаспорта,НомерПаспорта,ДатаВыдачиПаспорта) " +
                                             " values(" + contract.id_договор + ",'" + person.Фамилия.Trim().ToLower() + "','" + person.Имя.Trim().ToLower() + "','" + person.Отчество.Do(x => x, "").Trim().ToLower() + "','" + Время.Дата(person.ДатаРождения.ToShortDateString().Trim()) + "','" + person.СерияДокумента.Do(x => x, "").ToLower().Trim() + "','" + person.НомерДокумента.Do(x => x, "").ToLower().Trim() + "','" + Время.Дата(person.ДатаВыдачиДокумента.ToShortDateString().Trim()) + "',  " +
                                             " '" + person.СерияПаспорта.Do(x => x, "").Replace(" ","").Trim().ToLower() + "','" + person.НомерПаспорта.Do(x => x, "").Trim().ToLower() + "','" + Время.Дата(person.ДатаВыдачиПаспорта.ToShortDateString().Trim()) + "' ) ";

                        builderQuery.Append(queryInsert);
                    }
                }
            }

        }
        /// <summary>
        /// Поиск льготников по ФИо и номеру документа.
        /// </summary>
        /// <returns></returns>
        public string FindPersons(string nameDocPreferentCategory)
        {
            StringBuilder stringBuilder = new StringBuilder();

            // Создаем временную таблицу.
            stringBuilder.Append(CreateTempTable("#t1_temp"));

            InsertDateTempTable(stringBuilder);

            string queryJoin = @"select  " + nameTempTable + ".id_договор, OUID,Tab1.Фамилия,Tab1.Имя,Tab1.Отчество,DOCUMENTSTYPE,DOCUMENTSERIES as 'Серия документа',DOCUMENTSNUMBER as 'Номер документа',ISSUEEXTENSIONSDATE as 'дата выдачи',A_NAME,A_ADRTITLE as 'Адрес',BIRTHDATE as 'ДатаРождения',A_SNILS,A_REGREGIONCODE from ( " +
                               "  select WM_PERSONAL_CARD.OUID, SPR_FIO_SURNAME.A_NAME as Фамилия,dbo.SPR_FIO_NAME.A_NAME as 'Имя',SPR_FIO_SECONDNAME.A_NAME as 'Отчество',WM_ACTDOCUMENTS.DOCUMENTSTYPE, " +
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
                                  " where  WM_PERSONAL_CARD.A_PCSTATUS = 1  " +
                                  " and PPR_DOC.A_NAME in ('Удостоверение ветерана труда','Удостоверение ветерана труда Саратовской области', " +
                                " 'Удостоверение о праве на льготы (отметка - ст.20)','Свидетельство о праве на льготы для реабилитированных лиц', " +
                                " 'Справка о реабилитации','Свидетельство о праве на льготы для лиц, признанных пострадавшими от политических репрессий', " +
                                " 'Справка о признании пострадавшим от политических репрессий','Удостоверение ветерана военной службы','Паспорт гражданина России') ) as Tab1  " +
                                 " inner join " + nameTempTable + " " +
                                " on LOWER(RTRIM(LTRIM(Tab1.Фамилия))) = LOWER(RTRIM(LTRIM(" + nameTempTable + ".Фамилия))) " +
                                 " and LOWER(RTRIM(LTRIM(Tab1.Имя))) = LOWER(RTRIM(LTRIM(" + nameTempTable + ".Имя))) " +
                                 " and((LOWER(RTRIM(LTRIM(Tab1.Отчество))) = LOWER(RTRIM(LTRIM(" + nameTempTable + ".Отчество))) or  " + nameTempTable + ".Отчество is NULL)) " +
                                 "and REPLACE(CONVERT(char(10), Tab1.BIRTHDATE, 112),' ','') = #t1_temp.ДатаРождения " +
                                 //" and CONVERT(char(10), Tab1.BIRTHDATE, 112) = REPLACE(CONVERT(char(10), LOWER(RTRIM(LTRIM( " + nameTempTable + ".ДатаРождения))), 112), ' ','') " +
                                 //"and REPLACE(CONVERT(char(10), LOWER(RTRIM(LTRIM(Tab1.BIRTHDATE))), 112),' ','') = REPLACE(CONVERT(char(10), LOWER(RTRIM(LTRIM( #t1_temp.ДатаРождения))), 112), ' ','')  " +
            " and REPLACE(LOWER(RTRIM(LTRIM(Tab1.DOCUMENTSERIES))),' ','') = REPLACE(LOWER(RTRIM(LTRIM( " + nameTempTable + ".СерияПаспорта))),' ','') " +
                                 " and LOWER(RTRIM(LTRIM(Tab1.DOCUMENTSNUMBER))) = LOWER(RTRIM(LTRIM( " + nameTempTable + ".НомерПаспорта))) ";// +
                                 //" and CONVERT(char(10), LOWER(RTRIM(LTRIM(Tab1.ISSUEEXTENSIONSDATE))), 104) = CONVERT(char(10), LOWER(RTRIM(LTRIM( " + nameTempTable + ".ДатаВыдачиПаспорта))), 104) ";

            stringBuilder.Append(queryJoin);

            return stringBuilder.ToString();
             
        }
    }
}

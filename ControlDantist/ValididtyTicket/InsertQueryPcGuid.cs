using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.Querys;
using System.Data;

namespace ControlDantist.ValididtyTicket
{
    public class InsertQueryPcGuid : IQuery
    {
        DataTable tab;

        StringBuilder build;

        public InsertQueryPcGuid(DataTable tab)
        {
            this.tab = tab;

            build = new StringBuilder();
        }

        public string Query()
        {
            foreach(DataRow row in this.tab.Rows)
            {
                #region Не нужная реализация 
                //string insert = @" INSERT INTO [dbo].[DateEsrn]
                //               ([BAR_CODE]
                //               ,[Год]
                //               ,[Месяц]
                //               ,[МесяцГод]
                //               ,[Login]
                //               ,[Реализатор]
                //               ,[НазваниеПеревозчика]
                //               ,[ОткудаПеревозчик]
                //               ,[PC_GUID]
                //               ,[СерияДокумента]
                //               ,[НомерДокумента]
                //               ,[НазваниеДокумента]
                //               ,[ДатаДокумента])
                //         VALUES
                //               ( '" + row["BAR_CODE"].ToString() + "' " +
                //               " ,'" + row["Год"].ToString() + "' " +
                //               ",'" + row["Месяц"] + "' " +
                //               " , '" + row["МесяцГод"].ToString() + "' " +
                //               ", '" + row["Login"] + "' " +
                //               " , '" + row["Реализатор"].ToString() + "' " +
                //               " ,'" + row["НазваниеПеревозчика"].ToString() + "' " +
                //               " , '" + row["ОткудаПеревозчик"].ToString() + "' " +
                //               ", '" + row["PC_GUID"].ToString().Trim() + "' " +
                //               ", '" + row["СерияДокумента"].ToString() + "' " +
                //               " , '" + row["НомерДокумента"].ToString() + "' " +
                //               ", '" + row["НазваниеДокумента"].ToString() + "' " +
                //               ", '" + row["ДатаДокумента"] + "' ) ";

                #endregion

                string guid = string.Empty;
                string Фамилия = string.Empty;
                string Имя = string.Empty;
                string Отчество = string.Empty;
                string DocGuid = string.Empty;
                string СерияДокумента = string.Empty;
                string НомерДокумента = string.Empty;
                string A_NAME = string.Empty;
                DateTime date = new DateTime(1900, 1, 1);

                DateTime ДатаДокумента = date;
                string АдресЖительства = string.Empty;
                DateTime ДатаРождения = date;
                string A_SNILS = string.Empty;
                string A_REGREGIONCODE = string.Empty;

                if (!DBNull.Value.Equals(row["GUID"].ToString()))
                {
                    guid = row["GUID"].ToString();
                }

                if (!DBNull.Value.Equals(row["Фамилия"].ToString()))
                {
                    Фамилия = row["Фамилия"].ToString();
                }

                if (!DBNull.Value.Equals(row["Имя"].ToString()))
                {
                    Имя = row["Имя"].ToString();
                }

                if (!DBNull.Value.Equals(row["Отчество"].ToString()))
                {
                    Отчество = row["Отчество"].ToString();
                }

                if (!DBNull.Value.Equals(row["DocGuid"].ToString()))
                {
                    DocGuid = row["DocGuid"].ToString();
                }

                if (!DBNull.Value.Equals(row["СерияДокумента"].ToString()))
                {
                    СерияДокумента = row["СерияДокумента"].ToString();
                }

                if (!DBNull.Value.Equals(row["НомерДокумента"].ToString()))
                {
                    НомерДокумента = row["НомерДокумента"].ToString();
                }

                if (!DBNull.Value.Equals(row["A_NAME"].ToString()))
                {
                    A_NAME = row["A_NAME"].ToString();
                }

                if (!DBNull.Value.Equals(row["ISSUEEXTENSIONSDATE"]))
                {
                    ДатаДокумента = Convert.ToDateTime(row["ISSUEEXTENSIONSDATE"]);
                }

                if (!DBNull.Value.Equals(row["АдресЖительства"].ToString()))
                {
                    АдресЖительства = row["АдресЖительства"].ToString();
                }

                if (!DBNull.Value.Equals(row["ДатаРождения"]))
                {
                    ДатаРождения = Convert.ToDateTime(row["ДатаРождения"]);
                }

                if(!DBNull.Value.Equals(row["A_SNILS"].ToString()))
                {
                    A_SNILS = row["A_SNILS"].ToString();
                }

                if (!DBNull.Value.Equals(row["A_REGREGIONCODE"].ToString()))
                {
                    A_REGREGIONCODE = row["A_REGREGIONCODE"].ToString();
                }

                string insert = @" INSERT INTO [dbo].[DateЭсрн]
                                   ([GUID]
                                   ,[Фамилия]
                                   ,[Имя]
                                   ,[Отчество]
                                   ,[DocGuid]
                                   ,[СерияДокумента]
                                   ,[НомерДокумента]
                                   ,[A_NAME]
                                   ,[ДатаДокумента]
                                   ,[АдресЖительства]
                                   ,[ДатаРождения]
                                   ,[A_SNILS]
                                   ,[A_REGREGIONCODE])
                             VALUES
                                   ('" + guid + "'" +
                                   " ,'" + Фамилия + "' " +
                                   " , '" + Имя + "' " +
                                   " , '" + Отчество + "' " +
                                   " , '" + DocGuid + "' " +
                                   ", '" + СерияДокумента + "' " +
                                   ", '" + НомерДокумента + "' " +
                                   ", '" + A_NAME + "' " +
                                   ", '" + ДатаДокумента + "' " +
                                   " , '" + АдресЖительства + "' " +
                                   ", '" + ДатаРождения + "' " +
                                   ", '" + A_SNILS + "' " +
                                   ", '" + A_REGREGIONCODE + "' ) ";

                build.Append(insert);
            }

            return build.ToString();
        }
    }
}

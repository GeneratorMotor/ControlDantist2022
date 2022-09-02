using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.Querys;

namespace ControlDantist.ClassesForFindEspb
{
    public class QueryInsertResultat : IQuery
    {
        private string PC_GUID = string.Empty;
        private string СерияДокумента = string.Empty;
        private string НомерДокумента = string.Empty;
        private string НазваниеДокумента = string.Empty;
        private DateTime ДатаДокумента;
        private string Категория = string.Empty;
        private string Фамилия = string.Empty;
        private string Имя = string.Empty;
        private string Отчество = string.Empty;
        private DateTime ДатаРождения;
        private string Адрес = string.Empty;
        private string Район = string.Empty;
        private string Снилс = string.Empty;

        public QueryInsertResultat(string pC_GUID, string серияДокумента, string номерДокумента, string названиеДокумента, DateTime датаДокумента, string категория, string фамилия, string имя, string отчество, DateTime датаРождения, string адрес, string район, string снилс)
        {
            PC_GUID = pC_GUID;
            СерияДокумента = серияДокумента;
            НомерДокумента = номерДокумента;
            НазваниеДокумента = названиеДокумента;
            ДатаДокумента = датаДокумента;
            Категория = категория;
            Фамилия = фамилия;
            Имя = имя;
            Отчество = отчество;
            ДатаРождения = датаРождения;
            Адрес = адрес;
            Район = район;
            Снилс = снилс;
        }

        public string Query()
        {
            string insert = @" INSERT INTO [dbo].[TablePersonEsrn]
                               ([PC_GUID]
                               ,[СерияДокумента]
                               ,[НомерДокумента]
                               ,[НазваниеДокумента]
                               ,[ДатаДокумента]
                               ,[Категория]
                               ,[Фамилия]
                               ,[Имя]
                               ,[Отчество]
                               ,[ДатаРождения]
                               ,[Адрес]
                               ,[Район]
                               ,[Снилс])
                         VALUES
                               ( '" + this.PC_GUID + "' , " +
                               " '" + this.СерияДокумента + "'  , " +
                               " '" + this.НомерДокумента + "' , " +
                               " '" + this.НазваниеДокумента + "' , " +
                               " '" + this.ДатаДокумента + "' , " +
                               " '" + this.Категория + "' , " +
                               " '" + this.Фамилия + "' , " +
                               " '" + this.Имя + "' , " +
                               " '" + this.Отчество + "' , " +
                               " '" + this.ДатаРождения + "' , " +
                               " '" + this.Адрес + "' , " +
                               " '" + this.Район + "' , " +
                               " '" + this.Снилс + "' ) ";

            return insert;


        }
    }
}

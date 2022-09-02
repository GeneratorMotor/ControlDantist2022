using System;
using ControlDantist.Querys;

namespace ControlDantist.ClassesForFindEspb.Rezult
{
    public class QueryInsertRezultDoc : IQuery
    {

        private string PC_GUID = string.Empty;
        private string Снилс = string.Empty;
        private string НазваниеДокумента = string.Empty;
        private string Фамилия = string.Empty;
        private string Имя = string.Empty;
        private string Отчество = string.Empty;
        private DateTime ДатаРождения;
        private string Адрес = string.Empty;
        private string Район = string.Empty;

        public QueryInsertRezultDoc(string pC_GUID, string снилс, string названиеДокумента, string фамилия, string имя, string отчество, DateTime датаРождения, string адрес, string район)
        {
            PC_GUID = pC_GUID ?? throw new ArgumentNullException(nameof(pC_GUID));
            Снилс = снилс ?? throw new ArgumentNullException(nameof(снилс));
            НазваниеДокумента = названиеДокумента ?? throw new ArgumentNullException(nameof(названиеДокумента));
            Фамилия = фамилия ?? throw new ArgumentNullException(nameof(фамилия));
            Имя = имя ?? throw new ArgumentNullException(nameof(имя));
            Отчество = отчество ?? throw new ArgumentNullException(nameof(отчество));
            ДатаРождения = датаРождения;
            Адрес = адрес ?? throw new ArgumentNullException(nameof(адрес));
            Район = район ?? throw new ArgumentNullException(nameof(район));
        }

        public string Query()
        {
            string insert = @" INSERT INTO [dbo].[TablePersonEsrnResult]
                               ([PC_GUID]
                               ,[Снилс]
                               ,[НазваниеДокумента]
                               ,[Фамилия]
                               ,[Имя]
                               ,[Отчество]
                               ,[ДатаРождения]
                               ,[Адрес]
                               ,[Район]
                               )
                         VALUES
                               ( '" + this.PC_GUID + "' , " +
                               " '" + this.Снилс + "'  , " +
                               " '" + this.НазваниеДокумента + "' , " +
                               " '" + this.Фамилия + "' , " +
                               " '" + this.Имя + "' , " +
                               " '" + this.Отчество + "' , " +
                               " '" + this.ДатаРождения + "' , " +
                               " '" + this.Адрес + "' , " +
                               " '" + this.Район + "' ) ";
            return insert;
        }
    }
}

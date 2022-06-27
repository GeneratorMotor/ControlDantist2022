using ControlDantist.Querys;
using System;

namespace ControlDantist.ClassUpdateFind
{
    /// <summary>
    /// Аннулирование договора.
    /// </summary>
    public class CancelContract : IQuery
    {
        private string user = string.Empty;
        private string date = string.Empty;
        private int idContract = 0;

        public CancelContract(string user, string date, int idContract)
        {
            this.user = user ?? throw new ArgumentNullException(nameof(user));
            this.date = date ?? throw new ArgumentNullException(nameof(date));
            this.idContract = idContract;
        }

        public string Query()
        {
            string query = @"delete АктВыполненныхРабот
                            where id_договор  = (select id_договор from Договор
                            where id_договор = " + this.idContract + " and ФлагПроверки = 1 and ФлагНаличияАкта = 1) " +
                            @" update dbo.Договор 
                            set ДатаЗаписиДоговора = '" + date + "' , " +
                            @"ФлагПроверки = 'False' 
                            ,logWrite = '" + user + "' " +
                            @", ФлагВозвратНаДоработку = 0 
                            ,flagАнулирован = 1
                            , ФлагАнулирован = 1 
                            , flag2020 = 0 
                            , flag2019AddWrite = 0
                            ,ДатаДоговора = '19000101' 
                            ,ДатаАктаВыполненныхРабот = '19000101'
                            , СуммаАктаВыполненныхРабот = 0.0
                            ,ФлагНаличияАкта = 0
                            ,НомерРеестра = null
                            ,ДатаРеестра = null
                            , НомерСчётФактрура = null
                            ,ДатаСчётФактура = null
                            where id_договор = " + this.idContract + " ";

            return query;
        }
    }
}

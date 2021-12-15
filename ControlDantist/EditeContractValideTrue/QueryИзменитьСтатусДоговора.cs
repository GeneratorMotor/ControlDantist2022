using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.Querys;

namespace ControlDantist.EditeContractValideTrue
{
    /// <summary>
    /// SQL запрос на проверку наличичя акта
    /// </summary>
    class QueryИзменитьСтатусДоговора : IQuery
    {
        private int idContract = 0;
        private string user = " ";

        public QueryИзменитьСтатусДоговора(int idContract, string user)
        {
            this.idContract = idContract;

            this.user = user;
        }

        public string Query()
        {
            string query = @"declare @id int " +
                            "set @id = " + this.idContract + " " +
                            "delete АктВыполненныхРабот " +
                            "where id_договор in ( " +
                            "select id_договор  from Договор " +
                            "where id_договор = @id " +
                            ") " +
                            "update Договор " +
                            "set ФлагПроверки = 'False', " +
                            "ДатаАктаВыполненныхРабот = '19000101', " +
                            "СуммаАктаВыполненныхРабот = 0.0, " +
                            "НомерРеестра =  null, " +
                            "ДатаРеестра = null, " +
                            "НомерСчётФактрура = null, " +
                            "ДатаСчётФактура = null, " +
                            "logWrite = '" + user + "',  " +
                            " ФлагВозвратНаДоработку = 1 " +
                            "where id_договор in ( " +
                            "select id_договор  from Договор " +
                            "where id_договор = @id) ";

            return query;
        }
    }
}

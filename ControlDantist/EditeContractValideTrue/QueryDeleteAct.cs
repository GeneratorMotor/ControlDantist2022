using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.Querys;

namespace ControlDantist.EditeContractValideTrue
{
    public class QueryDeleteAct : IQuery
    {
        private int idДоговор = 0;

        private string user = string.Empty;

        public QueryDeleteAct(string user, int id_contract)
        {
            this.user = user ?? throw new ArgumentNullException(nameof(user));

            this.idДоговор = id_contract;
        }

        public string Query()
        {
            string query = " declare @id int " +
                            "set @id = " + idДоговор + " " +
                            "delete АктВыполненныхРабот " +
                            "where id_договор in ( " +
                            "select id_договор  from Договор " +
                            "where id_договор = @id " +
                            ") " +
                            "update Договор " +
                            "set ФлагПроверки = 'True', " +
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

using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.Querys;

namespace ControlDantist.EditeContractValideTrue
{
    public class QueryАнулироватьДоговор : IQuery
    {
        private int idContract = 0;
        private string user = " ";

        public QueryАнулироватьДоговор(string user, int idContract)
        {
            this.user = user;
            this.idContract = idContract;
        }

        public string Query()
        {
            return @" update Договор
                            set ФлагПроверки = 'False',
                            ДатаАктаВыполненныхРабот = '19000101', СуммаАктаВыполненныхРабот = 0.0,
                            НомерРеестра = null, ДатаРеестра = null, НомерСчётФактрура = null,
                            ДатаСчётФактура = null, ФлагАнулирован = 1, flagАнулирован = 1,
                            logWrite = '" + user + "', " +
                            @" ФлагВозвратНаДоработку = 1
                            where НомерДоговора = "+ this.idContract +" ";
        }
    }
}

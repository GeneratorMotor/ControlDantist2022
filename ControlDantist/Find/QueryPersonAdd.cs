using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.Find
{
    public class QueryPersonAdd : IQueryFind
    {
        private int id = 0;

        public QueryPersonAdd(int id)
        {
            this.id = id;
        }

        public string Query()
        {
            return @" SELECT top 1 ДоговорAdd.НомерДоговора, ДоговорAdd.ДатаДоговора, ЛьготникAdd.Фамилия, ЛьготникAdd.Имя, 
                                                ЛьготникAdd.Отчество, ЛьготникAdd.ДатаРождения, ЛьготникAdd.улица, ЛьготникAdd.НомерДома, 
                                                ЛьготникAdd.корпус, ЛьготникAdd.НомерКвартиры, ЛьготникAdd.СерияПаспорта, ЛьготникAdd.НомерПаспорта, 
                                                ЛьготникAdd.СерияДокумента, ЛьготникAdd.НомерДокумента, ЛьготникAdd.ДатаВыдачиДокумента, 
                                                ЛьготникAdd.ДатаВыдачиПаспорта,ДоговорAdd.ФлагПроверки FROM ДоговорAdd
                                                INNER JOIN УслугиПоДоговоруAdd
                                                ON ДоговорAdd.id_договор = УслугиПоДоговоруAdd.id_договор
                                                INNER JOIN ЛьготникAdd
                                                ON dbo.ДоговорAdd.id_льготник = dbo.ЛьготникAdd.id_льготник " +
                                                "where ДоговорAdd.id_договор = " + this.id + " ";
        }
    }
}

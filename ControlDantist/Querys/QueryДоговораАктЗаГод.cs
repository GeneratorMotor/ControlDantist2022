using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.Querys
{
    public class QueryДоговораАктЗаГод : IQuery
    {
        public string Query()
        {
            return @"select COUNT(id_договор) as id, SUM(Сумма) as Сумма,ЛьготнаяКатегория from (
                    select Договор.id_договор,SUM(УслугиПоДоговору.Сумма) as Сумма,ЛьготнаяКатегория.ЛьготнаяКатегория from Договор
                    inner join УслугиПоДоговору
                    on УслугиПоДоговору.id_договор = Договор.id_договор
                    inner join ЛьготнаяКатегория
                    on ЛьготнаяКатегория.id_льготнойКатегории = Договор.id_льготнаяКатегория
                    where ФлагПроверки = 1 and ФлагНаличияАкта = 1
                    group by Договор.id_договор,ЛьготнаяКатегория.ЛьготнаяКатегория) as Tab1
                    group by ЛьготнаяКатегория";
        }
    }
}

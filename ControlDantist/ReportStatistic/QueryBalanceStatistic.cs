using ControlDantist.Querys;
using System;

namespace ControlDantist.ReportStatistic
{
    public class QueryBalanceStatistic : IQuery
    {
        private string hospital = string.Empty;
        private string preferentCategory = string.Empty;
        private string dateStart = string.Empty;
        private string dateEnd = string.Empty;
        private bool flagAct = false;

        public QueryBalanceStatistic(string hospital, string preferentCategory, string dateStart, string dateEnd, bool flagAct)
        {
            this.hospital = hospital ?? throw new ArgumentNullException(nameof(hospital));
            this.preferentCategory = preferentCategory ?? throw new ArgumentNullException(nameof(preferentCategory));
            this.dateStart = dateStart ?? throw new ArgumentNullException(nameof(dateStart));
            this.dateEnd = dateEnd ?? throw new ArgumentNullException(nameof(dateEnd));
            this.flagAct = flagAct;
        }

        public string Query()
        {

            string query = @" select COUNT(Tab1.id_договор) as КоличествоДоговоров ,НаименованиеПоликлинники,ЛьготнаяКатегория,SUM(Сумма) as 'Сумма' from (
                            select Договор.id_договор, НаименованиеПоликлинники, ЛьготнаяКатегория from Договор
                            inner join(select id_поликлинника, ПоликлинникиИнн.F2 as 'НаименованиеПоликлинники' from Поликлинника
                            inner join ПоликлинникиИнн
                            on Поликлинника.ИНН = ПоликлинникиИнн.F3 " +
                            " where ПоликлинникиИнн.F2 = '" + this.hospital + "' " +
                            @" ) as TabHosp
                            on Договор.id_поликлинника = TabHosp.id_поликлинника
                            inner join ЛьготнаяКатегория
                            on Договор.id_льготнаяКатегория = ЛьготнаяКатегория.id_льготнойКатегории 
                            where ФлагПроверки = 1 and ФлагНаличияАкта = 0 " +
                            " and ДатаЗаписиДоговора >= '" + this.dateStart + "' and ДатаЗаписиДоговора <= '" + this.dateEnd + "') as Tab1 " +
                            @" inner join(select id_договор, SUM(Сумма) as 'Сумма' from УслугиПоДоговору
                            group by id_договор) as Tab2
                            on Tab2.id_договор = Tab1.id_договор
                            group by НаименованиеПоликлинники,ЛьготнаяКатегория
                            having LOWER(RTRIM(LTRIM(ЛьготнаяКатегория))) = LOWER(RTRIM(LTRIM('"+ this.preferentCategory + "'))) ";

            return query;

            //return @" select COUNT(Tab1.id_договор) as КоличествоДоговоров ,НаименованиеПоликлинники,ЛьготнаяКатегория,SUM(Сумма) as 'Сумма' from (
            //        select Договор.id_договор, НаименованиеПоликлинники, ЛьготнаяКатегория from Договор
            //        inner join(select id_поликлинника, ПоликлинникиИнн.F2 as 'НаименованиеПоликлинники' from Поликлинника
            //        inner join ПоликлинникиИнн
            //        on Поликлинника.ИНН = ПоликлинникиИнн.F3
            //         where LOWER(RTRIM(LTRIM(НаименованиеПоликлинники))) = LOWER(RTRIM(LTRIM('" + this.hospital + "'))) " +
            //       @" ) as TabHosp 
            //        on Договор.id_поликлинника = TabHosp.id_поликлинника
            //        inner join ЛьготнаяКатегория
            //        on Договор.id_льготнаяКатегория = ЛьготнаяКатегория.id_льготнойКатегории " +
            //        @" where ФлагПроверки = 1 and ФлагНаличияАкта = 0 
            //        and ДатаЗаписиДоговора >= '" + this.dateStart + "' and ДатаЗаписиДоговора <= '" + this.dateEnd + "') as Tab1 " +
            //        @" inner join(select id_договор, SUM(Сумма) as 'Сумма' from УслугиПоДоговору
            //        group by id_договор) as Tab2
            //        on Tab2.id_договор = Tab1.id_договор
            //        group by НаименованиеПоликлинники,ЛьготнаяКатегория " +
            //        " having LOWER(RTRIM(LTRIM(ЛьготнаяКатегория))) = LOWER(RTRIM(LTRIM('" + this.preferentCategory + "'))) ";

            
        }
    }
}

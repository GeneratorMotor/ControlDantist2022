using System;
using ControlDantist.Classes;

namespace ControlDantist.Querys
{
    public class QueryContractNoAct : IQuery
    {
        // Начало отчета.
        private string DateStart = string.Empty;

        // Дата окончания отчета.
        private string DateEnd = string.Empty;

        // Поликлинника.
        private ПоликлинникиИнн поликлинникиИнн;

        public QueryContractNoAct(string dateStart, string dateEnd, ПоликлинникиИнн поликлинникиИнн)
        {
            DateStart = dateStart ?? throw new ArgumentNullException(nameof(dateStart));
            DateEnd = dateEnd ?? throw new ArgumentNullException(nameof(dateEnd));
            this.поликлинникиИнн = поликлинникиИнн ?? throw new ArgumentNullException(nameof(поликлинникиИнн));
        }

        public string Query()
        {
            return @" select КоличествоДоговоров,Tab1.ЛьготнаяКатегория,Tab3.Сумма from(
                        select COUNT(Договор.id_договор) as 'КоличествоДоговоров', ЛьготнаяКатегория.ЛьготнаяКатегория from Договор
                        inner join ЛьготнаяКатегория
                        on Договор.id_льготнаяКатегория = ЛьготнаяКатегория.id_льготнойКатегории
                        inner join Поликлинника
                        on Поликлинника.id_поликлинника = Договор.id_поликлинника
                        inner join ПоликлинникиИнн
                        on Поликлинника.ИНН = ПоликлинникиИнн.F3
                        where ПоликлинникиИнн.F1 = "+ this.поликлинникиИнн.F1 + " and Договор.ФлагПроверки = 1 " +
                        " and ДатаЗаписиДоговора >= '"+ Время.Дата(DateStart) + "' and ДатаЗаписиДоговора <=  '" + Время.Дата(DateEnd) + "' and СуммаАктаВыполненныхРабот = 0.0 " +
                        @" group by ЛьготнаяКатегория.ЛьготнаяКатегория) as Tab1
                        inner join(
                        select ЛьготнаяКатегория, Сумма from (
                        select ЛьготнаяКатегория.ЛьготнаяКатегория, SUM(Сумма)as 'Сумма' from Договор
                         inner join ЛьготнаяКатегория
                         on Договор.id_льготнаяКатегория = ЛьготнаяКатегория.id_льготнойКатегории
                         inner
                                                                 join УслугиПоДоговору
                                                           on Договор.id_договор = УслугиПоДоговору.id_договор
                                                           inner
                                                                 join Поликлинника
                                                           on Поликлинника.id_поликлинника = Договор.id_поликлинника
                                                           inner
                                                                 join ПоликлинникиИнн
                                                           on Поликлинника.ИНН = ПоликлинникиИнн.F3 " +
                     " where ПоликлинникиИнн.F1 = " + this.поликлинникиИнн.F1 + " and Договор.ФлагПроверки = 1 " +
                    " and ДатаЗаписиДоговора >= '"+ Время.Дата(DateStart) +"' and ДатаЗаписиДоговора <= '" + Время.Дата(DateEnd) + "' and СуммаАктаВыполненныхРабот = 0.0 " +
                    @" group by ЛьготнаяКатегория.ЛьготнаяКатегория) as Tab2) as Tab3
                    on Tab1.ЛьготнаяКатегория = Tab3.ЛьготнаяКатегория ";
        }
    }
}

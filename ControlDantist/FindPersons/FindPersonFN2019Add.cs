using ControlDantist.Find;
using System;

namespace ControlDantist.FindPersons
{
    public class FindPersonFN2019Add : IFindPerson
    {
        private string firstName = string.Empty;

        public FindPersonFN2019Add(string firstName)
        {
            this.firstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        }

        public string Query()
        {
            string query = @"declare @firstName nchar(100)
                            set @firstName = '" + this.firstName + "'  " +
                            @"  SELECT ДоговорAdd.id_договор,ДоговорAdd.НомерДоговора, ЛьготникAdd.Фамилия, ЛьготникAdd.Имя, 
                            ЛьготникAdd.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория, sum(УслугиПоДоговоруAdd.Сумма) as 'Сумма',
                            ДоговорAdd.ДатаЗаписиДоговора, АктВыполненныхРаботAdd.НомерАкта, АктВыполненныхРаботAdd.ДатаПодписания, ДоговорAdd.logWrite,ДоговорAdd.flag2019AddWrite,ДоговорAdd.flagАнулирован,'2019' As Год FROM ДоговорAdd
                            INNER JOIN ЛьготникAdd
                            ON dbo.ДоговорAdd.id_льготник = dbo.ЛьготникAdd.id_льготник
                            INNER JOIN dbo.ЛьготнаяКатегория
                            ON dbo.ЛьготникAdd.id_льготнойКатегории = dbo.ЛьготнаяКатегория.id_льготнойКатегории
                            INNER JOIN dbo.УслугиПоДоговоруAdd
                            ON dbo.ДоговорAdd.id_договор = dbo.УслугиПоДоговоруAdd.id_договор
                            left outer join dbo.АктВыполненныхРаботAdd
                            on dbo.АктВыполненныхРаботAdd.id_договор = ДоговорAdd.id_ТабДоговор
                            where ДоговорAdd.ФлагПроверки = 'True' --and idFileRegistProgect is not null
                            --and flagОжиданиеПроверки = 1 and ФлагВозвратНаДоработку = 0 
							and LTRIM(RTRIM(LOWER(ЛьготникAdd.Фамилия))) = LTRIM(RTRIM(LOWER(@firstName)))
                            and АктВыполненныхРаботAdd.НомерАкта is not null
                            Group by ДоговорAdd.id_договор,ДоговорAdd.НомерДоговора, ЛьготникAdd.Фамилия, ЛьготникAdd.Имя, ЛьготникAdd.Отчество,
                            ЛьготнаяКатегория.ЛьготнаяКатегория,ДоговорAdd.ДатаЗаписиДоговора,АктВыполненныхРаботAdd.НомерАкта, 
                            АктВыполненныхРаботAdd.ДатаПодписания,ДоговорAdd.logWrite,ДоговорAdd.flag2019AddWrite,ДоговорAdd.flagАнулирован";

            return query;
        }
    }
}

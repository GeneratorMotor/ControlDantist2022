using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.Find;

namespace ControlDantist.FindPersons
{
    /// <summary>
    /// Поиск льготников в таблице ДоговорАрхив.
    /// </summary>
    public class FindPersonFName2019Aftar : IFindPerson
    {
        private string firstName = string.Empty;

        public FindPersonFName2019Aftar(string firstName)
        {
            this.firstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        }

        public string Query()
        {
            string query = "declare @firstName nchar(100) " +
                          " set @firstName = '" + this.firstName + "' " +
            @" SELECT ДоговорАрхив.id_договор,ДоговорАрхив.НомерДоговора, ЛьготникАрхив.Фамилия, ЛьготникАрхив.Имя, 
                            ЛьготникАрхив.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория, sum(УслугиПоДоговоруАрхив.Сумма) as 'Сумма',
                            ДоговорАрхив.ДатаЗаписиДоговора, АктВыполненныхРаботАрхив.НомерАкта, АктВыполненныхРаботАрхив.ДатаПодписания, ДоговорАрхив.logWrite,ДоговорАрхив.flag2019AddWrite,ДоговорАрхив.flagАнулирован,'2020' As Год FROM ДоговорАрхив
                            INNER JOIN ЛьготникАрхив
                            ON dbo.ДоговорАрхив.id_льготник = dbo.ЛьготникАрхив.id_льготник
                            INNER JOIN dbo.ЛьготнаяКатегория
                            ON dbo.ЛьготникАрхив.id_льготнойКатегории = dbo.ЛьготнаяКатегория.id_льготнойКатегории
                            INNER JOIN dbo.УслугиПоДоговоруАрхив
                            ON dbo.ДоговорАрхив.id_договор = dbo.УслугиПоДоговоруАрхив.id_договор
                            left outer join dbo.АктВыполненныхРаботАрхив
                            on dbo.АктВыполненныхРаботАрхив.id_договор = ДоговорАрхив.id_договор
                            left outer join ProjectRegistrFiles
                            ON ДоговорАрхив.idFileRegistProgect = ProjectRegistrFiles.IdProjectRegistr
                            where ДоговорАрхив.ФлагПроверки = 'True' and idFileRegistProgect is not null
                            -- and flagОжиданиеПроверки = 1 and ФлагВозвратНаДоработку = 0
                            and ЛьготникАрхив.Фамилия = @firstName and YEAR(ДоговорАрхив.ДатаЗаписиДоговора) > 2019
                            and АктВыполненныхРаботАрхив.НомерАкта is not null
                            Group by ДоговорАрхив.id_договор,ДоговорАрхив.НомерДоговора, ЛьготникАрхив.Фамилия, ЛьготникАрхив.Имя, ЛьготникАрхив.Отчество,
                            ЛьготнаяКатегория.ЛьготнаяКатегория,ДоговорАрхив.ДатаЗаписиДоговора,АктВыполненныхРаботАрхив.НомерАкта, 
                            АктВыполненныхРаботАрхив.ДатаПодписания,ДоговорАрхив.logWrite,ДоговорАрхив.flag2019AddWrite,ДоговорАрхив.flagАнулирован ";

            return query;
        }
    }
}

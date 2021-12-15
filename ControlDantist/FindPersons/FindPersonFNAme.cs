using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.Find;

namespace ControlDantist.FindPersons
{
    public class FindPersonFNAme : IFindPerson
    {
        string fistname = string.Empty;

        public FindPersonFNAme(string fistname)
        {
            this.fistname = fistname ?? throw new ArgumentNullException(nameof(fistname));
        }

        public string Query()
        {
            string query = "declare @fistname nchar(100) " +
                            " set @fistname = '" + this.fistname + "' " +
                            @"SELECT Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, 
                            Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория, sum(УслугиПоДоговору.Сумма) as 'Сумма',
                            Договор.ДатаЗаписиДоговора, АктВыполненныхРабот.НомерАкта, АктВыполненныхРабот.ДатаПодписания, Договор.logWrite,Договор.flag2019AddWrite,Договор.flagАнулирован,'2021' As Год FROM Договор
                            INNER JOIN Льготник
                            ON dbo.Договор.id_льготник = dbo.Льготник.id_льготник
                            INNER JOIN dbo.ЛьготнаяКатегория
                            ON dbo.Льготник.id_льготнойКатегории = dbo.ЛьготнаяКатегория.id_льготнойКатегории
                            INNER JOIN dbo.УслугиПоДоговору
                            ON dbo.Договор.id_договор = dbo.УслугиПоДоговору.id_договор
                            left outer join dbo.АктВыполненныхРабот
                            on dbo.АктВыполненныхРабот.id_договор = Договор.id_договор
                            left outer join ProjectRegistrFiles
                            ON Договор.idFileRegistProgect = ProjectRegistrFiles.IdProjectRegistr
                            where Договор.ФлагПроверки = 'True' --and idFileRegistProgect is not null
                            --and flagОжиданиеПроверки = 1 and ФлагВозвратНаДоработку = 0
                            and Льготник.Фамилия = @fistname and YEAR(Договор.ДатаЗаписиДоговора) > 2019 
                            Group by Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество,
                            ЛьготнаяКатегория.ЛьготнаяКатегория,Договор.ДатаЗаписиДоговора,АктВыполненныхРабот.НомерАкта, 
                            АктВыполненныхРабот.ДатаПодписания,Договор.logWrite,Договор.flag2019AddWrite,Договор.flagАнулирован ";

            return query;
        }
    }
}

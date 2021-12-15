using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.Find
{
    public class FindPersonAftar2019 : IFindPerson
    {
        string numContract = string.Empty;

        private bool flagValid = false;

        public FindPersonAftar2019(string numContract, bool flagValid)
        {
            this.numContract = numContract ?? throw new ArgumentNullException(nameof(numContract));

            this.flagValid = flagValid;
        }

        public string Query()
        {
            //string query = "declare @numContract nchar(100) " +
            //                " set @numContract = '"+ this.numContract +"' " +
            //                @"SELECT Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, 
            //                Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория, sum(УслугиПоДоговору.Сумма) as 'Сумма',
            //                Договор.ДатаЗаписиДоговора, АктВыполненныхРабот.НомерАкта, АктВыполненныхРабот.ДатаПодписания, Договор.logWrite,Договор.flag2019AddWrite,Договор.flagАнулирован,'2020' As Год FROM Договор
            //                INNER JOIN Льготник
            //                ON dbo.Договор.id_льготник = dbo.Льготник.id_льготник
            //                INNER JOIN dbo.ЛьготнаяКатегория
            //                ON dbo.Льготник.id_льготнойКатегории = dbo.ЛьготнаяКатегория.id_льготнойКатегории
            //                INNER JOIN dbo.УслугиПоДоговору
            //                ON dbo.Договор.id_договор = dbo.УслугиПоДоговору.id_договор
            //                left outer join dbo.АктВыполненныхРабот
            //                on dbo.АктВыполненныхРабот.id_договор = Договор.id_договор
            //                left outer join ProjectRegistrFiles
            //                ON Договор.idFileRegistProgect = ProjectRegistrFiles.IdProjectRegistr
            //                where Договор.ФлагПроверки = 'True' and idFileRegistProgect is not null
            //                --and flagОжиданиеПроверки = 1 and ФлагВозвратНаДоработку = 0
            //                and Договор.НомерДоговора = @numContract and YEAR(Договор.ДатаЗаписиДоговора) > 2019
            //                Group by Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество,
            //                ЛьготнаяКатегория.ЛьготнаяКатегория,Договор.ДатаЗаписиДоговора,АктВыполненныхРабот.НомерАкта, 
            //                АктВыполненныхРабот.ДатаПодписания,Договор.logWrite,Договор.flag2019AddWrite,Договор.flagАнулирован ";

            string query = "declare @numContract nchar(100) " +
                           " set @numContract = '" + this.numContract + "' " +
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
                            where ДоговорАрхив.ФлагПроверки = '" + this.flagValid + "' " +
                            @" and idFileRegistProgect is not null
                            -- and flagОжиданиеПроверки = 1 and ФлагВозвратНаДоработку = 0
                            and ДоговорАрхив.НомерДоговора = @numContract and YEAR(ДоговорАрхив.ДатаЗаписиДоговора) > 2019
                            and АктВыполненныхРаботАрхив.НомерАкта is not null
                            Group by ДоговорАрхив.id_договор,ДоговорАрхив.НомерДоговора, ЛьготникАрхив.Фамилия, ЛьготникАрхив.Имя, ЛьготникАрхив.Отчество,
                            ЛьготнаяКатегория.ЛьготнаяКатегория,ДоговорАрхив.ДатаЗаписиДоговора,АктВыполненныхРаботАрхив.НомерАкта, 
                            АктВыполненныхРаботАрхив.ДатаПодписания,ДоговорАрхив.logWrite,ДоговорАрхив.flag2019AddWrite,ДоговорАрхив.flagАнулирован ";

            return query;
        }
    }
}

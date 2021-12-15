﻿using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.Find;

namespace ControlDantist.FindPersonFullTrue
{
    public class FindPersonFirstNameSecondName2019Aftar : IFindPerson
    {
        private string firstName = string.Empty;
        private string secondName = string.Empty;
        private bool flgValidate = false;

        private bool findName = false;

        private string declareName = string.Empty;
        private string name = string.Empty;

        public FindPersonFirstNameSecondName2019Aftar(string firstName, string secondName, bool flagValidate, bool flagName)
        {
            this.firstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            this.secondName = secondName ?? throw new ArgumentNullException(nameof(secondName));

            this.flgValidate = flagValidate;

            this.findName = flagName;

            if(findName == true)
            {
                declareName = " declare @secondName nchar(100) " +
                            " set  @secondName = '" + this.secondName + "' ";

                name = "  and RTRIM(LTRIM(LOWER( ЛьготникАрхив.Имя))) = RTRIM(LTRIM(LOWER(@secondName))) ";
            }

        }

        public string Query()
        {
            string query = @"declare @firstName nchar(100)
                            set @firstName = '" + this.firstName + "'  " +
                            " " + declareName + " " +
                             @"SELECT ДоговорАрхив.id_договор,ДоговорАрхив.НомерДоговора, ЛьготникАрхив.Фамилия, ЛьготникАрхив.Имя, 
                            ЛьготникАрхив.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория, sum(УслугиПоДоговоруАрхив.Сумма) as 'Сумма',
                            ДоговорАрхив.ДатаЗаписиДоговора, АктВыполненныхРаботАрхив.НомерАкта, АктВыполненныхРаботАрхив.ДатаПодписания, ДоговорАрхив.logWrite,ДоговорАрхив.flag2019AddWrite,ДоговорАрхив.flagАнулирован,'2017' As Год FROM ДоговорАрхив
                            INNER JOIN ЛьготникАрхив
                            ON dbo.ДоговорАрхив.id_льготник = dbo.ЛьготникАрхив.id_льготник
                            INNER JOIN dbo.ЛьготнаяКатегория
                            ON dbo.ЛьготникАрхив.id_льготнойКатегории = dbo.ЛьготнаяКатегория.id_льготнойКатегории
                            INNER JOIN dbo.УслугиПоДоговоруАрхив
                            ON dbo.ДоговорАрхив.id_договор = dbo.УслугиПоДоговоруАрхив.id_договор
                            left outer join dbo.АктВыполненныхРаботАрхив
                            on dbo.АктВыполненныхРаботАрхив.id_договор = ДоговорАрхив.id_договор
                            where ДоговорАрхив.ФлагПроверки = '" + this.flgValidate + "' " +
                            @" and RTRIM(LTRIM(LOWER( ЛьготникАрхив.Фамилия))) = RTRIM(LTRIM(LOWER(@firstName))) " +
                            " " + name + " " +
                            @" and YEAR(ДоговорАрхив.ДатаЗаписиДоговора) > 2019 
                            and АктВыполненныхРаботАрхив.НомерАкта is not null
                            Group by ДоговорАрхив.id_договор,ДоговорАрхив.НомерДоговора, ЛьготникАрхив.Фамилия, ЛьготникАрхив.Имя, ЛьготникАрхив.Отчество,
                            ЛьготнаяКатегория.ЛьготнаяКатегория,ДоговорАрхив.ДатаЗаписиДоговора,АктВыполненныхРаботАрхив.НомерАкта, 
                            АктВыполненныхРаботАрхив.ДатаПодписания,ДоговорАрхив.logWrite,ДоговорАрхив.flag2019AddWrite,ДоговорАрхив.flagАнулирован";

            return query;
        }
    }
}

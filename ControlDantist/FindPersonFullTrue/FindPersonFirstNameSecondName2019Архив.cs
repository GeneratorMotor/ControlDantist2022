﻿using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.Find;

namespace ControlDantist.FindPersonFullTrue
{
    public class FindPersonFirstNameSecondName2019Архив : IFindPerson
    {

        public string firstName = string.Empty;
        public string secondName = string.Empty;

        private bool flagValidate = false;

        private string name = string.Empty;

        // Строка указывает что есть или нет акта выполненных работ.
        private string actFalseTrue = string.Empty;


        public FindPersonFirstNameSecondName2019Архив(string firstName, string secondName, bool flagValidate, bool findName)
        {
            this.firstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            this.secondName = secondName ?? throw new ArgumentNullException(nameof(secondName));

            this.flagValidate = flagValidate;

            if(findName == true)
            {
                name = " and RTRIM(LTRIM(LOWER( ЛьготникАрхив.Имя))) = RTRIM(LTRIM(LOWER(@secondName))) ";
            }

            if (flagValidate == true)
            {
                actFalseTrue = " and АктВыполненныхРаботАрхив.НомерАкта is not null ";
            }
            else
            {
                actFalseTrue = " and АктВыполненныхРаботАрхив.НомерАкта is null ";
            }
        }

        public string Query()
        {
            //string query = @"declare @firstName nchar(100)
            //                set @firstName = '" + this.firstName + "'  " +
            //                " declare @secondName nchar(100) " +
            //                " set  @secondName = '" + this.secondName + "' " +
            string query =  @"SELECT ДоговорАрхив.id_договор,ДоговорАрхив.НомерДоговора, ЛьготникАрхив.Фамилия, ЛьготникАрхив.Имя, 
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
                            where ДоговорАрхив.ФлагПроверки = '"+ flagValidate +"' " +
                            @" and RTRIM(LTRIM(LOWER( ЛьготникАрхив.Фамилия))) = RTRIM(LTRIM(LOWER(@firstName))) " +
                            " " +this.name +  " " +
                            @" and YEAR(ДоговорАрхив.ДатаЗаписиДоговора) = 2019 " +
                            " "+ actFalseTrue + " " +
                            @"Group by ДоговорАрхив.id_договор,ДоговорАрхив.НомерДоговора, ЛьготникАрхив.Фамилия, ЛьготникАрхив.Имя, ЛьготникАрхив.Отчество,
                            ЛьготнаяКатегория.ЛьготнаяКатегория,ДоговорАрхив.ДатаЗаписиДоговора,АктВыполненныхРаботАрхив.НомерАкта, 
                            АктВыполненныхРаботАрхив.ДатаПодписания,ДоговорАрхив.logWrite,ДоговорАрхив.flag2019AddWrite,ДоговорАрхив.flagАнулирован";

            return query;
        }
    }
}

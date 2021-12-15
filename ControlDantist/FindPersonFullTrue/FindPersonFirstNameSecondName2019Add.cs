using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.Find;

namespace ControlDantist.FindPersonFullTrue
{
    public class FindPersonFirstNameSecondName2019Add : IFindPerson
    {
        public string firstName = string.Empty;
        public string secondName = string.Empty;


        private bool flagValidate = false;

        private bool flagNAme = false;

        // Переменная указывает есть ли поиск по Имени.
        private string findName = string.Empty;

        private string name = string.Empty;

        private string isAct = string.Empty;

        public FindPersonFirstNameSecondName2019Add(string firstName, string secondName, bool flagValidate, bool flagNAme)
        {
            this.firstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            this.secondName = secondName ?? throw new ArgumentNullException(nameof(secondName));

            this.flagValidate = flagValidate;
            this.flagNAme = flagNAme;

            if (flagNAme == true)
            {
                findName = " declare @secondName nchar(100) " +
                            " set  @secondName = '" + this.secondName + "' ";

                name = " and RTRIM(LTRIM(LOWER(ЛьготникAdd.Имя))) = RTRIM(LTRIM(LOWER(@secondName))) ";
            }
            else
            {
                findName = " ";

                name = " ";
            }

            if(flagValidate == true)
            {
                this.isAct = " and АктВыполненныхРаботAdd.НомерАкта is not null ";
            }
            else
            {
                this.isAct = " and АктВыполненныхРаботAdd.НомерАкта is null ";
            }
        }

        public string Query()
        {
            string query = @"declare @firstName nchar(100)
                            set @firstName = '" + this.firstName + "'  " +
                            @" "+ findName + "  " +
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
                            where ДоговорAdd.ФлагПроверки = '" + flagValidate + "' " + //--and idFileRegistProgect is not null
                            //--and flagОжиданиеПроверки = 1 and ФлагВозвратНаДоработку = 0 
							@" and LTRIM(RTRIM(LOWER(ЛьготникAdd.Фамилия))) = LTRIM(RTRIM(LOWER(@firstName))) " +
                            " "+ name +" " +
                            //@" and АктВыполненныхРаботAdd.НомерАкта is not null
                            " "+ this.isAct + " "  +
                            @"Group by ДоговорAdd.id_договор,ДоговорAdd.НомерДоговора, ЛьготникAdd.Фамилия, ЛьготникAdd.Имя, ЛьготникAdd.Отчество,
                            ЛьготнаяКатегория.ЛьготнаяКатегория,ДоговорAdd.ДатаЗаписиДоговора,АктВыполненныхРаботAdd.НомерАкта, 
                            АктВыполненныхРаботAdd.ДатаПодписания,ДоговорAdd.logWrite,ДоговорAdd.flag2019AddWrite,ДоговорAdd.flagАнулирован";

            return query;
        }
    }
}

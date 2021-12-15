using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.Find;

namespace ControlDantist.FindPersonFullTrue
{
    public class FindPersonFirstNameSecondName : IFindPerson
    {
        private string firstName = string.Empty;
        private string secondName = string.Empty;
        private bool flgValidate = false;

        private bool flagFindName = false;

        private string declareName = string.Empty;

        private string name = string.Empty;

        // Строка указывает что есть или нет акта выполненных работ.
        private string actFalseTrue = string.Empty;

        public FindPersonFirstNameSecondName(string firstName, string secondName, bool flagValidate, bool flagFindName)
        {
            this.firstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            this.secondName = secondName ?? throw new ArgumentNullException(nameof(secondName));

            this.flgValidate = flagValidate;

            if(flagFindName == true)
            {
                declareName = " declare @secondName nchar(100) " +
                            " set @secondName = '" + this.secondName + "' ";

                name = " and Льготник.Имя = @secondName ";
            }
            else
            {
                declareName = " ";

                name = " ";
            }

            if (flagValidate == true)
            {
                //actFalseTrue = " and АктВыполненныхРабот.НомерАкта is not null ";
                actFalseTrue = " and ((АктВыполненныхРабот.НомерАкта is not null) or (АктВыполненныхРабот.НомерАкта is null)) ";
            }
            else
            {
                actFalseTrue = " and АктВыполненныхРабот.НомерАкта is null ";
            }

        }

        public string Query()
        {
            string query = "declare @fistname nchar(100) " +
                            " set @fistname = '" + this.firstName + "' " +
                            " "+ declareName +" " +
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
                            where Договор.ФлагПроверки = '" + this.flgValidate +"'  " + 
                            //and idFileRegistProgect is not null
                            //--and flagОжиданиеПроверки = 1 and ФлагВозвратНаДоработку = 0
                            @" and Льготник.Фамилия = @fistname " +
                            " "+ name +" " +
                            @" and YEAR(Договор.ДатаЗаписиДоговора) > 2019 " +
                            " "+ actFalseTrue + " " +
                            @"Group by Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество,
                            ЛьготнаяКатегория.ЛьготнаяКатегория,Договор.ДатаЗаписиДоговора,АктВыполненныхРабот.НомерАкта, 
                            АктВыполненныхРабот.ДатаПодписания,Договор.logWrite,Договор.flag2019AddWrite,Договор.flagАнулирован ";

            return query;
        }
    }
}

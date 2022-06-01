using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.Querys
{
    public class QueryFindContractFn21 : IQuery
    {
        // Фамилия льготника.
        string firstName = string.Empty;

        // Имя льготника.
        string name = string.Empty;

        // Дата рождения.
        string dr = string.Empty;

        // Номер текущего договора в реестре.
        string numContract = string.Empty;

        private int validYear = 0;

        public QueryFindContractFn21(string firstName, string name, string dr, string numContract, int year)
        {
            this.firstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.dr = dr ?? throw new ArgumentNullException(nameof(dr));
            this.numContract = numContract ?? throw new ArgumentNullException(nameof(numContract));
            this.validYear = year;
        }

        public string Query()
        {
            string query = @" declare @FistName nvarchar(100)
                            declare @Name nvarchar(100)
                            declare @Surname nvarchar(100)
                            declare @NumContract nvarchar(50)
                            declare @DR date
                            set @FistName = '" + this.firstName + "' " + " set @Name = '" + this.name + "' " + " set @DR = '" + this.dr + "' " + " set @NumContract = '" + this.numContract + "' " +
                            @"  select Фамилия, Имя, Отчество, ДоговорАрхив.НомерДоговора,ЛьготникАрхив.ДатаРождения,ДатаДоговора,[НомерАкта],
                            ДатаАктаВыполненныхРабот,
                            ДоговорАрхив.flagАнулирован,ДоговорАрхив.ФлагАнулирован, ФлагПроверки,ФлагНаличияАкта from ЛьготникАрхив
                            inner join ДоговорАрхив
                            on ЛьготникАрхив.id_льготник = ДоговорАрхив.id_льготник
                            left outer join АктВыполненныхРаботАрхив
                            on ДоговорАрхив.id_договор = АктВыполненныхРаботАрхив.id_договор
                            where((
							LOWER(RTRIM(LTRIM([Фамилия]))) = LOWER(LTRIM(RTRIM(@FistName))) and LOWER(LTRIM(RTRIM(Имя))) = LOWER(LTRIM(RTRIM(@Name)))
							--and  LOWER(LTRIM(RTRIM(Отчество))) = LOWER(LTRIM(RTRIM(@Surname)))
							and (ЛьготникАрхив.ДатаРождения =  @DR)
							and ((YEAR(ДатаАктаВыполненныхРабот) >= "+ this.validYear +" and flagАнулирован = 0 and ФлагНаличияАкта = 1) " +
							@"or (YEAR(ДатаАктаВыполненныхРабот)= 1900 and flagАнулирован = 1) or (YEAR(ДатаАктаВыполненныхРабот)= 1900 and flagАнулирован = 0)
							)))   ";
            return query;
        } 
    }
}

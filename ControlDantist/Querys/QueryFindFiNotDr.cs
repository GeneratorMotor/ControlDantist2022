using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.Querys
{
    public class QueryFindFiNotDr : IQuery
    {
        // Фамилия льготника.
        string firstName = string.Empty;

        // Имя льготника.
        string name = string.Empty;

        int yearValid = 0;

        public QueryFindFiNotDr(string firstName, string name, int yearValid)
        {
            this.firstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.yearValid = yearValid;
        }

        public string Query()
        {
            string query = @" declare @FistName nvarchar(100)
                    declare @Name nvarchar(100)
                    declare @Surname nvarchar(100)
                    declare @NumContract nvarchar(50) " +
                   @"set @FistName = '" + this.firstName + "' " + " set @Name = '" + this.name + "' " + 
                   @"select Фамилия, Имя, Отчество, Договор.НомерДоговора,Льготник.ДатаРождения,ДатаДоговора,[НомерАкта],
                                                ДатаАктаВыполненныхРабот,
                                                Договор.flagАнулирован,Договор.ФлагАнулирован, ФлагПроверки,ФлагНаличияАкта from Договор
                    inner join Льготник
                    on Договор.id_льготник = Льготник.id_льготник
                    left outer join АктВыполненныхРабот
                    on АктВыполненныхРабот.id_договор = Договор.id_договор
                     where((
							LOWER(RTRIM(LTRIM([Фамилия]))) = LOWER(LTRIM(RTRIM(@FistName))) and LOWER(LTRIM(RTRIM(Имя))) = LOWER(LTRIM(RTRIM(@Name)))
							and ((YEAR(ДатаАктаВыполненныхРабот) >= " + this.yearValid + ") " +
                   " ))) ";

            return query;
        }
    }
}

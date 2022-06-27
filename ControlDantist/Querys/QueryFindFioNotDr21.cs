using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.Querys
{
    /// <summary>
    /// Инкапсулирует запрос к БД на поиск ранее заключенных договоров по ФИО.
    /// </summary>
    public class QueryFindFioNotDr21 : IQuery
    {

        // Фамилия льготника.
        string firstName = string.Empty;

        // Имя льготника.
        string name = string.Empty;

        // Отчество льготника.
        string surname = string.Empty;

        int yearValid = 0;

        public QueryFindFioNotDr21(string firstName, string name, string surname, int yearValid)
        {
            this.firstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.surname = surname ?? throw new ArgumentNullException(nameof(surname));
            this.yearValid = yearValid;
        }

        public string Query()
        {
            string query = @" declare @FistName nvarchar(100)
                    declare @Name nvarchar(100)
                    declare @Surname nvarchar(100)
                    declare @NumContract nvarchar(50) " +
                  @"set @FistName = '" + this.firstName + "' " + " set @Name = '" + this.name + "' " +
                  @"select Фамилия, Имя, Отчество, ДоговорАрхив.НомерДоговора,ДатаДоговора,[НомерАкта],
                                                ДатаАктаВыполненныхРабот,
                                                ДоговорАрхив.flagАнулирован,ДоговорАрхив.ФлагАнулирован, ФлагПроверки,ФлагНаличияАкта from ДоговорАрхив
                    inner join ЛьготникАрхив
                    on ДоговорАрхив.id_льготник = ЛьготникАрхив.id_льготник
                    left outer join АктВыполненныхРаботАрхив
                    on АктВыполненныхРаботАрхив.id_договор = ДоговорАрхив.id_договор
                     where((
							LOWER(RTRIM(LTRIM([Фамилия]))) = LOWER(LTRIM(RTRIM(@FistName))) and LOWER(LTRIM(RTRIM(Имя))) = LOWER(LTRIM(RTRIM(@Name)))
							and ((YEAR(ДатаАктаВыполненныхРабот) >= " + this.yearValid + ") " +
                  " ))) ";

            return query;
        }
    }
}

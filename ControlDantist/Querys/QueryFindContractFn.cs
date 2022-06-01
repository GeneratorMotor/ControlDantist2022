using System;

namespace ControlDantist.Querys
{
    public class QueryFindContractFn : IQuery
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

        public QueryFindContractFn(string firstName, string name, string dr, string numContract, int year)
        {
            this.firstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.dr = dr ?? throw new ArgumentNullException(nameof(dr));
            this.numContract = numContract ?? throw new ArgumentNullException(nameof(numContract));
            this.validYear = year;
        }

        /// <summary>
        /// SQL запрос к БД.
        /// </summary>
        /// <returns>Sql запрос на поиск льготника и договоров</returns>
        public string Query()
        {
            string query = @" declare @FistName nvarchar(100)
                            declare @Name nvarchar(100)
                            declare @Surname nvarchar(100)
                            declare @NumContract nvarchar(50)
                            declare @DR date
                            set @FistName = '" + this.firstName + "' " +
                            " set @Name = '" + this.name + "' " +
                            " set @DR = '" + this.dr + "' " +
                            " set @NumContract = '" + this.numContract + "' " +
                            @" select Фамилия, Имя, Отчество, Договор.НомерДоговора,Льготник.ДатаРождения,ДатаДоговора,[НомерАкта],
                            ДатаАктаВыполненныхРабот,
                            Договор.flagАнулирован,Договор.ФлагАнулирован, ФлагПроверки,ФлагНаличияАкта from Льготник
                            inner join Договор
                            on Льготник.id_льготник = Договор.id_льготник
                            left outer join АктВыполненныхРабот
                            on Договор.id_договор = АктВыполненныхРабот.id_договор
                            where((
							LOWER(RTRIM(LTRIM([Фамилия]))) = LOWER(LTRIM(RTRIM(@FistName))) and LOWER(LTRIM(RTRIM(Имя))) = LOWER(LTRIM(RTRIM(@Name)))
							and (Льготник.ДатаРождения =  @DR)
							and ((YEAR(ДатаАктаВыполненныхРабот) >= 2020) 
							or (YEAR(ДатаАктаВыполненныхРабот)= 1900 and flagАнулирован = 1) or (flagАнулирован = 0)
							)))  ";
            

            return query;
        }
    }
}

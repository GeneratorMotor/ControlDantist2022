using System;
using ControlDantist.Classes;

namespace ControlDantist.ValidPersonContract
{
    public class ValidatePersonContractArchiv : IValidatePersonContract
    {
        private string famili = string.Empty;
        private string name = string.Empty;
        private string secondName = string.Empty;
        private DateTime dateBirth;
        //private string query = string.Empty;

        public ValidatePersonContractArchiv(string famili, string name, string secondName, DateTime dateBirth)//, string query)
        {
            this.famili = famili ?? throw new ArgumentNullException(nameof(famili));
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.secondName = secondName ?? throw new ArgumentNullException(nameof(secondName));
            this.dateBirth = dateBirth;
            //this.query = query ?? throw new ArgumentNullException(nameof(query));
        }


        public string Execute()
        {
            return @"declare @FistName nvarchar(100)
                            declare @Name nvarchar(100)
                            declare @Surname nvarchar(100)
                            declare @DR date
                            set @FistName = '" + this.famili + "' " +
                            " set @Name = '" + name + "' " +
                            " set @Surname = '" + this.secondName + "' " +
                            "set @DR = '" + Время.Дата(this.dateBirth.ToShortDateString()) + "' " +
                            @" select НомерДоговора, ДоговорАрхив.ДатаАктаВыполненныхРабот from ЛьготникАрхив
                            inner join ДоговорАрхив
                            on ЛьготникАрхив.id_льготник = ДоговорАрхив.id_льготник
                            --inner join АктВыполненныхРабот
                            --on АктВыполненныхРабот.id_договор = Договор.id_договор
                             where ДоговорАрхив.ФлагПроверки = 1 and LOWER(RTRIM(LTRIM([Фамилия]))) = LOWER(LTRIM(RTRIM(@FistName))) and LOWER(LTRIM(RTRIM(Имя))) = LOWER(LTRIM(RTRIM(@Name))) 
                            and((LOWER(LTRIM(RTRIM(Отчество))) = LOWER(LTRIM(RTRIM(@Surname)))) or Отчество is null) and ДатаРождения = @DR " +
                            " and YEAR(ДоговорАрхив.ДатаЗаписиДоговора) > 2019";
                           
        }
    }
}

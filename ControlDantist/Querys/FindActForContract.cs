using ControlDantist.DataBaseContext;
using System;
using System.Text;
using ControlDantist.Classes;

namespace ControlDantist.Querys
{
    public class FindActForContract : IQuery
    {
        ТЛЬготник тЛЬготник;

        public FindActForContract(ТЛЬготник тЛЬготник)
        {
            this.тЛЬготник = тЛЬготник ?? throw new ArgumentNullException(nameof(тЛЬготник));
        }

        public string Query()
        {
            // Переменная для хранения строки запроса.
            StringBuilder builder = new StringBuilder();

            string firstNameName = @"declare @FistName nvarchar(20)
                            set @FistName = '" + тЛЬготник.Фамилия.Trim() + "' " +
                           " declare @Name nvarchar(20) " +
                            " set @Name = '" + тЛЬготник.Имя.Trim() + "' ";

            // Добавим переменные Фамилия Имя.
            builder.Append(firstNameName);

            if( String.IsNullOrEmpty(тЛЬготник.Отчество) == false)
            {
                string surname = @" declare @Surname nvarchar(20)
                                   set @Surname = '"+ тЛЬготник.Отчество.Trim() +"' ";

                builder.Append(surname);
            }

            string query = @"declare @DR DateTime
                            set @DR = '" + Время.Дата(this.тЛЬготник.ДатаРождения.ToShortDateString()) + "' " +
                           @" select ФлагНаличияАкта, НомерДоговора from Договор
                             inner join Льготник
                             on Договор.id_льготник = Льготник.id_льготник
                             where Договор.ФлагПроверки = 1
                            and LOWER(RTRIM(LTRIM([Фамилия]))) = LOWER(LTRIM(RTRIM(@FistName))) and LOWER(LTRIM(RTRIM(Имя))) = LOWER(LTRIM(RTRIM(@Name)))
                            and((LOWER(LTRIM(RTRIM(Отчество))) = LOWER(LTRIM(RTRIM(@Surname))))) and ДатаРождения = @DR ";

            builder.Append(query);


            return builder.ToString();
        }
    }
}

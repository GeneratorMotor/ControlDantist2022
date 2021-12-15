using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.Find
{
    public class QueryPerson2021 : IQueryFind
    {
        private int id = 0;

        public QueryPerson2021(int id)
        {
            this.id = id;
        }

        public string Query()
        {
            // Получим данные по договору.
            return @" SELECT top 1 Договор.НомерДоговора, Договор.ДатаДоговора, Льготник.Фамилия, Льготник.Имя, 
                                            Льготник.Отчество, Льготник.ДатаРождения, Льготник.улица, Льготник.НомерДома, 
                                            Льготник.корпус, Льготник.НомерКвартиры, Льготник.СерияПаспорта, Льготник.НомерПаспорта, 
                                            Льготник.СерияДокумента, Льготник.НомерДокумента, Льготник.ДатаВыдачиДокумента, 
                                            Льготник.ДатаВыдачиПаспорта,Договор.ФлагПроверки,Договор.ДатаПроверки FROM Договор
                                            INNER JOIN УслугиПоДоговору
                                            ON Договор.id_договор = УслугиПоДоговору.id_договор
                                            INNER JOIN Льготник
                                            ON dbo.Договор.id_льготник = dbo.Льготник.id_льготник " +
                                    "where Договор.id_договор = " + this.id + " ";
        }
    }
}

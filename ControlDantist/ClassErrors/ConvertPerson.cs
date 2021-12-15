using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.DataBaseContext;

namespace ControlDantist.ClassErrors
{
    public class ConvertPerson
    {
        private Т2Льготник p;
        private ТЛЬготник pers;

        public ConvertPerson(ТЛЬготник pers)
        {
            this.pers = pers ?? throw new ArgumentNullException(nameof(pers));
            p = new Т2Льготник();
        }

        public Т2Льготник Convert()
        {
            DateTime dateTime = new DateTime(1900, 1, 1);

            p.Фамилия = pers.Фамилия;
            p.Имя = pers.Имя;
            p.Отчество = pers?.Отчество ?? "";
            p.ДатаРождения = pers?.ДатаРождения ?? dateTime;
            p.КемВыданДокумент = pers.КемВыданДокумент;
            p.КемВыданПаспорт = pers.КемВыданПаспорт;
            p.ДатаВыдачиДокумента = pers.ДатаВыдачиДокумента;
            p.ДатаВыдачиПаспорта = pers.ДатаВыдачиПаспорта;
            p.корпус = pers.корпус;
            p.НомерДокумента = pers.НомерДокумента;
            p.НомерДома = pers.НомерДома;
            p.НомерКвартиры = pers.НомерКвартиры;
            p.НомерПаспорта = pers.НомерПаспорта;
            p.СерияДокумента = pers.СерияДокумента;
            p.СерияПаспорта = pers.СерияПаспорта;
            p.Снилс = pers?.Снилс ?? null;
            p.улица = pers.улица;
            p.id_документ = pers.id_документ;
            p.id_льготнойКатегории = pers.id_льготнойКатегории;
            p.id_насПункт = pers.id_насПункт;
            p.id_область = pers.id_область;
            p.id_район = pers.id_район;
            return p;
        }


    }
}

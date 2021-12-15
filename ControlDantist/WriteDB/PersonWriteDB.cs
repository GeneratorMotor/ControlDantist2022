using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.DataBaseContext;

namespace ControlDantist.WriteDB
{
    public class PersonWriteDB : IValidBD<ТЛЬготник>
    {
        private ТЛЬготник person;

        private DContext dc;

        private bool flafValide = false;

        public PersonWriteDB(DContext dc,ТЛЬготник person)
        {
            this.person = person;
            this.dc = dc;
        }

        public ТЛЬготник Get()
        {
            return this.person;
        }

        public bool Validate()
        {
             // Флаг указывает что возмождна ли запись льгогтника.
             bool flagExesWritePerson = false;

             // Если вдруг у льготника нет отчества.
             if (person.Отчество == null)
             {
                person.Отчество = "";
             }

             // Поиск льготника.
             var persons = dc.ТабЛьгоготник.Where(w => w.Фамилия.Trim().ToLower() == person.Фамилия.Trim().ToLower() && w.Имя.Trim().ToLower() == person.Имя.ToLower().Trim() && w.Отчество.Trim().ToLower() == person.Отчество.Trim().ToLower() && w.ДатаРождения == person.ДатаРождения).OrderByDescending(w=>w.id_льготник).FirstOrDefault();

            // Если льготников нет в БД значит его можно писать в БД.
            if (persons != null)
            {
                    // Запретим запись льготника в БД.
                    flagExesWritePerson = false;

                    // Так как мы получаем 1-го льготника с конца согласно метода FirstOrDefault.
                    this.person.id_льготник = persons.id_льготник;  
            }
            else
            {
                    // Разрешим запись льготника в БД.
                    flagExesWritePerson = true;
            }

            return flagExesWritePerson;
        }
    }
}

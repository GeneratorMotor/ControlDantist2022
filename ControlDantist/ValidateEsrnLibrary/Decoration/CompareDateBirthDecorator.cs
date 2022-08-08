using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.DataBaseContext;

namespace ControlDantist.ValidateEsrnLibrary.Decoration
{
    public class CompareDateBirthDecorator : Decorator
    {
        // Переменная для хранения данных о льготнике полученных из ЭСРН.
        private List<DatePerson> listPerson;

        // Переменная для хранения данных о льготнике полученных из реестра.
        private ItemLibrary currentPerson;

        // Коллекция для хранения данных об ошибках найденных при проверке по ЭСРН.
        private List<DiscriptionValidate> listDiscriptionValidate;
        public Component GetComponent { get; set; }

        /// <summary>
        /// Сравнение данных из реестра с данными из ЭСРН.
        /// </summary>
        /// <param name="listPerson"></param>
        /// <param name="person"></param>
        public CompareDateBirthDecorator(List<DatePerson> listPerson, ItemLibrary person)
        {
            this.listPerson = listPerson ?? throw new ArgumentNullException(nameof(listPerson));
            this.currentPerson = person ?? throw new ArgumentNullException(nameof(person));

            listDiscriptionValidate = new List<DiscriptionValidate>();
        }

        public override IEnumerable<DiscriptionValidate> ComparePerson()
        {
            // Данные по текущему льготнику.
            var person = this.currentPerson.Packecge.льготник;

            var testPersonDR = person.ДатаРождения;

            var asd = this.listPerson.Select(w => w.ДатаРождения).FirstOrDefault();

            var testComponent = this.GetComponent;

            // Отчество.
            var secondname = person?.Отчество ?? "";

            // Сравним данные по льготнику из реестра с данными из ЭСРН.
            var rezultDb = this.listPerson.Any(w => w.ДатаРождения == person.ДатаРождения);

            if (rezultDb == false)
            {
                // Описание ошибки при проверки данных по льготнику.
                DiscriptionValidate discriptionError = new DiscriptionValidate();

                // Укажем что при проверки произошла ошибка.
                discriptionError.FlagErrorValidate = true;

                // Описание ошибки.
                //discriptionError.DescriptionError = " Ошибка в дате рождения ;";

                if (this.GetComponent != null)
                {
                    this.GetComponent.ComparePerson().ToList().Add(discriptionError);

                    this.listDiscriptionValidate.AddRange(GetComponent.ComparePerson().ToList());
                }
                else
                {
                    this.listDiscriptionValidate.Add(discriptionError);
                }
            }
            else
            {
                if (this.GetComponent != null)
                {
                    this.listDiscriptionValidate.AddRange(GetComponent.ComparePerson().ToList());
                }
            }

            return this.listDiscriptionValidate;
        }
    }
}

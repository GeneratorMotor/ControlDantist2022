using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.DataBaseContext;

namespace ControlDantist.ValidateEsrnLibrary.Decoration
{
    public class CompareFioPersonDecorator : Decorator
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
        public CompareFioPersonDecorator(List<DatePerson> listPerson, ItemLibrary person)
        {
            this.listPerson = listPerson ?? throw new ArgumentNullException(nameof(listPerson));
            this.currentPerson = person ?? throw new ArgumentNullException(nameof(person));

            listDiscriptionValidate = new List<DiscriptionValidate>();
        }

        public override IEnumerable<DiscriptionValidate> ComparePerson()
        {
            // Данные по текущему льготнику.
            var person = this.currentPerson.Packecge.льготник;

            // Отчество.
            var secondname = person?.Отчество ?? "";

            // Сравним данные по льготнику из реестра с данными из ЭСРН.
            var rezultfio = this.listPerson.Any(w => w.Фамилия.ToLower().Trim() == person.Фамилия.ToLower().Trim() && w.Имя.ToLower().Trim() == person.Имя.ToLower().Trim() && w.Отчество.ToLower().Trim() == secondname.ToLower().Trim());

            if (rezultfio == false)
            {
                // Описание ошибки при проверки данных по льготнику.
                 DiscriptionValidate discriptionError = new DiscriptionValidate();

                // Укажем что при проверки произошла ошибка.
                discriptionError.FlagErrorValidate = true;

                // Описание ошибки.
                //discriptionError.DescriptionError = " Ошибка в ФИО ;";

                //this.Component.ComparePerson().ToList().Add(discriptionError);

                this.listDiscriptionValidate.Add(discriptionError);
            }

            return this.listDiscriptionValidate;
        }
    }
}

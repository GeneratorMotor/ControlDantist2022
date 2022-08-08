using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.DataBaseContext;

namespace ControlDantist.ValidateEsrnLibrary.CompareDate
{
    /// <summary>
    /// Сравниваем данные из ЭСРН с данными по реестру по ФИО.
    /// </summary>
    public class CompareFioEsrn : ICompareRegistrPerson
    {
        // Переменная для хранения данных о льготнике полученных из ЭСРН.
        private List<DatePerson> listPerson;

        // Переменная для хранения данных о льготнике полученных из реестра.
        private ItemLibrary currentPerson;

        //// Коллекция для хранения данных об ошибках найденных при проверке по ЭСРН.
        private List<DiscriptionValidate> listDiscriptionValidate;

        /// <summary>
        /// Сравнение данных из реестра с данными из ЭСРН.
        /// </summary>
        /// <param name="listPerson"></param>
        /// <param name="person"></param>
        public CompareFioEsrn(List<DatePerson> listPerson, ItemLibrary person)
        {
            this.listPerson = listPerson ?? throw new ArgumentNullException(nameof(listPerson));
            this.currentPerson = person ?? throw new ArgumentNullException(nameof(person));

            listDiscriptionValidate = new List<DiscriptionValidate>();
        }

        /// <summary>
        /// Сравнение данных по льготнику с данными из ЭСРН.
        /// </summary>
        /// <returns></returns>
        public virtual string ComparePerson()
        {
            // Данные по текущему льготнику.
            var person = this.currentPerson.Packecge.льготник;

            // Отчество.
            var secondname = person?.Отчество ?? "";

            // Переменная для хранения описания ошибки.
            string messageError = string.Empty;

            if (string.IsNullOrEmpty(person.Фамилия) == true || string.IsNullOrEmpty(person.Имя) == true)
            {
                messageError = " Ошибка нет данных по льготнику ; ";

                return messageError;
            }

            // Сравним данные по льготнику из реестра с данными из ЭСРН.
            var rezultfio = this.listPerson.Any(w => w.Фамилия.ToLower().Trim() == person.Фамилия.ToLower().Trim() && w.Имя.ToLower().Trim() == person.Имя.ToLower().Trim() && w.Отчество.ToLower().Trim() == secondname.ToLower().Trim());

            if (rezultfio == false)
            {
                messageError = " Ошибка в ФИО ; ";
            }

            return messageError;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.DataBaseContext;

namespace ControlDantist.ValidateEsrnLibrary.CompareDate
{

    public class CompareDateBirth : CompareFioEsrn
    {

        // Переменная для хранения данных о льготнике полученных из ЭСРН.
        private List<DatePerson> listPerson;

        // Переменная для хранения данных о льготнике полученных из реестра.
        private ItemLibrary currentPerson;

        // Коллекция для хранения данных об ошибках найденных при проверке по ЭСРН.
        //private List<DiscriptionValidate> listDiscriptionValidate;

        public CompareDateBirth(List<DatePerson> listPerson, ItemLibrary currentPerson) : base(listPerson,currentPerson)
        {
            this.listPerson = listPerson ?? throw new ArgumentNullException(nameof(listPerson));
            this.currentPerson = currentPerson ?? throw new ArgumentNullException(nameof(currentPerson));

            //listDiscriptionValidate = new List<DiscriptionValidate>();

        }

        public override string ComparePerson()
        {
            var person = this.currentPerson.Packecge.льготник;

            string messageError = string.Empty;

            var rezultfio = this.listPerson.Any(w => w.ДатаРождения == person.ДатаРождения);

            if (rezultfio == false)
            {
                messageError = base.ComparePerson() + " Ошибка в Дате рождения ;";
            }
            else
            {
                messageError = base.ComparePerson();
            }

            return messageError;
        }
       
    }
}

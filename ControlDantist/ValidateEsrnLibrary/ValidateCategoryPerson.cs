using ControlDantist.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDantist.ValidateEsrnLibrary
{
    /// <summary>
    /// Проверка льготника по льготной категории.
    /// </summary>
    public class ValidateCategoryPerson : IValidateЭсрн
    {

        // Переменная для хранения данных о льготнике полученных из ЭСРН.
        private List<DatePerson> listPerson;

        // Переменная для хранения данных о льготнике полученных из реестра.
        private List<ItemLibrary> list;


        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
}

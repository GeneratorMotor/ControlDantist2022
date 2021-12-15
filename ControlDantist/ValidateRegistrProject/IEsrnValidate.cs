using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.ValidateRegistrProject
{
    public interface IEsrnValidate
    {
        /// <summary>
        /// Поиск льготника в ЭСРН
        /// </summary>
        /// <param name="nameDocPreferentCategory">наименование льготного документа</param>
        /// <returns></returns>
        string FindPersons(string nameDocPreferentCategory);

        string GetPreferentCategory();
    }
}

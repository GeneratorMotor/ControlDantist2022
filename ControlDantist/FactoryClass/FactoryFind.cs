using System;
using System.Collections.Generic;
using ControlDantist.ClassessForDB;
using ControlDantist.ConvertDataTableToList;
using ControlDantist.DataBaseContext;

namespace ControlDantist.FactoryClass
{
    /// <summary>
    /// Фабрика поиска льготников.
    /// </summary>
    public class FactoryFind
    {
        /// <summary>
        /// Преобразует список договоров в список льготников.
        /// </summary>
        /// <param name="listRegistr"></param>
        /// <returns></returns>
        public IConvertRegistr<PersonContract> ConvertRegistrToPerson(List<ItemLibrary> listRegistr)
        {
            return new GetPersonRegistr(listRegistr);
        }
    }
}

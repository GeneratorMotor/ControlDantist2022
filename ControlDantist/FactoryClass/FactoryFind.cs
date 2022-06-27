using System;
using System.Collections.Generic;
using ControlDantist.ClassessForDB;
using ControlDantist.ConvertDataTableToList;
using ControlDantist.DataBaseContext;
using ControlDantist.Classes;

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

        /// <summary>
        /// Сравнивает два списка контрактов и помечают найденные флагом ранее найденных договоров.
        /// </summary>
        /// <param name="listRegistr">Список договоров.</param>
        /// <param name="contracts">Список льготников с ранее найденными заключенными договорами.</param>
        /// <returns></returns>
        public IConvertRegistr<ItemLibrary> CompareListContracts(List<ItemLibrary> listRegistr, IEnumerable<PrintContractsValidate> listDocum)
        {
            return new PersonRegistrRemains(listRegistr, listDocum);
        }
    }
}

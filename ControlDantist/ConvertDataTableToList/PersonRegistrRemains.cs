using ControlDantist.ClassessForDB;
using ControlDantist.DataBaseContext;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.Classes;

namespace ControlDantist.ConvertDataTableToList
{
    public class PersonRegistrRemains : IConvertRegistr<ItemLibrary>
    {

        List<ItemLibrary> list = new List<ItemLibrary>();

        List<PrintContractsValidate> personList = new List<PrintContractsValidate>();

        public PersonRegistrRemains(List<ItemLibrary> listRegistr, IEnumerable<PrintContractsValidate> listDocum)
        {
            if (listRegistr != null && listRegistr.Count > 0)
            {
                this.list.AddRange(listRegistr);

                if (listDocum != null && listDocum.Count() > 0)
                {
                    this.personList.AddRange(listDocum);
                }
            }
            else
            {
                //throw new ArgumentNullException("Нет данных для ");
                return;
            }

        }

        public List<ItemLibrary> GetPersons()
        {

            foreach (var itm in this.personList)
            {
                foreach (var itmRegistr in list)
                {
                    if (itm.НомерТекущийДоговор == itmRegistr.NumContract)// && itm.FlagDateLetter == true)
                    {
                        // Пометим письмо как ранее найденное.
                        itmRegistr.FlagFoundLetter = true;
                    }
                }
            }

            return list.Where(w=>w.FlagFoundLetter == false).ToList();
        }
    }
}

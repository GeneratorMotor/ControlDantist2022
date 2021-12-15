using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.Repository;
using ControlDantist.Classes;
using ControlDantist.ClassValidRegions;
using ControlDantist.DataBaseContext;


namespace ControlDantist.DisplayDatePerson
{
    public class DisplayResultValidate
    {
        private int idContract = 0;

        private UnitDate unitDate;

        // Переменная для хранения номера договора.
        private string numContract = string.Empty;

        //public DisplayResultValidate(int idContract)
        //{
        //    this.idContract = idContract;

        //    unitDate = new UnitDate();
        //}

        public DisplayResultValidate(string numContract)
        {
            this.numContract = numContract;

        }

        public DatePersonForDisplay GetFioPerson(List<ItemLibrary> listProjectContrats)
        {
            // Переменная для хранения данных по льготнику.
            DatePersonForDisplay dp = new DatePersonForDisplay();

            //// Инициализируем
            //dp.Адрес= "";
            //dp.ДатаРождения = "";
            //dp.Удостоверение = "";
            //dp.Фио = "";

            var numContracts = listProjectContrats.Where(w => w.NumContract.Trim() == this.numContract.Trim()).FirstOrDefault();

            if (numContracts!= null)
            {

                dp.Адрес = numContracts.AddressPerson;
                dp.ДатаРождения = numContracts.Packecge.льготник.ДатаРождения.ToShortDateString();
                dp.Фио = numContracts.Packecge.льготник.Фамилия.Trim() + " " + numContracts.Packecge.льготник.Имя.Trim() + " " + numContracts.Packecge.льготник.Отчество ?? "";

                string numDoc = string.Empty;

                if(numContracts.Packecge.льготник.СерияДокумента != null)
                {
                    numDoc = numContracts.Packecge.льготник.СерияДокумента.Trim();
                }
                else
                {
                    numDoc = "";
                }

                string sDoc = string.Empty;

                if(numContracts.Packecge.льготник.НомерДокумента != null)
                {
                    sDoc = numContracts.Packecge.льготник.НомерДокумента.Trim();
                }
                else
                {
                    sDoc = "";
                }
                 
                dp.Удостоверение = numDoc.Trim() + " " + sDoc.Trim();
            }


            //ItemLibrary persons = listProjectContrats..ForEach(w=>w.Packecge.тДоговор.id_договор == this.idContract)
            return dp;
        }

        //public DatePersonForDisplay GetFioPerson(Dictionary<string,PersonValidEsrn> list)
        //{
        //    //var result = from c in unitDate.ДоговорRepository.SelectAll()
        //    //             join p in unitDate.ЛьготникRepository.SelectAll()
        //    //             on c.id_льготник equals p.id_льготник
        //    //             join t in unitDate.НаселенныйПунктRepository.SelectAll()
        //    //             on p.id_насПункт  equals t.id_насПункт
        //    //             where c.id_договор == idContract
        //    //             select new { ФИО = p.Фамилия + " " + p.Имя + " " + p.Отчество.Do(w => w, ""), Адрес = " г. " + t.Наименование + " ул. " + p.улица + " корп. " + p.корпус + " д. " + p.НомерДома + " кв. "  + p.НомерКвартиры, Удостоверение = "серия " + p.СерияДокумента + "№ " + p.НомерДокумента };

        //    var persons = list.Values.Where(w => w.IdContract == this.idContract);//.Select(new DatePersonForDisplay { Фио = w.Фамилия + " " + p.Имя + " " + p.Отчество.Do(w => w, ""))

        //    DatePersonForDisplay dp = persons.Select(w => new DatePersonForDisplay { Фио = w.фамилия + " " + w.имя + " " + w.отчество.Do(x => x, ""), Адрес = w.Адрес, Удостоверение = "серия " + w.серияДокумента + "№ " + w.номерДокумента, ДатаРождения = w.датаРождения.Trim() }).FirstOrDefault();

        //    return dp;
        //}
    }
}

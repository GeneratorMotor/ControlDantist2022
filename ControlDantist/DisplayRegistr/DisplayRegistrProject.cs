using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.ClassValidRegions;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ControlDantist.Classes;
using ControlDantist.DataBaseContext;

namespace ControlDantist.DisplayRegistr
{
    public class DisplayRegistrProject
    {
        private Dictionary<string, PersonValidEsrn> dictionary;

        // Список с проектами договоров после проверки в ЭСРН.
        private IEnumerable<ItemLibrary> listProjectContrats;

        // Сумма реестра проектов договоров.
        private decimal sumRegistr = 0.0m;

        private List<ResultValidEsrnDisplay> list;

        public DisplayRegistrProject(IEnumerable<ItemLibrary> listProgectContracts)
        {
            this.listProjectContrats = listProgectContracts;

            list = new List<ResultValidEsrnDisplay>();

            ConvertRegistr();
        }

        public DisplayRegistrProject(Dictionary<string, PersonValidEsrn> dictionary)
        {
            this.dictionary = dictionary;

            list = new List<ResultValidEsrnDisplay>();

            ConvertRegistr();
        }

        public List<ResultValidEsrnDisplay> GetRegistr()
        {
            return  this.list.OrderByDescending(w => w.FlagValidEsrn).ToList();// (w=>w.FlagValidEsrn);
        }


        /// <summary>
        /// Заполняет список результат проетков договоров.
        /// </summary>
        private void ConvertRegistr()
        {
            foreach(var itm in this.listProjectContrats)
            {
                ResultValidEsrnDisplay r = new ResultValidEsrnDisplay();

                // Здесь опусаем все проверки на null так как реестр прошел проверку до этого.

                // Текущий договор.
                ТДоговор contract = itm.Packecge.тДоговор;

                // Текущий льготник.
                ТЛЬготник person = itm.Packecge.льготник;

                // id договор.
                r.IdContract = contract.id_договор;

                // Номер договора.
                r.НомерДоговора = contract.НомерДоговора.Trim();

                // Проверим есть ли отчество у льготника.
                if (person.Отчество != null && person.Отчество.Trim() != "")
                {
                    r.ФиоЛьготник = person.Фамилия.Trim() + " " + person.Имя.Trim() + " " + person.Отчество.Trim();
                }
                else
                {
                    r.ФиоЛьготник = person.Фамилия.Trim() + " " + person.Имя.Trim();
                }

                // Адрес льготника по ЭСРН.
                r.Адрес = itm.AddressPerson;


                r.СерияНомерУдостоверения = person.СерияДокумента.Trim() + " " + person.НомерДокумента.Trim();

                // Флаг что прошел провреку в ЭСРН.
                r.FlagValidEsrn = itm.FlagValidateEsrn;

                // Флаг сверки услуг на сервере и в выгрузке.
                r.FlagValidServices = itm.FlagValidateMedicalServices;

                // Флаг сохранения в БД.
                r.FlagSaveContract = false;

                decimal sumContract = (decimal)itm.Packecge.listUSlug.Sum(w => w.Сумма);// ProjectContractSum(itm.IdContract);

                sumRegistr += sumContract;

                r.SumContract = sumContract.ToString("c");

                list.Add(r);
            }

        }

        private decimal ProjectContractSum(int idContract)
        {
            DataTable dataTable = ТаблицаБД.GetTableSQL(GetQuery(idContract), "Сумма");

            return Convert.ToDecimal(dataTable.Rows[0][0]);
        }

        private string GetQuery(int idContract)
        {
            string query = @"select SUM(Сумма) from УслугиПоДоговору
                            inner join Договор
                            ON Договор.id_договор = УслугиПоДоговору.id_договор
                            where Договор.id_договор = "+ idContract + " ";

            return query;
        }

        public decimal GetSumContracts()
        {
            return sumRegistr;
        }

        
    }
}

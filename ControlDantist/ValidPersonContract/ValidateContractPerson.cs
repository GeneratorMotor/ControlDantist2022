using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.Classes;
using ControlDantist.DataBaseContext;
using System.Data;
using System.Text;
using System.Threading;

namespace ControlDantist.ValidPersonContract
{
    public class ValidateContractPerson
    {
        private List<ItemLibrary> listRegistr = new List<ItemLibrary>();

        // Мьютекс.
        static Mutex mutexObj = new Mutex();

        //хранит данные для текущего льготника
        //private PrintContractsValidate contr;

        List<ValidItemsContract> listContracts;

        public ValidateContractPerson(List<ItemLibrary> listRegistr)
        {
            if (listRegistr != null && listRegistr.Count > 0)
            {
                this.listRegistr.AddRange(listRegistr);
            }
            else
            {
                //throw new ArgumentNullException("Нет данных для ");
                return;
            }

            //contr = new PrintContractsValidate();

            

            this.listContracts = new List<ValidItemsContract>();
        }

        public List<PrintContractsValidate> GetContract()
        {

            List<PrintContractsValidate> list = new List<PrintContractsValidate>();

            foreach (ItemLibrary it in listRegistr)
            {
                PrintContractsValidate contr = new PrintContractsValidate();
                contr.listContracts = new List<ValidItemsContract>();

                string famili = it.Packecge.льготник.Фамилия.Trim();
                string name = it.Packecge.льготник.Имя.Trim();
                string surname = it.Packecge.льготник.Отчество.Trim();
                DateTime dr = it.Packecge.льготник.ДатаРождения;

                if(famili.Trim() == "Гавриленко".Trim())// && name.Trim() == "Зинаида" && name.Trim() == "Михайловна".Trim())
                {
                    string asd = "";
                }

                // Проверка договоров по таблицам 2021 года.
                IValidatePersonContract validatePersonContract = new ValidatePersonForContract(famili, name, surname, dr);
                string queryPC = validatePersonContract.Execute();

                StringParametr stringParametr = new StringParametr();
                stringParametr.Query = queryPC;

                BuildingSpike(stringParametr, it.NumContract.Trim(), contr);

                // Возможно придется стирать

                // Проверка договоров по таблице Архив.
                IValidatePersonContract validatePersonContractArchiv = new ValidatePersonContractArchiv(famili, name, surname, dr);
                string queryPcArchiv = validatePersonContractArchiv.Execute();

                StringParametr stringParametrArchiv = new StringParametr();
                stringParametrArchiv.Query = queryPcArchiv;

                BuildingSpike(stringParametrArchiv, it.NumContract.Trim(), contr);

                // КОнец.

                // Запишем данные по договору.
                contr.НомерТекущийДоговор = it.NumContract;

                StringBuilder buildFio = new StringBuilder();

                buildFio.Append(famili + " " + name + " " + surname);

                contr.ФИО_Номер_ТекущийДоговор = buildFio.ToString();

                var testList = this.listContracts.Count();

                // List<ValidItemsContract> listResultFind = 

                IWriteNumContract writeNumContract = new WriteNumContract(contr.listContracts);
                contr.СписокДоговоров = writeNumContract.Write();

                if (writeNumContract != null)
                    list.Add(contr);

            }

            return list;
        }

        private void BuildingSpike(object objParam, string numContract, PrintContractsValidate contr)
        {

            StringParametr stringParametr = (StringParametr)objParam;

            mutexObj.WaitOne();

            //lock (listContracts)
            //{
            DataTable tabContr = ТаблицаБД.GetTableSQL(stringParametr.Query, "Дубли");//, con, transact);

            // Список с номерами ранее заключонных договоров для текущего льготника (как в текущей поликлиннике так и в других поликлинниках)
            StringBuilder listNumDog = new StringBuilder();

            foreach (DataRow row in tabContr.Rows)
            {
                // Данные по текущему договору в таблице.
                ValidItemsContract validItemsContract = new ValidItemsContract();

                // Проверим на null поле таблицы.
                if (DBNull.Value != row["НомерДоговора"])
                {
                    if(row["НомерДоговора"].ToString().Trim().ToLower() == "СПКМРСО/4717".ToLower().Trim())
                    {
                        var test = "test";
                    }

                    listNumDog.Append('\n' + " " + row["НомерДоговора"].ToString().Trim());// + " от " + Convert.ToDateTime(row["ДатаЗаписиДоговора"]).ToShortDateString().Trim());

                    //validItemsContract.IdContract = Convert.ToInt32(row["id_договор"]);

                    // Номер договора
                    validItemsContract.NumContract = row["НомерДоговора"].ToString().Trim();

                    //validItemsContract.flag2019Add = Convert.ToBoolean(row["flag2019AddWrite"]);

                    validItemsContract.CurrentNumContract = numContract;

                    // Запишем состояние договора, анулирован или нет.
                    if (row.Table.Columns.Contains("flagАнулирован") == true)
                    {
                        // Запишем состояние договора, анулирован или нет.
                        validItemsContract.flagАнулирован = Convert.ToBoolean(row["flagАнулирован"]);
                    }
                    else
                    {
                        // Если такого столбца нет значит считаем что договор не анулирован.
                        validItemsContract.flagАнулирован = false;
                    }

                    // Проверим если договор не анулирован.
                    if(validItemsContract.flagАнулирован == false)
                    {
                        // Тогда сравним его с текущим договором и не будем отображать в отчете.
                        if (numContract.Trim() == row["НомерДоговора"].ToString().Trim())
                        {
                            // Перейдем к следующей итерации.
                            continue;
                        }
                    }

                    //flagАнулирован

                }

                if (DBNull.Value != row["ДатаАктаВыполненныхРабот"])
                {
                    if (Convert.ToDateTime(row["ДатаАктаВыполненныхРабот"]).ToShortDateString().Trim() != "01.01.1900".Trim())
                    {
                        listNumDog.Append(" от - " + Convert.ToDateTime(row["ДатаАктаВыполненныхРабот"]).ToShortDateString() + "; ");

                        // Дата подписания договора.
                        validItemsContract.DateContract = Convert.ToDateTime(row["ДатаАктаВыполненныхРабот"]).ToShortDateString();
                    }
                    else
                    {
                        listNumDog.Append(" - проект договора ");

                        // Дата подписания договора.
                        validItemsContract.DateContract = Convert.ToDateTime(row["ДатаАктаВыполненныхРабот"]).ToShortDateString();
                    }
                }

                if (validItemsContract.flagАнулирован == true)
                {
                    listNumDog.Append(" - договор анулирован ");

                    // Дата подписания договора.
                    validItemsContract.DateContract = Convert.ToDateTime(row["ДатаАктаВыполненныхРабот"]).ToShortDateString();
                }


                contr.listContracts.Add(validItemsContract);

            }

            //если договоров нет
            if (listNumDog.Length == 0)
            {
                contr.НомераДоговоров = "нет";
            }
            else
            {
                contr.НомераДоговоров = "заключённые договора - " + listNumDog.ToString().Trim();
            }

            mutexObj.ReleaseMutex();
            //}
        }
    }
}

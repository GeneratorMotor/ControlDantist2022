using System;
using System.Collections.Generic;
using System.Text;
using ControlDantist.Classes;
using ControlDantist.ClassessForDB;
using System.Linq;

namespace ControlDantist.DisplayLetter
{
    public class FiltrContract : IFilterContract
    {
        private IEnumerable<DataPerson> dataPeople;

        List<PrintContractsValidate> listDoc;

        public FiltrContract(IEnumerable<DataPerson> dataPeople)
        {
            this.dataPeople = dataPeople ?? throw new ArgumentNullException(nameof(dataPeople));

            listDoc = new List<PrintContractsValidate>();
        }

        public IEnumerable<PrintContractsValidate> GetContracts()
        {
            // TODO: Удалить потом.
           // var test4 = "";
           
            foreach (var person in this.dataPeople)
            {
                PrintContractsValidate item = new PrintContractsValidate();

                // Переменная для хранения ФИО льготника.
                StringBuilder fio = new StringBuilder();

                // Сформируем строку с ФИО.
                fio.Append(person.PC.Фамилия + " ");
                fio.Append(person.PC.Имя + " ");
                fio.Append(person.PC?.Отчество + " ");

                // ФИО льготника.
                item.ФИО_Номер_ТекущийДоговор = fio.ToString();

                // Номер текущего договора.
                item.НомерТекущийДоговор = person.PC.NumContract.ToString();

                // Уажем есть ли ранее заключенные договра у льготника.
                item.FlagDateLetter = person.FlagDatatLetter;

                // Строка для хранения номеров договоров.
                StringBuilder builderContracts = new StringBuilder();

                // Посмотрим есть ли у данного льготника аннулированные договора.
                var itemAnnulirovan = person.DataContracts.Where(w => w.ФлагАнулирован == true).ToList();

                // Если есть аннулированные договра.
                if (itemAnnulirovan.Count > 0)
                {
                    foreach (var itmA in itemAnnulirovan)
                    {

                        if (Convert.ToDateTime(itmA.DateContract).Year == 1900)
                        {
                            itmA.DateContract = "".Trim();
                        }

                        // Запишем номера анулированных договоров.
                        builderContracts.Append(" " +itmA.NumContract.Trim() + "  " + itmA.DateContract.Trim() + " - анулирован  \n");

                        itmA.FlagAnulirovan = true;

                        person.FlagValidate = true;
                    }

                    // Найдем номер договора с у которого ФлагНаличиаАкта = true.
                    var contract = itemAnnulirovan.Select(w => w.NumContract).FirstOrDefault();

                    if (contract != null)
                    {
                        // Установим на всех пунктах с текущим договором флаг наличия акта = true;
                        foreach (var itmContract in person.DataContracts.Where(w => w.NumContract == contract))
                        {
                            itmContract.FlagAnulirovan = true;
                        }

                        person.FlagValidate = true;
                    }

                }

                // Проверим есть договра с актоми выполненных работ.
                var itemAct = person.DataContracts.Where(w => w.ФлагНаличияАкта == true).ToList();// && w.NumContract.Trim().ToLower() != item.НомерТекущийДоговор.Trim().ToLower()).ToList();

                // Если есть ли оплаченные договора.
                if (itemAct.Count > 0)
                { 
                    foreach(var itmAct in itemAct)
                    {
                        // Укажем что договор имеет акт.
                        //itmAct.ФлагНаличияАкта = true;

                      if(Convert.ToDateTime(itmAct.DateContract).Year == 1900)
                      {
                            itmAct.DateContract = "".Trim();
                      }

                        //builderContracts.Append(" " + itmAct.NumContract +" от "+itmAct.DateContract.Trim() +"  Акт - " + itmAct.NumAct.Trim() + " от " + itmAct.DateAct.Trim() + " \n");
                        builderContracts.Append(" " + itmAct.NumContract + " - (Акт - " + itmAct.DateAct.Trim() + ") \n");

                        person.FlagValidate = true;
                    }

                    // Найдем номер договора с у которого ФлагНаличиаАкта = true.
                    var contract = itemAct.Select(w => w.NumContract).FirstOrDefault();

                    if (contract != null)
                    {
                        // Установим на всех пунктах с текущим договором флаг наличия акта = true;
                        foreach (var itmContract in person.DataContracts.Where(w => w.NumContract == contract))
                        {
                            itmContract.ФлагНаличияАкта = true;
                        }

                        person.FlagValidate = true;
                    }

                }

                // Проверим оставшиеся договора.
                //var projectContracts = person.DataContracts.Where(w => w.FlagAnulirovan == false && w.ФлагНаличияАкта == false && w.NumContract.Trim().ToLower() != item.НомерТекущийДоговор.Trim().ToLower()).ToList();
                var projectContracts = person.DataContracts.Where(w => w.FlagAnulirovan == false && w.ФлагНаличияАкта == false).ToList();// && w.NumContract.Trim().ToLower() != item.НомерТекущийДоговор.Trim().ToLower()).ToList();

                if (projectContracts.Count > 0)
                {

                    // Сгруппируем.
                    var groupProjectContract = projectContracts.GroupBy(w => w.NumContract);

                    //foreach(var itmContr in projectContracts)
                        foreach (var itmContrTake in groupProjectContract.Take(1))
                        {
                            foreach (var itmContr in itmContrTake)
                            {
                                builderContracts.Append(itmContr.NumContract + " - проект \n");

                            person.FlagValidate = true;
                            }
                        }
                }

                item.СписокДоговоров = builderContracts.ToString();

                listDoc.Add(item);
            }

            return listDoc;
        }

       
    }
}

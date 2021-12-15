using System.Collections.Generic;
using ControlDantist.Classes;
using System.Linq;

namespace ControlDantist.ValidPersonContract
{
    public static class CompareContractForNumber
    {
        public static List<ValideContract> Compare(List<ValideContract> listContract)
        {
            
            List<ValideContract> list = new List<ValideContract>();

            // Список для хранения результатов поисковой выдачи.
            List<ValideContract> listRezult = new List<ValideContract>();

            // Сгруппируем поступивший список по ФИО и номеру договора и номеру акта.
            var groupCont = listContract.GroupBy(w => new { w.Фамилия, w.Имя, w.Отчество, w.НомерДоговора, w.НомерАкта });

            // Пройдемся по группе.
            foreach (var itms in groupCont)
            {
                // Если в группе больше одной записи.
                if (itms.Count() > 1)
                {
                    // Отфильтруем записанные в таблицу Add так как запись в таблице Add имеет приоритет.
                    var contractsAdd = itms.Where(w => w.Год == 2019);

                    // Если у льготника есть договора в таблице Add.
                    if (contractsAdd.Count() > 0)
                    {
                        if (contractsAdd.Count() >= 1)
                        {
                            // Отсортируем по возрастанию id договора (IdContract) и запишем первую запись.
                            // То есть мы пишем последную запись из БД так как она более актуальная.
                            foreach (var it in contractsAdd.OrderByDescending(x => x.IdContract).Take(1))
                            {
                                // Запишем первую запись из группы в списое а остальные проигнорируем.
                                list.Add(it);
                                continue;
                            }

                            continue;
                        }
                    }
                    else
                    {
                        // ЕСли записей с пометкой 2019 год нет. То есть нет заисей в NameTableAdd.
                        var contracts = itms.Where(w => w.Год != 2019);

                        if (contracts.Count() >= 1)
                        {
                            foreach (var it in contracts.OrderByDescending(x => x.IdContract).Take(1))
                            {
                                list.Add(it);
                                continue;
                            }

                            continue;
                        }
                    }

                }
                else
                {
                    // Если в группе по одной записи.
                    // Отфильтруем записанные в таблицу Add.
                    var contractsAdd = itms.Where(w => w.Год == 2019);

                    if (contractsAdd.Count() > 0)
                    {
                        foreach (var it in contractsAdd.OrderByDescending(x => x.IdContract).Take(1))
                        {
                            // Запишем первую запись из группы в списое а остальные проигнорируем.
                            list.Add(it);
                            continue;
                        }

                        continue;
                    }

                    // Отфильтруем записанные в БД.
                    var contracts = itms.Where(w => w.Год != 2019);

                    if (contracts.Count() > 0)
                    {
                        foreach (var it in contracts.OrderByDescending(x => x.IdContract).Take(1))
                        {
                            list.Add(it);
                            continue;
                        }

                        continue;
                    }
                }
            }

            // Сгруппируем полученный результат по номеру договора и по номеру акта.
            var groupPerson = list.GroupBy(w => new { НомерДоговора = w.НомерДоговора, НомерАкта = w.НомерАкта, Год = w.Год });

            // Если в группе есть запись от 2019 года, тогда ее и берем в качестве основной.
            var countPerson = groupPerson.SelectMany(w => w).Where(w => w.Год == 2019);

            // Проверим есть ли записи за 2019 год.
            if (countPerson.Count() > 0)
            {
                var itm = countPerson.OrderByDescending(w => w.id_договор)?.Take(1)?.FirstOrDefault();

                if (itm != null)
                {
                    listRezult.Add(itm);
                }
            }
            else
            {
                // Если записей за 2019 год нет тогда.
                // Пройдемся по группе.
                foreach (var itms in groupPerson)
                {

                    // Если в группе больше одной записи.
                    if (itms.Count() > 1)
                    {
                        // Отсортируем групу по возрастанию флага.
                        var itm = itms.OrderByDescending(w => w.Год).Take(1);

                        listRezult.AddRange(itm);
                    }
                    else
                    {
                        listRezult.AddRange(itms);
                    }
                }
            }

                return listRezult;
        }
    }
}

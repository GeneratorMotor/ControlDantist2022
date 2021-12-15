using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.DataBaseContext;

namespace ControlDantist.MedicalServices
{
    /// <summary>
    /// Сарвнивает медицинские услуги в реестре с услугами из БД.
    /// </summary>
    public class ValidateMedicalServices
    {
        private ReestrContract reestrContract;

        // Переменная для хранения медицинских услуг полученных из нашей БД.
        private IServices<ТВидУслуг> servisHospital;

        /// <summary>
        /// Реестр с даннвми по контракту.
        /// </summary>
        /// <param name="reestrContract">Реестр с даннвми по контракту.</param>
        /// <param name="servisHospital">Услуги по поликлиннике.</param>
        public ValidateMedicalServices(ReestrContract reestrContract, IServices<ТВидУслуг> servisHospital)
        {
            this.reestrContract = reestrContract;
            this.servisHospital = servisHospital;
        }

        /// <summary>
        /// Получает id поликлинники.
        /// </summary>
        /// <param name="servisHospital"></param>
        //public void GetHospital(IServices<КлассификаторУслуг> servisHospital)
        //{
        //    this.servisHospital = servisHospital;
        //    //int idHospital = this.reestrContract.IdHospital();

        //    //servisHospital.ServicesMedical.
        //}

        public void ValidateServices()
        {
            // Получаем id поликлинники.
            int idHospital = this.reestrContract.IdHospital();
            
            // Если id поликлинники найден корректно.
            if(idHospital != 0)
            {
                // Установим id для поликлинники.
                servisHospital.SetIdentificator(idHospital);

                // Получим услуги поликлинники.
                List<ТВидУслуг> listKU = servisHospital.ServicesMedical();

                // Если список услуг поликлинники не пуст.
                if (listKU.Count > 0)
                {
                    foreach (var item in this.reestrContract.SetRegistServices())
                    {
                        // Получим количество услуг в договоре.
                        int countServices = item.Packecge.listUSlug.Count();

                        // Группируем услуги в проекте договоров.
                        var resultGroupContract = from r in item.Packecge.listUSlug
                                                  group r by new { р1 = r.НаименованиеУслуги.Trim(), р2 = r.Сумма } into g
                                          //group r by new { р1 = r.НаименованиеУслуги.Trim(), р2 = r.Сумма } into g
                                          select g.Key;


                        // Приведем списки к одному типу.
                        var listServicesHosp = listKU.Select(x => new УслугиПоДоговору { НаименованиеУслуги = x.ВидУслуги, цена = x.Цена }).ToList();

                        //Сджойним усулуги по договру и услуги в поликлиннике.
                        var result = from x in item.Packecge.listUSlug
                                     join y in listServicesHosp
                                     on new { X1 = x.НаименованиеУслуги.Trim().ToLower(), X2 = x.цена } equals new { X1 = y.НаименованиеУслуги.Trim().ToLower(), X2 = y.цена }
                                     //on new { X1 = x.НаименованиеУслуги.Trim().ToLower().Replace(" ", "") } equals new { X1 = y.НаименованиеУслуги.Trim().ToLower().Replace(" ", "") }
                                     select new
                                     {
                                         x.НаименованиеУслуги,
                                         x.Сумма
                                     };

                        // Количество услуг в договоре сгруппированное.
                        int iCountContract = resultGroupContract.Count();

                        // Сгруппируем услуги.
                        var resultGroup = from r in result
                                group r by new { р1 = r.НаименованиеУслуги.Trim(), р2 = r.Сумма } into g
                                          //group r by new { р1 = r.НаименованиеУслуги.Trim(), р2 = r.Сумма } into g
                                select g.Key;

                        // Группируем количество улуг в Join.
                        int countJoin = resultGroup.Count();

                        // Если количество услуги в договоре совпало с Join 
                        if (iCountContract == countJoin)
                        {
                            // Считаем что договор прошёл проверку по медицинским услугам.
                            item.FlagValidateMedicalServices = true;
                        }
                    }
                }

            }

        }
    }
}

using System;
using System.Collections.Generic;
using ControlDantist.DataBaseContext;
using System.Linq;

namespace ControlDantist.MedicalServices
{
    /// <summary>
    /// Сравнение медицинских услуг.
    /// </summary>
    public class ServicesMedicalCompare 
    {
        // Список услуг поликлинники хранящиеся на нашем сервере.
        private List<ТВидУслуг> listHosp;

        // Список услуг в договоре.
        private List<ТВидУслуг> listContract;

        /// <summary>
        /// Сравение услуг на сервере с услугами в договоре.
        /// </summary>
        /// <param name="listHosp">Услуги поликлинники (сервер)</param>
        /// <param name="listCOntract">Услуги договора</param>
        public ServicesMedicalCompare(List<ТВидУслуг> listHosp, List<ТВидУслуг> listCOntract)
        {
            this.listHosp = listHosp;
            this.listContract = listCOntract;
        }

        private void Compare()
        {
            ////Сджойним усулуги по договру и услуги в поликлиннике.
            //var result = from x in listContract // Почему то компилятор не понимает.
            //             join y in listHosp
            //             on new { X1 = x.ВидУслуги.Trim().ToLower(), X2 = x.Цена } equals new ТВидУслуг { ВидУслуги = y.ВидУслуги.Trim().ToLower(), Цена = y.Цена }
            //             select new
            //             {
            //                 x.ВидУслуги,
            //                 x.Цена
            //             };


            //// Если количество услуги в договоре совпало с Join 
            //if (result.Count() == countServices)
            //{
            //    // Считаем что договор прошёл проверку по медицинским услугам.
            //    item.FlagValidateMedicalServices = true;
            //}
        }
    }
}

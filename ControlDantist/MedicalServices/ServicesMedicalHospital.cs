using ControlDantist.DataBaseContext;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.DataBaseContext;

namespace ControlDantist.MedicalServices
{
    /// <summary>
    /// Возвращает список медицинских услуг оказываемых поликлинникой.
    /// </summary>
    public class ServicesMedicalHospital : IServices<ТВидУслуг>
    {
        private DContext dc;

        private int idHospital { get; set; }

        public ServicesMedicalHospital(DContext dc)
        {
            this.dc = dc;
        }

        /// <summary>
        /// Возвращает список услуг поликлинники.
        /// </summary>
        /// <returns></returns>
        public List<ТВидУслуг> ServicesMedical()
        {
            // Создадим пустой список с услугами полклинники.
            List<ТВидУслуг> listServices = new List<ТВидУслуг>();

            // Заполним список.
            var list = dc.ТВидУслуг?.Where(w => w.id_поликлинника == this.idHospital)?.AsQueryable()?? null;

            // Если список услуг не пустой, тогда добавим услуги в 
            if (list != null)
                listServices.AddRange(list);

            // Если список с услугами из поликлинники пустй, тогда возвращаем пустой listServices (не null).
            return listServices;
        }

        /// <summary>
        /// Установим id госпиталя.
        /// </summary>
        /// <param name="id"></param>
        public void SetIdentificator(int id)
        {
            this.idHospital = id;
        }
    }
}

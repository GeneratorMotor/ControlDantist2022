using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.DataBaseContext;

namespace ControlDantist.MedicalServices
{
    /// <summary>
    /// Содержит в себе список услуг в пректе договора.
    /// </summary>
    public class ServicesMedialContract : IServices<ТУслугиПоДоговору>
    {
        private List<ItemLibrary> packegeDateContract;

        // id договора.
        private int IdContract { get; set; }

        public ServicesMedialContract(List<ItemLibrary> packegeDateContract) //, int idContract)
        {
            this.packegeDateContract = packegeDateContract;
            //this.idContract = idContract;
        }

        /// <summary>
        /// Возвращает список проектов договоров поликлинники.
        /// </summary>
        /// <returns></returns>
        public List<ТУслугиПоДоговору> ServicesMedical()
        {
            List<ТУслугиПоДоговору> list = new List<ТУслугиПоДоговору>();

            var pakeg = this.packegeDateContract?.Where(w => w.Packecge.тДоговор.id_договор == this.IdContract).FirstOrDefault();

            if(pakeg != null)
            {
                list.AddRange(pakeg.Packecge.listUSlug);
            }

            return list;

        }

        /// <summary>
        /// Получаем id контракта.
        /// </summary>
        /// <param name="id"></param>
        public void SetIdentificator(int id)
        {
            this.IdContract = id;
        }
    }
}

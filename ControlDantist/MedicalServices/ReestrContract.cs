using ControlDantist.DataBaseContext;
using System.Collections.Generic;

namespace ControlDantist.MedicalServices
{
    /// <summary>
    /// Возвращает список услуг по договору.
    /// </summary>
    public class ReestrContract
    {
        /// <summary>
        /// Переменная для хранения реестра проектов договоров.
        /// </summary>
        private List<ItemLibrary> packegeDateContract;
        //private ServicesMedialContract servisMedicalContr;

        // Переменная для хранения услуг договора.
        private IServices<ТУслугиПоДоговору> servisMedicalContr;

        private int idHospital = 0;

        /// <summary>
        /// Создает экземпляр класса услуг по договору.
        /// </summary>
        /// <param name="packegeDateContract"></param>
        public ReestrContract(List<ItemLibrary> packegeDateContract)
        {
            // Передаем данные из реестра проектов договоров.
            this.packegeDateContract = packegeDateContract;

            servisMedicalContr = new ServicesMedialContract(packegeDateContract);
        }

        public List<ItemLibrary> SetRegistServices()
        {
            return packegeDateContract;
        }

        /// <summary>
        /// Возвращает id поликлинники указанной в реестре.
        /// </summary>
        /// <returns></returns>
        public int IdHospital()
        {

            if (this.packegeDateContract != null && this.packegeDateContract[0].Packecge != null && this.packegeDateContract[0].Packecge.hosp != null)
            {
                idHospital = this.packegeDateContract[0].Packecge.hosp.id_поликлинника;
            }

            return idHospital;
        }

        /// <summary>
        /// Возвращает список услуг по договору.
        /// </summary>
        /// <param name="idContract"></param>
        /// <returns></returns>
        public List<ТУслугиПоДоговору> GetServicesContract(int idContract)
        {
            // Список для хранения услуг по договору.
            List<ТУслугиПоДоговору> list = new List<ТУслугиПоДоговору>();

            foreach (var contract in packegeDateContract)
            {
                // Получим номер договора которому соответсвует данный id договора.
                var doc = contract?.Packecge?.тДоговор ?? null;

                if (doc != null && doc.id_договор == idContract)
                {
                    list.AddRange(contract.Packecge.listUSlug);
                }

            }

            return list;

            //// Установим id контракта.
            //servisMedicalContr.SetIdentificator(idContract);

            //return servisMedicalContr.ServicesMedical();
        }

        /// <summary>
        /// Возвращает список услуг по договору.
        /// </summary>
        /// <param name="numContract"></param>
        /// <returns></returns>
        public List<ТУслугиПоДоговору> GetServicesContract(string numContract)
        {
            // Список для хранения услуг по договору.
            List<ТУслугиПоДоговору> list = new List<ТУслугиПоДоговору>();

            foreach(var contract in packegeDateContract)
            {
                // Проведем проверку на nyull
                var doc = contract?.Packecge?.тДоговор ?? null;

                if(doc != null && doc.НомерДоговора.Trim() == numContract.Trim())
                {
                    list.AddRange(contract.Packecge.listUSlug);
                }
            }

            return list;
        }

        


    }
}

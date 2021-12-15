using ControlDantist.DataBaseContext;
using System.Linq;
using System;

namespace ControlDantist.WriteDB
{
    public class ProjectContract : IValidBD<ТДоговор>
    {
        private ТДоговор договор;

        private bool flafValide = false;

        private DContext dc;

        //private delegate bool ValidateContract(ТДоговор тДоговор);

        public ProjectContract(DContext dc,ТДоговор contract)
        {
            if (contract != null)
            {
                договор = contract;
            }
            else
            {
                throw new NullReferenceException("Отсутствует договор в рееестре");
            }

            this.dc = dc;
        }

        public bool Validate()
        {
            // Делегат поиск договора по номеру при условии что он или прошёл проверк у или анулирован.
            bool flagWriteDB = false;

            // Поиск договора.
            //var contract = this.dc.ТДоговор.Where(w => w.НомерДоговора.Trim() == this.договор.НомерДоговора.Trim() && (w.ФлагПроверки == true || w.ФлагАнулирован == true)).OrderByDescending(w => w.id_договор).FirstOrDefault();
            var contract = this.dc.ТДоговор.Where(w => w.НомерДоговора.Trim() == this.договор.НомерДоговора.Trim()).OrderByDescending(w => w.id_договор).FirstOrDefault();

            // Ели договор найден значит писать в БД нельзя.
            if (contract != null)
            {
                bool flag = false;

                // Проверим имеет ли договор флаг проверки = true.
                Func<ТДоговор, bool> validTrue = ExecContractValid;

                if (validTrue(contract) == true)
                {
                    flag = true;
                }

                Func<ТДоговор, bool> execCanceled = ExecContractAnulirovan;

                if(execCanceled(contract) == true)
                {
                    flag = true;
                }

                if (flag == true)
                {
                    flagWriteDB = false;

                    this.договор = contract;
                }
                else
                {
                    // Договор записать можно.
                    flagWriteDB = true;
                }
            }
            else
            {
                // Разрешим запись договора в БД.
                flagWriteDB = true;
            }

            return flagWriteDB;
        }

        /// <summary>
        /// Проверяет записанный договор прошел проверку или нет.
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        private bool ExecContractValid(ТДоговор contract)
        {
            bool flagValid = false;

            // Если договор прошёл проверку писать нельзя.
            if (contract.ФлагПроверки == true)
            {
                flagValid = true;
            }

            return flagValid;
        }

        /// <summary>
        /// Проверяет анулирован договор или нет.
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        private bool ExecContractAnulirovan(ТДоговор contract)
        {
            bool flagExecAct = false;

            // Если договор прошёл проверку писать нельзя.
            if (contract.ФлагАнулирован == true)
            {
                flagExecAct = true;
            }

            return flagExecAct;
        }

        ///// <summary>
        ///// Проврека договора.
        ///// </summary>
        ///// <param name="predicate">Предикат</param>
        ///// <returns>false - в БД писать можно, true - писать в БД нельзя</returns>
        //public bool Validate(Func<ТДоговор, bool> predicate)
        //{
        //    if (predicate(договор) == true)
        //    {
        //        flafValide = false;
        //    }
        //    else
        //    {
        //        flafValide = true;
        //    }

        //    return flafValide;
        //}

        public ТДоговор Get()
        {
            ТДоговор tContract;

            if(flafValide == true)
            {
                tContract = договор;
            }
            else
            {
                tContract = null;
            }

            return tContract;
        }
    }
}

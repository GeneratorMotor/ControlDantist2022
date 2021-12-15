using System.Linq;
using System;
using ControlDantist.DataBaseContext;

namespace ControlDantist.WriteDB
{
    public class ProjectContractTemp : IValidBD<Т2Договор>
    {

        private Т2Договор договор;

        private bool flafValide = false;

        private DContext dc;

        public ProjectContractTemp(DContext dc, Т2Договор contract)
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


        public Т2Договор Get()
        {
            return this.договор;
        }

        public bool Validate()
        {
            // Делегат поиск договора по номеру при условии что он или прошёл проверк у или анулирован.
            bool flagWriteDB = false;

            // Поиск договора.
            //var contract = this.dc.ТДоговор.Where(w => w.НомерДоговора.Trim() == this.договор.НомерДоговора.Trim() && (w.ФлагПроверки == true || w.ФлагАнулирован == true)).OrderByDescending(w => w.id_договор).FirstOrDefault();
            var contract = this.dc.Т2Договор.Where(w => w.НомерДоговора.Trim() == this.договор.НомерДоговора.Trim()).OrderByDescending(w => w.id_договор).FirstOrDefault();

            // Ели договор найден значит писать в БД нельзя.
            if (contract != null)
            {
                bool flag = false;

                // Проверим имеет ли договор флаг проверки = true.
                Func<Т2Договор, bool> validTrue = ExecContractValid;

                if (validTrue(contract) == true)
                {
                    flag = true;
                }

                Func<Т2Договор, bool> execCanceled = ExecContractAnulirovan;

                if (execCanceled(contract) == true)
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
        private bool ExecContractValid(Т2Договор contract)
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
        private bool ExecContractAnulirovan(Т2Договор contract)
        {
            bool flagExecAct = false;

            // Если договор прошёл проверку писать нельзя.
            if (contract.ФлагАнулирован == true)
            {
                flagExecAct = true;
            }

            return flagExecAct;
        }
    }
}

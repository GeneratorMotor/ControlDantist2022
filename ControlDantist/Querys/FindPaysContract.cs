using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.Querys
{
    /// <summary>
    /// Поиск акта выполненных работ для данного договора.
    /// </summary>
    public class FindPaysContract : IQuery
    {

        // Переменная для хранения номера договора.
        private string numderContract = string.Empty;

        public FindPaysContract(string numderContract)
        {
            this.numderContract = numderContract ?? throw new ArgumentNullException(nameof(numderContract));
        }

        /// <summary>
        /// Возвращает SQL запрос.
        /// </summary>
        /// <returns></returns>
        public string Query()
        {
            numderContract = @"select НомерАкта from Договор
                            inner join АктВыполненныхРабот
                            on Договор.id_договор = АктВыполненныхРабот.id_договор
                            where Договор.НомерДоговора = '"+ this.numderContract + "' ";

            return numderContract;
        }
    }
}

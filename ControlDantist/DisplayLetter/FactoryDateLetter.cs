using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.ConvertDataTableToList;
using ControlDantist.ClassessForDB;
using ControlDantist.PatternSql;

namespace ControlDantist.DisplayLetter
{
    public class FactoryDateLetter
    {
        /// <summary>
        /// Возвращает проверку реестра для текущего льготника.
        /// </summary>
        /// <param name="convertRegistr"></param>
        /// <param name="factorySqlQuery"></param>
        /// <param name="strConnection"></param>
        /// <returns></returns>
        public IValidateContract<ValidContracts> ValidateContractRegistr(IConvertRegistr<PersonContract> convertRegistr, PatternSql.FactoryQuery factorySqlQuery, string strConnection)
        {
            return new ValidContracts(convertRegistr, factorySqlQuery, strConnection);
        }      
        
        ///// <summary>
        ///// Проверка реестра для текущего льготника без учета дня рождения.
        ///// </summary>
        ///// <param name="convertRegistr"></param>
        ///// <param name="factorySqlQuery"></param>
        ///// <param name="strConnection"></param>
        ///// <returns></returns>
        //public IValidateContract<ValidContractNotDR> ValidateContractRegistrNotDR(IConvertRegistr<PersonContract> convertRegistr, PatternSql.FactoryQuery factorySqlQuery, string strConnection)
        //{
        //    // TODO: Удалит после отладки.
        //    var test = "";

        //    return new ValidContractNotDR(convertRegistr, factorySqlQuery, strConnection);
        //}

      
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.ConvertDataTableToList;
using ControlDantist.ClassessForDB;

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
        public IValidateContract ValidateContractRegistr(IConvertRegistr<PersonContract> convertRegistr, PatternSql.FactoryQuery factorySqlQuery, string strConnection)
        {
            return new ValidContracts(convertRegistr, factorySqlQuery, strConnection);
        }

        
    }
}

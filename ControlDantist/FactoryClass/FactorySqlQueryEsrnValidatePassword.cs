using System.Collections.Generic;
using ControlDantist.ValidateEsrnLibrary;
using ControlDantist.Querys;
using ControlDantist.DataBaseContext;

namespace ControlDantist.FactoryClass
{
    public class FactorySqlQueryEsrnValidatePassword : FactorySqlQueryEsrnValidate
    {
        public override IQuery FindPersonSqlQuery(string tempNameTable)
        {
            return new FindPersonPasswordQuery(tempNameTable);
        }
    }
}

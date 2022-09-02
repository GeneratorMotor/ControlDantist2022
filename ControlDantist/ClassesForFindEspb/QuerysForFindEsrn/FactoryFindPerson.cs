using System;
using System.Collections.Generic;
using ControlDantist.Querys;

namespace ControlDantist.ClassesForFindEspb.QuerysForFindEsrn
{
    public class FactoryFindPerson : IFactoryFindPerson<ItemReportEspb>
    {
        public IQuery CreateTempTableSqlQuery(string nameTempTable)
        {
            return new QueryCreateTempTable(nameTempTable);
        }

        public IQuery FindPersonSqlQuery(string tempNameTable)
        {
            return new QueryFindPersonEspEsrn(tempNameTable);
        }

        public IQuery InsertDateTempTableSqlQuery(List<ItemReportEspb> list, string nameTable)
        {
            return new QueryInsertTempTable(list, nameTable);
        }
    }

}

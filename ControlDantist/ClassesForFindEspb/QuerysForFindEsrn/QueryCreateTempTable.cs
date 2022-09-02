using System;
using ControlDantist.Querys;

namespace ControlDantist.ClassesForFindEspb.QuerysForFindEsrn
{
    public class QueryCreateTempTable : IQuery
    {
        string nameTempTable = string.Empty;

        public QueryCreateTempTable(string nameTempTable)
        {
            this.nameTempTable = nameTempTable ?? throw new ArgumentNullException(nameof(nameTempTable));
        }

        public string Query()
        {
            string createTable = "  create table " + nameTempTable + " ([id_карточки] [int] IDENTITY(1,1) NOT NULL, " +
                                "id_договор int NULL, " +
                                "[PC_GUID] [nvarchar](500) NULL, " +
                                "[СНИЛС] [nvarchar](50) NULL, " +
                                "[НазваниеДокумента] nvarchar (1000) )";
            return createTable;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.Querys;

namespace ControlDantist.ClassesForFindEspb
{
    public class QueryCreateTempTable : IQuery
    {
        private string nameTempTable = string.Empty;

        public QueryCreateTempTable(string nameTempTable)
        {
            this.nameTempTable = nameTempTable ?? throw new ArgumentNullException(nameof(nameTempTable));
        }

        public string Query()
        {
            string createTable = "  create table " + nameTempTable + " ([id_карточки] [int] IDENTITY(1,1) NOT NULL, " +
                                "id_договор int NULL, " +
                                "[PC_GUID] [nvarchar](500) NULL, " +
                                "[СерияДокумента] [nvarchar](50) NULL, " +
                                "[НомерДокумента] [nvarchar](50) NULL, " +
                                "[НазваниеДокумента] nvarchar (1000), " +
                                "[ДатаДокумента] [DateTime] NULL, " +
                                "[Категория] [nvarchar](1000) NULL )";

            return createTable;
        }
    }
}

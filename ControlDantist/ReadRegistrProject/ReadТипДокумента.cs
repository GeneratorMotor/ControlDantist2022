using System;
using System.Linq;
using System.Data;
using ControlDantist.DataBaseContext;

namespace ControlDantist.ReadRegistrProject
{
    public class ReadТипДокумента : IReadRegistr<ТТипДокумент>
    {
        private DContext dc;
        private DataTable tabDoc;

        public ReadТипДокумента(DContext dc, DataTable tabDoc)
        {
            this.dc = dc;
            this.tabDoc = tabDoc;
        }

        public ТТипДокумент Get()
        {
            ТТипДокумент тТипДокумент = new ТТипДокумент();

            if(tabDoc != null && tabDoc.Rows.Count >0 && tabDoc.Rows[0]["НаименованиеТипаДокумента"] != DBNull.Value)
            {
                RepositoryТипДокумента rDoc = new RepositoryТипДокумента(dc);
                IQueryable<ТТипДокумент> типДок = rDoc.Select(tabDoc.Rows[0]["НаименованиеТипаДокумента"].ToString());

                if (типДок != null)
                {
                    тТипДокумент = типДок.FirstOrDefault();
                }
            }

            return тТипДокумент;
        }
    }
}

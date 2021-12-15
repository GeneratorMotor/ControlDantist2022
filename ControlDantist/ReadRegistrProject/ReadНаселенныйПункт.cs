using System;
using System.Linq;
using System.Data;
using ControlDantist.DataBaseContext;

namespace ControlDantist.ReadRegistrProject
{
    public class ReadНаселенныйПункт : IReadRegistr<ТНаселённыйПункт>
    {
        private DContext dc;
        private DataTable tabSity;

        public ReadНаселенныйПункт(DContext dc, DataTable tabSity)
        {
            this.dc = dc;
            this.tabSity = tabSity;
        }

        public ТНаселённыйПункт Get()
        {
            // Населенный пункт в которм прописан льготник.
            ТНаселённыйПункт населённыйПункт = new ТНаселённыйПункт();

            if (tabSity.Rows.Count > 0 && tabSity.Rows[0]["Наименование"] != DBNull.Value)
            {
                RepositoryНаселенныйПункт repositoryНаселенныйПункт = new RepositoryНаселенныйПункт(dc);
                IQueryable<ТНаселённыйПункт> населённыйПункты = repositoryНаселенныйПункт.Select(tabSity.Rows[0]["Наименование"].ToString());

                if (населённыйПункты != null && населённыйПункты.Count() > 0)
                {
                    населённыйПункт = населённыйПункты.FirstOrDefault();
                }
                else
                {
                    населённыйПункт.Наименование = tabSity.Rows[0]["Наименование"].ToString();
                    repositoryНаселенныйПункт.Insert(населённыйПункт);
                }

            }

            return населённыйПункт;
        }
    }
}

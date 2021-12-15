using System;
using System.Text;
using ControlDantist.Classes;
using System.Data;
using ControlDantist.Querys;

namespace ControlDantist.EditeContractValideTrue
{
    public static class ExistsAct
    {

        public static string Exists(int idContract, int idYear, ref bool flagExistsAct)
        {
            // Построим текстовое пердставление содержимого запроса к таблице актов.
            StringBuilder build = new StringBuilder();

            //if (idYear == 2021)
            //{
                // Получим есть ли у текущего договора акт выполненных работ.
                IQuery query = new QueryCountActs(idContract);

                DataTable tabActs = ТаблицаБД.GetTableSQL(query.Query(), "КоличествоАктов");

                if (tabActs != null && tabActs.Rows != null && tabActs.Rows.Count > 0)
                {
                    // Получим номер акта выполненных работ.
                    string numAct = " № акта - " + tabActs.Rows[0]["НомерАкта"].ToString();

                    // Получим дату акта выполненных работ.
                    string dateAct = " Дата - " + Convert.ToDateTime(tabActs.Rows[0]["ДатаАктаВыполненныхРабот"]).ToShortDateString();

                    build.Append(numAct);
                    build.Append(dateAct);

                    flagExistsAct = true;

                }
                else
                {
                    flagExistsAct = false;
                }

            //}

            return build.ToString();
        }
    }
}

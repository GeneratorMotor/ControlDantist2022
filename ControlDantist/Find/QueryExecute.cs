using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ControlDantist.Classes;
using ControlDantist.ValidPersonContract;

namespace ControlDantist.Find
{
    public static class QueryExecute
    {
        public static List<ValideContract> ExecuteQuery(object obj)
        {
            StringParametr stringParametr = (StringParametr)obj;

            // Список для хранения резултатов поиска.
            List<ValideContract> listDisplay = new List<ValideContract>();

            using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            {
                con.Open();
                //SqlTransaction transact = con.BeginTransaction();

                //DataTable dtContract = ТаблицаБД.GetTableSQL(query, "ТаблицаДоговоров", con, transact);
                SqlCommand com = new SqlCommand(stringParametr.Query, con);
                com.CommandTimeout = 0;
                //com.Transaction = transact;

                SqlDataReader read = com.ExecuteReader();

                IFindContract findContract = new FindContractForNumber(read);

                var list = findContract.Adapter();

                listDisplay.AddRange(list);

            }

            return listDisplay;

        }

    }
}

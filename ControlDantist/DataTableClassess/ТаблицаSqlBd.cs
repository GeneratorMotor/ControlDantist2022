using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using ControlDantist.ErrorHandlid;

namespace ControlDantist.DataTableClassess
{
    public class ТаблицаSqlBd
    {
        /// <summary>
        /// Строка подключения к БД.
        /// </summary>
        private string connectionString = string.Empty;
        private string query = string.Empty;
        private string nameTable = string.Empty;

        public ТаблицаSqlBd(string connectionString, string query, string nameTable)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            this.query = query ?? throw new ArgumentNullException(nameof(query));
            this.nameTable = nameTable ?? throw new ArgumentNullException(nameof(nameTable));
        }

        public DataTable GetTableSQL()
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand com = new SqlCommand(query, con);
                    com.CommandTimeout = 0;

                    //SqlDataAdapter da = new SqlDataAdapter(query, con);
                    SqlDataAdapter da = new SqlDataAdapter(com);

                    da.Fill(ds, nameTable);
                    con.Close();

                    return ds?.Tables[0];
                }
                catch(Exception ex)
                {
                    string paph = @"F:\!\111\ErrorFileESPB.txt";

                    IWriteError writeError = new WriteErrorFileText(paph, ex.Message.Trim());

                    //System.Windows.Forms.MessageBox.Show(ex.Message.Trim());

                }
            }

            return null;
        }
    }
}

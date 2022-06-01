using System;
using System.Data;
using System.Data.SqlClient;


namespace ControlDantist.ClassessForDB
{
    /// <summary>
    /// Получение таблицы с данными из БД.
    /// </summary>
    public class TableBD : IGetDataTableSQL
    {
        private string query = string.Empty;
        private string nameTable = string.Empty;
        private string strConnection = string.Empty;

        public TableBD(string query, string nameTable, string strConnection)
        {
            this.query = query ?? throw new ArgumentNullException(nameof(query));
            this.nameTable = nameTable ?? throw new ArgumentNullException(nameof(nameTable));
            this.strConnection = strConnection ?? throw new ArgumentNullException(nameof(strConnection));
        }

        /// <summary>
        /// Возвращает таблицу с данными получеными из базы данных.
        /// </summary>
        /// <returns></returns>
        public DataTable GetTableSQL()
        {
            //Заполним таблицу данными
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(this.strConnection))
            {
                //try
                //{
                    con.Open();

                    SqlCommand com = new SqlCommand(query, con);
                    com.CommandTimeout = 0;

                    SqlDataAdapter da = new SqlDataAdapter(com);

                    da.Fill(ds, nameTable);
                    con.Close();

                //}
                //catch 
                //{
                //    // Пустая таблица.
                //    DataTable dataTable = new DataTable();

                //    // Запишем пустую таблицу в DataSet.
                //    ds.Tables.Add(dataTable);
                //}
            }
           
            return ds.Tables[nameTable];
        }
    }
}

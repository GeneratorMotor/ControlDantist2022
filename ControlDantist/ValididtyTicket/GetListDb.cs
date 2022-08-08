using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ControlDantist.ValididtyTicket
{
    /// <summary>
    ///  Вспомогательный класс для получения списка BAR_CODE или PC_GUID. за БД.
    /// </summary>
    public class GetListDb 
    {
        // Название таблицы.
        private string query = string.Empty;

        // Строка подключения к БД.
        private string conString = string.Empty;

        public GetListDb(string query, string conString)
        {
            this.query = query;
            this.conString = conString;
        }

        public List<string> GetList()
        {
            List<string> list = new List<string>();

            using (SqlConnection con = new SqlConnection(this.conString))
            {
                con.Open();

                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader read = com.ExecuteReader();

                while(read.Read())
                {
                    string pole = read[0].ToString();

                    list.Add(pole);
                }
            }

            return list;
        }
    }
}

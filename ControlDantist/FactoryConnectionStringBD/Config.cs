using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ControlDantist.FactoryConnectionStringBD
{
    public class Config : IConfigConnectionString
    {
        string connectionString = "Data Source=10.159.102.21;Initial Catalog=ConnectionStringsDirectory; user id = phone; password = phone1234sql";

        public Dictionary<string, string> Select()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string config = "";
            string key = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = @" select district.id, district.[район], ip, [имя БД], [Логин], [пароль] from district
                                                 inner join ConnectionDatabases
                                                 on district.id = ConnectionDatabases.Район
                                                  order by district.id desc";


                SqlCommand command = new SqlCommand(sql, connection);

                //SqlDataAdapter da = new SqlDataAdapter(command);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    key = reader.GetValue(1).ToString();
                    config = @"Data Source=" + reader.GetValue(2).ToString().Trim() + ";Initial Catalog=" + reader.GetValue(3).ToString() + ";User ID=" + reader.GetValue(4).ToString() + ";Password=" + reader.GetValue(5).ToString() + "";
                    dic.Add(key, config);
                }
            }
            return dic;
        }

    }
}

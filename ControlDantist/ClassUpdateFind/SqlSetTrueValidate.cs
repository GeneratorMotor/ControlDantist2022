using ControlDantist.Querys;
using System;

namespace ControlDantist.ClassUpdateFind
{

    /// <summary>
    /// Устанавливает договор как прошедший проверку.
    /// </summary>
    public class SqlSetTrueValidate : IQuery
    {
        private string user = string.Empty;
        private string date = string.Empty;
        private int idContract = 0;

        public SqlSetTrueValidate(string user, string date, int idContract)
        {
            this.user = user ?? throw new ArgumentNullException(nameof(user));
            this.date = date ?? throw new ArgumentNullException(nameof(date));
            this.idContract = idContract;
        }

        public string Query()
        {
            string query = @"update dbo.Договор " +
                           "set ДатаЗаписиДоговора = '" + date + "' " +
                           ",ФлагПроверки = 'True' " +
                           ",logWrite = '" + user.Trim() + "' " +
                           ", ФлагВозвратНаДоработку = 0 " +
                           ", flagАнулирован = 0" +
                           ", ФлагАнулирован = 0 " +
                           ", flag2020 = 0 " +
                           ", flag2019AddWrite = 0 " +
                           "where id_договор = " + idContract + " ";

            return query;
        }
    }
}

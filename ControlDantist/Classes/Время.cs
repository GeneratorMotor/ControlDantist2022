using System;
using System.Collections.Generic;
using System.Text;

namespace ControlDantist.Classes
{
    public class Время
    {
        /// <summary>
        /// Преобразует дату в формат SQL
        /// </summary>
        /// <param name="дата">дата</param>
        /// <returns></returns>
        public static string Дата(string дата)
        {
            //string BeginDateSQL = System.Text.RegularExpressions.Regex.Replace(дата, "\\b(?<day>\\d{1,2}).(?<month>\\d{1,2}).(?<year>\\d{2,4})\\b", "${month}-${day}-${year}");
            string BeginDateSQL = System.Text.RegularExpressions.Regex.Replace(дата, "\\b(?<day>\\d{1,2}).(?<month>\\d{1,2}).(?<year>\\d{2,4})\\b", "${year}${month}${day}");

            return BeginDateSQL;
        }

        /// <summary>
        /// Возвращает дату в текущем часовом поясее.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime LocalDate(DateTime date)
        {

            //var testDate = date;
            ////var unspecified = new DateTime(2016, 12, 12, 10, 10, 10, DateTimeKind.Unspecified);
            ////var specified = DateTime.SpecifyKind(unspecified, DateTimeKind.Utc);

            //var dateTest = DateTimeOffset.Parse(date.ToShortDateString()); 

            //string iTest = "";

            //var testDate2 = date.Date.ToLocalTime();

            return date.ToLocalTime().Date;

            //return date.ToLocalTime().Date;
        }

    }
}

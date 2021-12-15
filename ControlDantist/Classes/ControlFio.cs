using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.Classes
{
    public static class ControlFio
    {
        public static string FioConvert(this string strInput)
        {
            // Результат.
            string strResult = string.Empty;

            // Уберем лишние пробелы из строки.
            string fio = System.Text.RegularExpressions.Regex.Replace(strInput, @"\s+", " ");

            // Разделим строку на массив.
            string[] strArray = fio.Split(' ');

            if(strArray.Length == 4)
            {
                // Надо соединить 3 и 4 элемент массива в единую строку.
                string strКызыОглы = strArray[2].Trim() + strArray[3].Trim();

                strResult = strArray[0] + " " + strArray[1] + " " + strКызыОглы;
            }
            else if(strArray.Length <= 3)
            {
                strResult = strInput;
            }

            return strResult;
        }

        public static string SecondNameАглыКызы(this string strSeconDame)
        {
            string кызыОглы = string.Empty;

            // Уберем лишние пробелы из строки.
            string strКызыОглы = System.Text.RegularExpressions.Regex.Replace(strSeconDame, @"\s+", " ");

            // Разделим строку на массив.
            string[] strArray = strКызыОглы.Split(' ');

            if (strArray.Length == 2)
            {
                // Надо соединить 3 и 4 элемент массива в единую строку.
                string strКОглы = strArray[0].Trim() + strArray[1].Trim();

                кызыОглы = strКОглы;
            }
            else if (strArray.Length <= 3)
            {
                кызыОглы = strSeconDame;
            }

            return кызыОглы;
        }
    }
}

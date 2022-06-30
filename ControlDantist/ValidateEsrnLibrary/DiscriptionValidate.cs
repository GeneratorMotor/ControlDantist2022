using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.ValidateEsrnLibrary
{
    /// <summary>
    /// Вспомогательный класс описания ошибки.
    /// </summary>
    public class DiscriptionValidate
    {
        List<bool> listFlagError; // = new List<bool>();
        List<string> listTextError; // = new List<string>();

        public DiscriptionValidate()
        {
            listFlagError = new List<bool>();
            listTextError = new List<string>();
        }

        /// <summary>
        /// Устанавливает значения после проверки.
        /// </summary>
        /// <param name="flag">Булевое значение ошибки есть False, нет True</param>
        /// <param name="message">Описание ошибки</param>
        public void SetFlag(bool flag,string message)
        {
            listFlagError.Add(flag);

            if (flag == false)
            {
                listTextError.Add(message);
            }
        }

        /// <summary>
        /// Возвращает список флагов с результатами работы.
        /// </summary>
        /// <returns>Список с логическими значениями</returns>
        public List<bool> GetFlag()
        {
            return this.listFlagError;
        }

        /// <summary>
        /// Строку с описанием ошибок
        /// </summary>
        /// <returns>Список текстовых сообщений.</returns>
        public string GetErrorMessage()
        {
            StringBuilder builder = new StringBuilder();

            foreach(string str in listTextError)
            {
                builder.Append(str + " ");
            }

            return builder.ToString().Trim();
        }

    }
}

using ControlDantist.DataBaseContext;
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
        /// <summary>
        /// Флаг указывающий что льготник прошел проверку.
        /// </summary>
        public bool FlagErrorValidate { get; set; }

        // Строка для хранения описания ошибки.
        private StringBuilder descriptionError;

        // Список логических отметок с результатами проверок.
        List<bool> listFlagError;

        // Список логических отметок с результатами проверок по паспорту.
        List<bool> listFlagErrorPassword;

       // private ItemLibrary person;

        public DiscriptionValidate()
        {
            listFlagError = new List<bool>();

            listFlagErrorPassword = new List<bool>();

            descriptionError = new StringBuilder();

           // person = person;
        }

        /// <summary>
        /// Устанавливает значения после проверки.
        /// </summary>
        /// <param name="flag">Булевое значение ошибки есть False, нет True</param>
        /// <param name="message">Описание ошибки</param>
        public void SetFlag(bool flag, string message)
        {
            listFlagError.Add(flag);

            if (flag == false)
            {
                descriptionError.Append(message);
            }
        }

        /// <summary>
        /// Устанавливает значения после проверки паспорта.
        /// </summary>
        /// <param name="flag">Булевое значение ошибки есть False, нет True</param>
        /// <param name="message">Описание ошибки</param>
        public void SetFlagPassword(bool flag, string message)
        {
            listFlagErrorPassword.Add(flag);

            if (flag == false)
            {
                descriptionError.Append(message);
            }
        }

        /// <summary>
        /// Возвращает результтат проверки по Фио и докуменам.
        /// </summary>
        /// <returns>Список с логическими значениями</returns>
        public bool GetFlag()
        {
            int i = 0;

            bool flagResult = false;

            foreach (bool flag in listFlagError)
            {
                if (flag == true)
                {
                    i++;
                }
            }

            if (listFlagError.Count == i)
            {
                flagResult = true;
            }

            return flagResult;
        }

        /// <summary>
        /// Возвращает результат проверки паспорта.
        /// </summary>
        /// <returns></returns>
        public bool GetFlagPassword()
        {
            int i = 0;

            bool flagResult = false;

            foreach (bool flag in listFlagErrorPassword)
            {
                if (flag == true)
                {
                    i++;
                }
            }

            if (listFlagError.Count == i)
            {
                flagResult = true;
            }

            return flagResult;
        }

        /// <summary>
        /// Возвращает описание ошибки.
        /// </summary>
        /// <returns></returns>
        public string DescriptionError()
        {
            return this.descriptionError.ToString();
        }

        /// <summary>
        /// Для теста потом убрать.
        /// </summary>
        /// <returns></returns>
        public int CountListFlagErrorTrue()
        {
            return listFlagError.Count;
        }
    }
}

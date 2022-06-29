using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.ValidateEsrnLibrary
{
    /// <summary>
    /// Класс директор по созданию SQL запрос на поиск льготников в удаленной базе данных.
    /// </summary>
    public class ControlBuilderQueryFindPerson 
    {
        IBuilderTempTable builderQuery;

        /// <summary>
        /// Создание директора SQL запроса.
        /// </summary>
        /// <param name="builderQuery">Фабрика по созданию методов поиска данных на удаленном сервере.</param>
        public ControlBuilderQueryFindPerson(IBuilderTempTable builderQuery)
        {
            this.builderQuery = builderQuery ?? throw new ArgumentNullException(nameof(builderQuery));
        }

        /// <summary>
        /// Формирование запроса по поиску льготников на удаленной базе данных.
        /// </summary>
        /// <returns></returns>
        public string CreateBuilder()
        {
            // Создадим временную таблицу.
            this.builderQuery.CreateTempTable();

            // Заполняем данными временную таблицу.
            this.builderQuery.InsertDateTempTable();

            // Join временной таблицы с таблицами удаленной базы.
            this.builderQuery.FindPerson();

            // Возвращаем sql запрос на поиск льготников.
            return this.builderQuery.GetQuery();
        }
    }
}

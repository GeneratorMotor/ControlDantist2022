using System.Text;
using ControlDantist.ExceptionClassess;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.DataBaseContext;

namespace ControlDantist.ValidateRegistrProject
{
    /// <summary>
    /// Псотроение строки запроса к БД.
    /// </summary>
    public class BuilderQueryValidator : IBuilderQueryValidate
    {
        private IEsrnValidate validate;

        // Переменная для хранения наименования льготной категории.
        private string preferentKategory = string.Empty;

        // Льготная категория.
        public string PreferntCategory { get; set; }

        public BuilderQueryValidator(IEsrnValidate validate)
        {
            if (validate != null)
            {
                this.validate = validate;
            }
        }

        public string Query()
        {
            // Строка для хранения запроса к БД.
            StringBuilder builder = new StringBuilder();

            try
            {
                // Узнаем льготную актегорию.
                //string category = string.Empty;

                // Получим название льготной категории.
                preferentKategory = this.validate.GetPreferentCategory();

                if (preferentKategory == "Категория_не_установлена")
                    throw new ExceptionNoPreferentCategory();

                // Формирование SQl запроса для поиска ВВС.
                builder.Append(AddQuery("Ветеранвоеннойслужбы", " PPR_DOC.A_NAME in ('Удостоверение ветерана военной службы')"));

                // Формирование SQl запроса для поиска ВТ.
                builder.Append(AddQuery("Ветерантруда", " PPR_DOC.A_NAME in ('Удостоверение ветерана труда') "));

                // Формирование SQl запроса для поиска ВТ.
                builder.Append(AddQuery("Тружениктыла", " PPR_DOC.A_NAME in ('Удостоверение о праве на льготы (отметка - ст.20)','Удостоверение ветерана ВОВ (отметка - ст.20)') "));

                // Формирование SQl запроса для поиска ВТСО.
                builder.Append(AddQuery("ВетерантрудаСаратовскойобласти", " PPR_DOC.A_NAME in ('Удостоверение ветерана труда Саратовской области') "));

                // Формирование SQl запроса для поиска Реабелитированных.
                builder.Append(AddQuery("Реабилитированныелица", " PPR_DOC.A_NAME in ('Свидетельство о праве на льготы для реабилитированных лиц','Справка о реабилитации' ) "));
            }
            catch(ExceptionNoPreferentCategory ex)
            {
                builder.Append("Категория_не_установлена");
            }

            return builder.ToString();
        }

        private string AddQuery(string preferencCategory, string nameDoc)
        {
            string query = string.Empty;

            // Проверим льготника по льготной категории.
            if (preferentKategory.ToLower().Trim().Replace(" ", "") == preferencCategory.ToLower().Trim())
            {
                string docPreferencyCategory = nameDoc;

                query = this.validate.FindPersons(docPreferencyCategory);
            }

            return query;
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControlDantist.Classes;
using ControlDantist.DataBaseContext;
using System.Threading;
using ConfigLibrary;
using ControlDantist.FactoryConnectionStringBD;
using ControlDantist.FactoryClass;
using ControlDantist.ValidateEsrnLibrary;
using Config = ControlDantist.FactoryConnectionStringBD.Config;

namespace ControlDantist.ValidateRegistrProject
{
    public class EsrnPersonValidate
    {
        private bool flagError = false;

        private List<ItemLibrary> list;

        private FactoryConnectionStringDB factoryConnection;

        // Фабрика для поиска льготника по ФИО и номеру удостоверания.
        private FactorySqlQueryEsrnValidate factorySqlQuery;

        // Перменнеая для поиска льготника по ФИО и номеру удостоверения.
        private IBuilderTempTable builderTempTable;

        // Фабрика для поиска льготника по ФИО и номеру паспорта.
        private FactorySqlQueryEsrnValidatePassword factoryPasswordSqlQuery;

        // Переменная для поиска льготника по ФИО и номеру пасспорта.
        private IBuilderTempTable builderTempTablePassword;

        static Mutex mutexObj = new Mutex();

        private IConfigConnectionString configConnectionString;

        private CancellationToken token;

        //// Прогресс бар на выполнение процевсса проверки.
        //System.Windows.Forms.ProgressBar progressBar;

        //private Progress<int> progress;

        /// <summary>
        /// Сверка данных из реестра с данными из ЭСРН.
        /// </summary>
        /// <param name="list">Данные из реестра.</param>
        public EsrnPersonValidate(List<ItemLibrary> list, IConfigConnectionString configConnectionString, CancellationToken token)
        {
            if (list != null && list.Count > 0)
            {
                this.list = list;

                this.configConnectionString = configConnectionString;
            }
            else
            {
                throw new System.Exception("Отсутствуют данные для проверки по ЭСРН - EsrnPersonValidate");
            }

            // Создадим экземпляр фабрики подключения строк к БД.
            factoryConnection = new FactoryConnectionStringDB();

            // Фабрика формирования SQL запросв к БД.
            factorySqlQuery = new FactorySqlQueryEsrnValidate();

            factoryPasswordSqlQuery = new FactorySqlQueryEsrnValidatePassword();

            this.token = token;

        }

        public void Validate(IProgress<int> progress)
        {

            // Пройдемся по строкам подключения .
            foreach (KeyValuePair<string, string> dStringConnect in this.configConnectionString.Select())
            {
                // Заблокируем текущий поток.
                mutexObj.WaitOne();

                // Для теста.
                string sKey = string.Empty;
                sKey = dStringConnect.Key.Trim();


                // Выход из проверки если пользователь запросил окончание проверки.
                if(this.token.IsCancellationRequested == true)
                {
                    return;
                }

                // TODO: Отключем проверку договоров.
                // Отключим проверку по ЭСРН.
                //continue;

                //// TODO: Отключим районы.
                //////Оставим участок кода для отработки быстроого подключения
                /////// К нужному району.
                //if (sKey.Trim() != "Балаковский".Trim())
                //{
                //    //Отключим проверку договоров по ЭСРН кроме Ленинского района.
                //    continue;
                //}

                //if (sKey.Trim() != "Советский".Trim())
                //{
                //    Отключим проверку договоров по ЭСРН кроме Ленинского района.
                //    continue;
                //}

                if (sKey.Trim() != "Вольский".Trim())
                {
                    //Отключим проверку договоров по ЭСРН кроме Ленинского района.
                    continue;
                }

                // Переменная хранит строку подключения к БД.
                string sConnection = string.Empty;
                sConnection = dStringConnect.Value.ToString().Trim();

                // Переменная для хранения счетчика продвижения по району.
                int countProgress = 0;

                //Выполним проверку в единой транзакции для конкретного района (строки подключения)
                using (SqlConnection con = new SqlConnection(sConnection))
                {
                    try
                    {
                        // ПОдключемся к БД.
                        con.Open();
                      
                    }
                    catch
                    {
                        // Счетчик прогресса.
                        countProgress++;

                        // Передадим значение счетчик прогресса.
                        progress?.Report(countProgress);

                        System.Windows.Forms.MessageBox.Show("Сервер в районе " + dStringConnect.Key + " в настоящий момент не доступен.");
                        continue;
                    }

                    // Счетчик прогресса.
                    countProgress++;

                    // Передадим значение счетчик прогресса.
                    progress?.Report(countProgress);

                    // Переменная для хранения льготной категории.
                    string cateroryPerson = string.Empty;

                    // Переменная дя хранения строки запроса.
                    string queryФИО = string.Empty;

                    // Воспользуемся Фабрикой с паттерном Builder.
                    builderTempTable = new BuilderQueryFindPerson(factorySqlQuery, "#t2_temp", this.list);

                    // Сформируем SQL запрос к БД на поиск льготника по ФИО и по номеру документа.
                    ControlBuilderQueryFindPerson controlBuilderQueryFindPerson = new ControlBuilderQueryFindPerson(builderTempTable);
                    queryФИО = controlBuilderQueryFindPerson.CreateBuilder();

                    // Объявление начала транзакции.
                    SqlTransaction sqlTransaction = con.BeginTransaction();

                    // Данные по льготникам найденные в ЭСРН по ФИО и документам дающим право на получение льгот.
                    DataTable tabФИО = ТаблицаБД.GetTableSQL(queryФИО, "ФИО", con, sqlTransaction);

                    if (tabФИО != null && tabФИО.Rows != null && tabФИО.Rows.Count > 0)
                    {

                        // Сконвертируем данные из таблицы в список.
                        IConvertor<DatePerson> convertor = new ConvertDTableToList(tabФИО);

                        // Сконвертированные данные из таблицы в список по льготнику и документам льготника полученных из ЭСРН.
                        List<DatePerson> listDate = convertor.ConvertDate();

                        // Проверим есть ли записи в результате выгрузки из ЭСРН.
                        if(listDate.Count > 0)
                        {
                            // Проведем сверку данных по льготникам из реесстра с данными полученными из ЭСРН.
                            IValidateЭсрн validateЭсрн = new ПроверкаЭсрн(listDate, this.list);
                            validateЭсрн.Validate();
                        }
                    }
                }

                mutexObj.ReleaseMutex();
            }
        }

        
    }
}

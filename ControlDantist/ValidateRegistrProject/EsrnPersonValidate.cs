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
        public EsrnPersonValidate(List<ItemLibrary> list)
        {
            if (list != null && list.Count > 0)
            {
                this.list = list;
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
        }

        public void Validate()
        {
            // Получим строки подключения к БД в районах области.
            IConfig pullConnection = factoryConnection.ConnectionStringDB();

            // Пройдемся по строкам подключения.
            foreach (KeyValuePair<string, string> dStringConnect in pullConnection.Select())
            {
                mutexObj.WaitOne();

                // Для теста.
                string sKey = string.Empty;
                sKey = dStringConnect.Key.Trim();

                // TODO: Отключем проверку договоров.
                // Отключим проверку по ЭСРН.
                //continue;

                //// TODO: Отключим районы.
                //////Оставим участок кода для отработки быстроого подключения
                /////// К нужному району.
                if (sKey.Trim() != "Ленинский".Trim())
                {

                    //Отключим проверку договоров по ЭСРН кроме Ленинского района.
                    var sTestTestt = "";
                    continue;
                }
                //else
                //{
                //    var testConnecrt = "Ленинский";
                //}

                // Переменная хранит строку подключения к БД.
                string sConnection = string.Empty;
                sConnection = dStringConnect.Value.ToString().Trim();


                bool isConnected = false;

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
                        System.Windows.Forms.MessageBox.Show("Сервер в районе " + dStringConnect.Key + " в настоящий момент не доступен.");
                        continue;
                    }

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

                    // Получим льготников найденных в ЭСРН по ФИО и документам дающим право на получение льгот.
                    DataTable tabФИО = ТаблицаБД.GetTableSQL(queryФИО, "ФИО", con, sqlTransaction);

                    var test3 = tabФИО;

                    var test4 = "";

                    if (tabФИО != null && tabФИО.Rows != null && tabФИО.Rows.Count > 0)
                    {
                        // Сконвертируем данные из таблицв в список.
                        IConvertor<DatePerson> convertor = new ConvertDTableToList(tabФИО);

                        List<DatePerson> listDate = convertor.ConvertDate();

                        var iTest1 = "";

                        if(listDate.Count > 0)
                        {
                            string iTest2 = "";

                            // Проведем проверку данных по льготникам.
                            IValidateЭсрн validateЭсрн = new ПроверкаЭсрн(listDate, this.list);
                            validateЭсрн.Validate();

                        }

                    }


                    string ads = "";

                    // Пока закоментируем проверку по номеру документа.
                    /*

                    builderTempTablePassword = new BuilderQueryFindPerson(factoryPasswordSqlQuery, "#t3_temp", this.list);

                    // Переменная дя хранения строки запроса.
                    string queryФиоPassword = string.Empty;

                    // Сформируем SQL запрос к БД на поиск льготника по ФИО и по номеру паспорта.
                    ControlBuilderQueryFindPerson controlBuilderQueryFindPersonPassword = new ControlBuilderQueryFindPerson(builderTempTablePassword);
                    queryФиоPassword = controlBuilderQueryFindPersonPassword.CreateBuilder();

                    // Получим льготников найденных в ЭСРН по паспорту.
                    DataTable tabФиоPassword = ТаблицаБД.GetTableSQL(queryФиоPassword, "ФиоPassword", con, sqlTransaction);

                    */

                    // Словарь для хранения id договоров которые прошли проверку.
                    //Dictionary<int, int> idContracts = new Dictionary<int, int>();

                    // Проверка список договоров.
                    var listEsrnPerson = this.list;

                    string iTest = "test";

                    /*

                    // Пометим прошедших проверку.
                    //if (tabФИО?.Rows?.Count > 1 && tabФиоPassword?.Rows?.Count > 1)
                    if (tabФИО?.Rows?.Count >=1 || tabФиоPassword?.Rows?.Count >= 1)
                    { 
                        // Пометим прошедших проверку.
                        CompareRegistr compareRegistr = new CompareRegistr(this.list);
                        compareRegistr.Compare(tabФИО, tabФиоPassword);
                    }

                    */
                    
                }

                mutexObj.ReleaseMutex();

            }
        }

        
    }
}

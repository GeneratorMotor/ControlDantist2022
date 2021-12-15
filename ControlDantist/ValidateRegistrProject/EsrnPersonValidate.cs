using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControlDantist.Classes;
using ControlDantist.DataBaseContext;
using System.Threading;
using ConfigLibrary;

namespace ControlDantist.ValidateRegistrProject
{
    public class EsrnPersonValidate
    {
        private bool flagError = false;

        private List<ItemLibrary> list;

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
        }

        public void Validate()
        {
            // Строки подключения к БД АИС ЭСРН.
            ConfigLibrary.Config config = new ConfigLibrary.Config();

            //Пока закоментирован.
            //Получаем словарь со строками подключения к АИС ЭСРН.
            Dictionary<string, string> pullConnect = config.Select();

            // Пройдемся по строкам подключения.
            foreach (KeyValuePair<string, string> dStringConnect in pullConnect)
            {
                mutexObj.WaitOne();

                // Для теста.
                string sKey = string.Empty;
                sKey = dStringConnect.Key.Trim();

                // Отключим проверку по ЭСРН.
                continue;

                //// TODO: Отключим районы.
                //////Оставим участок кода для отработки быстроого подключения
                /////// К нужному району.
                //if (sKey.Trim() != "Ленинский".Trim())
                //{

                //    //Отключим проверку договоров по ЭСРН кроме Ленинского района.
                //    var sTestTestt = "";
                //    continue;
                //}
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

                    // Проверим льготника по ФИО и номеру документа.
                    IEsrnValidate validatePrefCategoryList2 = new ValidatePrefCategoryList2(this.list);


                    IBuilderQueryValidate queryFindPersonToFioNumDoc = new BuilderQueryValidator(validatePrefCategoryList2);

                    // Переменная дя хранения строки запроса.
                    string queryФИО = string.Empty;

                    // Строка SQL Запроса.
                    queryФИО = queryFindPersonToFioNumDoc.Query();

                    SqlTransaction sqlTransaction = con.BeginTransaction();

                    // Получим льготников найденных в ЭСРН по документам дающим право на получение льгот.
                    DataTable tabФИО = ТаблицаБД.GetTableSQL(queryФИО, "ФИО", con, sqlTransaction);

                    // Поиск по ФИО и паспорту.
                    IEsrnValidate validatePersonPassword = new ValidatePersonFioPassword(this.list);

                    // Поиск по ФИО и номеру документа.
                    IBuilderQueryValidate queryFindPersonToPassword = new BuilderQueryValidator(validatePersonPassword);

                    // Переменная дя хранения строки запроса.
                    string queryФиоPassword = string.Empty;

                    // Строка SQL Запроса.
                    queryФиоPassword = queryFindPersonToPassword.Query();

                    // Получим льготников найденных в ЭСРН по документам дающим право на получение льгот.
                    DataTable tabФиоPassword = ТаблицаБД.GetTableSQL(queryФиоPassword, "ФиоPassword", con, sqlTransaction);

                    // Словарь для хранения id договоров которые прошли проверку.
                    Dictionary<int, int> idContracts = new Dictionary<int, int>();

                    // Проверка список договоров.
                    var listEsrnPerson = this.list;

                    // Пометим прошедших проверку.
                    //if (tabФИО?.Rows?.Count > 1 && tabФиоPassword?.Rows?.Count > 1)
                    if (tabФИО?.Rows?.Count >=1 || tabФиоPassword?.Rows?.Count >= 1)
                    { 
                        // Пометим прошедших проверку.
                        CompareRegistr compareRegistr = new CompareRegistr(this.list);
                        compareRegistr.Compare(tabФИО, tabФиоPassword);
                    }

                    //if(tabФиоPassword?.Rows?.Count > 1)
                    //{
                    //    // Пометим прошедших проверку.
                    //    CompareRegistr compareRegistr = new CompareRegistr(this.list);
                    //    compareRegistr.Compare(tabФиоPassword);
                    //}

                    // Генерируем запрос на поиск льготников в ЭСРН по Фио и номеру паспората.
                    //validatePrefCategoryList2.FindPersonsFioDoc()
                }

                mutexObj.ReleaseMutex();

            }
        }

        
    }
}

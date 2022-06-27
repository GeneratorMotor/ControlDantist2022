using ControlDantist.Querys;

namespace ControlDantist.PatternSql
{
    /// <summary>
    /// Фабрика запросов к БД на поиск договоров для текущего льготника.
    /// </summary>
    public class FactoryQuery
    {
        /// <summary>
        /// SQL запрос на поиск договоров по ФИО и дате рождения льготника.
        /// </summary>
        /// <param name="firstName">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="surName">Отчество</param>
        /// <param name="dr">Дата рождения</param>
        /// <param name="numContract">текущий номер договора</param>
        /// <returns>Класс инкапсулирующий SQL запрос к БД</returns>
        public IQuery QueryFindContractFio(string firstName, string name, string surName, string dr, string numContract, int year, string dateLocalTime)
        {
            return new QueryFindContractFio(firstName, name, surName, dr, numContract, year, dateLocalTime);
        }

        public IQuery QueryFindContractFio21(string firstName, string name, string surName, string dr, string numContract, int year)
        {
            return new QueryFindContractFio21(firstName, name, surName, dr, numContract, year);
        }

        /// <summary>
        /// SQL запрос на поиск договоров по Фамилии и имени и дате рождения льготника.
        /// </summary>
        /// <param name="firstName">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="dr">Дата рождения</param>
        /// <param name="numContract">текущий номер договора</param>
        /// <returns>Класс инкапсулирующий SQL запрос к БД</returns>
        public IQuery QueryFindContractFi(string firstName, string name, string dr, string numContract, int year)
        {
            return new QueryFindContractFn(firstName, name, dr, numContract, year);
        }

        public IQuery QueryFindContractFi21(string firstName, string name, string dr, string numContract, int year)
        {
            return new QueryFindContractFn21(firstName, name, dr, numContract, year);
        }

        /// <summary>
        /// SQL запрос на поиск рнанее заключенных договоров по ФИО льготника
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="name"></param>
        /// <param name="surName"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public IQuery QueryFindContractFioNotDr(string firstName, string name, string surName, int year)
        {
            return new QueryFindFiNotDr(firstName, name, year);
        }

        public IQuery QueryFindFiNotDr(string firstName, string name, int year)
        {
            return new QueryFindFiNotDr(firstName, name, year);
        }

        /// <summary>
        /// Поиск ранее заключенных договоров по Фамилии и имени.
        /// </summary>
        /// <param name="firstName">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="year">Год меньше которого номера договров не показываються</param>
        /// <returns></returns>
        public IQuery QueryFindFiNotDr21(string firstName, string name, int year)
        {
            return new QueryFindFiNotDr21(firstName, name, year);
        }

        /// <summary>
        /// Поиск ранее заключенных договоров по Фамилии и имени.
        /// </summary>
        /// <param name="firstName">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="year">Год меньше которого номера договров не показываються</param>
        /// <returns></returns>
        public IQuery QueryFindFioNotDr21(string firstName, string name, string surName, int year)
        {
            return new QueryFindFioNotDr21(firstName, name, surName, year);
        }


    }
}

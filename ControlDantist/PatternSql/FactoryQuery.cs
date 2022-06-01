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
        public IQuery QueryFindContractFio(string firstName, string name, string surName, string dr, string numContract, int year)
        {
            return new QueryFindContractFio(firstName, name, surName, dr, numContract, year);
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
    }
}

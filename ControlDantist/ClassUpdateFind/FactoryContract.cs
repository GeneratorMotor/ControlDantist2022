using ControlDantist.Querys;
using ControlDantist.ClassUpdateFind;

namespace ControlDantist.ClassUpdateFind
{
    public class FactoryContract
    {
        /// <summary>
        /// SQL скрипт перевод договора в состояние не прошедшего проверку.
        /// </summary>
        /// <param name="user">Пользователь который перевел договор в другой статус</param>
        /// <param name="date">Дата операции</param>
        /// <param name="idContract">id договора</param>
        /// <returns>Класс наследующий IQUery</returns>
        public IQuery SetFalsetContract(string user,string date,int idContract)
        {
            return new SqlSetFalseValidate(user, date, idContract);
        }

        /// <summary>
        /// SQL скрипт перевод договора в состояние прошедшего проверку.
        /// </summary>
        /// <param name="user">Пользователь который перевел договор в другой статус</param>
        /// <param name="date">Дата операции</param>
        /// <param name="idContract">id договора</param>
        /// <returns>Класс наследующий IQUery</returns>
        public IQuery SetTrueContract(string user, string date, int idContract)
        {
            return new SqlSetTrueValidate(user, date, idContract);
        }

        /// <summary>
        /// SQL скрипт анулирования договора в состояние прошедшего проверку.
        /// </summary>
        /// <param name="user">Пользователь который перевел договор в другой статус</param>
        /// <param name="date">Дата операции</param>
        /// <param name="idContract">id договора</param>
        /// <returns>Класс наследующий IQUery</returns>
        public IQuery SetCancelContract(string user, string date, int idContract)
        {
            return new CancelContract(user, date, idContract);
        }

        /// <summary>
        /// SQL скрипт анулирования договора в состояние прошедшего проверку.
        /// </summary>
        /// <param name="user">Пользователь который перевел договор в другой статус</param>
        /// <param name="date">Дата операции</param>
        /// <param name="idContract">id договора</param>
        /// <returns>Класс наследующий IQUery</returns>
        public IQuery SetdeleteAct(string user, string date, int idContract)
        {
            return new DeleteAct(user, date, idContract);
        }
    }
}

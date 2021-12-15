using System;
using ControlDantist.DataBaseContext;
using ControlDantist.Classes;
using System.Data;

namespace ControlDantist.WriteDB
{
    /// <summary>
    /// Проверяет есть ли у льготника еще договорпрошедший проверку но не закрытый актом выполненнных работ
    /// </summary>
    public class ValidContractForPerson : IValidBD<ТДоговор>
    {
        private ТЛЬготник тЛЬготник;
        private DataTable tabControl;

        public ValidContractForPerson(ТЛЬготник тЛЬготник)
        {
            this.тЛЬготник = тЛЬготник ?? throw new ArgumentNullException(nameof(тЛЬготник));
        }


    /// <summary>
    /// Возвращает номер договра который прошёл проверку но у него нет акта выполненных работ.
    /// </summary>
    /// <returns></returns>
    public virtual ТДоговор Get()
    {
        ТДоговор договор = new ТДоговор();

        if (tabControl != null)
        {
            // Запишем номер договора.
            договор.НомерДоговора = tabControl.Rows[0]["НомерДоговора"].ToString().Trim();

            // Запишем значение флага наличия акта выполненных работ.
            договор.ФлагНаличияАкта = Convert.ToBoolean(tabControl.Rows[0]["ФлагНаличияАкта"]);
        }

        return договор;
    }

    /// <summary>
    /// Имеет ли договор прошедший проверку акт выполненных работ.
    /// </summary>
    /// <returns>true - акт выполненных работ имеется</returns>
    public virtual bool Validate()
    {
        // Переменная хранит флаг указывающий что акта у договора прошедшего проверку нет. 
        bool flagAct = false;

            // Получим строку запроса к БД.
            ControlDantist.Querys.IQuery query = new ControlDantist.Querys.FindActForContract(тЛЬготник);

            string querySQL = query.Query();

        // Получим таблицу содержащую сведения о договоре и акте выполненных работ.
            DataTable tabValid = ТаблицаБД.GetTableSQL(query.Query(), "ТаблицаПоискАкта", ConnectDB.ConnectionString());

        // Проверим есть ли таблица в результате выборки и есть ли у ней данные.
        if (tabValid != null && tabValid.Rows != null && tabValid.Rows.Count > 0)
        {
            DataRow row = tabValid.Rows[0];

            tabControl = tabValid;

            if (Convert.ToBoolean(row["ФлагНаличияАкта"]) == false)
            {
                // Если у договора прошедшего проверку нет акта.
                flagAct = true;
            }
        }

        return flagAct;

    }

}
}
    


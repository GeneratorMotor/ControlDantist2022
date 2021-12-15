using System;
using ControlDantist.DataBaseContext;
using ControlDantist.Classes;
using System.Data;

namespace ControlDantist.WriteDB
{
    public class ValidContractDateAct : ValidContractForPerson //, IValidBD<ТДоговор>
    {
        private ТЛЬготник тЛЬготник;
        private DataTable tabControl;

        public ValidContractDateAct(ТЛЬготник тЛЬготник) : base(тЛЬготник)
        {
            this.тЛЬготник = тЛЬготник ?? throw new ArgumentNullException(nameof(тЛЬготник));
        }

        /// <summary>
        /// Возвращает номер договра который прошёл проверку но у него нет акта выполненных работ.
        /// </summary>
        /// <returns></returns>
        public override ТДоговор Get()
        {
            ТДоговор договор = new ТДоговор();

            if (tabControl != null)
            {
                // Запишем номер договора.
                договор.НомерДоговора = tabControl.Rows[0]["НомерДоговора"].ToString().Trim();

                // Запишем значение флага наличия акта выполненных работ.
                договор.ФлагНаличияАкта = Convert.ToBoolean(tabControl.Rows[0]["ФлагНаличияАкта"]);

                if (tabControl.Rows != null && tabControl.Rows.Count > 0)
                {
                    // Запишем дату акта выполненных работ.
                    договор.ДатаАктаВыполненныхРабот = Convert.ToDateTime(tabControl.Rows[0]["ДатаАктаВыполненныхРабот"]);
                }
            }

            return договор;
        }

        /// <summary>
        /// Имеет ли договор прошедший проверку акт выполненных работ.
        /// </summary>
        /// <returns>true - акт выполненных работ не прошло больше 2-х лет</returns>
        public override bool Validate()
        {
            // Переменная хранит флаг указывающий что акта у договора прошедшего проверку нет. 
            bool flagAct = false;

            // Получим строку запроса к БД.
            ControlDantist.Querys.IQuery query = new ControlDantist.Querys.FindActForCnotractDate(тЛЬготник);

            string querySQL = query.Query();

            // Получим таблицу содержащую сведения о договоре и акте выполненных работ.
            DataTable tabValid = ТаблицаБД.GetTableSQL(query.Query(), "ТаблицаПоискАкта", ConnectDB.ConnectionString());

            // Проверим есть ли таблица в результате выборки и есть ли у ней данные.
            if (tabValid != null && tabValid.Rows != null && tabValid.Rows.Count > 0)
            {
                DataRow row = tabValid.Rows[0];

                // Присвоим локальной типа таблицы содержимое из запроса к БД.
                tabControl = tabValid;

                // Получим дату акта выполненных работ.
                DateTime dtAct = Convert.ToDateTime(row["ДатаАктаВыполненныхРабот"]);

                // Получим текущую дату.
                DateTime dateCurrent = DateTime.Now;

                // Проверим прошло 2- годда с момента подписания предыдущего акта.
                if (Years(dtAct, dateCurrent) <= 2)
                {
                    // Мы не можем записать договор, так как у него есть акт выполненных работ
                    // меньше 2-х лет.
                    flagAct = true;
                }
            }

            return flagAct;

        }

        /// <summary>
        /// Вычисляем разницу в годах.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private int Years(DateTime start, DateTime end)
        {
           return (end.Year - start.Year - 1) +
                 (((end.Month > start.Month) ||
                 ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
        }
    }
}

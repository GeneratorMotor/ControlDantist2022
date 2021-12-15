using ControlDantist.DataBaseContext;
using System.Linq;
using ControlDantist.Querys;
using ControlDantist.Classes;

namespace ControlDantist.WriteDB
{
    public class FindActForContract : IValidBD<ТАктВыполненныхРабот>
    {
        private ТАктВыполненныхРабот актВыполненныхРабот;
        private DContext dc;

        // Переменная для хранения номера договора.
        private string numContract = string.Empty;

        //public FindActForContract(DContext dc,ТАктВыполненныхРабот актВыполненныхРабот, string numContract)
        public FindActForContract(DContext dc, string numContract)
        {
            //this.актВыполненныхРабот = актВыполненныхРабот;
            this.dc = dc;
            this.numContract = numContract;
        }

        public ТАктВыполненныхРабот Get()
        {
            return this.актВыполненныхРабот;
        }

        public bool Validate()
        {
            // Флаг указывающий что акт выполненных работ для текущего договора существует.
            bool flagValidate = false;

            // Строка запроса к БД для поиска акта выполненных работ.
            IQuery query = new FindPaysContract(this.numContract.Trim());

            // Получим таблицу с результатами поиска.
            System.Data.DataTable dataTableAct = ТаблицаБД.GetTableSQL(query.Query(), "НомераАкта");

            //var acts = from c in dc.ТДоговор.Where(w => w.НомерДоговора.Trim() == this.numContract.Trim())
            //           join a in dc.ТАктВыполненныхРабот on c.id_договор equals a.id_договор
            //           select new { idAct = a.id_акт, idContract = a.id_договор, NumAct = a.НомерАкта };

            //if (acts.Count() > 0)

            if(dataTableAct != null && dataTableAct.Rows != null && dataTableAct.Rows.Count > 0 )
            {
                flagValidate = true;

                // Создадим экземпляр актк выполненных работ.
                this.актВыполненныхРабот = new ТАктВыполненныхРабот();

                // Получим последний акт.
                //var act = acts.OrderByDescending(w => w.idAct).FirstOrDefault();

                // Присвоим номер акта.
                //this.актВыполненныхРабот.НомерАкта = act.NumAct;
                this.актВыполненныхРабот.НомерАкта = dataTableAct.Rows[0]["НомерАкта"].ToString();
            }

            return flagValidate;
        }
    }
}

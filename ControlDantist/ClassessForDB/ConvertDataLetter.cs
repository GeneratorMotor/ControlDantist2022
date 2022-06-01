using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ControlDantist.ClassessForDB
{
    public class ConvertDataLetter : IConvertDataLetter
    {
        private DataPerson dataPerson;

        public ConvertDataLetter(DataPerson dataPerson)
        {
            this.dataPerson = dataPerson ?? throw new ArgumentNullException(nameof(dataPerson));
        }

        public void ConvertDate(DataTable table)
        {
            List<DataPersonContract> listContracts = new List<DataPersonContract>();

            if (table != null && table.Rows != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    DataPersonContract dpc = new DataPersonContract();
                    dpc.NumContract = row["НомерДоговора"].ToString().Trim();
                    dpc.DateContract = Convert.ToDateTime(row["ДатаДоговора"]).ToShortDateString();
                    dpc.NumAct = row["НомерАкта"]?.ToString().Trim();
                    dpc.DateAct = Convert.ToDateTime(row["ДатаАктаВыполненныхРабот"]).ToShortDateString();
                    if(!DBNull.Value.Equals(row["flagАнулирован"]))
                    {
                        dpc.FlagAnulirovan = Convert.ToBoolean(row["flagАнулирован"]);
                    }
                    else
                    {
                        dpc.FlagAnulirovan = false;
                    }
                    
                    if(!DBNull.Value.Equals(row["ФлагАнулирован"]))
                    {
                        dpc.ФлагАнулирован = Convert.ToBoolean(row["ФлагАнулирован"]);
                    }
                    
                    dpc.ФлагПроверки = Convert.ToBoolean(row["ФлагПроверки"]);
                    dpc.ФлагНаличияАкта = Convert.ToBoolean(row["ФлагНаличияАкта"]);
                    listContracts.Add(dpc);
                }
            }

            this.dataPerson.DataContracts = listContracts;
        }
    }
}

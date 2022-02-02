using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.Querys;

namespace ControlDantist.ClassessLimitYear
{

    public class InsertLimit : IQuery
    {
        // Годовой лимит.
        private string money = string.Empty;
        private int year = 0;

        public InsertLimit(string money, int Year)
        {
            this.money = money;
            this.year = Year;
        }

        public string Query()
        {
            return @"INSERT INTO [LimitMoneyYear]
                   ([Year]
                   ,[LimitMoneyYear])
                    VALUES " +
                   " ("+ this.year +" " +
                   " ,CAST("+ this.money +" AS money) ) ";
        }
    }
}

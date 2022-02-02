using ControlDantist.Querys;

namespace ControlDantist.ClassessLimitYear
{
    public class SqlGetLimit : IQuery
    {
        private int year = 0;

        public SqlGetLimit(int year)
        {
            this.year = year;
        }

        public string Query()
        {
            return @" SELECT LimitMoneyYear
                  FROM[LimitMoneyYear] where Year = " + this.year + "  ";
        }
    }
}

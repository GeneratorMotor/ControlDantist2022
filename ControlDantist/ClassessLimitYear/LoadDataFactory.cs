using System;
using System.Data;

namespace ControlDantist.ClassessLimitYear
{
    /// <summary>
    /// Фабрика для списков.
    /// </summary>
    public class LoadDataFactory : ILoadDateFactory<ItemYear, ItemLimit>
    {
        private DataTable dateTableYear;
        private DataTable dateTableLimit;

        public LoadDataFactory(DataTable dateTableYear, DataTable dateTableLimit)
        {
            this.dateTableYear = dateTableYear ?? throw new ArgumentNullException(nameof(dateTableYear));
            this.dateTableLimit = dateTableLimit ?? throw new ArgumentNullException(nameof(dateTableLimit));
        }

        public ILoadData<ItemLimit> GetLimit()
        {
            return new LoadDataLimit(this.dateTableLimit);
        }

        public ILoadData<ItemYear> GetYears()
        {
            return new LoadDataYear(this.dateTableYear);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace ControlDantist.ReadRegistrProject
{
    public static class DataRowExtensions
    {
        public static T FieldOfDefault<T>(this DataRow row, string columnName)
        {
            return row.IsNull(columnName) ? default(T) : row.Field<T>(columnName);
        }
    }
}

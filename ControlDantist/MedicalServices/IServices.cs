using System;
using System.Collections.Generic;
using ControlDantist.DataBaseContext;
using System.Text;

namespace ControlDantist.MedicalServices
{
    public interface IServices<T>
        where T : class
    {
        /// <summary>
        /// Список услуг.
        /// </summary>
        /// <returns></returns>
        List<T> ServicesMedical();

        // Идентификатор для класса предоставляющего список услуг.
        void SetIdentificator(int id);
    }
}

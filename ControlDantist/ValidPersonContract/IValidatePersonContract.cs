using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.ValidPersonContract
{
    /// <summary>
    /// Проверка наличия договоров у льготника.
    /// </summary>
    public interface IValidatePersonContract
    {
        string Execute();

        //string GetQuery();
    }
}

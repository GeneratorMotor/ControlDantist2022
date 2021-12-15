using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.DataBaseContext
{
    /// <summary>
    /// Вспомогательный класс данные связанные с льготником и договором из проекта договра.
    /// </summary>
    public class PackageClass
    {
        /// <summary>
        /// Населенный пункт.
        /// </summary>
        public ТНаселённыйПункт населённыйПункт { get; set; }

        /// <summary>
        /// Льготная категория.
        /// </summary>
        public ТЛьготнаяКатегория тЛьготнаяКатегория { get; set; }

        /// <summary>
        /// Льготник.
        /// </summary>
        public ТЛЬготник льготник { get; set; }

        /// <summary>
        /// Поликлинника.
        /// </summary>
        public ТПоликлинника hosp { get; set; }

        /// <summary>
        /// Договор.
        /// </summary>
        public ТДоговор тДоговор { get; set; }

        /// <summary>
        /// Услуги по договору.
        /// </summary>
        public List<ТУслугиПоДоговору> listUSlug { get; set; }

        /// <summary>
        /// ТЬип документа.
        /// </summary>
        public ТТипДокумент ТипДокумента { get; set; }

        /// <summary>
        /// Наименование района области.
        /// </summary>
        public string НаименованиеРайонОбласти { get; set; }

    }
}

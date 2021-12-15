using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlDantist.DataBaseContext
{
    /// <summary>
    /// Сущьность описывающая тип документа.
    /// </summary>
    [Table("ТипДокумента")]
    public class ТТипДокумент
    {
        [Key]
        public int id_документ { get; set; }

        [MaxLength(255)]
        public string НаименованиеТипаДокумента { get; set; }
    }
}

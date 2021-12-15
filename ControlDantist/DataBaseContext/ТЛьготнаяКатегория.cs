using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlDantist.DataBaseContext
{
    [Table("ЛьготнаяКатегория")]
    public class ТЛьготнаяКатегория
    {
        [Key]
        public int id_льготнойКатегории { get; set; }

        [MaxLength(50)]
        public string ЛьготнаяКатегория { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlDantist.DataBaseContext;

namespace ControlDantist.DataBaseContext
{
    [Table("УслугиПоДоговору")]
    public class ТУслугиПоДоговору
    {
        [Key]
        public int id_услугиДоговор { get; set; }

        [MaxLength(500)]
        public string НаименованиеУслуги { get; set; }

        [DataType("money")]
        public decimal? цена { get; set; }

        public int? Количество { get; set; }

        public int? id_договор { get; set; }

        [ForeignKey("id_договор")]
        public ТДоговор ТДоговор { get; set; }

        [MaxLength(50)]
        public string НомерПоПеречню { get; set; }

        [DataType("money")]
        public decimal? Сумма { get; set; }

        public int? ТехЛист { get; set; }
    }
}

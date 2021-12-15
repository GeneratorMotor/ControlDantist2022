using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlDantist.DataBaseContext
{
    [Table("ВидУслуг")]
    public class ТВидУслуг
    {
        [Key]
        public int id_услуги { get; set; }

        public string ВидУслуги { get; set; }

        [DataType("money")]
        public decimal Цена { get; set; }

        public int id_поликлинника { get; set; }

        [ForeignKey("id_поликлинника")]
        public ТПоликлинника ТПоликлинника { get; set; }

        [MaxLength(100)]
        public string НомерПоПеречню { get; set; }

        [DataType("bit")]
        public bool Выбрать { get; set; }

        public int id_кодУслуги { get; set; }

        public int ТехЛист { get; set; }

        public int id_время { get; set; }

        public int id_постановление { get; set; }

    }
}

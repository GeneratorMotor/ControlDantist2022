using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlDantist.DataBaseContext
{
    [Table("КлассификаторУслуг")]
    public class КлассификаторУслуг
    {
        [Key]
        public int id_кодУслуги { get; set; }

        [MaxLength(255)]
        public string КлассификаторУслуги { get; set; }

        public int id_поликлинника { get; set; }

        [ForeignKey("id_поликлинника")]
        public ТПоликлинника ТПоликлинника { get; set; }

        public int? id_постановление { get; set; }

    }
}

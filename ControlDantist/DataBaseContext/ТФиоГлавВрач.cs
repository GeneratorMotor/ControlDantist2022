using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlDantist.DataBaseContext
{
    [Table("ФиоГлавВрач")]
    public class ТФиоГлавВрач
    {
        [Key]
        public int id_главВрач { get; set; }

        [MaxLength(150)]
        public string ФИО_ГлавВрач { get; set; }

        [MaxLength(20)]
        public string ИНН_поликлинники { get; set; }

    }
}

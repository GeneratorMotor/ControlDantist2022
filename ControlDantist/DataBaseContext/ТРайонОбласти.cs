using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlDantist.DataBaseContext
{
    [Table("РайонОбласти")]
    public class ТРайонОбласти
    {
        [Key]
        public int idRegion { get; set; }

        [MaxLength(80)]
        public string NameRegion { get; set; }
    }
}

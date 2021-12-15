using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlDantist.DataBaseContext
{
    [Table("НаселённыйПункт")]
    public class ТНаселённыйПункт
    {
        [Key]
        public int id_насПункт { get; set; }
        
        public string Наименование { get; set; }
    }
}

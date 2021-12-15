using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlDantist.DataBaseContext
{
    [Table("ГлавБух")]
    public class ТГлавБух
    {
        [Key]
        public int id_главБух { get; set; }

        [MaxLength(250)]
        public string ФИО_ГлавБух { get; set; }

        [MaxLength(250)]
        public string ФИО_ГлавБухПадеж { get; set; }

        [MaxLength(50)]
        public string Должность { get; set; }

        [MaxLength(50)]
        public string ДолжностьРодПадеж { get; set; }

        [MaxLength(50)]
        public string Основание { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace ControlDantist.DataBaseContext
{
    [Table("АктВыполненныхРабот")]
    public class ТАктВыполненныхРабот
    {
        [Key]
        public int id_акт { get; set; }

        [MaxLength(100)]
        public string НомерАкта { get; set; }

        public int? id_договор { get; set; }

        [ForeignKey("id_договор")]
        public ТДоговор ТДоговор { get; set; }

        public bool? ФлагПодписания { get; set; }

        [Column(TypeName ="Date")]
        public DateTime? ДатаПодписания { get; set; }

        [MaxLength(30)]
        public string НомерПоПеречню { get; set; }

        [MaxLength(300)]
        public string НаименованиеУслуги { get; set; }

        [Column(TypeName ="Money")]
        public decimal? Цена { get; set; }

        public int? Количество { get; set; }

        [Column(TypeName ="Money")]
        public decimal? Сумма { get; set; }

        [MaxLength(5)]
        public string ФлагДопСоглашение { get; set; }

        [MaxLength(10)]
        public string НомерРеестра { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? ДатаРеестра { get; set; }

        [MaxLength(10)]
        public string НомерСчётФактуры { get; set; }

        [Column(TypeName ="Date")]
        public DateTime? ДатаСчётФактуры { get; set; }

        // Дата зачем то указана в виде сроки в БД.
        [MaxLength(10)]
        public string ДатаОплаты { get; set; }

        [MaxLength(150)]
        public string logWrite { get; set; }

        [MaxLength(12)]
        public string logDate { get; set; }

        public bool? flagDIsplay { get; set; }

    }
}

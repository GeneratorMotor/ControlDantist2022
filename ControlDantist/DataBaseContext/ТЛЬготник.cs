using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace ControlDantist.DataBaseContext
{
    [Table("ЛЬготник")]
    public class ТЛЬготник
    {
        [Key]
        public int id_льготник { get; set; }

        [MaxLength(150)]
        public string Фамилия { get; set; }

        [MaxLength(150)]
        public string Имя { get; set; }

        [MaxLength(150)]
        public string Отчество { get; set; }

        public DateTime ДатаРождения { get; set; }

        [MaxLength(200)]
        public string улица { get; set; }

        [MaxLength(10)]
        public string НомерДома { get; set; }

        [MaxLength(10)]
        public string корпус { get; set; }

        [MaxLength(40)]
        public string НомерКвартиры { get; set; }

        [MaxLength(10)]
        public string СерияПаспорта { get; set; }

        [MaxLength(10)]
        public string НомерПаспорта { get; set; }

        public DateTime ДатаВыдачиПаспорта { get; set; }

        [MaxLength(500)]
        public string КемВыданПаспорт { get; set; }

        public int id_льготнойКатегории { get; set; }

        [ForeignKey("id_льготнойКатегории")]
        public ТЛьготнаяКатегория ТЛьготнаяКатегория { get; set; }
        public int id_документ { get; set; }

        [ForeignKey("id_документ")]
        public ТТипДокумент ТТипДокумента { get; set; }

        [MaxLength(50)]
        public string СерияДокумента { get; set; }

        [MaxLength(50)]
        public string НомерДокумента { get; set; }

        public DateTime ДатаВыдачиДокумента { get; set; }

        [MaxLength(500)]
        public string КемВыданДокумент { get; set; }

        public int id_область { get; set; }

        public int id_район { get; set; }

        [ForeignKey("id_район")]
        public ТРайонОбласти ТРайонОбласти { get; set; }
        
        public int id_насПункт { get; set; }

        [ForeignKey("id_насПункт")]
        public ТНаселённыйПункт ТНаселенныйПункт { get; set; }

        [MaxLength(11)]
        public string Снилс { get; set; }

    }
}

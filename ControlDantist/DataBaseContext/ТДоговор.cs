using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ControlDantist.DataBaseContext
{
    [Table("Договор")]
    public class ТДоговор
    {
        [Key]
        public int id_договор { get; set; }

        [MaxLength(100)]
        public string НомерДоговора { get; set; }

        public DateTime ДатаДоговора { get; set; }

        public DateTime ДатаАктаВыполненныхРабот { get; set; }

        [Column(TypeName ="Money")]
        public decimal СуммаАктаВыполненныхРабот { get; set; }

        public int id_льготнаяКатегория { get; set; }

        [ForeignKey("id_льготнаяКатегория")]
        public ТЛьготнаяКатегория ТЛьготнаяКатегория { get; set; }

        public int id_поликлинника { get; set; }

        [ForeignKey("id_поликлинника")]
        public ТПоликлинника ТПоликлинника { get; set; }

        [MaxLength(300)]
        public string Примечание { get; set; }

        public int id_комитет { get; set; }

        public bool ФлагНаличияДоговора { get; set; }

        public bool ФлагНаличияАкта { get; set; }

        public int id_льготник { get; set; }

        [ForeignKey("id_льготник")]
        public ТЛЬготник ТЛЬготник { get; set; }

        [MaxLength(50)]
        public string ФлагДопСоглашения { get; set; }

        public bool? флагСРН { get; set; }

        public bool? флагУслуги { get; set; }

        public DateTime? датаВозврата { get; set; }
        public DateTime? ДатаЗаписиДоговора { get; set; }
        public bool ФлагПроверки { get; set; }

        [MaxLength(30)]
        public string НомерРеестра { get; set; }

        public DateTime? ДатаРеестра { get; set; }

        [MaxLength(30)]
        public string НомерСчётФактрура { get; set; }

        public DateTime? ДатаСчётФактура { get; set; }

        public bool flagАнулирован { get; set; }

        [MaxLength(150)]
        public string logWrite { get; set; }

        public bool flagОжиданиеПроверки { get; set; }

        public int? idFileRegistProgect { get; set; }

        public bool ФлагАнулирован { get; set; }

        public bool ФлагВозвратНаДоработку { get; set; }

        public DateTime? ДатаПроверки { get; set; }

        public bool flag2020 { get; set; }

        public bool flag2019AddWrite { get; set; }

    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ControlDantist.DataBaseContext
{
    [Table("Поликлинника")]
    public class ТПоликлинника
    {
        [Key]
        public int id_поликлинника { get; set; }
        [MaxLength(300)]
        public string НаименованиеПоликлинники { get; set; }
        [MaxLength(10)]
        public string КодПоликлинники { get; set; }

        [MaxLength(500)]
        public string ЮридическийАдрес { get; set; }

        [MaxLength(500)]
        public string ФактическийАдрес { get; set; }

        public int id_главВрач { get; set; }

        [ForeignKey("id_главВрач")]
        public ТФиоГлавВрач ТФиоГлавВрач { get; set; }

        public int id_главБух { get; set; }

        [MaxLength(30)]
        public string СвидетельствоРегистрации { get; set; }

        [MaxLength(20)]
        public string ИНН { get; set; }

        [MaxLength(20)]
        public string КПП { get; set; }

        [MaxLength(20)]
        public string БИК { get; set; }

        [MaxLength(200)]
        public string НаименованиеБанка { get; set; }

        [MaxLength(20)]
        public string РасчётныйСчёт { get; set; }

        [MaxLength(10)]
        public string ЛицевойСчёт { get; set; }

        [MaxLength(20)]
        public string НомерЛицензии { get; set; }

        public DateTime ДатаРегистрацииЛицензии { get; set; }

        [MaxLength(20)]
        public string ОГРН { get; set; }

        [MaxLength(255)]
        public string СвидетельствоРегистрацииЕГРЮЛ { get; set; }

        [MaxLength(255)]
        public string ОрганВыдавшийЛицензию { get; set; }

        [MaxLength(4000)]
        public string Постановление { get; set; }

        [MaxLength(50)]
        public string ОКПО { get; set; }

        [MaxLength(50)]
        public string ОКАТО { get; set; }

        public int Flag { get; set; }

        public int НачальныйНомерДоговора { get; set; }

    }
}

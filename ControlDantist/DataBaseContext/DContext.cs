using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;


namespace ControlDantist.DataBaseContext
{
    public class DContext : DbContext
    {
        public DContext(string connectionString) : base(connectionString)
        {
            // Запретим генерировать новую БД, подключемся к существующей.
            Database.SetInitializer<DContext>(null);
        }

        /// <summary>
        /// Таблица населенный пункт.
        /// </summary>
        public DbSet<ТНаселённыйПункт> ТабНаселенныйПункт { get; set; }

        /// <summary>
        /// Таблица льготная категория.
        /// </summary>
        public DbSet<ТЛьготнаяКатегория> ТабЛьготнаяКатегория { get; set; }

        /// <summary>
        /// Таблица льготник.
        /// </summary>
        public DbSet<ТЛЬготник> ТабЛьгоготник { get; set; }
        public DbSet<Т2Льготник> Т2Льготник { get; set; }

        /// <summary>
        /// Содержит тип документа.
        /// </summary>
        public DbSet<ТТипДокумент> ТабТипДокумент { get; set; }

        /// <summary>
        /// Содержит Фио глав врача.
        /// </summary>
        public DbSet<ТФиоГлавВрач> ТФиоГлавВрач { get; set; }

        /// <summary>
        /// Содержит Поликлиннику.
        /// </summary>
        public DbSet<ТПоликлинника> ТПоликлинника { get; set; }

        /// <summary>
        /// Услуги по договору.
        /// </summary>
        public DbSet<ТУслугиПоДоговору> ТУслугиПоДоговору { get; set; }

        /// <summary>
        /// Услуги в госпиталя.
        /// </summary>
        public DbSet<КлассификаторУслуг> КлассификаторУслугs { get; set; }

        /// <summary>
        /// Услуги в поликлиннике.
        /// </summary>
        public DbSet<ТВидУслуг> ТВидУслуг { get; set; }

        /// <summary>
        /// Договор.
        /// </summary>
        public DbSet<ТДоговор> ТДоговор { get; set; }

        /// <summary>
        /// Временная таблица Договор для поиска пропажи.
        /// </summary>
        public DbSet<Т2Договор> Т2Договор { get; set; }

        /// <summary>
        /// Акт выполненных работ.
        /// </summary>
        public DbSet<ТАктВыполненныхРабот> ТАктВыполненныхРабот { get; set; }

        /// <summary>
        /// Временная таблица ддля проведения тестов.
        /// </summary>
        public DbSet<Т2УслугиПоДоговору> Т2УслугиПоДоговору { get; set; }

        
    }
}

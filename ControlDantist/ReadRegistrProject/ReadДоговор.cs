using System;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.DataBaseContext;
using System.Data;

namespace ControlDantist.ReadRegistrProject
{
    public class ReadДоговор : IReadRegistr<ТДоговор>
    {
        private DContext dc;
        private DataRow rPers;

        public ReadДоговор(DContext dc, DataRow row)
        {
            this.dc = dc;
            this.rPers = row;
        }

        public ТДоговор Get()
        {
            ТДоговор personFull = new ТДоговор();

            personFull.id_договор = this.rPers.FieldOfDefault<int>("id_договор");
            personFull.НомерДоговора = this.rPers.FieldOfDefault<string>("НомерДоговора");
            personFull.ДатаДоговора = this.rPers.FieldOfDefault<DateTime>("ДатаДоговора");
            personFull.ДатаАктаВыполненныхРабот = this.rPers.FieldOfDefault<DateTime>("ДатаАктаВыполненныхРабот");
            personFull.СуммаАктаВыполненныхРабот = this.rPers.FieldOfDefault<decimal>("СуммаАктаВыполненныхРабот");
            personFull.id_льготнаяКатегория = this.rPers.FieldOfDefault<int>("id_льготнаяКатегория");
            personFull.id_поликлинника = this.rPers.FieldOfDefault<int>("id_поликлинника");
            personFull.Примечание = this.rPers.FieldOfDefault<string>("Примечание");
            personFull.id_комитет = this.rPers.FieldOfDefault<int>("id_комитет");
            personFull.ФлагНаличияДоговора = this.rPers.FieldOfDefault<bool>("ФлагНаличияДоговора");
            personFull.ФлагНаличияАкта = this.rPers.FieldOfDefault<bool>("ФлагНаличияАкта");
            personFull.id_льготник = this.rPers.FieldOfDefault<int>("id_льготник");
            personFull.ФлагДопСоглашения = this.rPers.FieldOfDefault<string>("ФлагДопСоглашения");
            personFull.флагСРН = null;// this.rPers.FieldOfDefault<bool>("флагСРН");
            personFull.флагУслуги = null;// this.rPers.FieldOfDefault<bool>("флагУслуги");
            personFull.датаВозврата = null;//  this.rPers.FieldOfDefault<DateTime>("датаВозврата");
            personFull.ДатаЗаписиДоговора = null; //this.rPers.FieldOfDefault<DateTime>("ДатаЗаписиДоговора");
            personFull.ФлагПроверки = false; // this.rPers.FieldOfDefault<bool>("ФлагПроверки");

            personFull.НомерРеестра = null;// this.rPers.FieldOfDefault<string>("НомерРеестра");
            personFull.ДатаРеестра = null;// this.rPers.FieldOfDefault<DateTime>("ДатаРеестра");
            personFull.НомерСчётФактрура = null;// this.rPers.FieldOfDefault<string>("НомерСчётФактрура");
            personFull.ДатаСчётФактура = null;// this.rPers.FieldOfDefault<DateTime>("ДатаСчётФактура");
            personFull.flagАнулирован = false;// this.rPers.FieldOfDefault<bool>("flagАнулирован");
            personFull.logWrite = null;// this.rPers.FieldOfDefault<string>("logWrite");
            personFull.flagОжиданиеПроверки = false;// this.rPers.FieldOfDefault<bool>("flagОжиданиеПроверки");

            personFull.idFileRegistProgect = 0;// this.rPers.FieldOfDefault<int>("idFileRegistProgect");
            personFull.ФлагАнулирован = false;// this.rPers.FieldOfDefault<bool>("ФлагАнулирован");
            personFull.ФлагВозвратНаДоработку = false;// this.rPers.FieldOfDefault<bool>("ФлагВозвратНаДоработку");
            personFull.ДатаПроверки = null;// this.rPers.FieldOfDefault<DateTime>("ДатаПроверки");
            personFull.flag2020 = false;// this.rPers.FieldOfDefault<bool>("flag2020");
            personFull.flag2019AddWrite = false;// this.rPers.FieldOfDefault<bool>("flag2019AddWrite");

            return personFull;
        }
    }
}

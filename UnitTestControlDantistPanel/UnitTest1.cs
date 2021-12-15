using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControlDantist.DataBaseContext;
using System.Collections.Generic;
using ControlDantist.ValidateRegistrProject;
using ControlDantist.ExceptionClassess;
using ControlDantist.Classes;
using System;
using System.Data;
using ControlDantist.MedicalServices;

namespace UnitTestControlDantistPanel
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Тестируем получение льготной категории если льготная категория не установлена.
        /// </summary>
        [TestMethod]
        public void TestValidatePrefCategoryList2GetPreferentCategoryNull()
        {
            // arrange.
            List<ItemLibrary> list = new List<ItemLibrary>();

            //ТЛьготнаяКатегория тЛьготнаяКатегория = new ТЛьготнаяКатегория();
            //тЛьготнаяКатегория.ЛьготнаяКатегория = "";


            ItemLibrary itemLibrary = new ItemLibrary();

            //PackageClass package = new PackageClass();
            //package.тЛьготнаяКатегория = тЛьготнаяКатегория;

            PackageClass package = new PackageClass();

            itemLibrary.Packecge = package;

            list.Add(itemLibrary);

            // act.
            ValidatePrefCategoryList2 validatePrefCategoryList2 = new ValidatePrefCategoryList2(list);
            var asd = validatePrefCategoryList2.GetPreferentCategory();

            // result
            Assert.AreEqual(asd, "Категория_не_установлена");
        }

        [TestMethod]
        public void TestValidatePrefCategoryList2GetPreferentCategoryВетеран_Труда()
        {
            // arrange.
            List<ItemLibrary> list = new List<ItemLibrary>();

            ТЛьготнаяКатегория тЛьготнаяКатегория = new ТЛьготнаяКатегория();
            тЛьготнаяКатегория.ЛьготнаяКатегория = "Ветеран труда";

            PackageClass package = new PackageClass();
            package.тЛьготнаяКатегория = тЛьготнаяКатегория;

            ItemLibrary itemLibrary = new ItemLibrary();

            itemLibrary.Packecge = package;

            list.Add(itemLibrary);

            // act.
            ValidatePrefCategoryList2 validatePrefCategoryList2 = new ValidatePrefCategoryList2(list);
            var asd = validatePrefCategoryList2.GetPreferentCategory();

            // result
            Assert.AreEqual(asd, "Ветеран труда");

        }

        [TestMethod]
        public void TestValidErrorPerson()
        {
            // arrange.
            List<ItemLibrary> list = new List<ItemLibrary>();


            ItemLibrary itemLibrary = new ItemLibrary();

            ТЛЬготник person = new ТЛЬготник();

            person.Фамилия = "Иванов";
            person.Имя = "Иван";

            string s = person.Отчество.Do(w => w, "");

            DateTime dr = new DateTime(1957, 1, 10);
            person.ДатаРождения = dr;

            DateTime dateDoc = new DateTime(2000, 1, 10);
            person.ДатаВыдачиДокумента = dateDoc;

            DateTime datePassword = new DateTime(2002, 3, 15);
            person.ДатаВыдачиПаспорта = dateDoc;

            // act.
            ValidErrorPerson ver = new ValidErrorPerson(person);


            Assert.IsTrue(ver.Validate());
        }

        [TestMethod]
        public void TestCompareRegistr()
        {
            // arrange.
            List<ItemLibrary> list = new List<ItemLibrary>();

            // Список договоров.
            ItemLibrary itemLibrary = new ItemLibrary();

            // id договора.
            ТДоговор contract = new ТДоговор();
            contract.id_договор = 100;

            // Пакет с данными реестра.
            PackageClass packageClass = new PackageClass();
            itemLibrary.Packecge = packageClass;

            itemLibrary.Packecge.тДоговор = contract;

            list.Add(itemLibrary);

            // arrange result.
            List<ItemLibrary> list2 = new List<ItemLibrary>();

            // Список договоров.
            ItemLibrary itemLibrary2 = new ItemLibrary();

            // Пометим как прошедший проверку.
            itemLibrary2.FlagValidateEsrn = true;

            // id договора.
            ТДоговор contract2 = new ТДоговор();
            contract2.id_договор = 100;

            // Пакет с данными реестра.
            PackageClass packageClass2 = new PackageClass();

            itemLibrary2.Packecge = packageClass2;

            itemLibrary2.Packecge.тДоговор = contract2;

            list2.Add(itemLibrary2);

            // Создаем таблицу.
            DataSet bookStore = new DataSet("BookStore");
            DataTable booksTable = new DataTable("Books");
            // добавляем таблицу в dataset
            bookStore.Tables.Add(booksTable);

            // создаем столбцы для таблицы Books
            DataColumn idColumn = new DataColumn("id_договор", Type.GetType("System.Int32"));
            idColumn.Unique = true; // столбец будет иметь уникальное значение
            idColumn.AllowDBNull = false; // не может принимать null
            idColumn.AutoIncrement = true; // будет автоинкрементироваться
            idColumn.AutoIncrementSeed = 99; // начальное значение
            idColumn.AutoIncrementStep = 1; // приращении при добавлении новой строки

            booksTable.Columns.Add(idColumn);

            DataRow dataRow = booksTable.NewRow();

            DataRow row = booksTable.NewRow();
            booksTable.Rows.Add(row); // добавляем первую строку

            //dataRow["Id"] = 100;

            // act.
            //CompareRegistr compareRegistr = new CompareRegistr(list);
            //compareRegistr.Compare(booksTable);

            // result.
            Assert.AreEqual(itemLibrary2.FlagValidateEsrn, itemLibrary.FlagValidateEsrn,"Проверка не прошла");
        }

        [TestMethod]
        public void TestServicesMedical()
        {
            // arrang.

            //DContext dc = new DContext(ConnectDB.ConnectionString());

            //dc.ТВидУслуг = null;

            //// act.

            //ServicesMedicalHospital smh = new ServicesMedicalHospital(dc);

            //smh.SetIdentificator(210);

            //var listServicesMedical = smh.ServicesMedical();

            //List<ТВидУслуг> listResult = new List<ТВидУслуг>();

            // result.
           // Assert.AreEqual(listServicesMedical, listResult, "Тест не пройден");


        }



    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ControlDantist.Classes;
using DantistLibrary;
using ControlDantist.Find;
using System.Linq;
using ControlDantist.ValidPersonContract;


namespace ControlDantist
{
    public partial class FormValidContract : Form
    {
        //хранит значение результатов проверки
        private bool flagValid;
        private bool flagCheck;

        // Список для хранения результатов поиска.
        private List<FindPersonNumContractItem> listPerson = new List<FindPersonNumContractItem>();

        /// <summary>
        /// Хранит значенеие какие должна отображать данные форма. Прошедшие проверку = TRUE и не прошедшие проверку = FALSE
        /// </summary>
        public bool FlagValid
        {
            get
            {
                return flagValid;
            }
            set
            {
                flagValid = value;
            }
        }

        /// <summary>
        /// Хранит флаг указывающий что договор направлен на проверку (true) если (false или null = то договор прошол проверку или отклонёо)
        /// </summary>
        public bool FlagCheck
        {
            get
            {
                return flagCheck;
            }
            set
            {
                flagCheck = value;
            }
        }

        public FormValidContract()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //Закроем форму
            this.Close();
        }

        private void FormValidContract_Load(object sender, EventArgs e)
        {
            //Не будем при загрузке отображать льготников (чтобы не тормозила программа)
            //string query = string.Empty;

            ////если прошли проверку
            //if (this.FlagValid == true && this.FlagCheck == false)
            //{
            //    query = "SELECT Договор.id_договор, Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория, sum(УслугиПоДоговору.Сумма) as 'Сумма',Договор.ДатаЗаписиДоговора,Договор.logWrite  " +
            //              "FROM Договор INNER JOIN Льготник " +
            //              "ON dbo.Договор.id_льготник = dbo.Льготник.id_льготник INNER JOIN " +
            //              "dbo.ЛьготнаяКатегория ON dbo.Льготник.id_льготнойКатегории = dbo.ЛьготнаяКатегория.id_льготнойКатегории INNER JOIN " +
            //              "dbo.УслугиПоДоговору ON dbo.Договор.id_договор = dbo.УслугиПоДоговору.id_договор " +
            //              "where Договор.ФлагПроверки = 'True' " +
            //              "Group by Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория,Договор.ДатаЗаписиДоговора,Договор.logWrite  ";
            //}
            ////else

            ////если не прошли проверку
            //if (this.FlagValid == false && this.FlagCheck == false)
            //{
            //    query = "SELECT Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория, sum(УслугиПоДоговору.Сумма) as 'Сумма',Договор.ДатаЗаписиДоговора,Договор.logWrite  " +
            //              "FROM Договор INNER JOIN Льготник " +
            //              "ON dbo.Договор.id_льготник = dbo.Льготник.id_льготник INNER JOIN " +
            //              "dbo.ЛьготнаяКатегория ON dbo.Льготник.id_льготнойКатегории = dbo.ЛьготнаяКатегория.id_льготнойКатегории INNER JOIN " +
            //              "dbo.УслугиПоДоговору ON dbo.Договор.id_договор = dbo.УслугиПоДоговору.id_договор " +
            //              "where Договор.ФлагПроверки = 'False' " +
            //              "Group by Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория,Договор.ДатаЗаписиДоговора,Договор.logWrite  ";

            //}

            ////Направлен запрос
            ////если не прошли проверку
            //if (this.FlagValid == false && this.FlagCheck == true)
            //{
            //    query = "SELECT Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория, sum(УслугиПоДоговору.Сумма) as 'Сумма',Договор.ДатаЗаписиДоговора,Договор.logWrite  " +
            //          "FROM Договор INNER JOIN Льготник " +
            //          "ON dbo.Договор.id_льготник = dbo.Льготник.id_льготник INNER JOIN " +
            //          "dbo.ЛьготнаяКатегория ON dbo.Льготник.id_льготнойКатегории = dbo.ЛьготнаяКатегория.id_льготнойКатегории INNER JOIN " +
            //          "dbo.УслугиПоДоговору ON dbo.Договор.id_договор = dbo.УслугиПоДоговору.id_договор " +
            //          "where Договор.ФлагПроверки = 'False' and Договор.ФлагЗапрос = 'True' " +
            //          "Group by Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория,Договор.ДатаЗаписиДоговора,Договор.logWrite  ";

            //}


            //using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            //{
            //    con.Open();
            //    SqlTransaction transact = con.BeginTransaction();

            //    //DataTable dtContract = ТаблицаБД.GetTableSQL(query, "ТаблицаДоговоров", con, transact);
            //    SqlCommand com = new SqlCommand(query, con);
            //    com.Transaction = transact;

            //    DataTable tab = new DataTable();
            //    DataColumn col0 = new DataColumn("id_договор", typeof(int));
            //    tab.Columns.Add(col0);
            //    DataColumn col1 = new DataColumn("НомерДоговора",typeof(string));
            //    tab.Columns.Add(col1);
            //    DataColumn col2 = new DataColumn("Фамилия", typeof(string));
            //    tab.Columns.Add(col2);
            //    DataColumn col3 = new DataColumn("Имя", typeof(string));
            //    tab.Columns.Add(col3);
            //    DataColumn col4 = new DataColumn("Отчество", typeof(string));
            //    tab.Columns.Add(col4);
            //    DataColumn col5 = new DataColumn("ЛьготнаяКатегория", typeof(string));
            //    tab.Columns.Add(col5);
            //    DataColumn col6 = new DataColumn("Сумма", typeof(string));
            //    tab.Columns.Add(col6);
            //    DataColumn col7 = new DataColumn("Дата", typeof(string));
            //    tab.Columns.Add(col7);
            //    DataColumn col8 = new DataColumn("КтоЗаписал", typeof(string));
            //    tab.Columns.Add(col8);


            //    SqlDataReader read = com.ExecuteReader();
            //    while (read.Read())
            //    {
            //        DataRow row = tab.NewRow();
            //        row["id_договор"] = read["id_договор"].ToString().Trim();
            //        row["НомерДоговора"] = read["НомерДоговора"].ToString().Trim();
            //        row["Фамилия"] = read["Фамилия"].ToString().Trim();
            //        row["Имя"] = read["Имя"].ToString().Trim();
            //        row["Отчество"] = read["Отчество"].ToString().Trim();
            //        row["ЛьготнаяКатегория"] = read["ЛьготнаяКатегория"].ToString().Trim();
            //        row["Сумма"] = Convert.ToDecimal(read["Сумма"]).ToString("c").Trim();
            //        if (read["ДатаЗаписиДоговора"] != DBNull.Value)
            //        {
            //            row["Дата"] = Convert.ToDateTime(read["ДатаЗаписиДоговора"]).ToShortDateString().Trim();
            //        }
            //        if (read["logWrite"] != DBNull.Value)
            //        {
            //            row["КтоЗаписал"] = read["logWrite"].ToString().Trim();
            //        }
            //        tab.Rows.Add(row);
            //    }

            //    this.dataGridView1.DataSource = tab;

            //    this.dataGridView1.Columns["id_договор"].Width = 100;
            //    this.dataGridView1.Columns["id_договор"].Visible = false;

            //    this.dataGridView1.Columns["НомерДоговора"].Width = 100;
            //    this.dataGridView1.Columns["НомерДоговора"].DisplayIndex = 0;

            //    this.dataGridView1.Columns["Фамилия"].Width = 200;
            //    this.dataGridView1.Columns["Фамилия"].DisplayIndex = 1;

            //    this.dataGridView1.Columns["Имя"].Width = 200;
            //    this.dataGridView1.Columns["Имя"].DisplayIndex = 2;

            //    this.dataGridView1.Columns["Отчество"].Width = 200;
            //    this.dataGridView1.Columns["Отчество"].DisplayIndex = 3;

            //    this.dataGridView1.Columns["ЛьготнаяКатегория"].Width = 300;
            //    this.dataGridView1.Columns["ЛьготнаяКатегория"].DisplayIndex = 4;

            //    this.dataGridView1.Columns["Сумма"].Width = 200;
            //    this.dataGridView1.Columns["Сумма"].DisplayIndex = 5;

            //    this.dataGridView1.Columns["Дата"].Width = 70;
            //    this.dataGridView1.Columns["Дата"].DisplayIndex = 6;

            //    this.dataGridView1.Columns["КтоЗаписал"].Width = 70;
            //    this.dataGridView1.Columns["КтоЗаписал"].DisplayIndex = 7;
            //}

            // Запишем справочник год.
            string query = "select YEAR(ДатаЗаписиДоговора) from Договор " +
                                    "group by ДатаЗаписиДоговора " +
                                    "having ДатаЗаписиДоговора is not null";


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string query = string.Empty;

            if (this.textBox1.Text.Trim() == "5/8785")
            {
                var asd = "";
            }

            ShowResultPerson showResultPerson;

            // Список для отображения результатов поиска.
            List<ValideContract> listDisplay = new List<ValideContract>();

            //если прошли проверку
            if (this.FlagValid == true && this.FlagCheck == false)
            {
                // Временный cписок содержащий все найденные договора.
                List<ValideContract> listTempDisplay = new List<ValideContract>();

                // Переменная для хранения номера договора который необходимо найти.
                string numContract = this.textBox1.Text;

                //// Поиск льготника прошедшего проверку по номеру договора.
                //IFindPerson findPerson = new FindContractTo2019(this.textBox1.Text);
                //string queryTo2019 = findPerson.Query();

                //StringParametr stringParametr = new StringParametr();
                //stringParametr.Query = queryTo2019;

                // Поиск номера договора за 2019 год по таблицам TableAdd.
                //IFindPerson fintPerson2019Add = new FindContract2019Add(numContract);
                //string query2019Add = fintPerson2019Add.Query();

                //StringParametr stringParametr2019Add = new StringParametr();
                //stringParametr2019Add.Query = query2019Add;

                // Пока скроем поиск льготников в основной таблице за 2019 год.
                //  Поиск номера договора за 2019 год по обычным таблицам.
                //IFindPerson findPerson2019 = new FindContract2019(numContract);
                //string query2019 = findPerson2019.Query();


                //StringParametr stringParametr2019 = new StringParametr();
                //stringParametr2019.Query = query2019;

                // Поиск номера договора позже 2019 года.
                IFindPerson fintPersonAftar2019 = new FindPersonAftar2019(numContract, true);
                string query2019Aftar = fintPersonAftar2019.Query();

                StringParametr stringParametr2019Aftar = new StringParametr();
                stringParametr2019Aftar.Query = query2019Aftar;

                // Поиск договора по таблицам 2021 года.
                IFindPerson findPerson2021 = new FindPersonAftar(numContract, true);
                string query2021 = findPerson2021.Query();

                StringParametr stringParametr2021 = new StringParametr();
                stringParametr2021.Query = query2021;


                //// Выполним запрос до 2019 года.
                //listTempDisplay.AddRange(ExecuteQuery(stringParametr));

                //// Выполним запрос 2019 года таблицы Add.
                //listTempDisplay.AddRange(ExecuteQuery(stringParametr2019Add));

                //// Выполним запрос 2019 года.
                //listTempDisplay.AddRange(ExecuteQuery(stringParametr2019));

                // Выполним запрос больше 2019 года.
                listTempDisplay.AddRange(ExecuteQuery(stringParametr2019Aftar));

                // Выполним запрос 2021.
                listTempDisplay.AddRange(ExecuteQuery(stringParametr2021));

                var listRes = CompareContractForNumber.Compare(listTempDisplay);

                // Временный список договоров.
                //List<ValideContract> listDisplayTemp = new List<ValideContract>();

                //listDisplayTemp.AddRange(listRes);

                listDisplay.AddRange(listRes);

                

            }
            //else
            if (this.FlagValid == false && this.FlagCheck == false)
            {
                // Временный cписок содержащий все найденные договора.
                List<ValideContract> listTempDisplay = new List<ValideContract>();

                // Поиск льготника прошедшего проверку по номеру договора.
                IFindPerson findPerson = new FindContractTo2019NoValid(this.textBox1.Text);
                string queryTo2019 = findPerson.Query();

                StringParametr stringParametr = new StringParametr();
                stringParametr.Query = queryTo2019;

                // Поиск номера договора за 2019 год по таблицам TableAdd.
                IFindPerson fintPerson2019Add = new FindContract2019AddNoValid(this.textBox1.Text);
               string query2019Add = fintPerson2019Add.Query();

                StringParametr stringParametr2019Add = new StringParametr();
                stringParametr2019Add.Query = query2019Add;

                //IFindPerson findPerson2019 = new FindContract2019NoValid(this.textBox1.Text);
                //string query2019 = findPerson2019.Query();

                //StringParametr stringParametr2019 = new StringParametr();
                //stringParametr2019.Query = query2019;

                // Поиск номера договора позже 2019 года.
                IFindPerson fintPersonAftar2019 = new FindPersonAftar2019NoValid(this.textBox1.Text);
                string query2019Aftar = fintPersonAftar2019.Query();

                StringParametr stringParametr2019Aftar = new StringParametr();
                stringParametr2019Aftar.Query = query2019Aftar;

                // Поиск льготников по таблицам 2021 года.
                IFindPerson findPersonNoValid = new FindPersonNoValid(this.textBox1.Text);
                string queryNoValid2021 = findPersonNoValid.Query();

                StringParametr stringParametrFindNoValid2021 = new StringParametr();
                stringParametrFindNoValid2021.Query = queryNoValid2021;

                // Выполним запрос до 2019 года.
                listTempDisplay.AddRange(ExecuteQuery(stringParametr));

                // Выполним запрос 2019 года таблицы Add.
                listTempDisplay.AddRange(ExecuteQuery(stringParametr2019Add));

                // Выполним запрос 2019 года.
                //listTempDisplay.AddRange(ExecuteQuery(stringParametr2019));

                // Выполним запрос больше 2019 года.
                listTempDisplay.AddRange(ExecuteQuery(stringParametr2019Aftar));

                // Выполним запрос 2021 года.
                listTempDisplay.AddRange(ExecuteQuery(stringParametrFindNoValid2021));

                var listRes = CompareContractForNumber.Compare(listTempDisplay);

                listDisplay.AddRange(listRes);
            }

            if (this.FlagValid == false && this.FlagCheck == true)
            {

                IFindPerson findPersonNumberDoc = new FindPersonNumContract(this.textBox1.Text);

                showResultPerson = new ShowResultPerson(findPersonNumberDoc);

                // Заполним список результатами поиска.  
                listDisplay.AddRange(showResultPerson.DisplayDate());

                //var test = listDisplay.GroupBy(w => w.НомерДоговора);

                var test = listDisplay;

            }

            this.dataGridView1.DataSource = listDisplay;

            this.dataGridView1.Columns["id_договор"].Width = 100;
            this.dataGridView1.Columns["id_договор"].Visible = false;
            this.dataGridView1.Columns["НомерДоговора"].Width = 100;
            this.dataGridView1.Columns["НомерДоговора"].DisplayIndex = 0;

            this.dataGridView1.Columns["Фамилия"].Width = 150;
            this.dataGridView1.Columns["Фамилия"].DisplayIndex = 1;

            this.dataGridView1.Columns["Имя"].Width = 150;
            this.dataGridView1.Columns["Имя"].DisplayIndex = 2;

            this.dataGridView1.Columns["Отчество"].Width = 150;
            this.dataGridView1.Columns["Отчество"].DisplayIndex = 3;

            this.dataGridView1.Columns["ЛьготнаяКатегория"].Width = 300;
            this.dataGridView1.Columns["ЛьготнаяКатегория"].DisplayIndex = 4;
            this.dataGridView1.Columns["ЛьготнаяКатегория"].HeaderText = "Льготная категория";

            this.dataGridView1.Columns["Сумма"].Width = 100;
            this.dataGridView1.Columns["Сумма"].DisplayIndex = 5;

            this.dataGridView1.Columns["Дата"].Width = 100;
            this.dataGridView1.Columns["Дата"].DisplayIndex = 6;
            this.dataGridView1.Columns["Дата"].HeaderText = "Дата записи проекта договора в нашу БД";

            this.dataGridView1.Columns["НомерАкта"].Width = 100;
            this.dataGridView1.Columns["НомерАкта"].DisplayIndex = 7;
            this.dataGridView1.Columns["НомерАкта"].HeaderText = "Номер акта";

            this.dataGridView1.Columns["ДатаПодписания"].Width = 100;
            this.dataGridView1.Columns["ДатаПодписания"].DisplayIndex = 8;
            this.dataGridView1.Columns["ДатаПодписания"].HeaderText = "Дата подписания акта";

            this.dataGridView1.Columns["КтоЗаписал"].Width = 150;
            this.dataGridView1.Columns["КтоЗаписал"].DisplayIndex = 9;

            this.dataGridView1.Columns["flag2019AddWrite"].Width = 150;
            this.dataGridView1.Columns["flag2019AddWrite"].DisplayIndex = 10;
            this.dataGridView1.Columns["flag2019AddWrite"].Visible = false;

            this.dataGridView1.Columns["flagАнулирован"].Width = 150;
            this.dataGridView1.Columns["flagАнулирован"].DisplayIndex = 11;
            this.dataGridView1.Columns["flagАнулирован"].Visible = true;// false;



            // Окрасим строку в красный цвет.
            for (int i = 0; i <= this.dataGridView1.Rows.Count - 1; i++)
            {
                if(Convert.ToBoolean(this.dataGridView1.Rows[i].Cells["flagАнулирован"].Value) == true)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private List<ValideContract> ExecuteQuery(object obj)
        {
            StringParametr stringParametr = (StringParametr)obj;

            // Список для хранения резултатов поиска.
            List<ValideContract> listDisplay = new List<ValideContract>();

            using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            {
                con.Open();
                //SqlTransaction transact = con.BeginTransaction();

                //DataTable dtContract = ТаблицаБД.GetTableSQL(query, "ТаблицаДоговоров", con, transact);
                SqlCommand com = new SqlCommand(stringParametr.Query, con);
                //com.Transaction = transact;

                SqlDataReader read = com.ExecuteReader();

                IFindContract findContract = new FindContractForNumber(read);

                var list = findContract.Adapter();

                listDisplay.AddRange(list);

            }

            return listDisplay;

        }

        private List<ValideContract> FIltrDowbel(IEnumerable<IGrouping<string, ValideContract>> listG)
        {
            // Список для отображения на экране.
            List<ValideContract> listDisplay = new List<ValideContract>();

            foreach (var itms in listG)
            {
                if (itms.All(w => w.flag2019AddWrite == false) == true)
                {
                    foreach (var itm in itms)
                    {
                        listDisplay.Add(itm);
                    }
                }
                else
                {
                    foreach (var itm in itms)
                    {
                        if (itm.flag2019AddWrite == true)
                        {
                            listDisplay.Add(itm);
                        }
                    }
                }
            }

            return listDisplay;
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           // string query = string.Empty;

           // //если прошли проверку
           //if (this.FlagValid == true && this.FlagCheck == false)
           // {
           //     query = "SELECT Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория, sum(УслугиПоДоговору.Сумма) as 'Сумма',Договор.ДатаЗаписиДоговора,Договор.logWrite,Договор.НомерРеестра,Договор.НомерСчётФактрура " +
           //               "FROM Договор INNER JOIN Льготник " +
           //               "ON dbo.Договор.id_льготник = dbo.Льготник.id_льготник INNER JOIN " +
           //               "dbo.ЛьготнаяКатегория ON dbo.Льготник.id_льготнойКатегории = dbo.ЛьготнаяКатегория.id_льготнойКатегории INNER JOIN " +
           //               "dbo.УслугиПоДоговору ON dbo.Договор.id_договор = dbo.УслугиПоДоговору.id_договор " +
           //               //"where Договор.ФлагПроверки = 'True' and Льготник.Фамилия like '" + this.textBox2.Text + "%' " +
           //               //"where Договор.ФлагПроверки = 'True' and Льготник.Фамилия = '" + this.textBox2.Text + "' " + ==== работало правильно
           //               "where Договор.ФлагПроверки = 'True' and Льготник.Фамилия = '" + this.textBox2.Text + "' " + //and logWrite <>  'Носова Марина Владимировна' " +
           //               "Group by Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория,Договор.ДатаЗаписиДоговора,Договор.logWrite,Договор.НомерРеестра,Договор.НомерСчётФактрура ";
           // }
           // //else

           // if (this.FlagValid == false && this.FlagCheck == false)
           // {
           //     query = "SELECT Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория, sum(УслугиПоДоговору.Сумма) as 'Сумма',Договор.ДатаЗаписиДоговора,Договор.logWrite,Договор.НомерРеестра,Договор.НомерСчётФактрура " +
           //               "FROM Договор INNER JOIN Льготник " +
           //               "ON dbo.Договор.id_льготник = dbo.Льготник.id_льготник INNER JOIN " +
           //               "dbo.ЛьготнаяКатегория ON dbo.Льготник.id_льготнойКатегории = dbo.ЛьготнаяКатегория.id_льготнойКатегории INNER JOIN " +
           //               "dbo.УслугиПоДоговору ON dbo.Договор.id_договор = dbo.УслугиПоДоговору.id_договор " +
           //               //"where Договор.ФлагПроверки = 'False' and Льготник.Фамилия like '" + this.textBox2.Text + "%' " +
           //               //========="where Договор.ФлагПроверки = 'False' and Льготник.Фамилия = '" + this.textBox2.Text + "' " + // работало правильно
           //               "where Договор.ФлагПроверки = 'False' and Льготник.Фамилия = '" + this.textBox2.Text + "' " + //and logWrite <> 'Носова Марина Владимировна' " +
           //               "Group by Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория,Договор.ДатаЗаписиДоговора,Договор.logWrite,Договор.НомерРеестра,Договор.НомерСчётФактрура ";

           // }

           // if (this.FlagValid == false && this.FlagCheck == true)
           // {
           //     query = "SELECT Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория, sum(УслугиПоДоговору.Сумма) as 'Сумма',Договор.ДатаЗаписиДоговора,Договор.logWrite,Договор.НомерРеестра,Договор.НомерСчётФактрура " +
           //               "FROM Договор INNER JOIN Льготник " +
           //               "ON dbo.Договор.id_льготник = dbo.Льготник.id_льготник INNER JOIN " +
           //               "dbo.ЛьготнаяКатегория ON dbo.Льготник.id_льготнойКатегории = dbo.ЛьготнаяКатегория.id_льготнойКатегории INNER JOIN " +
           //               "dbo.УслугиПоДоговору ON dbo.Договор.id_договор = dbo.УслугиПоДоговору.id_договор " +
           //               //"where Договор.ФлагПроверки = 'False' and Льготник.Фамилия like '" + this.textBox2.Text + "%' " +
           //               "where Договор.ФлагПроверки = 'False' and Льготник.Фамилия = '" + this.textBox2.Text + "' " + //and logWrite <> 'Носова Марина Владимировна' " +
           //               "Group by Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория,Договор.ДатаЗаписиДоговора,Договор.logWrite,Договор.НомерРеестра,Договор.НомерСчётФактрура ";

           // }

           // using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
           // {
           //     con.Open();
           //     SqlTransaction transact = con.BeginTransaction();

           //     //DataTable dtContract = ТаблицаБД.GetTableSQL(query, "ТаблицаДоговоров", con, transact);
           //     SqlCommand com = new SqlCommand(query, con);
           //     com.Transaction = transact;

           //     DataTable tab = new DataTable();
           //     DataColumn col0 = new DataColumn("id_договор", typeof(int));
           //     tab.Columns.Add(col0);
           //     DataColumn col1 = new DataColumn("НомерДоговора", typeof(string));
           //     tab.Columns.Add(col1);
           //     DataColumn col2 = new DataColumn("Фамилия", typeof(string));
           //     tab.Columns.Add(col2);

           //     DataColumn col3 = new DataColumn("Имя", typeof(string));
           //     tab.Columns.Add(col3);
           //     DataColumn col4 = new DataColumn("Отчество", typeof(string));
           //     tab.Columns.Add(col4);

           //     DataColumn col5 = new DataColumn("ЛьготнаяКатегория", typeof(string));
           //     tab.Columns.Add(col5);
           //     DataColumn col6 = new DataColumn("Сумма", typeof(string));
           //     tab.Columns.Add(col6);

           //     DataColumn col7 = new DataColumn("Дата", typeof(string));
           //     tab.Columns.Add(col7);
           //     DataColumn col8 = new DataColumn("КтоЗаписал", typeof(string));
           //     tab.Columns.Add(col8);

           //     // отобразим номер счёт факутры и номер реестра
           //     DataColumn col9 = new DataColumn("НомерРеестра", typeof(string));
           //     tab.Columns.Add(col9);

           //     DataColumn col10 = new DataColumn("НомерСчётФактрура", typeof(string));
           //     tab.Columns.Add(col10);


           //     SqlDataReader read = com.ExecuteReader();
           //     while (read.Read())
           //     {
           //         DataRow row = tab.NewRow();
           //         row["id_договор"] = read["id_договор"].ToString().Trim();
           //         row["НомерДоговора"] = read["НомерДоговора"].ToString().Trim();
           //         row["Фамилия"] = read["Фамилия"].ToString().Trim();
           //         row["Имя"] = read["Имя"].ToString().Trim();
           //         row["Отчество"] = read["Отчество"].ToString().Trim();
           //         row["ЛьготнаяКатегория"] = read["ЛьготнаяКатегория"].ToString().Trim();
           //         row["Сумма"] = Convert.ToDecimal(read["Сумма"]).ToString("c").Trim();
           //         //string sComp = row["Дата"].ToString();
           //         if (read["ДатаЗаписиДоговора"] != DBNull.Value)//
           //         {
           //             row["Дата"] = Convert.ToDateTime(read["ДатаЗаписиДоговора"]).ToShortDateString().Trim();
           //         }
           //         else
           //         {

           //         }
           //         if (read["logWrite"] != DBNull.Value)
           //         {
           //             row["КтоЗаписал"] = read["logWrite"].ToString().Trim();
           //         }

           //         // отобразми номер счёт фактуры
           //         if (read["НомерРеестра"] != DBNull.Value && read["НомерРеестра"].ToString().Trim() != "NULL")
           //         {
           //             row["НомерРеестра"] = read["НомерРеестра"].ToString().Trim();
           //         }

           //         // отобразми номер реестра
           //         if (read["НомерСчётФактрура"] != DBNull.Value && read["НомерСчётФактрура"].ToString().Trim() != "NULL" )
           //         {
           //             row["НомерСчётФактрура"] = read["НомерСчётФактрура"].ToString().Trim();
           //         }


           //         tab.Rows.Add(row);
           //     }

           //     this.dataGridView1.DataSource = tab;

           //     this.dataGridView1.Columns["id_договор"].Width = 100;
           //     this.dataGridView1.Columns["id_договор"].Visible = false;
           //     this.dataGridView1.Columns["НомерДоговора"].Width = 100;
           //     this.dataGridView1.Columns["НомерДоговора"].DisplayIndex = 0;

           //     this.dataGridView1.Columns["Фамилия"].Width = 200;
           //     this.dataGridView1.Columns["Фамилия"].DisplayIndex = 1;

           //     this.dataGridView1.Columns["Имя"].Width = 200;
           //     this.dataGridView1.Columns["Имя"].DisplayIndex = 2;

           //     this.dataGridView1.Columns["Отчество"].Width = 200;
           //     this.dataGridView1.Columns["Отчество"].DisplayIndex = 3;

           //     this.dataGridView1.Columns["ЛьготнаяКатегория"].Width = 300;
           //     this.dataGridView1.Columns["ЛьготнаяКатегория"].DisplayIndex = 4;

           //     this.dataGridView1.Columns["Сумма"].Width = 200;
           //     this.dataGridView1.Columns["Сумма"].DisplayIndex = 5;

           //     this.dataGridView1.Columns["Дата"].Width = 70;
           //     this.dataGridView1.Columns["Дата"].DisplayIndex = 6;

           //     this.dataGridView1.Columns["КтоЗаписал"].Width = 70;
           //     this.dataGridView1.Columns["КтоЗаписал"].DisplayIndex = 7;

           //     this.dataGridView1.Columns["НомерРеестра"].Width = 70;
           //     this.dataGridView1.Columns["НомерРеестра"].DisplayIndex = 7;

           //     this.dataGridView1.Columns["НомерСчётФактрура"].Width = 70;
           //     this.dataGridView1.Columns["НомерСчётФактрура"].DisplayIndex = 7;


           // }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //получим индекс столбца
            int i = e.ColumnIndex;

            //переменная хранит таблицу с данными о контракте
            DataTable tabContract;

            //Переменная хранит данные по льготнику
            DataTable tabPerson;

            int id_договор = 0;

            // Указывает, что таблица (в положении true) содержит данные 2019 года.
            bool flag2019AddWrite = false;

            // получим id договора.
            id_договор = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_договор"].Value);

            //flag2019AddWrite = Convert.ToBoolean(this.dataGridView1.CurrentRow.Cells["flag2019AddWrite"].Value);
            int flarYear = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["Год"].Value);

            // Переменная хранит текст запроса по услугам.
            string queruContract = string.Empty;

            // Переменная хранит текст запроса по договору.
            string queruPerson = string.Empty;

            // Если договор раньше 2019 года или равен 2020 году.
            if (flarYear < 2019 || flarYear == 2020)
            {
                // Поиск по номеру договора.
                if (this.textBox2.Text == "" && this.txtИмя.Text == "")
                {
                    // Получим данные по услугам к договору.
                    IQueryFind queryFindId2019 = new QueryFindContractBefor2019(id_договор);

                    queruContract = queryFindId2019.Query();

                    // Получим данные по договору.
                    IQueryFind queryFindBefor2019 = new QueryFindIdContractBefore2019(id_договор);

                    queruPerson = queryFindBefor2019.Query();
                }
                else if(this.textBox2.Text != "" && this.txtИмя.Text == "")
                {
                    // Получим данные по услугам к договору.
                    IQueryFind queryFindId2019 = new QueryFindContractBefor2019(id_договор);

                    queruContract = queryFindId2019.Query();

                    // Получим данные по договору.
                    IQueryFind queryFindBefor2019 = new QueryFindIdContractBefore2019(id_договор);

                    queruPerson = queryFindBefor2019.Query();
                }
                else if (this.textBox2.Text != "" && this.txtИмя.Text != "")
                {
                    // Получим данные по услугам к договору.
                    IQueryFind queryFindId2019 = new QueryFindContractBefor2019(id_договор);

                    queruContract = queryFindId2019.Query();

                    // Получим данные по договору.
                    IQueryFind queryFindBefor2019 = new QueryFindIdContractBefore2019(id_договор);

                    queruPerson = queryFindBefor2019.Query();
                }
            }

            if(flarYear == 2019)
            {
                //flag2019AddWrite = true;
                // Поиск по номеру договора в 2019 году.
                if (this.textBox2.Text == "" && this.txtИмя.Text == "")
                {
                    IQueryFind queryPersonAdd = new QueryPersonAdd(id_договор);

                    // Получим данные по договору.
                    queruPerson = queryPersonAdd.Query();

                    IQueryFind queryFindContractAdd = new QueryContractAdd(id_договор);

                    // Получим данные по услугам к договору.
                    queruContract = queryFindContractAdd.Query();
                }

                if (this.textBox2.Text != "" && this.txtИмя.Text == "")
                {
                    IQueryFind queryPersonAdd = new QueryPersonAdd(id_договор);

                    // Получим данные по договору.
                    queruPerson = queryPersonAdd.Query();

                    IQueryFind queryFindContractAdd = new QueryContractAdd(id_договор);

                    // Получим данные по услугам к договору.
                    queruContract = queryFindContractAdd.Query();
                }

                if (this.textBox2.Text != "" && this.txtИмя.Text != "")
                {
                    IQueryFind queryPersonAdd = new QueryPersonAdd(id_договор);

                    // Получим данные по договору.
                    queruPerson = queryPersonAdd.Query();

                    IQueryFind queryFindContractAdd = new QueryContractAdd(id_договор);

                    // Получим данные по услугам к договору.
                    queruContract = queryFindContractAdd.Query();
                }

            }

            if(flarYear > 2020)
            {
                if (this.textBox2.Text == "" && this.txtИмя.Text == "")
                {
                    IQueryFind queryPerson2021 = new QueryPerson2021(id_договор);

                    // Получим данные по договору.
                    queruPerson = queryPerson2021.Query();

                    IQueryFind queryContract2021 = new QueryContract2021(id_договор);

                    queruContract = queryContract2021.Query();
                }
                else if (this.textBox2.Text != "" && this.txtИмя.Text == "")
                {
                    QueryPerson2021 queryPerson2021 = new QueryPerson2021(id_договор);

                    // Получим данные по договору.
                    queruPerson = queryPerson2021.Query();

                    QueryContract2021 queryContract2021 = new QueryContract2021(id_договор);

                    queruContract = queryContract2021.Query();
                }
                else if (this.textBox2.Text != "" && this.txtИмя.Text != "")
                {
                    QueryPerson2021 queryPerson2021 = new QueryPerson2021(id_договор);

                    // Получим данные по договору.
                    queruPerson = queryPerson2021.Query();

                    QueryContract2021 queryContract2021 = new QueryContract2021(id_договор);

                    queruContract = queryContract2021.Query();
                }
            }




            //else if(this.textBox2.Text != "" && this.txtИмя.Text != "" && flag2019AddWrite == true)
            //{
            //    // Получим данные по договору.
            //    queruPerson = @" SELECT top 1 ДоговорAdd.НомерДоговора, ДоговорAdd.ДатаДоговора, ЛьготникAdd.Фамилия, ЛьготникAdd.Имя, 
            //                                    ЛьготникAdd.Отчество, ЛьготникAdd.ДатаРождения, ЛьготникAdd.улица, ЛьготникAdd.НомерДома, 
            //                                    ЛьготникAdd.корпус, ЛьготникAdd.НомерКвартиры, ЛьготникAdd.СерияПаспорта, ЛьготникAdd.НомерПаспорта, 
            //                                    ЛьготникAdd.СерияДокумента, ЛьготникAdd.НомерДокумента, ЛьготникAdd.ДатаВыдачиДокумента, 
            //                                    ЛьготникAdd.ДатаВыдачиПаспорта,ДоговорAdd.ФлагПроверки FROM ДоговорAdd
            //                                    INNER JOIN УслугиПоДоговору
            //                                    ON ДоговорAdd.id_договор = УслугиПоДоговору.id_договор
            //                                    INNER JOIN ЛьготникAdd
            //                                    ON dbo.ДоговорAdd.id_льготник = dbo.ЛьготникAdd.id_льготник " +
            //                                   "where ДоговорAdd.id_договор = " + id_договор + " ";

            //    // Получим данные по услугам к договору.
            //    queruContract = "SELECT УслугиПоДоговоруAdd.НаименованиеУслуги, УслугиПоДоговоруAdd.цена, " +
            //                           "УслугиПоДоговоруAdd.Количество, УслугиПоДоговоруAdd.Сумма " +
            //                           "FROM ДоговорAdd INNER JOIN " +
            //                           "УслугиПоДоговоруAdd ON ДоговорAdd.id_договор = УслугиПоДоговоруAdd.id_договор " +
            //                           "where ДоговорAdd.id_договор = " + id_договор + " ";
            //}
            //else if(this.textBox2.Text != "" && this.txtИмя.Text == "" && flag2019AddWrite == true)
            //{
            //    // Получим данные по договору.
            //    queruPerson = @" SELECT top 1 ДоговорAdd.НомерДоговора, ДоговорAdd.ДатаДоговора, ЛьготникAdd.Фамилия, ЛьготникAdd.Имя, 
            //                                    ЛьготникAdd.Отчество, ЛьготникAdd.ДатаРождения, ЛьготникAdd.улица, ЛьготникAdd.НомерДома, 
            //                                    ЛьготникAdd.корпус, ЛьготникAdd.НомерКвартиры, ЛьготникAdd.СерияПаспорта, ЛьготникAdd.НомерПаспорта, 
            //                                    ЛьготникAdd.СерияДокумента, ЛьготникAdd.НомерДокумента, ЛьготникAdd.ДатаВыдачиДокумента, 
            //                                    ЛьготникAdd.ДатаВыдачиПаспорта,ДоговорAdd.ФлагПроверки FROM ДоговорAdd
            //                                    INNER JOIN УслугиПоДоговоруAdd
            //                                    ON ДоговорAdd.id_договор = УслугиПоДоговоруAdd.id_договор
            //                                    INNER JOIN ЛьготникAdd
            //                                    ON dbo.ДоговорAdd.id_льготник = dbo.ЛьготникAdd.id_льготник " +
            //                                   "where ДоговорAdd.id_договор = " + id_договор + " ";

            //    // Получим данные по услугам к договору.
            //    queruContract = "SELECT УслугиПоДоговоруAdd.НаименованиеУслуги, УслугиПоДоговоруAdd.цена, " +
            //                           "УслугиПоДоговоруAdd.Количество, УслугиПоДоговоруAdd.Сумма " +
            //                           "FROM ДоговорAdd INNER JOIN " +
            //                           "УслугиПоДоговоруAdd ON ДоговорAdd.id_договор = УслугиПоДоговоруAdd.id_договор " +
            //                           "where ДоговорAdd.id_договор = " + id_договор + " ";
            //}
            //else if (this.textBox2.Text != "" && this.txtИмя.Text == "" && flag2019AddWrite == false)
            //{
            //    // Получим данные по договору.
            //    queruPerson = @" SELECT top 1 Договор.НомерДоговора, Договор.ДатаДоговора, Льготник.Фамилия, Льготник.Имя, 
            //                                    Льготник.Отчество, Льготник.ДатаРождения, Льготник.улица, Льготник.НомерДома, 
            //                                    Льготник.корпус, Льготник.НомерКвартиры, Льготник.СерияПаспорта, Льготник.НомерПаспорта, 
            //                                    Льготник.СерияДокумента, Льготник.НомерДокумента, Льготник.ДатаВыдачиДокумента, 
            //                                    Льготник.ДатаВыдачиПаспорта,Договор.ФлагПроверки FROM Договор
            //                                    INNER JOIN УслугиПоДоговору
            //                                    ON Договор.id_договор = УслугиПоДоговору.id_договор
            //                                    INNER JOIN Льготник
            //                                    ON dbo.Договор.id_льготник = dbo.Льготник.id_льготник " +
            //                                   "where Договор.id_договор = " + id_договор + " ";

            //    // Получим данные по услугам к договору.
            //    queruContract = "SELECT УслугиПоДоговору.НаименованиеУслуги, УслугиПоДоговору.цена, " +
            //                           "УслугиПоДоговору.Количество, УслугиПоДоговору.Сумма " +
            //                           "FROM Договор INNER JOIN " +
            //                           "УслугиПоДоговору ON Договор.id_договор = УслугиПоДоговору.id_договор " +
            //                           "where Договор.id_договор = " + id_договор + " ";
            //}
            //else if(flag2019AddWrite == true && this.textBox1.Text != "")
            //{
            //    // Получим данные по услугам к договору.
            //    queruContract = "SELECT УслугиПоДоговоруAdd.НаименованиеУслуги, УслугиПоДоговоруAdd.цена, " +
            //                           "УслугиПоДоговоруAdd.Количество, УслугиПоДоговоруAdd.Сумма " +
            //                           "FROM ДоговорAdd INNER JOIN " +
            //                           "УслугиПоДоговоруAdd ON ДоговорAdd.id_договор = УслугиПоДоговоруAdd.id_договор " +
            //                           "where ДоговорAdd.id_договор = " + id_договор + " ";


            //    queruPerson = @" SELECT top 1 ДоговорAdd.НомерДоговора, ДоговорAdd.ДатаДоговора, ЛьготникAdd.Фамилия, ЛьготникAdd.Имя, 
            //                           ЛьготникAdd.Отчество, ЛьготникAdd.ДатаРождения, ЛьготникAdd.улица, ЛьготникAdd.НомерДома, 
            //                           ЛьготникAdd.корпус, ЛьготникAdd.НомерКвартиры, ЛьготникAdd.СерияПаспорта, ЛьготникAdd.НомерПаспорта, 
            //                           ЛьготникAdd.СерияДокумента, ЛьготникAdd.НомерДокумента, ЛьготникAdd.ДатаВыдачиДокумента, 
            //                           ЛьготникAdd.ДатаВыдачиПаспорта,ДоговорAdd.ФлагПроверки FROM ДоговорAdd
            //                           INNER JOIN УслугиПоДоговоруAdd
            //                           ON ДоговорAdd.id_договор = УслугиПоДоговоруAdd.id_договор
            //                           INNER JOIN ЛьготникAdd
            //                           ON dbo.ДоговорAdd.id_льготник = dbo.ЛьготникAdd.id_льготник " +
            //                           " where ДоговорAdd.id_договор = " + id_договор + " and ДоговорAdd.НомерДоговора = '" + this.textBox1.Text + "' ";
            //}
            //else if(flag2019AddWrite == false &&  this.textBox1.Text != "")
            //{
            //    //Получим данные по договору 
            //    queruContract = "SELECT УслугиПоДоговору.НаименованиеУслуги, УслугиПоДоговору.цена, " +
            //                           "УслугиПоДоговору.Количество, УслугиПоДоговору.Сумма " +
            //                           "FROM Договор INNER JOIN " +
            //                           "УслугиПоДоговору ON Договор.id_договор = УслугиПоДоговору.id_договор " +
            //                           "where Договор.id_договор = " + id_договор + " ";

            //    queruPerson = "SELECT top 1 Договор.НомерДоговора, Договор.ДатаДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, " +
            //                           "Льготник.ДатаРождения, Льготник.улица, Льготник.НомерДома, Льготник.корпус, Льготник.НомерКвартиры, " +
            //                           "Льготник.СерияПаспорта, Льготник.НомерПаспорта, Льготник.СерияДокумента, Льготник.НомерДокумента, " +
            //                           "Льготник.ДатаВыдачиДокумента, Льготник.ДатаВыдачиПаспорта,Договор.ФлагПроверки " +
            //                           "FROM Договор INNER JOIN " +
            //                           "УслугиПоДоговору ON Договор.id_договор = УслугиПоДоговору.id_договор " +
            //                           "INNER JOIN Льготник ON dbo.Договор.id_льготник = dbo.Льготник.id_льготник " +
            //                           "where Договор.id_договор = " + id_договор + " and Договор.НомерДоговора = '" + this.textBox1.Text + "' ";
            //}





            using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            {
                  con.Open();
                  SqlTransaction transact = con.BeginTransaction();

                  tabContract = ТаблицаБД.GetTableSQL(queruContract, "Договор", con, transact);

                  tabPerson = ТаблицаБД.GetTableSQL(queruPerson, "Льготник", con, transact);
            }
                //}

                FormDispContract fdc = new FormDispContract();
                fdc.ДанныеПоКонтракту = tabContract;
                fdc.ДанныеПоЛьготнику = tabPerson;
               
                fdc.TopMost = true;
                fdc.IdДоговор = id_договор;
                fdc.Show();


            //}
            //MessageBox.Show(i.ToString());
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex > -1)
                {
                    DataGridViewRow rows = this.dataGridView1.Rows[e.RowIndex];

                    //запишем id выбранного льготника

                    //id_льготникУдалить = Convert.ToInt32(rows.Cells["id_льготник"].Value);
                    //фиоЛьготникУдалить = rows.Cells["Фамилия"].Value.ToString().Trim();

                    this.dataGridView1.ClearSelection();
                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];


                }
            }
        }

        private void записатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id_договор = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_договор"].Value);

            string sTest = "update dbo.Договор " +
                           "set ДатаЗаписиДоговора = '"+ DateTime.Today.ToShortDateString() +"' " +
                           ",ФлагПроверки = 'True' " +
                           ",logWrite = '"+ MyAplicationIdentity.GetUses() +"' " +
                           ", ФлагВозвратНаДоработку = 0 " +
                           ", flagАнулирован = 0" +
                           ", ФлагАнулирован = 0 " +
                           ", flag2020 = 0 " +
                           ", flag2019AddWrite = 0 " + 
                           "where id_договор = " + id_договор +" ";

            Classes.ExecuteQuery.Execute(sTest);
            this.Close();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            // Очистим список происка.
            listPerson.Clear();

            // Выполним поиск.
            LoadAfterClickFind();

            #region Старый алгоритм пока оставим
            //string query = string.Empty;

            ////если прошли проверку
            //if (this.FlagValid == true && this.FlagCheck == false)
            //{
            //    if (this.textBox2.Text.Trim().Length > 0 && this.txtИмя.Text.Trim().Length == 0)
            //    {
            //        query = "SELECT Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория, sum(УслугиПоДоговору.Сумма) as 'Сумма',Договор.ДатаЗаписиДоговора,Договор.logWrite,Договор.НомерРеестра,Договор.НомерСчётФактрура, АктВыполненныхРабот.НомерАкта, АктВыполненныхРабот.ДатаПодписания " +
            //                  "FROM Договор INNER JOIN Льготник " +
            //                  "ON dbo.Договор.id_льготник = dbo.Льготник.id_льготник INNER JOIN " +
            //                  "dbo.ЛьготнаяКатегория ON dbo.Льготник.id_льготнойКатегории = dbo.ЛьготнаяКатегория.id_льготнойКатегории INNER JOIN " +
            //                  "dbo.УслугиПоДоговору ON dbo.Договор.id_договор = dbo.УслугиПоДоговору.id_договор " +
            //                  "inner join dbo.АктВыполненныхРабот " +
            //                  "on dbo.АктВыполненныхРабот.id_договор = Договор.id_договор " +
            //                  "where Договор.ФлагПроверки = 'True' and Льготник.Фамилия = '" + this.textBox2.Text + "' " +
            //                  "Group by Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория,Договор.ДатаЗаписиДоговора,Договор.logWrite,Договор.НомерРеестра,Договор.НомерСчётФактрура,АктВыполненныхРабот.НомерАкта, АктВыполненныхРабот.ДатаПодписания ";
            //    }
            //    else if (this.textBox2.Text.Trim().Length > 0 && this.txtИмя.Text.Trim().Length > 0)
            //    {
            //        query = "SELECT Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория, sum(УслугиПоДоговору.Сумма) as 'Сумма',Договор.ДатаЗаписиДоговора,Договор.logWrite,Договор.НомерРеестра,Договор.НомерСчётФактрура, АктВыполненныхРабот.НомерАкта, АктВыполненныхРабот.ДатаПодписания " +
            //                 "FROM Договор INNER JOIN Льготник " +
            //                 "ON dbo.Договор.id_льготник = dbo.Льготник.id_льготник INNER JOIN " +
            //                 "dbo.ЛьготнаяКатегория ON dbo.Льготник.id_льготнойКатегории = dbo.ЛьготнаяКатегория.id_льготнойКатегории INNER JOIN " +
            //                 "dbo.УслугиПоДоговору ON dbo.Договор.id_договор = dbo.УслугиПоДоговору.id_договор " +
            //                 "inner join dbo.АктВыполненныхРабот " +
            //                 "on dbo.АктВыполненныхРабот.id_договор = Договор.id_договор " +
            //                 "where Договор.ФлагПроверки = 'True' and  (Льготник.Фамилия = '"+ this.textBox2.Text.Trim() +"' and Льготник.Имя = '"+ this.txtИмя.Text.Trim() +"') " +
            //                 "Group by Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория,Договор.ДатаЗаписиДоговора,Договор.logWrite,Договор.НомерРеестра,Договор.НомерСчётФактрура,АктВыполненныхРабот.НомерАкта, АктВыполненныхРабот.ДатаПодписания ";
            //    }
                
            //}
            ////else

            //if (this.FlagValid == false && this.FlagCheck == false)
            //{
            //    query = "SELECT Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория, sum(УслугиПоДоговору.Сумма) as 'Сумма',Договор.ДатаЗаписиДоговора,Договор.logWrite,Договор.НомерРеестра,Договор.НомерСчётФактрура " +
            //              "FROM Договор INNER JOIN Льготник " +
            //              "ON dbo.Договор.id_льготник = dbo.Льготник.id_льготник INNER JOIN " +
            //              "dbo.ЛьготнаяКатегория ON dbo.Льготник.id_льготнойКатегории = dbo.ЛьготнаяКатегория.id_льготнойКатегории INNER JOIN " +
            //              "dbo.УслугиПоДоговору ON dbo.Договор.id_договор = dbo.УслугиПоДоговору.id_договор " +
            //               "where Договор.ФлагПроверки = 'False' and Льготник.Фамилия = '" + this.textBox2.Text + "' and Льготник.Имя = '" + this.txtИмя.Text.Trim() + "' " +
            //              "Group by Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория,Договор.ДатаЗаписиДоговора,Договор.logWrite,Договор.НомерРеестра,Договор.НомерСчётФактрура ";
            //}

            //if (this.FlagValid == false && this.FlagCheck == true)
            //{
            //    if (this.textBox2.Text.Trim().Length > 0 && this.txtИмя.Text.Trim().Length == 0)
            //    {
            //        query = "SELECT Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория, sum(УслугиПоДоговору.Сумма) as 'Сумма',Договор.ДатаЗаписиДоговора,Договор.logWrite,Договор.НомерРеестра,Договор.НомерСчётФактрура " +
            //                  "FROM Договор INNER JOIN Льготник " +
            //                  "ON dbo.Договор.id_льготник = dbo.Льготник.id_льготник INNER JOIN " +
            //                  "dbo.ЛьготнаяКатегория ON dbo.Льготник.id_льготнойКатегории = dbo.ЛьготнаяКатегория.id_льготнойКатегории INNER JOIN " +
            //                  "dbo.УслугиПоДоговору ON dbo.Договор.id_договор = dbo.УслугиПоДоговору.id_договор " +
            //                  "where Договор.ФлагПроверки = 'False' and Льготник.Фамилия = '" + this.textBox2.Text + "' " + // and Льготник.Имя = '" + this.txtИмя.Text.Trim() + "' " +
            //                  "Group by Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория,Договор.ДатаЗаписиДоговора,Договор.logWrite,Договор.НомерРеестра,Договор.НомерСчётФактрура ";
            //    }
            //    else if (this.textBox2.Text.Trim().Length > 0 && this.txtИмя.Text.Trim().Length > 0)
            //    {
            //        query = "SELECT Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория, sum(УслугиПоДоговору.Сумма) as 'Сумма',Договор.ДатаЗаписиДоговора,Договор.logWrite,Договор.НомерРеестра,Договор.НомерСчётФактрура " +
            //                  "FROM Договор INNER JOIN Льготник " +
            //                  "ON dbo.Договор.id_льготник = dbo.Льготник.id_льготник INNER JOIN " +
            //                  "dbo.ЛьготнаяКатегория ON dbo.Льготник.id_льготнойКатегории = dbo.ЛьготнаяКатегория.id_льготнойКатегории INNER JOIN " +
            //                  "dbo.УслугиПоДоговору ON dbo.Договор.id_договор = dbo.УслугиПоДоговору.id_договор " +
            //                  "where Договор.ФлагПроверки = 'False' and Льготник.Фамилия = '" + this.textBox2.Text + "'  and Льготник.Имя = '" + this.txtИмя.Text.Trim() + "' " +
            //                  "Group by Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория,Договор.ДатаЗаписиДоговора,Договор.logWrite,Договор.НомерРеестра,Договор.НомерСчётФактрура ";
            //    }

            //}

            //using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            //{
            //    con.Open();
            //    SqlTransaction transact = con.BeginTransaction();

            //    //DataTable dtContract = ТаблицаБД.GetTableSQL(query, "ТаблицаДоговоров", con, transact);
            //    SqlCommand com = new SqlCommand(query, con);
            //    com.Transaction = transact;

            //    // Создадим временную таблицу для отображения данных в DataGrid.
            //    DataTable tab = new DataTable();
            //    DataColumn col0 = new DataColumn("id_договор", typeof(int));
            //    tab.Columns.Add(col0);
            //    DataColumn col1 = new DataColumn("НомерДоговора", typeof(string));
            //    tab.Columns.Add(col1);
            //    DataColumn col2 = new DataColumn("Фамилия", typeof(string));
            //    tab.Columns.Add(col2);

            //    DataColumn col3 = new DataColumn("Имя", typeof(string));
            //    tab.Columns.Add(col3);
            //    DataColumn col4 = new DataColumn("Отчество", typeof(string));
            //    tab.Columns.Add(col4);

            //    DataColumn col5 = new DataColumn("ЛьготнаяКатегория", typeof(string));
            //    tab.Columns.Add(col5);
            //    DataColumn col6 = new DataColumn("Сумма", typeof(string));
            //    tab.Columns.Add(col6);

            //    DataColumn col7 = new DataColumn("Дата", typeof(string));
            //    tab.Columns.Add(col7);
            //    DataColumn col8 = new DataColumn("КтоЗаписал", typeof(string));
            //    tab.Columns.Add(col8);

            //    // отобразим номер счёт факутры и номер реестра
            //    DataColumn col9 = new DataColumn("НомерРеестра", typeof(string));
            //    tab.Columns.Add(col9);

            //    DataColumn col10 = new DataColumn("НомерСчётФактрура", typeof(string));
            //    tab.Columns.Add(col10);

            //    if (this.FlagValid == true && this.FlagCheck == false)
            //    {
            //        DataColumn col11 = new DataColumn("НомерАкта", typeof(string));
            //        tab.Columns.Add(col11);

            //        DataColumn col12 = new DataColumn("ДатаПодписания", typeof(string));
            //        tab.Columns.Add(col12);
            //    }


            //    SqlDataReader read = com.ExecuteReader();
            //    while (read.Read())
            //    {
            //        DataRow row = tab.NewRow();
            //        row["id_договор"] = read["id_договор"].ToString().Trim();
            //        row["НомерДоговора"] = read["НомерДоговора"].ToString().Trim();
            //        row["Фамилия"] = read["Фамилия"].ToString().Trim();
            //        row["Имя"] = read["Имя"].ToString().Trim();
            //        row["Отчество"] = read["Отчество"].ToString().Trim();
            //        row["ЛьготнаяКатегория"] = read["ЛьготнаяКатегория"].ToString().Trim();
            //        row["Сумма"] = Convert.ToDecimal(read["Сумма"]).ToString("c").Trim();
            //        //string sComp = row["Дата"].ToString();
            //        if (read["ДатаЗаписиДоговора"] != DBNull.Value)//
            //        {
            //            row["Дата"] = Convert.ToDateTime(read["ДатаЗаписиДоговора"]).ToShortDateString().Trim();
            //        }
            //        else
            //        {

            //        }
            //        if (read["logWrite"] != DBNull.Value)
            //        {
            //            row["КтоЗаписал"] = read["logWrite"].ToString().Trim();
            //        }

            //        // отобразми номер счёт фактуры
            //        if (read["НомерРеестра"] != DBNull.Value && read["НомерРеестра"].ToString().Trim() != "NULL")
            //        {
            //            row["НомерРеестра"] = read["НомерРеестра"].ToString().Trim();
            //        }

            //        // отобразми номер реестра
            //        if (read["НомерСчётФактрура"] != DBNull.Value && read["НомерСчётФактрура"].ToString().Trim() != "NULL")
            //        {
            //            row["НомерСчётФактрура"] = read["НомерСчётФактрура"].ToString().Trim();
            //        }

            //        if (this.FlagValid == true && this.FlagCheck == false)
            //        {
            //            if (read["НомерАкта"] != DBNull.Value)// && read["НомерАкта"] != null)
            //            {
            //                var asd = read["НомерАкта"].Do(x => x, "").ToString().Trim();

            //                row["НомерАкта"] = read["НомерАкта"].ToString().Trim();
            //            }

            //            if (read["ДатаПодписания"] != DBNull.Value)// && read["НомерАкта"] != null)
            //            {
            //                var asd = read["ДатаПодписания"].Do(x => x, "").ToString().Trim();

            //                row["ДатаПодписания"] = Convert.ToDateTime(read["ДатаПодписания"]).ToShortDateString().Trim();
            //            }
            //        }


            //        tab.Rows.Add(row);
            //    }

            //    this.dataGridView1.DataSource = tab;

            //    this.dataGridView1.Columns["id_договор"].Width = 100;
            //    this.dataGridView1.Columns["id_договор"].Visible = false;
            //    this.dataGridView1.Columns["НомерДоговора"].Width = 100;
            //    this.dataGridView1.Columns["НомерДоговора"].DisplayIndex = 0;

            //    this.dataGridView1.Columns["Фамилия"].Width = 150;
            //    this.dataGridView1.Columns["Фамилия"].DisplayIndex = 1;

            //    this.dataGridView1.Columns["Имя"].Width = 150;
            //    this.dataGridView1.Columns["Имя"].DisplayIndex = 2;

            //    this.dataGridView1.Columns["Отчество"].Width = 150;
            //    this.dataGridView1.Columns["Отчество"].DisplayIndex = 3;

            //    this.dataGridView1.Columns["ЛьготнаяКатегория"].Width = 200;
            //    this.dataGridView1.Columns["ЛьготнаяКатегория"].DisplayIndex = 4;
            //    this.dataGridView1.Columns["ЛьготнаяКатегория"].HeaderText = "Льготная категория";

            //    this.dataGridView1.Columns["Сумма"].Width = 100;
            //    this.dataGridView1.Columns["Сумма"].DisplayIndex = 5;

            //    this.dataGridView1.Columns["Дата"].Width = 120;
            //    this.dataGridView1.Columns["Дата"].DisplayIndex = 6;
            //    this.dataGridView1.Columns["Дата"].HeaderText = "Дата записи проекта договора в нашу БД";
               
            //    this.dataGridView1.Columns["НомерРеестра"].Width = 80;
            //    this.dataGridView1.Columns["НомерРеестра"].DisplayIndex = 7;
            //    this.dataGridView1.Columns["НомерРеестра"].HeaderText = "Номер реестра";

            //    this.dataGridView1.Columns["НомерСчётФактрура"].Width = 100;
            //    this.dataGridView1.Columns["НомерСчётФактрура"].DisplayIndex = 8;
            //    this.dataGridView1.Columns["НомерСчётФактрура"].HeaderText = "Номер счет фактуры";

            //    if (this.FlagValid == true && this.FlagCheck == false)
            //    {
            //        this.dataGridView1.Columns["НомерАкта"].Width = 100;
            //        this.dataGridView1.Columns["НомерАкта"].DisplayIndex = 9;
            //        this.dataGridView1.Columns["НомерАкта"].HeaderText = "Номер акта";
            //        this.dataGridView1.Columns["НомерАкта"].Visible = false;

            //        this.dataGridView1.Columns["ДатаПодписания"].Width = 100;
            //        this.dataGridView1.Columns["ДатаПодписания"].DisplayIndex = 10;
            //        this.dataGridView1.Columns["ДатаПодписания"].HeaderText = "Дата подписания акта";

            //        this.dataGridView1.Columns["КтоЗаписал"].Width = 100;
            //        this.dataGridView1.Columns["КтоЗаписал"].DisplayIndex = 11;

            //    }
            //    else
            //    {
            //    this.dataGridView1.Columns["КтоЗаписал"].Width = 100;
            //    this.dataGridView1.Columns["КтоЗаписал"].DisplayIndex = 9;
            //    }


            //}
            #endregion
        }

        private void договорОтменитьПроверкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flagAct = false;
            // Получим текущий договор.
            int idДоговор = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_договор"].Value);

            bool flagWrite2019 = false;

            flagWrite2019 = Convert.ToBoolean(this.dataGridView1.CurrentRow.Cells["flag2019AddWrite"].Value);

            string queryValidAct = string.Empty;

            if(flagWrite2019 == false)
            {
                queryValidAct = "select COUNT(id_акт) as 'КоличествоАктов' from АктВыполненныхРабот " +
                                   "where id_договор in ( " +
                                    "select id_договор  from Договор " +
                                    "where id_договор = " + idДоговор + " ) ";
            }
            else
            {
                queryValidAct = @"select  COUNT(id_акт) as 'КоличествоАктов' from АктВыполненныхРаботAdd 
                                        inner join ДоговорAdd
                                        on АктВыполненныхРаботAdd.id_договор = ДоговорAdd.id_ТабДоговор
                                         where ДоговорAdd.id_договор = " + idДоговор + " ";
            

            //queryValidAct = "select COUNT(id_акт) as 'КоличествоАктов' from АктВыполненныхРаботAdd " +
            //                       "where id_договор in ( " +
            //                        "select id_договор  from ДоговорAdd " +
            //                        "where id_договор = " + idДоговор + " ) ";
            }

            DataTable tabAct = ТаблицаБД.GetTableSQL(queryValidAct, "ТаблицаАкт");

            StringBuilder build = new StringBuilder();

            if (Convert.ToInt32(tabAct.Rows[0]["КоличествоАктов"]) > 0)
            {
                flagAct = true;

                string queryValidNumAct = string.Empty;

                if (flagWrite2019 == false)
                {
                    //queryValidNumAct = "select НомерАкта,ДатаПодписания from АктВыполненныхРабот " +
                    //                      "where id_договор in ( " +
                    //                      "select id_договор  from Договор " +
                    //                      "where id_договор = " + idДоговор + " ) ";

                    queryValidNumAct = @"select НомерАкта,ДатаПодписания,СуммаАктаВыполненныхРабот from АктВыполненныхРабот 
                                        inner join Договор
                                        on АктВыполненныхРабот.id_договор = Договор.id_договор
                                         where ДоговорAdd.id_договор = " + idДоговор + " ";
                }
                else
                {
                    //queryValidNumAct = "select НомерАкта,ДатаПодписания from АктВыполненныхРаботAdd " +
                    //                      "where id_договор in ( " +
                    //                      "select id_договор  from ДоговорAdd " +
                    //                      "where id_договор = " + idДоговор + " ) ";

                    queryValidNumAct = @"select НомерАкта,ДатаПодписания,СуммаАктаВыполненныхРабот from АктВыполненныхРаботAdd 
                                        inner join ДоговорAdd
                                        on АктВыполненныхРаботAdd.id_договор = ДоговорAdd.id_ТабДоговор
                                         where ДоговорAdd.id_договор = " + idДоговор + " ";
                }

                DataTable tabNumAct = ТаблицаБД.GetTableSQL(queryValidNumAct, "ТаблицаАктНомер");

                // Запишем номер акта
                build.Append("Номер акта - " + tabNumAct.Rows[0]["НомерАкта"].ToString().Trim());
                build.Append(" от " + Convert.ToDateTime(tabNumAct.Rows[0]["ДатаПодписания"]).ToShortDateString());

                // Проверим есть ли акт.
                // Так как мы заливали 2019 год без таблицы акт то проверям по наличию суммы акта в таблице договор
                if (Convert.ToDecimal(tabNumAct.Rows[0]["СуммаАктаВыполненныхРабот"]) != 0.0m)
                {
                    flagAct = true;
                }
                else
                {
                    flagAct = false;
                }
            }

            //// Выведим диалоговое окно.
            //FormMessageAct formMessAct = new FormMessageAct();
            //formMessAct.НомерАкта = build.ToString();
            //formMessAct.ShowDialog();
            DialogResult dialogResult = MessageBox.Show("Удалить акт выполненных работ " + build.ToString().Trim(), "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            string test = "test";

            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {

                string query = string.Empty;

                string user = MyAplicationIdentity.GetUses();

                if (flagWrite2019 == false)
                {
                        query = " declare @id int " +
                                "set @id = " + idДоговор + " " +
                                "delete АктВыполненныхРабот " +
                                "where id_договор in ( " +
                                "select id_договор  from Договор " +
                                "where id_договор = @id " +
                                ") " +
                                "update Договор " +
                                "set ФлагПроверки = 'True', " +
                                "ДатаАктаВыполненныхРабот = '19000101', " +
                                "СуммаАктаВыполненныхРабот = 0.0, " +
                                "НомерРеестра =  null, " +
                                "ДатаРеестра = null, " +
                                "НомерСчётФактрура = null, " +
                                "ДатаСчётФактура = null, " +
                                "logWrite = '" + user + "',  " +
                                " ФлагВозвратНаДоработку = 1 " +
                                "where id_договор in ( " +
                                "select id_договор  from Договор " +
                                "where id_договор = @id) ";
                }
                else
                {
                    query = "declare @id int " +
                               "set @id = " + idДоговор + " " +
                               @" delete Act
                                from АктВыполненныхРаботAdd as Act
                                inner join ДоговорAdd
                                on ДоговорAdd.id_договор = Act.id_договор
                                where ДоговорAdd.id_договор = @id
                                delete Act2
                                from АктВыполненныхРабот as Act2
                                inner join ДоговорAdd
                                on ДоговорAdd.id_ТабДоговор = Act2.id_договор
                                where ДоговорAdd.id_договор = @id " +
                               "update ДоговорAdd " +
                               "set ФлагПроверки = 'True', " +
                               " ФлагНаличияАкта = 1 " + 
                               "ДатаАктаВыполненныхРабот = '19000101', " +
                               "СуммаАктаВыполненныхРабот = 0.0, " +
                               "НомерРеестра =  null, " +
                               "ДатаРеестра = null, " +
                               "НомерСчётФактрура = null, " +
                               "ДатаСчётФактура = null, " +
                               "logWrite = '" + user + "',  " +
                                " ФлагВозвратНаДоработку = 1 " +
                               "where id_договор in ( " +
                               "select id_договор  from ДоговорAdd " +
                               "where id_договор = @id) ";
                }

                // Выполним запрос.
                using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
                {
                    SqlTransaction transact = con.BeginTransaction();
                    con.Open();

                    Classes.ExecuteQuery.Execute(query, con, transact);
                }

                // Временно переведем в режимокрытия прошедших проверку.
                this.FlagValid = true;

                // Временно введем текст, что бы отработали условия отображения льготников при поиске.
                this.textBox2.Text = "Обновление";

                // Обновим DataGrid.
                LoadAfterClickFind();

                // Очистим поле ввода Фамилии.
                this.textBox2.Text = string.Empty;

                // 
                this.FlagValid = false;

            }

        }


        /// <summary>
        /// Вывод результата поиска льготника.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private void ExecuteFind(object query)
        {
            StringParametr stringParametr = (StringParametr)query;

            //// Список для хранения результатов поиска.
            //List<FindPersonNumContractItem> listPerson = new List<FindPersonNumContractItem>();

            using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            {
                con.Open();
                //SqlTransaction transact = con.BeginTransaction();

                //DataTable dtContract = ТаблицаБД.GetTableSQL(query, "ТаблицаДоговоров", con, transact);
                SqlCommand com = new SqlCommand(stringParametr.Query, con);
                //com.Transaction = transact;

                // Создадим временную таблицу для отображения данных в DataGrid.
                DataTable tab = new DataTable();
                DataColumn col0 = new DataColumn("id_договор", typeof(int));
                tab.Columns.Add(col0);
                DataColumn col1 = new DataColumn("НомерДоговора", typeof(string));
                tab.Columns.Add(col1);
                DataColumn col2 = new DataColumn("Фамилия", typeof(string));
                tab.Columns.Add(col2);

                DataColumn col3 = new DataColumn("Имя", typeof(string));
                tab.Columns.Add(col3);
                DataColumn col4 = new DataColumn("Отчество", typeof(string));
                tab.Columns.Add(col4);

                DataColumn col5 = new DataColumn("ЛьготнаяКатегория", typeof(string));
                tab.Columns.Add(col5);
                DataColumn col6 = new DataColumn("Сумма", typeof(string));
                tab.Columns.Add(col6);

                DataColumn col7 = new DataColumn("Дата", typeof(string));
                tab.Columns.Add(col7);
                DataColumn col8 = new DataColumn("КтоЗаписал", typeof(string));
                tab.Columns.Add(col8);

                // отобразим номер счёт факутры и номер реестра
                DataColumn col9 = new DataColumn("НомерРеестра", typeof(string));
                tab.Columns.Add(col9);

                DataColumn col10 = new DataColumn("НомерСчётФактрура", typeof(string));
                tab.Columns.Add(col10);

                if (this.FlagValid == true && this.FlagCheck == false)
                {
                    DataColumn col11 = new DataColumn("НомерАкта", typeof(string));
                    tab.Columns.Add(col11);

                    DataColumn col12 = new DataColumn("ДатаПодписания", typeof(string));
                    tab.Columns.Add(col12);

                    DataColumn col13 = new DataColumn("flag2019AddWrite", typeof(bool));
                    tab.Columns.Add(col13);
                }
                else
                {
                    DataColumn col14 = new DataColumn("flag2019AddWrite", typeof(bool));
                    tab.Columns.Add(col14);
                }

                DataColumn col15 = new DataColumn("Год", typeof(int));
                tab.Columns.Add(col15);

                DataColumn col16 = new DataColumn("flagАнулирован", typeof(bool));
                tab.Columns.Add(col16);

                SqlDataReader read = com.ExecuteReader();
                while (read.Read())
                {
                    DataRow row = tab.NewRow();

                    // Пункт результата поиска.
                    FindPersonNumContractItem it = new FindPersonNumContractItem();

                    it.id_договор = read["id_договор"].ToString().Trim();
                    it.НомерДоговора = read["НомерДоговора"].ToString().Trim();
                    it.Фамилия = read["Фамилия"].ToString().Trim();
                    it.Имя = read["Имя"].ToString().Trim();
                    it.Отчество = read["Отчество"].ToString().Trim();
                    it.ЛьготнаяКатегория = read["ЛьготнаяКатегория"].ToString().Trim();
                    it.Сумма = Convert.ToDecimal(read["Сумма"]).ToString("c").Trim();
                    if (read["ДатаЗаписиДоговора"] != DBNull.Value)//
                    {
                        it.Дата = Convert.ToDateTime(read["ДатаЗаписиДоговора"]).ToShortDateString().Trim();
                    }
                    if (read["logWrite"] != DBNull.Value)
                    {
                        it.КтоЗаписал = read["logWrite"].ToString().Trim();
                    }
                    if (read["НомерРеестра"] != DBNull.Value && read["НомерРеестра"].ToString().Trim() != "NULL")
                    {
                        it.НомерРеестра = read["НомерРеестра"].ToString().Trim();
                    }
                    if (read["НомерСчётФактрура"] != DBNull.Value && read["НомерСчётФактрура"].ToString().Trim() != "NULL")
                    {
                        it.НомерСчётФактрура = read["НомерСчётФактрура"].ToString().Trim();
                    }

                    if (this.FlagValid == true && this.FlagCheck == false)
                    {
                        if (read["НомерАкта"] != DBNull.Value)// && read["НомерАкта"] != null)
                        {
                            it.НомерАкта = read["НомерАкта"].ToString().Trim();
                        }

                        if (read["ДатаПодписания"] != DBNull.Value)// && read["НомерАкта"] != null)
                        {
                            it.ДатаПодписания = Convert.ToDateTime(read["ДатаПодписания"]).ToShortDateString().Trim();
                        }
                    }

                    if (read["flag2019AddWrite"] != DBNull.Value)
                    {
                        it.flag2019AddWrite = Convert.ToBoolean(read["flag2019AddWrite"]);
                    }

                    if (read["Год"] != DBNull.Value && read["Год"].ToString().Trim() != "NULL")
                    {
                        it.Год = Convert.ToUInt16(read["Год"]);
                    }

                    if (read["flagАнулирован"] != DBNull.Value && read["flagАнулирован"].ToString().Trim() != "NULL")
                    {
                        var test = Convert.ToBoolean(read["flagАнулирован"]);

                        it.flagАнулирован = Convert.ToBoolean(read["flagАнулирован"]);
                    }

                    listPerson.Add(it);
                }

            }

            //return listPerson;
        }

        private void LoadAfterClickFind()
        {
            // Обнулим результат поиска.
            this.dataGridView1.DataSource = null;

            // Переменная для хранения SQL запроса для поиска льготника.
            string query = string.Empty;

            // Вспомогательный флаг позволяющий обойти старый код.
            bool flagExecute = false;

            //если прошли проверку
            if (this.FlagValid == true && this.FlagCheck == false)
            {
               
                // Поиск льготников прошедших по фамилии.
                if (this.textBox2.Text.Trim().Length > 0 && this.txtИмя.Text.Trim().Length == 0)
                {
                    // Переменная для хранения фамилии льготника которого хотим найти в БД.
                    string personFamili = string.Empty;

                    personFamili = this.textBox2.Text.Trim();

                    // Поиск льготников до 2019 года.
                    //IFindPerson findPersonTo2019 = new FindPersonFamiliTo2019(personFamili);
                    //string queryTo2019 = findPersonTo2019.Query();

                    //StringParametr stringParametr = new StringParametr();
                    //stringParametr.Query = queryTo2019;

                    //// Поиск льготников по таблицам NameTableAdd.
                    //IFindPerson findPerson2019Add = new FindPersonFamiliTableAdd(personFamili);
                    //string query2019Add = findPerson2019Add.Query();

                    //StringParametr stringParametr2019Add = new StringParametr();
                    //stringParametr2019Add.Query = query2019Add;

                    //// Поиск льготников по таблицам БД 2019 год.
                    //IFindPerson findPerson2019 = new FindPerson2019(personFamili);
                    //string query2019 = findPerson2019.Query();

                    //StringParametr stringParametr2019 = new StringParametr();
                    //stringParametr2019.Query = query2019;

                    // Поиск льгот ника по позже 2019 года.
                    IFindPerson findPerson2019Afftar = new FIndPersonFamili2019Aftar(personFamili);
                    string query2019Aftar = findPerson2019Afftar.Query();

                    StringParametr stringParametr2019Aftar = new StringParametr();
                    stringParametr2019Aftar.Query = query2019Aftar;

                    // Поиск льготника прошедшего проверку по фмаилии в 2021 году.
                    FindPersonFamilyValidTrue2021 findPersonFamilyValidTrue2021 = new FindPersonFamilyValidTrue2021(personFamili);
                    string queryFindPerson2021FamilyTrue = findPersonFamilyValidTrue2021.Query();

                    StringParametr stringfindPersonFamilyValidTrue2021 = new StringParametr();
                    stringfindPersonFamilyValidTrue2021.Query = queryFindPerson2021FamilyTrue;

                    // Выполнинм поиск льготников до 2019 года.
                    //ExecuteFind(stringParametr);

                    //// Выполнинм поиск льготников 2019 по таблицам TableName2019 года.
                    //ExecuteFind(stringParametr2019Add);

                    // Выполнинм поиск льготников 2019 по таблицам базы данных года.
                    //ExecuteFind(stringParametr2019);

                    // Выполнинм поиск льготников после 2019 по таблицам базы данных года.
                    ExecuteFind(stringParametr2019Aftar);

                    ExecuteFind(stringfindPersonFamilyValidTrue2021);

                    flagExecute = true;

                    //IFindPerson findPerson = new FindPersonFamili(this.textBox2.Text.Trim());
                    //query = findPerson.Query();
                }
                // Поиск логотников прошедших проверку по фамилии и имени.
                else if (this.textBox2.Text.Trim().Length > 0 && this.txtИмя.Text.Trim().Length > 0)
                {
                    //// Поиск льготника по Фамилии и имени.
                    //IFindPerson findPerson = new FindPersonByFamiliName(this.textBox2.Text.Trim(), this.txtИмя.Text.Trim());
                    //query = findPerson.Query();

                    // Переменная для хранения фамилии льготника которого хотим найти в БД.
                    string personFamili = string.Empty;

                    string personName = string.Empty;

                    personFamili = this.textBox2.Text.Trim();

                    personName = this.txtИмя.Text.Trim();

                    // Поиск льготников до 2019 года.
                    //IFindPerson findPersonTo2019 = new FindPersonFioTo2019(personFamili,personName);
                    //string queryTo2019 = findPersonTo2019.Query();

                    //StringParametr stringParametr = new StringParametr();
                    //stringParametr.Query = queryTo2019;

                    // Все что ранеьше 2020 года убираем из поиска.
                    //// Поиск льготников по таблицам NameTableAdd.
                    //IFindPerson findPerson2019Add = new FindPersonFioTableAdd(personFamili, personName);
                    //string query2019Add = findPerson2019Add.Query();

                    //StringParametr stringParametr2019Add = new StringParametr();
                    //stringParametr2019Add.Query = query2019Add;

                    //// Поиск льготников по таблицам БД 2019 год.
                    //IFindPerson findPerson2019 = new FindPersonFio2019(personFamili, personName);
                    //string query2019 = findPerson2019.Query();

                    //StringParametr stringParametr2019 = new StringParametr();
                    //stringParametr2019.Query = query2019;

                    //// Поиск льгот ника по позже 2019 года.
                    IFindPerson findPerson2019Afftar = new FIndPersonFio2019Aftar(personFamili, personName);
                    string query2019Aftar = findPerson2019Afftar.Query();

                    StringParametr stringParametr2019Aftar = new StringParametr();
                    stringParametr2019Aftar.Query = query2019Aftar;

                    IFindPerson findPersonFio2021 = new FindPersonFioValidate2021(personFamili, personName);
                    string query2021Fio = findPersonFio2021.Query();

                    StringParametr stringParametrfindPersonFio2021 = new StringParametr();
                    stringParametrfindPersonFio2021.Query = query2021Fio;
                    

                    // Выполнинм поиск льготников до 2019 года.
                    //ExecuteFind(stringParametr);

                    // Выполнинм поиск льготников 2019 по таблицам TableName2019 года.
                    //ExecuteFind(stringParametr2019Add);

                    // Выполнинм поиск льготников 2019 по таблицам базы данных года.
                    //ExecuteFind(stringParametr2019);

                    // Выполнинм поиск льготников после 2019 по таблицам базы данных года.
                    //ExecuteFind(stringParametr2019Aftar);

                    ExecuteFind(stringParametrfindPersonFio2021);

                    flagExecute = true;
                }

            }
            //else
            
            if (this.FlagValid == false && this.FlagCheck == false)
            {
                // Не прошли проверку. Поиск по Фамилии.
                if (this.textBox2.Text.Trim().Length > 0 && this.txtИмя.Text.Trim().Length == 0)
                {
                    // Переменная для хранения фамилии льготника которого хотим найти в БД.
                    string personFamili = string.Empty;

                    personFamili = this.textBox2.Text.Trim();

                    //// Поиск льготников до 2019 года.
                    //IFindPerson findPersonTo2019 = new FindPersonFamiliTo2019NoValid(personFamili);
                    //string queryTo2019 = findPersonTo2019.Query();

                    //StringParametr stringParametr = new StringParametr();
                    //stringParametr.Query = queryTo2019;

                    //// Поиск льготников по таблицам NameTableAdd 2019 год.
                    //IFindPerson findPerson2019Add = new FindPersonFamiliTableAddNoValid(personFamili);
                    //string query2019Add = findPerson2019Add.Query();

                    //StringParametr stringParametr2019Add = new StringParametr();
                    //stringParametr2019Add.Query = query2019Add;

                    //// Поиск льготников по таблицам БД 2019 год.
                    //IFindPerson findPerson2019 = new FindPersonFamili2019NoValid(personFamili);
                    //string query2019 = findPerson2019.Query();

                    //StringParametr stringParametr2019 = new StringParametr();
                    //stringParametr2019.Query = query2019;

                    // Поиск льгот ника по позже 2019 года.
                    IFindPerson findPerson2019Afftar = new FIndPersonFamili2019AftarNoValid(personFamili);
                    string query2019Aftar = findPerson2019Afftar.Query();

                    StringParametr stringParametr2019Aftar = new StringParametr();
                    stringParametr2019Aftar.Query = query2019Aftar;

                    IFindPerson findPersonFamiliNoValid2021 = new IFindPersonFamiliNoValid2021(personFamili);
                    string queryFamiliNoValid2021 = findPersonFamiliNoValid2021.Query();

                    StringParametr stringParametrFamiliNoValid2021 = new StringParametr();
                    stringParametrFamiliNoValid2021.Query = queryFamiliNoValid2021;

                    // Выполнинм поиск льготников до 2019 года.
                    //ExecuteFind(stringParametr);

                    //Выполнинм поиск льготников 2019 по таблицам TableName2019 года.
                    //ExecuteFind(stringParametr2019Add);

                    // Выполнинм поиск льготников 2019 по таблицам базы данных года.
                    //ExecuteFind(stringParametr2019);

                    // Выполнинм поиск льготников после 2019 по таблицам базы данных года.
                    ExecuteFind(stringParametr2019Aftar);

                    ExecuteFind(stringParametrFamiliNoValid2021);

                    flagExecute = true;

                }
                else if (this.textBox2.Text.Trim().Length > 0 && this.txtИмя.Text.Trim().Length > 0)
                {
                    //IFindPerson findPerson = new FindPersonFirstNameNameNoValid(this.textBox2.Text.Trim(), this.txtИмя.Text.Trim());
                    //query = findPerson.Query();

                    // Поиск льготников не прошедших проверку по Фио.
                    string personFamili = string.Empty;

                    string personName = string.Empty;

                    personFamili = this.textBox2.Text.Trim();

                    personName = this.txtИмя.Text.Trim();

                    // Поиск льготников до 2019 года.
                    //IFindPerson findPersonTo2019 = new FindPersonFioTo2019NoValid(personFamili, personName);
                    //string queryTo2019 = findPersonTo2019.Query();

                    //StringParametr stringParametr = new StringParametr();
                    //stringParametr.Query = queryTo2019;

                    //// Поиск льготников по таблицам NameTableAdd.
                    //IFindPerson findPerson2019Add = new FindPersonFioTableAddNoValid(personFamili, personName);
                    //string query2019Add = findPerson2019Add.Query();

                    //StringParametr stringParametr2019Add = new StringParametr();
                    //stringParametr2019Add.Query = query2019Add;

                    //// Поиск льготников по таблицам БД 2019 год.
                    //IFindPerson findPerson2019 = new FindPersonFio2019NoValid(personFamili, personName);
                    //string query2019 = findPerson2019.Query();

                    //StringParametr stringParametr2019 = new StringParametr();
                    //stringParametr2019.Query = query2019;

                    //// Поиск льгот ника по позже 2019 года.
                    IFindPerson findPerson2019Afftar = new FIndPersonFio2019AftarNoValid(personFamili, personName);
                    string query2019Aftar = findPerson2019Afftar.Query();

                    StringParametr stringParametr2019Aftar = new StringParametr();
                    stringParametr2019Aftar.Query = query2019Aftar;

                    // Поиск льготника по Фамилии и имени в таблицах 2021.
                    IFindPerson findPerson2021FioNoValid = new FindPersonFioNoValid2021(personFamili, personName);
                    string queryFindPersonFio2021NoValid = findPerson2021FioNoValid.Query();

                    StringParametr stringfindPerson2021FioNoValid = new StringParametr();
                    stringfindPerson2021FioNoValid.Query = queryFindPersonFio2021NoValid;

                    // Выполнинм поиск льготников до 2019 года.
                    //ExecuteFind(stringParametr);

                    // Выполнинм поиск льготников 2019 по таблицам TableName2019 года.
                    //ExecuteFind(stringParametr2019Add);

                    // Выполнинм поиск льготников 2019 по таблицам базы данных года.
                    //ExecuteFind(stringParametr2019);

                    // Выполнинм поиск льготников после 2019 по таблицам базы данных 2020 года.
                    ExecuteFind(stringParametr2019Aftar);

                    // Выполнинм поиск льготников после 2020 по таблицам базы данных 2021 года.
                    ExecuteFind(stringfindPerson2021FioNoValid);

                    flagExecute = true;

                }
            }

            if (this.FlagValid == false && this.FlagCheck == true)
            {
                if (this.textBox2.Text.Trim().Length > 0 && this.txtИмя.Text.Trim().Length == 0)
                {
                    query = "SELECT Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория, sum(УслугиПоДоговору.Сумма) as 'Сумма',Договор.ДатаЗаписиДоговора,Договор.logWrite,Договор.НомерРеестра,Договор.НомерСчётФактрура " +
                              "FROM Договор INNER JOIN Льготник " +
                              "ON dbo.Договор.id_льготник = dbo.Льготник.id_льготник INNER JOIN " +
                              "dbo.ЛьготнаяКатегория ON dbo.Льготник.id_льготнойКатегории = dbo.ЛьготнаяКатегория.id_льготнойКатегории INNER JOIN " +
                              "dbo.УслугиПоДоговору ON dbo.Договор.id_договор = dbo.УслугиПоДоговору.id_договор " +
                              "where Договор.ФлагПроверки = 'False' " + " or (Договор.ФлагПроверки = 'False' and flagАнулирован = 0)  " +
                              "and Льготник.Фамилия = '" + this.textBox2.Text + "' " + // and Льготник.Имя = '" + this.txtИмя.Text.Trim() + "' " +
                              "Group by Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория,Договор.ДатаЗаписиДоговора,Договор.logWrite,Договор.НомерРеестра,Договор.НомерСчётФактрура ";
                }
                else if (this.textBox2.Text.Trim().Length > 0 && this.txtИмя.Text.Trim().Length > 0)
                {
                    query = "SELECT Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория, sum(УслугиПоДоговору.Сумма) as 'Сумма',Договор.ДатаЗаписиДоговора,Договор.logWrite,Договор.НомерРеестра,Договор.НомерСчётФактрура " +
                              "FROM Договор INNER JOIN Льготник " +
                              "ON dbo.Договор.id_льготник = dbo.Льготник.id_льготник INNER JOIN " +
                              "dbo.ЛьготнаяКатегория ON dbo.Льготник.id_льготнойКатегории = dbo.ЛьготнаяКатегория.id_льготнойКатегории INNER JOIN " +
                              "dbo.УслугиПоДоговору ON dbo.Договор.id_договор = dbo.УслугиПоДоговору.id_договор " +
                              "where Договор.ФлагПроверки = 'False' " + " or (Договор.ФлагПроверки = 'False' and flagАнулирован = 0)  " +
                              "and Льготник.Фамилия = '" + this.textBox2.Text + "'  and Льготник.Имя = '" + this.txtИмя.Text.Trim() + "' " +
                              "Group by Договор.id_договор,Договор.НомерДоговора, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, ЛьготнаяКатегория.ЛьготнаяКатегория,Договор.ДатаЗаписиДоговора,Договор.logWrite,Договор.НомерРеестра,Договор.НомерСчётФактрура ";
                }

            }

            using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            {
                /*

                if (flagExecute == false)
                {
                    con.Open();
                    SqlTransaction transact = con.BeginTransaction();

                    var queryScript = query;

                    SqlCommand com = new SqlCommand(query, con);
                    com.Transaction = transact;

                    // Создадим временную таблицу для отображения данных в DataGrid.
                    DataTable tab = new DataTable();
                    DataColumn col0 = new DataColumn("id_договор", typeof(int));
                    tab.Columns.Add(col0);
                    DataColumn col1 = new DataColumn("НомерДоговора", typeof(string));
                    tab.Columns.Add(col1);
                    DataColumn col2 = new DataColumn("Фамилия", typeof(string));
                    tab.Columns.Add(col2);

                    DataColumn col3 = new DataColumn("Имя", typeof(string));
                    tab.Columns.Add(col3);
                    DataColumn col4 = new DataColumn("Отчество", typeof(string));
                    tab.Columns.Add(col4);

                    DataColumn col5 = new DataColumn("ЛьготнаяКатегория", typeof(string));
                    tab.Columns.Add(col5);
                    DataColumn col6 = new DataColumn("Сумма", typeof(string));
                    tab.Columns.Add(col6);

                    DataColumn col7 = new DataColumn("Дата", typeof(string));
                    tab.Columns.Add(col7);
                    DataColumn col8 = new DataColumn("КтоЗаписал", typeof(string));
                    tab.Columns.Add(col8);

                    // отобразим номер счёт факутры и номер реестра
                    DataColumn col9 = new DataColumn("НомерРеестра", typeof(string));
                    tab.Columns.Add(col9);

                    DataColumn col10 = new DataColumn("НомерСчётФактрура", typeof(string));
                    tab.Columns.Add(col10);

                    if (this.FlagValid == true && this.FlagCheck == false)
                    {
                        DataColumn col11 = new DataColumn("НомерАкта", typeof(string));
                        tab.Columns.Add(col11);

                        DataColumn col12 = new DataColumn("ДатаПодписания", typeof(string));
                        tab.Columns.Add(col12);

                        DataColumn col13 = new DataColumn("flag2019AddWrite", typeof(bool));
                        tab.Columns.Add(col13);
                    }
                    else
                    {
                        DataColumn col14 = new DataColumn("flag2019AddWrite", typeof(bool));
                        tab.Columns.Add(col14);
                    }

                    DataColumn col15 = new DataColumn("flagАнулирован", typeof(bool));
                    tab.Columns.Add(col15);

                    // Список для хранения результатов поиска.
                    List<FindPersonNumContractItem> listPerson = new List<FindPersonNumContractItem>();

                    SqlDataReader read = com.ExecuteReader();
                    while (read.Read())
                    {
                        DataRow row = tab.NewRow();

                        // Пункт результата поиска.
                        FindPersonNumContractItem it = new FindPersonNumContractItem();

                        it.id_договор = read["id_договор"].ToString().Trim();
                        it.НомерДоговора = read["НомерДоговора"].ToString().Trim();
                        it.Фамилия = read["Фамилия"].ToString().Trim();
                        it.Имя = read["Имя"].ToString().Trim();
                        it.Отчество = read["Отчество"].ToString().Trim();
                        it.ЛьготнаяКатегория = read["ЛьготнаяКатегория"].ToString().Trim();
                        it.Сумма = Convert.ToDecimal(read["Сумма"]).ToString("c").Trim();
                        if (read["ДатаЗаписиДоговора"] != DBNull.Value)//
                        {
                            it.Дата = Convert.ToDateTime(read["ДатаЗаписиДоговора"]).ToShortDateString().Trim();
                        }
                        if (read["logWrite"] != DBNull.Value)
                        {
                            it.КтоЗаписал = read["logWrite"].ToString().Trim();
                        }
                        if (read["НомерРеестра"] != DBNull.Value && read["НомерРеестра"].ToString().Trim() != "NULL")
                        {
                            it.НомерРеестра = read["НомерРеестра"].ToString().Trim();
                        }
                        if (read["НомерСчётФактрура"] != DBNull.Value && read["НомерСчётФактрура"].ToString().Trim() != "NULL")
                        {
                            it.НомерСчётФактрура = read["НомерСчётФактрура"].ToString().Trim();
                        }

                        if (this.FlagValid == true && this.FlagCheck == false)
                        {
                            if (read["НомерАкта"] != DBNull.Value)// && read["НомерАкта"] != null)
                            {
                                it.НомерАкта = read["НомерАкта"].ToString().Trim();
                            }

                            if (read["ДатаПодписания"] != DBNull.Value)// && read["НомерАкта"] != null)
                            {
                                it.ДатаПодписания = Convert.ToDateTime(read["ДатаПодписания"]).ToShortDateString().Trim();
                            }
                        }

                        if (read["flag2019AddWrite"] != DBNull.Value)
                        {
                            it.flag2019AddWrite = Convert.ToBoolean(read["flag2019AddWrite"]);
                        }

                        if (read["flagАнулирован"] != DBNull.Value)
                        {
                            it.flagАнулирован = Convert.ToBoolean(read["flagАнулирован"]);
                        }
       
                        listPerson.Add(it);
                    }

                }
                */

                var listCount = listPerson.ToList();

                var listCountA = listPerson.Where(w=>w.flagАнулирован == true).ToList();

                if (flagExecute == false)
                {
                    this.dataGridView1.DataSource = listPerson;

                    this.dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.Automatic;
                }
                else
                {
                    this.dataGridView1.DataSource = CompareContractPerson.Compare(listPerson);
                }

                this.dataGridView1.Columns["id_договор"].Width = 100;
                this.dataGridView1.Columns["id_договор"].Visible = false;
                this.dataGridView1.Columns["НомерДоговора"].Width = 100;
                this.dataGridView1.Columns["НомерДоговора"].DisplayIndex = 0;

                this.dataGridView1.Columns["Фамилия"].Width = 150;
                this.dataGridView1.Columns["Фамилия"].DisplayIndex = 1;
                this.dataGridView1.Columns["Фамилия"].SortMode = DataGridViewColumnSortMode.Automatic;

                this.dataGridView1.Columns["Имя"].Width = 150;
                this.dataGridView1.Columns["Имя"].DisplayIndex = 2;

                this.dataGridView1.Columns["Отчество"].Width = 150;
                this.dataGridView1.Columns["Отчество"].DisplayIndex = 3;

                this.dataGridView1.Columns["ЛьготнаяКатегория"].Width = 200;
                this.dataGridView1.Columns["ЛьготнаяКатегория"].DisplayIndex = 4;
                this.dataGridView1.Columns["ЛьготнаяКатегория"].HeaderText = "Льготная категория";

                this.dataGridView1.Columns["Сумма"].Width = 100;
                this.dataGridView1.Columns["Сумма"].DisplayIndex = 5;

                this.dataGridView1.Columns["Дата"].Width = 120;
                this.dataGridView1.Columns["Дата"].DisplayIndex = 6;
                this.dataGridView1.Columns["Дата"].HeaderText = "Дата записи проекта договора в нашу БД";

                this.dataGridView1.Columns["НомерРеестра"].Width = 80;
                this.dataGridView1.Columns["НомерРеестра"].DisplayIndex = 7;
                this.dataGridView1.Columns["НомерРеестра"].HeaderText = "Номер реестра";

                this.dataGridView1.Columns["НомерСчётФактрура"].Width = 100;
                this.dataGridView1.Columns["НомерСчётФактрура"].DisplayIndex = 8;
                this.dataGridView1.Columns["НомерСчётФактрура"].HeaderText = "Номер счет фактуры";

                if (this.FlagValid == true && this.FlagCheck == false)
                {
                    this.dataGridView1.Columns["НомерАкта"].Width = 100;
                    this.dataGridView1.Columns["НомерАкта"].DisplayIndex = 9;
                    this.dataGridView1.Columns["НомерАкта"].HeaderText = "Номер акта";
                    this.dataGridView1.Columns["НомерАкта"].Visible = false;

                    this.dataGridView1.Columns["ДатаПодписания"].Width = 100;
                    this.dataGridView1.Columns["ДатаПодписания"].DisplayIndex = 10;
                    this.dataGridView1.Columns["ДатаПодписания"].HeaderText = "Дата подписания акта";

                    this.dataGridView1.Columns["КтоЗаписал"].Width = 100;
                    this.dataGridView1.Columns["КтоЗаписал"].DisplayIndex = 11;
                    this.dataGridView1.Columns["flag2019AddWrite"].DisplayIndex = 12;
                    this.dataGridView1.Columns["flag2019AddWrite"].Visible = false;
                }
                else
                {
                    this.dataGridView1.Columns["КтоЗаписал"].Width = 100;
                    this.dataGridView1.Columns["КтоЗаписал"].DisplayIndex = 9;
                    this.dataGridView1.Columns["flag2019AddWrite"].DisplayIndex = 10;
                    this.dataGridView1.Columns["flag2019AddWrite"].Visible = false;
                }

                // Окрасим строку в красный цвет.
                for (int i = 0; i <= this.dataGridView1.Rows.Count - 1; i++)
                {
                    if (Convert.ToBoolean(this.dataGridView1.Rows[i].Cells["flagАнулирован"].Value) == true)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                }

                //this.dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.Automatic;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                }
            }
        }

        private void ValidateActForContract(int idДоговор)
        {
            // Прповерим есть ли у данного договора акт выполненных работ.
            string queryValidAct = "select COUNT(id_акт) as 'КоличествоАктов' from АктВыполненныхРабот " +
                                   "where id_договор in ( " +
                                    "select id_договор  from Договор " +
                                    "where id_договор = " + idДоговор + " ) ";

            DataTable tabAct = ТаблицаБД.GetTableSQL(queryValidAct, "ТаблицаАкт");

            StringBuilder build = new StringBuilder();

            if (Convert.ToInt32(tabAct.Rows[0]["КоличествоАктов"]) > 0)
            {
                string queryValidNumAct = "select НомерАкта,ДатаПодписания from АктВыполненныхРабот " +
                                          "where id_договор in ( " +
                                          "select id_договор  from Договор " +
                                          "where id_договор = " + idДоговор + " ) ";

                DataTable tabNumAct = ТаблицаБД.GetTableSQL(queryValidNumAct, "ТаблицаАктНомер");

                // Запишем номер акта
                build.Append("Номер акта - " + tabNumAct.Rows[0]["НомерАкта"].ToString().Trim());
                build.Append(" от " + Convert.ToDateTime(tabNumAct.Rows[0]["ДатаПодписания"]).ToShortDateString());

                DialogResult dialogResult = MessageBox.Show("Изменить статус договора нельзя, договор связан с актом -  " + build.ToString().Trim(), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }


            }
        }

        private void изменитьСтатусВНепрошедшийПроверкуToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Получим текущий договор.
            int idДоговор = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_договор"].Value);

            bool flagWrite2019 = false;

            flagWrite2019 = Convert.ToBoolean(this.dataGridView1.CurrentRow.Cells["flag2019AddWrite"].Value);

            string queryValidAct = string.Empty;
            

            if (flagWrite2019 == false)
            {
                // Прповерим есть ли у данного договора акт выполненных работ.
                queryValidAct = "select COUNT(id_акт) as 'КоличествоАктов' from АктВыполненныхРабот " +
                                   "where id_договор in ( " +
                                    "select id_договор  from Договор " +
                                    "where id_договор = " + idДоговор + " ) ";
            }
            else
            {
                queryValidAct = @"select  COUNT(id_акт) as 'КоличествоАктов' from АктВыполненныхРаботAdd 
                                        inner join ДоговорAdd
                                        on АктВыполненныхРаботAdd.id_договор = ДоговорAdd.id_ТабДоговор
                                         where ДоговорAdd.id_договор = " + idДоговор + " ";
            }

            DataTable tabAct = ТаблицаБД.GetTableSQL(queryValidAct, "ТаблицаАкт");

            StringBuilder build = new StringBuilder();

            string queryValidNumAct = string.Empty;

            if (Convert.ToInt32(tabAct.Rows[0]["КоличествоАктов"]) > 0)
            {
                if (flagWrite2019 == false)
                {

                    queryValidNumAct = "select НомерАкта,ДатаПодписания from АктВыполненныхРабот " +
                                          "where id_договор in ( " +
                                          "select id_договор  from Договор " +
                                          "where id_договор = " + idДоговор + " ) ";
                }
                else
                {
                    queryValidNumAct = @"select НомерАкта,ДатаПодписания,СуммаАктаВыполненныхРабот from АктВыполненныхРаботAdd 
                                        inner join ДоговорAdd
                                        on АктВыполненныхРаботAdd.id_договор = ДоговорAdd.id_ТабДоговор
                                         where ДоговорAdd.id_договор = " + idДоговор + " ";
                }

                DataTable tabNumAct = ТаблицаБД.GetTableSQL(queryValidNumAct, "ТаблицаАктНомер");

                // Запишем номер акта
                build.Append("Номер акта - " + tabNumAct.Rows[0]["НомерАкта"].ToString().Trim());
                build.Append(" от " + Convert.ToDateTime(tabNumAct.Rows[0]["ДатаПодписания"]).ToShortDateString());

                //// Проверим есть ли акт.
                //// Так как мы заливали 2019 год без таблицы акт то проверям по наличию суммы акта в таблице договор
                //if (Convert.ToDecimal(tabNumAct.Rows[0]["СуммаАктаВыполненныхРабот"]) != 0.0m)
                //{
                //    flagAct = true;
                //}
                //else
                //{
                //    flagAct = false;
                //}

                DialogResult dialogResult = MessageBox.Show("Изменить статус договора нельзя, договор связан с актом -  " + build.ToString().Trim(), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }

            }


            DialogResult dialogResult2 = MessageBox.Show("Изменить статус договора как НЕПРОШЕДШИЙ ПРОВЕРКУ " + build.ToString().Trim(), "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            if (dialogResult2 == System.Windows.Forms.DialogResult.OK)
            {
                string query = string.Empty;

                string user = MyAplicationIdentity.GetUses();

                if (flagWrite2019 == false)
                {
                    query = "declare @id int " +
                               "set @id = " + idДоговор + " " +
                               "delete АктВыполненныхРабот " +
                               "where id_договор in ( " +
                               "select id_договор  from Договор " +
                               "where id_договор = @id " +
                               ") " +
                               "update Договор " +
                               "set ФлагПроверки = 'False', " +
                               "ДатаАктаВыполненныхРабот = '19000101', " +
                               "СуммаАктаВыполненныхРабот = 0.0, " +
                               "НомерРеестра =  null, " +
                               "ДатаРеестра = null, " +
                               "НомерСчётФактрура = null, " +
                               "ДатаСчётФактура = null, " +
                               " ФлагАнулирован = 0, " +
                               " flagАнулирован = 0, " +
                               " logWrite = '" + MyAplicationIdentity.GetUses() + "' , " +
                               " ФлагВозвратНаДоработку = 1 " +
                               "where id_договор in ( " +
                               "select id_договор  from Договор " +
                               "where id_договор = @id) ";
                }
                else
                {
                    query = "declare @id int " +
                               "set @id = " + idДоговор + " " +
                               @" delete Act
                                from АктВыполненныхРаботAdd as Act
                                inner join ДоговорAdd
                                on ДоговорAdd.id_договор = Act.id_договор
                                where ДоговорAdd.id_договор = @id
                                delete Act2
                                from АктВыполненныхРабот as Act2
                                inner join ДоговорAdd
                                on ДоговорAdd.id_ТабДоговор = Act2.id_договор
                                where ДоговорAdd.id_договор = @id " +
                               "update ДоговорAdd " +
                               "set ФлагПроверки = 'False', " +
                               " ФлагНаличияАкта = 0 ," +
                               "ДатаАктаВыполненныхРабот = '19000101', " +
                               "СуммаАктаВыполненныхРабот = 0.0, " +
                               "НомерРеестра =  null, " +
                               "ДатаРеестра = null, " +
                               "НомерСчётФактрура = null, " +
                               "ДатаСчётФактура = null, " +
                               "logWrite = '" + user + "' , " +
                               " ФлагВозвратНаДоработку = 1 " +
                               "where id_договор in ( " +
                               "select id_договор  from ДоговорAdd " +
                               "where id_договор = @id) ";
                }

                // Выполним запрос.
                using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
                {
                    con.Open();
                    SqlTransaction transact = con.BeginTransaction();

                    // Выполним Sql Запрос.
                    Classes.ExecuteQuery.Execute(query, con, transact);

                    // Завершим транзакцию.
                    transact.Commit();
                }

                this.textBox2.Text = "Обновление";

                this.FlagValid = true;

                // Обновим DataGrid.
                LoadAfterClickFind();

                this.FlagValid = false;

                this.textBox2.Text = string.Empty;


            }
        }

        private void аннулироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Получим текущий договор.
            int idДоговор = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_договор"].Value);

            string numContract = this.dataGridView1.CurrentRow.Cells["НомерДоговора"].Value.ToString();

            // id договора из 
           // int idContract = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["idContract"].Value);

            int flagГод =  Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["Год"].Value);

            bool flagWrite2019 = false;

            flagWrite2019 = Convert.ToBoolean(this.dataGridView1.CurrentRow.Cells["flag2019AddWrite"].Value);

            string queryValidAct = string.Empty;

            if (flagГод != 2019 && flagГод != 1)
            {
                queryValidAct = "select COUNT(id_акт) as 'КоличествоАктов' from АктВыполненныхРабот " +
                                   "where id_договор in ( " +
                                    "select id_договор  from Договор " +
                                    "where id_договор = " + idДоговор + " ) ";
            }
            //else if(flagГод != 1)
            //{
            //    queryValidAct = "select COUNT(id_акт) as 'КоличествоАктов' from АктВыполненныхРабот " +
            //                       "where id_договор in ( " +
            //                        "select id_договор  from Договор " +
            //                        "where id_договор = " + idДоговор + " ) ";
            //}
            else if(flagГод == 2019 || flagГод == 1)
            {
                queryValidAct = @"select  COUNT(id_акт) as 'КоличествоАктов' from АктВыполненныхРаботAdd 
                                        inner join ДоговорAdd
                                        on АктВыполненныхРаботAdd.id_договор = ДоговорAdd.id_ТабДоговор
                                         where ДоговорAdd.id_договор = " + idДоговор + " ";
            }
            //else if(flagГод == 1)
            //{
            //    queryValidAct = @"select  COUNT(id_акт) as 'КоличествоАктов' from АктВыполненныхРаботAdd 
            //                            inner join ДоговорAdd
            //                            on АктВыполненныхРаботAdd.id_договор = ДоговорAdd.id_ТабДоговор
            //                             where ДоговорAdd.id_договор = " + idДоговор + " ";
            //}

            DataTable tabAct = ТаблицаБД.GetTableSQL(queryValidAct, "ТаблицаАкт");

            StringBuilder build = new StringBuilder();

            string queryValidNumAct = string.Empty;

            // Проверим есть ли у договора акт.
            if (Convert.ToInt32(tabAct.Rows[0]["КоличествоАктов"]) > 0)
            {
                if (flagГод != 2019)
                {
                    queryValidNumAct = "select НомерАкта,ДатаПодписания from АктВыполненныхРабот " +
                                          "where id_договор in ( " +
                                          "select id_договор  from Договор " +
                                          "where id_договор = " + idДоговор + " ) ";
                }
                else if(flagГод != 1)
                {
                    queryValidNumAct = "select НомерАкта,ДатаПодписания from АктВыполненныхРабот " +
                                          "where id_договор in ( " +
                                          "select id_договор  from Договор " +
                                          "where id_договор = " + idДоговор + " ) ";
                }
                else if(flagГод == 2019)
                {
                    queryValidNumAct = @"select НомерАкта,ДатаПодписания,СуммаАктаВыполненныхРабот from АктВыполненныхРаботAdd 
                                        inner join ДоговорAdd
                                        on АктВыполненныхРаботAdd.id_договор = ДоговорAdd.id_ТабДоговор
                                         where ДоговорAdd.id_договор = " + idДоговор + " ";
                }
                else if(flagГод == 1)
                {
                    queryValidNumAct = @"select НомерАкта,ДатаПодписания,СуммаАктаВыполненныхРабот from АктВыполненныхРаботAdd 
                                        inner join ДоговорAdd
                                        on АктВыполненныхРаботAdd.id_договор = ДоговорAdd.id_ТабДоговор
                                         where ДоговорAdd.id_договор = " + idДоговор + " ";
                }

                DataTable tabNumAct = ТаблицаБД.GetTableSQL(queryValidNumAct, "ТаблицаАктНомер");

                // Запишем номер акта
                build.Append("Номер акта - " + tabNumAct.Rows[0]["НомерАкта"].ToString().Trim());
                build.Append(" от " + Convert.ToDateTime(tabNumAct.Rows[0]["ДатаПодписания"]).ToShortDateString());


                DialogResult dialogResult = MessageBox.Show("Изменить статус договора нельзя, договор связан с актом -  " + build.ToString().Trim(), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }

            }

            DialogResult dialogResult2 = MessageBox.Show("Изменить статус договора АНУЛИРОВАННЫЙ " + build.ToString().Trim(), "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            if (dialogResult2 == System.Windows.Forms.DialogResult.OK)
            {
                string query = string.Empty;

                string user = Environment.UserName;// MyAplicationIdentity.GetUses();

                string user2 = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

                //string user3 = MyAplicationIdentity.GetUses();

                if (flagГод != 2019 && flagГод != 1)
                {
                    query = @"declare @id int 
                            set @id = 150106
                            declare @numContract nvarchar(50)
                            set @numContract = '"+ numContract + "' " +
                            @" delete АктВыполненныхРабот
                            where id_договор in (select id_договор from Договор where НомерДоговора = @numContract ) 
                            update Договор
                            set ФлагПроверки = 'False',
                            ДатаАктаВыполненныхРабот = '19000101', СуммаАктаВыполненныхРабот = 0.0,
                            НомерРеестра = null, ДатаРеестра = null, НомерСчётФактрура = null,
                            ДатаСчётФактура = null, ФлагАнулирован = 1, flagАнулирован = 1,
                            logWrite = '" + user + "', " +
                            @" ФлагВозвратНаДоработку = 1
                            where НомерДоговора = @numContract
                            delete АктВыполненныхРаботAdd
                            where АктВыполненныхРаботAdd.id_договор in (select id_договор from ДоговорAdd where НомерДоговора = @numContract) 
                            update ДоговорAdd set ФлагПроверки = 'False', ФлагНаличияАкта = 0,
                            ДатаАктаВыполненныхРабот = '19000101', СуммаАктаВыполненныхРабот = 0.0, " +
                            //НомерРеестра = null,
                            //ДатаРеестра = null, НомерСчётФактрура = null, ДатаСчётФактура = null
                            @" ФлагАнулирован = 1, flagАнулирован = 1,
                            logWrite = '"+ user +"', ФлагВозвратНаДоработку = 1 " +
                            " where ДоговорAdd.НомерДоговора = @numContract  ";

                    #region Старый код
                    //query = "declare @id int " +
                    //           "set @id = " + idДоговор + " " +
                    //           " declare @numContract nvarchar(50) " +
                    //           "delete АктВыполненныхРабот " +
                    //           "where id_договор in ( " +
                    //           "select id_договор  from Договор " +
                    //           "where id_договор = @id " +
                    //           ") " +
                    //           "update Договор " +
                    //           "set ФлагПроверки = 'False', " +
                    //           "ДатаАктаВыполненныхРабот = '19000101', " +
                    //           "СуммаАктаВыполненныхРабот = 0.0, " +
                    //           "НомерРеестра =  null, " +
                    //           "ДатаРеестра = null, " +
                    //           "НомерСчётФактрура = null, " +
                    //           "ДатаСчётФактура = null, " +
                    //           " ФлагАнулирован = 1, " +
                    //           " flagАнулирован = 1, " +
                    //           //" logWrite = '" + MyAplicationIdentity.GetUses() + "' , " +
                    //           " logWrite = '" + user + "' , " +
                    //           " ФлагВозвратНаДоработку = 1 " +
                    //           "where id_договор in (select Договор.id_договор from Договор " +
                    //            " inner join Договор as T1 " +
                    //            " on T1.НомерДоговора = Договор.НомерДоговора " +
                    //            " where Договор.id_договор = @id)  " +
                    //            " select @numContract = Договор.НомерДоговора from Договор  " +
                    //            " inner join Договор as T1  on T1.НомерДоговора = Договор.НомерДоговора " +
                    //            " where Договор.id_договор = @id ";
                    //query += " delete АктВыполненныхРаботAdd " +
                    //         " where АктВыполненныхРаботAdd.id_договор in (select id_договор from ДоговорAdd " +
                    //          " where НомерДоговора = @numContract) " +
                    //           "update ДоговорAdd " +
                    //           "set ФлагПроверки = 'False', " +
                    //           " ФлагНаличияАкта = 0 ," +
                    //           "ДатаАктаВыполненныхРабот = '19000101', " +
                    //           "СуммаАктаВыполненныхРабот = 0.0, " +
                    //           "НомерРеестра =  null, " +
                    //           "ДатаРеестра = null, " +
                    //           "НомерСчётФактрура = null, " +
                    //           "ДатаСчётФактура = null, " +
                    //            " ФлагАнулирован = 1, " +
                    //           " flagАнулирован = 1, " +
                    //           "logWrite = '" + user + "' , " +
                    //           " ФлагВозвратНаДоработку = 1 " +
                    //           "where ДоговорAdd.НомерДоговора =  @numContract  ";
                    #endregion
                }
                else if(flagГод == 2019 || flagГод == 1)
                {
                    query = @"declare @id int 
                            set @id = 150106
                            declare @numContract nvarchar(50)
                            set @numContract = '" + numContract + "' " +
                            @" delete АктВыполненныхРабот
                            where id_договор in (select id_договор from Договор where НомерДоговора = @numContract ) 
                            update Договор
                            set ФлагПроверки = 'False',
                            ДатаАктаВыполненныхРабот = '19000101', СуммаАктаВыполненныхРабот = 0.0,
                            НомерРеестра = null, ДатаРеестра = null, НомерСчётФактрура = null,
                            ДатаСчётФактура = null, ФлагАнулирован = 1, flagАнулирован = 1,
                            logWrite = '" + user + "', " +
                            @" ФлагВозвратНаДоработку = 1
                            where НомерДоговора = @numContract
                            delete АктВыполненныхРаботAdd
                            where АктВыполненныхРаботAdd.id_договор in (select id_договор from ДоговорAdd where НомерДоговора = @numContract) 
                            update ДоговорAdd set ФлагПроверки = 'False', ФлагНаличияАкта = 0,
                            ДатаАктаВыполненныхРабот = '19000101', СуммаАктаВыполненныхРабот = 0.0, НомерРеестра = null,
                            ДатаРеестра = null, НомерСчётФактрура = null, ДатаСчётФактура = null, ФлагАнулирован = 1, flagАнулирован = 1,
                            logWrite = '" + user + "', ФлагВозвратНаДоработку = 1 " +
                            " where ДоговорAdd.НомерДоговора = @numContract  ";

                    #region Старый код
                    //query = "declare @id int " +
                    //           "set @id = " + idДоговор + " " +
                    //           " declare @idContract int  " +
                    //           @" delete Act
                    //            from АктВыполненныхРаботAdd as Act
                    //            inner join ДоговорAdd
                    //            on ДоговорAdd.id_договор = Act.id_договор
                    //            where ДоговорAdd.id_договор = @id
                    //            delete Act2
                    //            from АктВыполненныхРабот as Act2
                    //            inner join ДоговорAdd
                    //            on ДоговорAdd.id_ТабДоговор = Act2.id_договор
                    //            where ДоговорAdd.id_договор = @id " +
                    //           "update ДоговорAdd " +
                    //           "set ФлагПроверки = 'False', " +
                    //           " ФлагНаличияАкта = 0 ," +
                    //           "ДатаАктаВыполненныхРабот = '19000101', " +
                    //           "СуммаАктаВыполненныхРабот = 0.0, " +
                    //           "НомерРеестра =  null, " +
                    //           "ДатаРеестра = null, " +
                    //           "НомерСчётФактрура = null, " +
                    //           "ДатаСчётФактура = null, " +
                    //            " ФлагАнулирован = 1, " +
                    //           " flagАнулирован = 1, " +
                    //           "logWrite = '" + user + "' , " +
                    //           " ФлагВозвратНаДоработку = 1 " +
                    //           "where id_договор in ( " +
                    //           "select id_договор  from ДоговорAdd " +
                    //           "where id_договор = @id) " +
                    //           " select @idContract = id_ТабДоговор from ДоговорAdd where id_договор = @id ";

                    //query += @" delete АктВыполненныхРабот  
                    //            where id_договор in (
                    //            select id_договор from Договор
                    //            where НомерДоговора = '"+ this.textBox1.Text.Trim() + "' ) " +
                    //            @" update Договор
                    //            set ФлагПроверки = 'False', ДатаАктаВыполненныхРабот = '19000101',
                    //            СуммаАктаВыполненныхРабот = 0.0, НомерРеестра = null, ДатаРеестра = null,
                    //            НомерСчётФактрура = null, ДатаСчётФактура = null, ФлагАнулирован = 1,
                    //            flagАнулирован = 1, logWrite = 'dugin', ФлагВозвратНаДоработку = 1
                    //            where НомерДоговора = '" + this.textBox1.Text.Trim() + "' ";
                    #endregion
                }

                var testQuery = query;

                // Выполним запрос.
                using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
                {
                    con.Open();
                    SqlTransaction transact = con.BeginTransaction();


                    Classes.ExecuteQuery.Execute(query, con, transact);

                    // Завершим транзакцию.
                    transact.Commit();
                }

                this.textBox2.Text = "Обновление";

                this.FlagValid = true;

                // Обновим DataGrid.
                LoadAfterClickFind();

                this.FlagValid = false;

                this.textBox2.Text = string.Empty;


            }

            //ValidateActForContract(idДоговор);


        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
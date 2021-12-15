using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Drawing;
using ControlDantist.Querys;
using ControlDantist.EditeContractValideTrue;
using System.Windows.Forms;
using ControlDantist.Classes;
using ControlDantist.Find;
using ControlDantist.FindPersons;
using ControlDantist.ValidPersonContract;
using ControlDantist.FindPersonFullTrue;
using DantistLibrary;

namespace ControlDantist
{
    public partial class FormFIndVAlidTrueCOntracts : Form
    {
        public FormFIndVAlidTrueCOntracts()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FindToNumber(string numContract, bool flagValidate)
        {
            // Временный cписок содержащий все найденные договора.
            List<ValideContract> listTempDisplay = new List<ValideContract>();

            // Переменная для хранения номера договора который необходимо найти.
            //string numContract = this.textBox1.Text;

            // Поиск льготника прошедшего проверку по номеру договора.
            IFindPerson findPerson = new FindContractTo2019(numContract,flagValidate);
            string queryTo2019 = findPerson.Query();

            // Загрузим данные до 2019 года.
            listTempDisplay.AddRange(QueryExecute.ExecuteQuery(GetDate(queryTo2019)));

            ////Поиск номера договора за 2019 год по таблицам TableAdd.
            IFindPerson fintPerson2019Add = new FindContract2019Add(numContract, flagValidate);
            string query2019Add = fintPerson2019Add.Query();

            ////Пока скроем поиск льготников в основной таблице за 2019 год.
            ////  Поиск номера договора за 2019 год по обычным таблицам.
            IFindPerson findPerson2019 = new FindContract2019(numContract, flagValidate);
            string query2019 = findPerson2019.Query();

            string queryUnionAll = query2019Add + " union all " + query2019;

            listTempDisplay.AddRange(QueryExecute.ExecuteQuery(GetDate(queryUnionAll)));

            //// Поиск номера договора позже 2019 года.
            IFindPerson fintPersonAftar2019 = new FindPersonAftar2019(numContract, flagValidate);
            string query2019Aftar = fintPersonAftar2019.Query();

            listTempDisplay.AddRange(QueryExecute.ExecuteQuery(GetDate(query2019Aftar)));

            // Поиск договора по таблицам 2021 года.
            IFindPerson findPerson2021 = new FindPersonAftar(numContract, flagValidate);
            string query2021 = findPerson2021.Query();

            listTempDisplay.AddRange(QueryExecute.ExecuteQuery(GetDate(query2021)));

            // Загрузим данными.
            LoadDate(listTempDisplay);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            FindToNumber(this.textBox1.Text, true);

           // // Временный cписок содержащий все найденные договора.
           // List<ValideContract> listTempDisplay = new List<ValideContract>();

            // // Переменная для хранения номера договора который необходимо найти.
            //string numContract = this.textBox1.Text;

            // // Поиск льготника прошедшего проверку по номеру договора.
            // IFindPerson findPerson = new FindContractTo2019(this.textBox1.Text);
            // string queryTo2019 = findPerson.Query();

            // // Загрузим данные до 2019 года.
            // listTempDisplay.AddRange(QueryExecute.ExecuteQuery(GetDate(queryTo2019)));

            //////Поиск номера договора за 2019 год по таблицам TableAdd.
            //IFindPerson fintPerson2019Add = new FindContract2019Add(numContract);
            //string query2019Add = fintPerson2019Add.Query();

            //////Пока скроем поиск льготников в основной таблице за 2019 год.
            //////  Поиск номера договора за 2019 год по обычным таблицам.
            //IFindPerson findPerson2019 = new FindContract2019(numContract);
            //string query2019 = findPerson2019.Query();

            //string queryUnionAll = query2019Add + " union all " + query2019;

            //listTempDisplay.AddRange(QueryExecute.ExecuteQuery(GetDate(queryUnionAll)));

            ////// Поиск номера договора позже 2019 года.
            //IFindPerson fintPersonAftar2019 = new FindPersonAftar2019(numContract);
            //string query2019Aftar = fintPersonAftar2019.Query();

            //listTempDisplay.AddRange(QueryExecute.ExecuteQuery(GetDate(query2019Aftar)));

            //// Поиск договора по таблицам 2021 года.
            //IFindPerson findPerson2021 = new FindPersonAftar(numContract);
            //string query2021 = findPerson2021.Query();

            //listTempDisplay.AddRange(QueryExecute.ExecuteQuery(GetDate(query2021)));

            //// Загрузим данными.
            //LoadDate(listTempDisplay);

        }

        private StringParametr GetDate(string query)
        {
            StringParametr stringParametr = new StringParametr();
            stringParametr.Query = query;

            return stringParametr;
        }

        private void LoadDate(List<ValideContract> listDisplay)
        {

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
                if (Convert.ToBoolean(this.dataGridView1.Rows[i].Cells["flagАнулирован"].Value) == true)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            List<ValideContract> listDisplay = new List<ValideContract>();

            // Очистим DataGridView от записеий.
            LoadDate(listDisplay);

            this.textBox1.Text = "";

            if (this.textBox2.Text.Trim() != "" && this.txtИмя.Text.Trim() == "")
            {
                // Поиск льготников по фамилии.
                PersonFnSn(true, false);
            }
            else if(this.textBox2.Text.Trim() != "" && this.txtИмя.Text.Trim() != "")
            {
                // Поиск льготников по фамилии и отчеству.
                PersonFnSn(true,true);
            }

        }

        /// <summary>
        /// Поиск по фамилиии и имени.
        /// </summary>
        private void PersonFnSn(bool flagValidity, bool flagSecondName)
        {
            // Временный cписок содержащий все найденные договора.
            List<ValideContract> listTempDisplay = new List<ValideContract>();
            
            // Здесь я применяю паттерн фабричный метод. !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            FindPersonFactory findFullTo2019 = new FindPersonFSName2019To(this.textBox2.Text, this.txtИмя.Text, flagValidity, flagSecondName);
            IFindPerson findPerson2019To = findFullTo2019.Query();

            // Загрузим данные до 2019 года.
            listTempDisplay.AddRange(QueryExecute.ExecuteQuery(GetDate(findPerson2019To.Query())));

            // Поиск льготников по таблице ДоговорAdd - 2019 год.
            findFullTo2019 = new FindPersonFSName2019Add(this.textBox2.Text, this.txtИмя.Text, flagValidity, flagSecondName);
            IFindPerson findPerson2019Add = findFullTo2019.Query();

            // Сртока запроса на поиск льготников в таблице ЛьготникAdd.
            string query2019Add = findPerson2019Add.Query();

            // Поиск льготников в таблице ДоговорАрхив 2019 год.
            findFullTo2019 = new FindPersonFSName2019Архив(this.textBox2.Text, this.txtИмя.Text, flagValidity, flagSecondName);
            IFindPerson findPerson2019Архив = findFullTo2019.Query();

            // Строка содержащая SQL запрос.
            string query2019Архив = findPerson2019Архив.Query();

            // Сформируем union all запрос по 2019 году.
            string query2019UnionAll = query2019Add + " union all " + query2019Архив;

            listTempDisplay.AddRange(QueryExecute.ExecuteQuery(GetDate(query2019UnionAll)));

            // Поиск льготников ДоговорАрхив > 2019 года.
            findFullTo2019 = new FindPersonFS2019After(this.textBox2.Text, this.txtИмя.Text, flagValidity, flagSecondName);
            IFindPerson findPerson2019Aftar = findFullTo2019.Query();

            listTempDisplay.AddRange(QueryExecute.ExecuteQuery(GetDate(findPerson2019Aftar.Query())));

            // Посик льготников в таблице Договор, 2021 и дольше год.
            findFullTo2019 = new FindPersonFSName(this.textBox2.Text, this.txtИмя.Text, flagValidity, flagSecondName);
            IFindPerson findPersonSecondName = findFullTo2019.Query();

            var asd = findPersonSecondName.Query();

            listTempDisplay.AddRange(QueryExecute.ExecuteQuery(GetDate(findPersonSecondName.Query())));

            var test = listTempDisplay;

            // Загрузим данными.
            LoadDate(listTempDisplay);

        }

        private void установитьКакПрошедшийПроверкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id_договор = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_договор"].Value);

            string sTest = "update dbo.Договор " +
                           "set ДатаЗаписиДоговора = '" + DateTime.Today.ToShortDateString() + "' " +
                           ",ФлагПроверки = 'True' " +
                           ",logWrite = '" + MyAplicationIdentity.GetUses() + "' " +
                           ", ФлагВозвратНаДоработку = 0 " +
                           ", flagАнулирован = 0" +
                           ", ФлагАнулирован = 0 " +
                           ", flag2020 = 0 " +
                           ", flag2019AddWrite = 0 " +
                           "where id_договор = " + id_договор + " ";

            Classes.ExecuteQuery.Execute(sTest);
            this.Close();
        }

        private void отмениитьАктToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flagAct = false;
            // Получим текущий договор.
            int idДоговор = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_договор"].Value);

            int idYear = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["Год"].Value);

            if(idYear == 2021)
            {
                //// Получим есть ли у текущего договора акт выполненных работ.
                //IQuery query = new QueryCountActs(idДоговор);

                //DataTable tabActs = ТаблицаБД.GetTableSQL(query.Query(), "КоличествоАктов");

                //if(tabActs != null && tabActs.Rows != null && tabActs.Rows.Count > 0)
                //{
                //    // Получим номер акта выполненных работ.
                //    string numAct = " № акта - " + tabActs.Rows[0]["НомерАкта"].ToString();

                //    // Получим дату акта выполненных работ.
                //    string dateAct = " Дата - " + Convert.ToDateTime(tabActs.Rows[0]["ДатаАктаВыполненныхРабот"]).ToShortDateString();

                //    // Построим текстовое пердставление содержимого запроса к таблице актов.
                //    StringBuilder build = new StringBuilder();

                //    build.Append(numAct);
                //    build.Append(dateAct);

                bool flagExistAct = false;

                // Получим 
                string strDateAct = ExistsAct.Exists(idДоговор, idYear, ref flagExistAct);

                if (flagExistAct == true)
                {

                    DialogResult dialogResult = MessageBox.Show("Удалить акт выполненных работ " + strDateAct.ToString().Trim(), "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    {

                        if (dialogResult == DialogResult.OK)
                        {

                            // Перменная для хранит пользователя домена.
                            string user = "";

                            try
                            {
                                user = MyAplicationIdentity.GetUses();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка получения имени пользователя из домена");
                            }

                            // Запросм нра удаление акта выполненных работ.
                            IQuery queryDelete = new QueryDeleteAct(user, idДоговор);

                            // Удаление акта выполненных работ.
                            ExecuteQuery.Execute(queryDelete.Query());

                            // Обновим DataGridView.
                            string num = this.textBox1.Text;

                            string firstName = this.textBox2.Text;

                            string secondName = this.txtИмя.Text;

                            List<ValideContract> listDisplay = new List<ValideContract>();

                            // Очистим DataGridView от записеий.
                            LoadDate(listDisplay);

                            if (num != "")
                            {

                            }
                            if (this.textBox2.Text.Trim() != "" && this.txtИмя.Text.Trim() == "")
                            {
                                // Поиск льготников по фамилии.
                                PersonFnSn(true, false);
                            }
                            else if (this.textBox2.Text.Trim() != "" && this.txtИмя.Text.Trim() != "")
                            {
                                // Поиск льготников по фамилии и отчеству.
                                PersonFnSn(true, true);
                            }

                        }

                    }
                }
                else
                {
                    MessageBox.Show("Данный Договор не содержит акт выполненных работ");
                }

            }
            else
            {
                MessageBox.Show("Договор отредактировать нельзя");
            }


            //bool flagWrite2019 = false;

            //flagWrite2019 = Convert.ToBoolean(this.dataGridView1.CurrentRow.Cells["flag2019AddWrite"].Value);

            //string queryValidAct = string.Empty;

            //if (flagWrite2019 == false)
            //{
            //    queryValidAct = "select COUNT(id_акт) as 'КоличествоАктов' from АктВыполненныхРабот " +
            //                       "where id_договор in ( " +
            //                        "select id_договор  from Договор " +
            //                        "where id_договор = " + idДоговор + " ) ";
            //}
            //else
            //{
            //    queryValidAct = @"select  COUNT(id_акт) as 'КоличествоАктов' from АктВыполненныхРаботAdd 
            //                            inner join ДоговорAdd
            //                            on АктВыполненныхРаботAdd.id_договор = ДоговорAdd.id_ТабДоговор
            //                             where ДоговорAdd.id_договор = " + idДоговор + " ";


            //    //queryValidAct = "select COUNT(id_акт) as 'КоличествоАктов' from АктВыполненныхРаботAdd " +
            //    //                       "where id_договор in ( " +
            //    //                        "select id_договор  from ДоговорAdd " +
            //    //                        "where id_договор = " + idДоговор + " ) ";
            //}

            //DataTable tabAct = ТаблицаБД.GetTableSQL(queryValidAct, "ТаблицаАкт");

            //StringBuilder build = new StringBuilder();

            //if (Convert.ToInt32(tabAct.Rows[0]["КоличествоАктов"]) > 0)
            //{
            //    flagAct = true;

            //    string queryValidNumAct = string.Empty;

            //    if (flagWrite2019 == false)
            //    {
            //        //queryValidNumAct = "select НомерАкта,ДатаПодписания from АктВыполненныхРабот " +
            //        //                      "where id_договор in ( " +
            //        //                      "select id_договор  from Договор " +
            //        //                      "where id_договор = " + idДоговор + " ) ";

            //        queryValidNumAct = @"select НомерАкта,ДатаПодписания,СуммаАктаВыполненныхРабот from АктВыполненныхРабот 
            //                            inner join Договор
            //                            on АктВыполненныхРабот.id_договор = Договор.id_договор
            //                             where ДоговорAdd.id_договор = " + idДоговор + " ";
            //    }
            //    else
            //    {
            //        //queryValidNumAct = "select НомерАкта,ДатаПодписания from АктВыполненныхРаботAdd " +
            //        //                      "where id_договор in ( " +
            //        //                      "select id_договор  from ДоговорAdd " +
            //        //                      "where id_договор = " + idДоговор + " ) ";

            //        queryValidNumAct = @"select НомерАкта,ДатаПодписания,СуммаАктаВыполненныхРабот from АктВыполненныхРаботAdd 
            //                            inner join ДоговорAdd
            //                            on АктВыполненныхРаботAdd.id_договор = ДоговорAdd.id_ТабДоговор
            //                             where ДоговорAdd.id_договор = " + idДоговор + " ";
            //    }

            //    DataTable tabNumAct = ТаблицаБД.GetTableSQL(queryValidNumAct, "ТаблицаАктНомер");

            //    // Запишем номер акта
            //    build.Append("Номер акта - " + tabNumAct.Rows[0]["НомерАкта"].ToString().Trim());
            //    build.Append(" от " + Convert.ToDateTime(tabNumAct.Rows[0]["ДатаПодписания"]).ToShortDateString());

            //    // Проверим есть ли акт.
            //    // Так как мы заливали 2019 год без таблицы акт то проверям по наличию суммы акта в таблице договор
            //    if (Convert.ToDecimal(tabNumAct.Rows[0]["СуммаАктаВыполненныхРабот"]) != 0.0m)
            //    {
            //        flagAct = true;
            //    }
            //    else
            //    {
            //        flagAct = false;
            //    }
            //}

            ////// Выведим диалоговое окно.
            ////FormMessageAct formMessAct = new FormMessageAct();
            ////formMessAct.НомерАкта = build.ToString();
            ////formMessAct.ShowDialog();
            //DialogResult dialogResult = MessageBox.Show("Удалить акт выполненных работ " + build.ToString().Trim(), "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            //string test = "test";

            //if (dialogResult == System.Windows.Forms.DialogResult.OK)
            //{

            //    string query = string.Empty;

            //    string user = MyAplicationIdentity.GetUses();

            //    if (flagWrite2019 == false)
            //    {
            //        query = " declare @id int " +
            //                "set @id = " + idДоговор + " " +
            //                "delete АктВыполненныхРабот " +
            //                "where id_договор in ( " +
            //                "select id_договор  from Договор " +
            //                "where id_договор = @id " +
            //                ") " +
            //                "update Договор " +
            //                "set ФлагПроверки = 'True', " +
            //                "ДатаАктаВыполненныхРабот = '19000101', " +
            //                "СуммаАктаВыполненныхРабот = 0.0, " +
            //                "НомерРеестра =  null, " +
            //                "ДатаРеестра = null, " +
            //                "НомерСчётФактрура = null, " +
            //                "ДатаСчётФактура = null, " +
            //                "logWrite = '" + user + "',  " +
            //                " ФлагВозвратНаДоработку = 1 " +
            //                "where id_договор in ( " +
            //                "select id_договор  from Договор " +
            //                "where id_договор = @id) ";
            //    }
            //    else
            //    {
            //        query = "declare @id int " +
            //                   "set @id = " + idДоговор + " " +
            //                   @" delete Act
            //                    from АктВыполненныхРаботAdd as Act
            //                    inner join ДоговорAdd
            //                    on ДоговорAdd.id_договор = Act.id_договор
            //                    where ДоговорAdd.id_договор = @id
            //                    delete Act2
            //                    from АктВыполненныхРабот as Act2
            //                    inner join ДоговорAdd
            //                    on ДоговорAdd.id_ТабДоговор = Act2.id_договор
            //                    where ДоговорAdd.id_договор = @id " +
            //                   "update ДоговорAdd " +
            //                   "set ФлагПроверки = 'True', " +
            //                   " ФлагНаличияАкта = 1 " +
            //                   "ДатаАктаВыполненныхРабот = '19000101', " +
            //                   "СуммаАктаВыполненныхРабот = 0.0, " +
            //                   "НомерРеестра =  null, " +
            //                   "ДатаРеестра = null, " +
            //                   "НомерСчётФактрура = null, " +
            //                   "ДатаСчётФактура = null, " +
            //                   "logWrite = '" + user + "',  " +
            //                    " ФлагВозвратНаДоработку = 1 " +
            //                   "where id_договор in ( " +
            //                   "select id_договор  from ДоговорAdd " +
            //                   "where id_договор = @id) ";
            //    }

            //    // Выполним запрос.
            //    using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString()))
            //    {
            //        SqlTransaction transact = con.BeginTransaction();
            //        con.Open();

            //        Classes.ExecuteQuery.Execute(query, con, transact);
            //    }

            //    // Временно переведем в режимокрытия прошедших проверку.
            //    //this.FlagValid = true;

            //    // Временно введем текст, что бы отработали условия отображения льготников при поиске.
            //    this.textBox2.Text = "Обновление";

            //    List<ValideContract> listDisplay = new List<ValideContract>();

            //    // Очистим DataGridView от записеий.
            //    LoadDate(listDisplay);

            //    // Обновим DataGrid.
            //    //LoadAfterClickFind();

            //    // Очистим поле ввода Фамилии.
            //    this.textBox2.Text = string.Empty;

            //    // 
            //    //this.FlagValid = false;

            //}
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void изменитьСтатусНаНепрошедшийПроверкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Получим id договора.
            int idДоговор = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_договор"].Value);

            // Получим год когда заключен договор.
            int idYear = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["Год"].Value);

            if (idYear == 2021)
            {
                // флаг налчия акта выполненных работ.
                bool flagExistAct = false;

                // Получим 
                string strDateAct = ExistsAct.Exists(idДоговор, idYear, ref flagExistAct);

                if (flagExistAct == true)
                {
                    DialogResult dialogResult = MessageBox.Show("Изменить статус договора нельзя, договор связан с актом -  " + strDateAct.Trim(), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    if (dialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        return;
                    }
                }
                else
                {
                    DialogResult dialogResult2 = MessageBox.Show("Изменить статус договора как НЕПРОШЕДШИЙ ПРОВЕРКУ " + strDateAct.Trim(), "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

                    if (dialogResult2 == System.Windows.Forms.DialogResult.OK)
                    {
                        string query = string.Empty;

                        // Переменная для хранения имени пользователя полученного из домена.
                        string user = string.Empty;

                        try
                        {
                            // Получим пользователя 
                            user = MyAplicationIdentity.GetUses();
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("Ну удалось получит имя пользователя в домене - " + ex.Message.Trim());
                        }

                        // SQL запрос на удаление акта выполненных работ.
                        IQuery queryDelete = new QueryИзменитьСтатусДоговора(idДоговор, user);

                        string strQueryDelete = queryDelete.Query();

                        // Выполним скрипт.
                        ExecuteQuery.Execute(strQueryDelete);

                        // Обновим содержимое DataGridView.
                        // Обновим DataGridView.
                        string num = this.textBox1.Text;

                        string firstName = this.textBox2.Text;

                        string secondName = this.txtИмя.Text;

                        List<ValideContract> listDisplay = new List<ValideContract>();

                        // Очистим DataGridView от записеий.
                        LoadDate(listDisplay);

                        if (num != "")
                        {

                        }
                        if (this.textBox2.Text.Trim() != "" && this.txtИмя.Text.Trim() == "")
                        {
                            // Поиск льготников по фамилии.
                            PersonFnSn(true, false);
                        }
                        else if (this.textBox2.Text.Trim() != "" && this.txtИмя.Text.Trim() != "")
                        {
                            // Поиск льготников по фамилии и отчеству.
                            PersonFnSn(true, true);
                        }
                    }
                }

            }

        }

        private void аннулироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Получим id договора.
            int idДоговор = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_договор"].Value);

            // Получим год когда заключен договор.
            int idYear = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["Год"].Value);

            if (idYear == 2021)
            {
                // флаг налчия акта выполненных работ.
                bool flagExistAct = false;

                // Получим 
                string strDateAct = ExistsAct.Exists(idДоговор, idYear, ref flagExistAct);

                if (flagExistAct == true)
                {
                    DialogResult dialogResult = MessageBox.Show("Изменить статус договора нельзя, договор связан с актом -  " + strDateAct.Trim(), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    if (dialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        return;
                    }
                }
                else
                {
                    // Переменная для хранения имени пользователя полученного из домена.
                    string user = string.Empty;

                    try
                    {
                        // Получим пользователя 
                        user = MyAplicationIdentity.GetUses();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ну удалось получит имя пользователя в домене - " + ex.Message.Trim());
                    }

                    // SQL запрос изменить статуст договора (паттер стратегия.)
                    IQuery queryАнулировать = new QueryИзменитьСтатусДоговора(idДоговор, user);

                    string strQueryDelete = queryАнулировать.Query();

                    // Выполним скрипт.
                    ExecuteQuery.Execute(strQueryDelete);

                    // Обновим содержимое DataGridView.
                    // Обновим DataGridView.
                    string num = this.textBox1.Text;

                    string firstName = this.textBox2.Text;

                    string secondName = this.txtИмя.Text;

                    List<ValideContract> listDisplay = new List<ValideContract>();

                    // Очистим DataGridView от записеий.
                    LoadDate(listDisplay);

                    if (num != "")
                    {

                    }
                    if (this.textBox2.Text.Trim() != "" && this.txtИмя.Text.Trim() == "")
                    {
                        // Поиск льготников по фамилии.
                        PersonFnSn(true, false);
                    }
                    else if (this.textBox2.Text.Trim() != "" && this.txtИмя.Text.Trim() != "")
                    {
                        // Поиск льготников по фамилии и отчеству.
                        PersonFnSn(true, true);
                    }

                }
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //// Получим id договора.
            //int idДоговор = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_договор"].Value);

            //// Получим год когда заключен договор.
            //int idYear = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["Год"].Value);

            //// Строка для ъхранения SQL запроса для получения улсг по договору.
            //string query = string.Empty;

            //// Строка для хранения SQL запроса для получения данных по льготнику.
            //string queryPerson = string.Empty;

            //if(idYear < 2019 || idYear == 2020)
            //{
            //    // Услуги по договору по таблице Архив.
            //    IQueryFind queryFind = new QueryContractАрхив(idДоговор);
            //    query = queryFind.Query();

            //    // Данные по льготнику.
            //    IQueryFind queryFindPerson = new QueryPersonАрхив(idДоговор);
            //    queryPerson = queryFindPerson.Query();
            //}
            //else if(idYear == 2019)
            //{
            //    // Услуги по договору по таблице Add.
            //    IQueryFind queryFind = new QueryContractAdd(idДоговор);
            //    query = queryFind.Query();

            //    // Данные по льготнику.
            //    IQueryFind queryFindPerson = new QueryPersonAdd(idДоговор);
            //    queryPerson = queryFindPerson.Query();

            //}
            //else if(idYear >= 2021)
            //{
            //    // Услуги по договору по таблице Договор.
            //    IQueryFind queryFind = new QueryContract2021(idДоговор);
            //    query = queryFind.Query();

            //    // Данные по льготнику.
            //    IQueryFind queryFindPerson = new QueryPerson2021(idДоговор);
            //    queryPerson = queryFindPerson.Query();
            //}

            //DataTable tabServices =  ТаблицаБД.GetTableSQL(query,"УслугиПоДоговору");

            //// Получим данные по льготнику.
            //DataTable tabPerson = ТаблицаБД.GetTableSQL(queryPerson, "ДанныеПоЛьготнику");

            //if (tabServices != null && tabServices.Rows != null)
            //{

            //    FormDispContract fdc = new FormDispContract();
            //    fdc.ДанныеПоКонтракту = tabServices;
            //    fdc.ДанныеПоЛьготнику = tabPerson;

            //    fdc.TopMost = true;
            //    fdc.IdДоговор = idДоговор;
            //    fdc.Show();
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Паттерн стратегия.

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Получим id договора.
            int idДоговор = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_договор"].Value);

            // Получим год когда заключен договор.
            int idYear = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["Год"].Value);

            // Строка для ъхранения SQL запроса для получения улсг по договору.
            string query = string.Empty;

            // Строка для хранения SQL запроса для получения данных по льготнику.
            string queryPerson = string.Empty;

            if (idYear < 2019 || idYear == 2020)
            {
                // Услуги по договору по таблице Архив.
                IQueryFind queryFind = new QueryContractАрхив(idДоговор);
                query = queryFind.Query();

                // Данные по льготнику.
                IQueryFind queryFindPerson = new QueryPersonАрхив(idДоговор);
                queryPerson = queryFindPerson.Query();
            }
            else if (idYear == 2019)
            {
                // Услуги по договору по таблице Add.
                IQueryFind queryFind = new QueryContractAdd(idДоговор);
                query = queryFind.Query();

                // Данные по льготнику.
                IQueryFind queryFindPerson = new QueryPersonAdd(idДоговор);
                queryPerson = queryFindPerson.Query();

            }
            else if (idYear >= 2021)
            {
                // Услуги по договору по таблице Договор.
                IQueryFind queryFind = new QueryContract2021(idДоговор);
                query = queryFind.Query();

                // Данные по льготнику.
                IQueryFind queryFindPerson = new QueryPerson2021(idДоговор);
                queryPerson = queryFindPerson.Query();
            }

            DataTable tabServices = ТаблицаБД.GetTableSQL(query, "УслугиПоДоговору");

            // Получим данные по льготнику.
            DataTable tabPerson = ТаблицаБД.GetTableSQL(queryPerson, "ДанныеПоЛьготнику");

            if (tabServices != null && tabServices.Rows != null)
            {

                FormDispContract fdc = new FormDispContract();
                fdc.ДанныеПоКонтракту = tabServices;
                fdc.ДанныеПоЛьготнику = tabPerson;

                fdc.TopMost = true;
                fdc.IdДоговор = idДоговор;
                fdc.Show();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ControlDantist.Classes;
using ControlDantist.Find;
using ControlDantist.Querys;
using DantistLibrary;
using ControlDantist.ClassUpdateFind;

namespace ControlDantist
{
    public partial class FormFindValidFalseContract : Form
    {
        private FactoryContract factoryContract;

        public FormFindValidFalseContract()
        {
            InitializeComponent();

            factoryContract = new FactoryContract();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FindByContract fbc = new FindByContract(this.textBox1.Text, false);
            this.LoadDate(fbc.GetNumber());
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
                FindPersonToSernameName findPerson = new FindPersonToSernameName(this.textBox2.Text, this.txtИмя.Text, false, false);
                this.LoadDate(findPerson.GetPersonFnSn());

                //PersonFnSn(true, false);
            }
            else if (this.textBox2.Text.Trim() != "" && this.txtИмя.Text.Trim() != "")
            {
                // Поиск льготников по фамилии и отчеству.
                // Поиск льготников по фамилии.
                FindPersonToSernameName findPerson = new FindPersonToSernameName(this.textBox2.Text, this.txtИмя.Text, false, true);
                this.LoadDate(findPerson.GetPersonFnSn());
                //PersonFnSn(true, true);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             // Получим id договора.
            int idДоговор = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_договор"].Value);

            // Получим год когда заключен договор.
            int idYear = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["Год"].Value);

            // Строка для ъхранения SQL запроса для получения улсг по договору.
            string query = string.Empty;

            // Строка для хранения SQL запроса для получения данных по льготнику.
            string queryPerson = string.Empty;

            if (idYear < 2019)
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

        private void анулироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //// Получим id договора.
            int idДоговор = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_договор"].Value);

            // Получим год когда заключен договор.
            int idYear = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["Год"].Value);

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

            // Получим текущую дату.
            string dateToday = DateTime.Now.Date.ToShortDateString();

            DialogResult dialogResult = MessageBox.Show("Анулировать договор?", "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if(dialogResult == DialogResult.OK)
            {
                IQuery queryCancelContract = factoryContract.SetCancelContract(user, dateToday, idДоговор);
                
                // Выполним скрипт.
                ExecuteQuery.Execute(queryCancelContract.Query());

                // Обновим страницу.
                FindByContract fbc = new FindByContract(this.textBox1.Text, false);
                this.LoadDate(fbc.GetNumber());

            }
            else
            {
                return;
            }


            //if (idYear == 2021)
            //{
            //    // флаг налчия акта выполненных работ.
            //    bool flagExistAct = false;

            //    // Получим 
            //    string strDateAct = ExistsAct.Exists(idДоговор, idYear, ref flagExistAct);

            //    if (flagExistAct == true)
            //    {
            //        DialogResult dialogResult = MessageBox.Show("Изменить статус договора нельзя, договор связан с актом -  " + strDateAct.Trim(), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            //        if (dialogResult == System.Windows.Forms.DialogResult.OK)
            //        {
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        // Переменная для хранения имени пользователя полученного из домена.
            //        string user = string.Empty;

            //        try
            //        {
            //            // Получим пользователя 
            //            user = MyAplicationIdentity.GetUses();
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("Ну удалось получит имя пользователя в домене - " + ex.Message.Trim());
            //        }

            //        // SQL запрос изменить статуст договора (паттер стратегия.)
            //        IQuery queryАнулировать = new QueryИзменитьСтатусДоговора(idДоговор, user);

            //        string strQueryDelete = queryАнулировать.Query();

            //        // Выполним скрипт.
            //        ExecuteQuery.Execute(strQueryDelete);

            //        // Обновим содержимое DataGridView.
            //        // Обновим DataGridView.
            //        string num = this.textBox1.Text;

            //        string firstName = this.textBox2.Text;

            //        string secondName = this.txtИмя.Text;

            //        List<ValideContract> listDisplay = new List<ValideContract>();

            //        // Очистим DataGridView от записеий.
            //        LoadDate(listDisplay);

            //        if (num != "")
            //        {

            //        }
            //        if (this.textBox2.Text.Trim() != "" && this.txtИмя.Text.Trim() == "")
            //        {
            //            // Поиск льготников по фамилии.
            //            PersonFnSn(true, false);
            //        }
            //        else if (this.textBox2.Text.Trim() != "" && this.txtИмя.Text.Trim() != "")
            //        {
            //            // Поиск льготников по фамилии и отчеству.
            //            PersonFnSn(true, true);
            //        }

            //    }
            //}
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ControlDantist.ClassessLimitYear;
using ControlDantist.Querys;
using ControlDantist.DataTableClassess;
using ControlDantist.Classes;

namespace ControlDantist
{
    public partial class FormLimitYear : Form
    {
        // Фабрика SQl запросов.
        private IGetSqlQuery getSqlQuerysYear;

        private IGetSqlQuery getSqlQueryLimit;

        // Фабрика DataTable.
        private GetTableSQL getTableSql;

        private ILoadData<ItemYear> loadDataYear;

        private ILoadData<ItemLimit> loadDataLimit;

        // Флаг указывающий форма загрузилась или нет.
        private bool flagLoad = false;

        // Переменная для хранения года.
        private int year = 0;

        public FormLimitYear()
        {
            InitializeComponent();

            getSqlQuerysYear = new GetQueryStringYar();

            // Получим строку подключения к БД.
            getTableSql = new GetTableSQL(ConnectDB.ConnectionString());

            // Получим текеущий год.
            DateTime dt = DateTime.Now;

            // Получим текущий год.
            year = dt.Year;

            getSqlQueryLimit = new GetQueryStringLimit(year);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormLimitYear_Load(object sender, EventArgs e)
        {
            // SQl запрос на получение года.
            IQuery queryYear = getSqlQuerysYear.GetSqlString();

            ТаблицаSqlBd tab = getTableSql.GetTable(queryYear.Query(), "Year");

            // Получаем таблицу с годами.
            DataTable tabYear = tab.GetTableSQL();

            if(tabYear != null && tabYear.Rows != null && tabYear.Rows.Count > 0)
            {

                LoadYear(tabYear);

                // Установим значение по умолчанию.
                ГодПоУмолчанию();

                // Строка запроса к БД на получение данных по лимитам.
                IQuery queryLimit = getSqlQueryLimit.GetSqlString();

                // Получим таблицу с данными по лимитам.
                ТаблицаSqlBd tabSqlLimit = getTableSql.GetTable(queryLimit.Query(), "Limit");

                // Получим табличное представление лимитов за год.
                DataTable tabLimit = tabSqlLimit.GetTableSQL();

                if(tabLimit != null && tabLimit.Rows != null && tabLimit.Rows.Count > 0)
                {
                    // Загружаем данные в DataGrid.
                    LoadLimit(tabLimit);

                    // Так как данные уже существуют то кнопку добавления данных сделаем не активной.
                    this.btnAdd.Enabled = false;
                }

            }

            // Флаг указывает что форма загрузилась.
            flagLoad = true;

        }

        private void LoadLimit(DataTable tab)
        {
            loadDataLimit = new LoadDataLimit(tab);

            this.dataGridView1.DataSource = loadDataLimit.Load();

        }

        private void LoadYear(DataTable tab)
        {
            loadDataYear = new LoadDataYear(tab);

            this.comboBox1.DataSource = loadDataYear.Load();
            this.comboBox1.DisplayMember = "Year";
            this.comboBox1.ValueMember = "intYear";
        }

        private void ГодПоУмолчанию()
        {
            int index = this.comboBox1.Items.Count;

            this.comboBox1.SelectedIndex = index-1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Проверим есть ли данные для текущего года.
            int seletYear = Convert.ToInt16(this.comboBox1.Text);

            //getSqlQueryLimit = new GetQueryStringLimit(year);
            getSqlQueryLimit = new GetQueryStringLimit(seletYear);

            IQuery queryLimit = getSqlQueryLimit.GetSqlString();

            // Получим таблицу с данными по лимитам.
            ТаблицаSqlBd tabSqlLimit = getTableSql.GetTable(queryLimit.Query(), "Limit");

            // Получим табличное представление лимитов за год.
            DataTable tabLimit = tabSqlLimit.GetTableSQL();

            if (tabLimit != null && tabLimit.Rows != null && tabLimit.Rows.Count > 0)
            {
                // Загружаем данные в DataGrid.
                //LoadLimit(tabLimit);
                //MessageBox.Show("Добавляем лимит за год");
            }
            else
            {
                FormAddLimit formAddLimit = new FormAddLimit();
                formAddLimit.Year = seletYear;
                formAddLimit.FlagInsert = true;
                DialogResult dialogResult =  formAddLimit.ShowDialog();

                if (formAddLimit.FlagErrorInsertUpdate == false)
                {
                    if (dialogResult == DialogResult.OK)
                    {
                        // Обновим DataGrid.
                    }
                }
            }


        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (flagLoad == true)
            {

                if (string.IsNullOrEmpty(this.comboBox1.Text) == false)
                {
                    // Проверим есть ли данные для текущего года.
                    int seletYear = Convert.ToInt16(this.comboBox1.Text);

                    //getSqlQueryLimit = new GetQueryStringLimit(year);
                    getSqlQueryLimit = new GetQueryStringLimit(seletYear);

                    IQuery queryLimit = getSqlQueryLimit.GetSqlString();

                    // Получим таблицу с данными по лимитам.
                    ТаблицаSqlBd tabSqlLimit = getTableSql.GetTable(queryLimit.Query(), "Limit");

                    // Получим табличное представление лимитов за год.
                    DataTable tabLimit = tabSqlLimit.GetTableSQL();

                    var rowsSql = queryLimit.Query();

                    var asd = tabLimit.Rows.Count;

                    if (tabLimit.Rows.Count == 0)
                    {
                        // Загружаем данные в DataGrid.
                        LoadLimit(tabLimit);
                        this.btnAdd.Enabled = true;
                    }
                    else if (tabLimit.Rows.Count == 1)
                    {
                        // Загружаем данные в DataGrid.
                        LoadLimit(tabLimit);
                        this.btnAdd.Enabled = false;
                    }
                }
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Проверим есть ли данные для текущего года.
            int seletYear = Convert.ToInt16(this.comboBox1.Text);

            string summ = this.dataGridView1.CurrentRow.Cells["LimitMoneyYear"].Value.ToString();

            int idLimit = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["idLimitYear"].Value);

            // Отредактируем данные.
            FormAddLimit formAddLimit = new FormAddLimit();
            formAddLimit.Year = seletYear;
            formAddLimit.FlagInsert = false;
            formAddLimit.Summ = summ;
            formAddLimit.IdLimit = idLimit;
            DialogResult dialogResult = formAddLimit.ShowDialog();

            if (formAddLimit.FlagErrorInsertUpdate == false)
            {
                if (dialogResult == DialogResult.OK)
                {
                    // Обновим DataGrid.
                    //getSqlQueryLimit = new GetQueryStringLimit(year);
                    getSqlQueryLimit = new GetQueryStringLimit(seletYear);

                    IQuery queryLimit = getSqlQueryLimit.GetSqlString();

                    // Получим таблицу с данными по лимитам.
                    ТаблицаSqlBd tabSqlLimit = getTableSql.GetTable(queryLimit.Query(), "Limit");

                    // Получим табличное представление лимитов за год.
                    DataTable tabLimit = tabSqlLimit.GetTableSQL();

                    LoadLimit(tabLimit);
                }
            }
        }
    }
}

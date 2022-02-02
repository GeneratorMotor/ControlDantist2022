using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using ControlDantist.Querys;
using System.Windows.Forms;
using ControlDantist.ClassessLimitYear;
using ControlDantist.Classes;

namespace ControlDantist
{
    public partial class FormAddLimit : Form
    {
        /// <summary>
        /// Выбранный год.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Флаг указывает что форма работает в режиме добавления - True
        /// или в режиме редактирования записи или удаления записи.
        /// </summary>
        public bool FlagInsert { get; set; }

        /// <summary>
        /// ID записи которую необходимо редактировать.
        /// </summary>
        public int IdLimit { get; set; }

        private CreateLimitYear createLimit;

        public bool FlagErrorInsertUpdate { get; set; }

        /// <summary>
        /// Сумма передаваемая в форму.
        /// </summary>
        public string Summ { get; set; }

        public FormAddLimit()
        {
            InitializeComponent();

            createLimit = new CreateLimitYear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string summ = string.Empty;
            // Сумма годового лимита
            if (String.IsNullOrEmpty(this.textBox1.Text) == false)
            {
                summ = this.textBox1.Text.Replace(',','.');

                var testSumm = summ;
            }
            else
            {
                MessageBox.Show("Внесите сумму");
                return;
            }    

            try
            {
                // Если форма в режиме добавления новой записи.
                if (FlagInsert == true)
                {

                    // SQL запрос на добавление годового лимита.
                    IQuery queryInsert = createLimit.CreateLimited(summ, this.Year);

                    // Выполнить SQL скрипт.
                    ExecuteQuery.Execute(queryInsert.Query(), ConnectDB.ConnectionString());
                }
                else if (FlagInsert == false)
                {
                    // SQL запрос на добавление годового лимита.
                    IQuery queryUpdate = createLimit.UpdateLimited(this.Year, summ, this.IdLimit);

                    var sqlTest = queryUpdate.Query();

                    // Выполнить SQL скрипт.
                    ExecuteQuery.Execute(queryUpdate.Query(), ConnectDB.ConnectionString());
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

                // Ошибка.
                this.FlagErrorInsertUpdate = true;

            }


        }

        

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44) // цифры, клавиша BackSpace и запятая
            {
                e.Handled = true;
            }
        }

        private void FormAddLimit_Load(object sender, EventArgs e)
        {
            if(FlagInsert == false)
            {
                this.textBox1.Text = this.Summ;
            }
        }
    }
}

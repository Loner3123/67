using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.Windows.Forms;
using Mysqlx.Crud;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace My_exam
{
    public partial class login : Form
    {
        public string myconnection = "datasource=localhost;port=3306;username=root;password=1234;database=pdm";

        public login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string dbUsername = "";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(myconnection))
                {
                    conn.Open();
                    string query = "select * from users where username=@u and password=@p";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", password);

                    MySqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        dbUsername = dr["username"].ToString();
                    }

                    if (string.IsNullOrEmpty(dbUsername))
                    {
                        MessageBox.Show("WRONG USERNAME OR PASSWORD", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Welcome: " + dbUsername, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                        // ПЕРЕДАЕМ ИМЯ В НОВУЮ ФОРМУ
                        home mainForm = new home(dbUsername);
                        mainForm.Show();
                        this.Hide(); // Hide лучше чем Dispose, если нужно вернуться
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            singup regisForm = new singup();
            regisForm.Show();
            this.Hide();
        }

        // Оставляем пустыми, если они были созданы случайно
        private void Label6_Click(object sender, EventArgs e) { }
        private void DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
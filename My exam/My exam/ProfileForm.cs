using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace My_exam
{

        public partial class ProfileForm : Form
        {
            public string myconnection = "datasource=localhost;port=3306;username=root;password=1234;database=pdm";
            private string currentUser;

        public ProfileForm(string username)
        {
            InitializeComponent();
            currentUser = username;
        }

        private void ProfileForm_Load(object sender, EventArgs e)
        {
            lblUserHeader.Text = "Пользователь: " + currentUser;
            LoadStats();
            LoadUserData();
        }

        private void LoadStats()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(myconnection))
                {
                    conn.Open();
                    MySqlCommand cmdSent = new MySqlCommand("SELECT COUNT(*) FROM usermessage WHERE username = @u", conn);
                    cmdSent.Parameters.AddWithValue("@u", currentUser);
                    int sent = Convert.ToInt32(cmdSent.ExecuteScalar());

                    MySqlCommand cmdRec = new MySqlCommand("SELECT COUNT(*) FROM usermessage WHERE touser = @u", conn);
                    cmdRec.Parameters.AddWithValue("@u", currentUser);
                    int received = Convert.ToInt32(cmdRec.ExecuteScalar());

                    lblSent.Text = "Отправлено сообщений: " + sent;
                    lblReceived.Text = "Получено сообщений: " + received;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки статистики: " + ex.Message);
            }
        }

        private void LoadUserData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(myconnection))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT fullname, age, password FROM users WHERE username = @u", conn);
                    cmd.Parameters.AddWithValue("@u", currentUser);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtFullname.Text = reader["fullname"].ToString();
                            txtAge.Text = reader["age"].ToString();
                            txtPassword.Text = reader["password"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(myconnection))
                {
                    conn.Open();
                    string query = "UPDATE users SET fullname=@fn, age=@age, password=@pass WHERE username=@u";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@fn", txtFullname.Text);
                    cmd.Parameters.AddWithValue("@age", txtAge.Text);
                    cmd.Parameters.AddWithValue("@pass", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@u", currentUser);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Данные успешно обновлены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Обновляем статистику на всякий случай
                    LoadStats();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка обновления: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
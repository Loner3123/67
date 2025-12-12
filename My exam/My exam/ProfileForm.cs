using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public string myconnection = "datasource=localhost;port=3306;username=root;password=1111;database=pdm";
        private string currentUser;

        public ProfileForm(string username)
        {
            InitializeComponent();
            this.currentUser = username;
        }

        private void ProfileForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentUser) || currentUser == "Гость")
            {
                MessageBox.Show("Пользователь не авторизован.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblUserHeader.Text = "Пользователь: " + currentUser;
            LoadStats(currentUser);
            LoadUserData(currentUser);
        }

        private void LoadStats(string username)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(myconnection))
                {
                    conn.Open();
                    MySqlCommand cmdSent = new MySqlCommand("SELECT COUNT(*) FROM usermessage WHERE username = @u", conn);
                    cmdSent.Parameters.AddWithValue("@u", username);
                    int sent = Convert.ToInt32(cmdSent.ExecuteScalar());

                    MySqlCommand cmdRec = new MySqlCommand("SELECT COUNT(*) FROM usermessage WHERE touser = @u", conn);
                    cmdRec.Parameters.AddWithValue("@u", username);
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

        private void LoadUserData(string username)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(myconnection))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT fullname, age, password FROM users WHERE username = @u", conn);
                    cmd.Parameters.AddWithValue("@u", username);
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
            catch (Exception ex) { MessageBox.Show("Ошибка загрузки данных: " + ex.Message); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string username = this.currentUser;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(myconnection))
                {
                    conn.Open();
                    string query = "UPDATE users SET fullname=@fn, age=@age, password=@pass WHERE username=@u";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@fn", txtFullname.Text);
                        cmd.Parameters.AddWithValue("@age", txtAge.Text);
                        cmd.Parameters.AddWithValue("@pass", txtPassword.Text);
                        cmd.Parameters.AddWithValue("@u", username);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Данные успешно обновлены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadStats(username);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка обновления: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            home backToHome = new home(currentUser);
            backToHome.Show();
            this.Close();
        }
    }
}
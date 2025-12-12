using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_exam
{
    public partial class message : Form
    {
        public string myconnection = "datasource=localhost;port=3306;username=root;password=1111;database=pdm";
        private string senderUsername;

        public message(string username)
        {
            InitializeComponent();
            this.senderUsername = username;
            textBox1.Text = username;
            textBox1.ReadOnly = true;
        }

        public message()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            home homeForm = new home(senderUsername);
            homeForm.Show();
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string usernameToSend = this.senderUsername;
                string messageText = richTextBox1.Text;
                string receiver = textBox2.Text;

                if (string.IsNullOrEmpty(usernameToSend) || string.IsNullOrEmpty(messageText) || string.IsNullOrEmpty(receiver))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля (Отправитель, Сообщение, Получатель).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (MySqlConnection conn = new MySqlConnection(myconnection))
                {
                    conn.Open();

                    string sql = @"
                        INSERT INTO usermessage (username, messege, userid, touser) 
                        VALUES (@username, @messege, (SELECT id FROM users WHERE username = @username), @touser);";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", usernameToSend);
                        cmd.Parameters.AddWithValue("@messege", messageText);
                        cmd.Parameters.AddWithValue("@touser", receiver);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Сообщение успешно отправлено!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка: Сообщение не было отправлено. Возможно, пользователь-отправитель не существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                home backtohome = new home(usernameToSend);
                backtohome.Show();
                this.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при отправке сообщения: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
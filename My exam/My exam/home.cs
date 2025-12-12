using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace My_exam
{
    public partial class home : Form
    {
        public string myconnection = "datasource=localhost;port=3306;username=root;password=1111;database=pdm";
        private string currentUser;

        public home(string username)
        {
            InitializeComponent();
            this.currentUser = username;
        }

        public home()
        {
            InitializeComponent();
            this.currentUser = "Гость";
        }

        private void Home_Load(object sender, EventArgs e)
        {
            LoadUserHeader();
            LoadMessages();
        }

        private void LoadUserHeader()
        {
            if (currentUser != "Гость" && !string.IsNullOrEmpty(currentUser))
            {
                label6.Text = $"Welcome, {currentUser}";
            }
            else
            {
                label6.Text = "Welcome, Guest (Please Sign In)";
                btnProfile.Enabled = false;
                button1.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void LoadMessages(string messageId = null)
        {
            if (currentUser == "Гость") return;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(myconnection))
                {
                    conn.Open();

                    string query = "SELECT id, username AS Sender, messege, date_sent FROM usermessage WHERE touser = @u";

                    if (!string.IsNullOrEmpty(messageId))
                    {
                        if (int.TryParse(messageId, out int id))
                        {
                            query += " AND id = @id";
                        }
                        else
                        {
                            MessageBox.Show("Введен некорректный ID сообщения.", "Ошибка ввода");
                            messageId = null;
                        }
                    }

                    query += " ORDER BY date_sent DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@u", currentUser);

                        if (!string.IsNullOrEmpty(messageId) && int.TryParse(messageId, out int idValue))
                        {
                            cmd.Parameters.AddWithValue("@id", idValue);
                        }

                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        asfasdfsadfsda.DataSource = table;

                        if (asfasdfsadfsda.Columns.Contains("id"))
                        {
                            asfasdfsadfsda.Columns["id"].Visible = false;
                        }

                        if (table.Rows.Count == 0 && !string.IsNullOrEmpty(messageId))
                        {
                            MessageBox.Show($"Сообщение с ID {messageId} не найдено среди ваших полученных сообщений.", "Результат поиска");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки/поиска сообщений: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            message messageForm = new message(currentUser);
            messageForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (currentUser == "Гость") return;

            if (int.TryParse(textBox1.Text, out int messageId))
            {
                if (MessageBox.Show($"Вы уверены, что хотите удалить сообщение ID: {messageId}?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }

                try
                {
                    using (MySqlConnection conn = new MySqlConnection(myconnection))
                    {
                        conn.Open();
                        string query = "DELETE FROM usermessage WHERE id = @id AND touser = @u";

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", messageId);
                            cmd.Parameters.AddWithValue("@u", currentUser);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Сообщение успешно удалено.", "Успех");
                                LoadMessages();
                            }
                            else
                            {
                                MessageBox.Show("Сообщение не найдено или у вас нет прав на его удаление.", "Ошибка");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректный ID сообщения.", "Ошибка ввода");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string searchId = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(searchId))
            {
                LoadMessages();
                MessageBox.Show("Показан полный список полученных сообщений.", "Поиск сброшен");
            }
            else
            {
                LoadMessages(searchId);
            }
        }


        private void BtnSignOut_Click(object sender, EventArgs e)
        {
            this.Dispose();
            MessageBox.Show("Вы вышли из системы.", "Выход");
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            if (currentUser != "Гость" && !string.IsNullOrEmpty(currentUser))
            {
                ProfileForm profileForm = new ProfileForm(currentUser);
                profileForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Для просмотра профиля необходимо войти.", "Внимание");
            }
        }

        private void Asfasdfsadfsda_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (asfasdfsadfsda.Columns.Contains("id"))
                {
                    DataGridViewCell idCell = asfasdfsadfsda.Rows[e.RowIndex].Cells["id"];

                    if (idCell.Value != null)
                    {
                        textBox1.Text = idCell.Value.ToString();
                    }
                }
            }
        }
    }
}
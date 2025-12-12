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
    public partial class home : Form
    {
        public string myconnection = "datasource=localhost;port=3306;username=root;password=1234;database=pdm";
        public string currentUsername;

        // Новый конструктор с параметром
        public home(string user)
        {
            InitializeComponent();
            currentUsername = user;
            label6.Text = "HOME (" + user + ")"; // Показываем имя в заголовке
        }

        // Старый конструктор (на всякий случай)
        public home()
        {
            InitializeComponent();
            currentUsername = "Guest";
        }

        private void Home_Load(object sender, EventArgs e)
        {
            LoadMessages();
        }

        private void LoadMessages()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(myconnection))
                {
                    conn.Open();
                    string query = "SELECT * from usermessage";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    asfasdfsadfsda.DataSource = dt;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            // Открываем новую форму профиля
            ProfileForm pf = new ProfileForm(currentUsername);
            pf.ShowDialog();
        }

        private void BtnSignOut_Click(object sender, EventArgs e)
        {
            login loginForm = new login();
            loginForm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            message messegeForm = new message();
            messegeForm.Show();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sherch sherchForm = new sherch();
            sherchForm.Show();
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(myconnection))
                {
                    conn.Open();
                    MySqlCommand delete = new MySqlCommand("delete from usermessage where id = @id", conn);
                    delete.Parameters.AddWithValue("@id", textBox1.Text);
                    delete.ExecuteNonQuery();
                    MessageBox.Show("Сообщение удалено");
                    LoadMessages(); // Обновить таблицу
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void Asfasdfsadfsda_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void Label1_Click(object sender, EventArgs e) { }
    }
}
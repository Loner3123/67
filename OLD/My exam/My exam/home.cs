using System;

using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace My_exam
{
    public partial class home : Form
    {

        public string myconnection = "datasource=localhost;port=3306;username=root;password=;database=pdm";
        public home()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Home_Load(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(myconnection);
            conn.Open();

            string query = "SELECT * from usermessage";
            MySqlCommand asdas = new MySqlCommand(query, conn);
            MySqlDataAdapter DataAd = new MySqlDataAdapter();
            DataAd.SelectCommand = asdas;
            DataTable dataTab = new DataTable();
            DataAd.Fill(dataTab);
            asfasdfsadfsda.DataSource = dataTab;
        }

        private void BtnSignOut_Click(object sender, EventArgs e)
        {
            login login = new login();
            login.Show();
            this.Close();
        }

        private void Asfasdfsadfsda_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
            MySqlConnection conn = new MySqlConnection(myconnection);
            conn.Open();
            MySqlCommand delete = new MySqlCommand("delete from usermessage where iduserinfo = @iduserinfo", conn);
            delete.Parameters.AddWithValue("@iduserinfo", textBox1.Text);
            MySqlDataAdapter adap2 = new MySqlDataAdapter(delete);
            DataTable dt2 = new DataTable();
            adap2.Fill(dt2);
            conn.Close();
        }
    }
}

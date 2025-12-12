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
    public partial class delete : Form
    {
        public string myconnection = "datasource=localhost;port=3306;username=root;password=;database=pdm";
        public delete()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(myconnection);
            conn.Open();
            MySqlCommand delete = new MySqlCommand("delete from users where username = @username and password = @password", conn);
            delete.Parameters.AddWithValue("@username", textBox2.Text);
            delete.Parameters.AddWithValue("@password", textBox3.Text);
            MySqlDataAdapter adap2 = new MySqlDataAdapter(delete);
            DataTable dt2 = new DataTable();
            adap2.Fill(dt2);
            login login = new login();
            login.Show();
            this.Close();
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            login login = new login();
            login.Show();
            this.Close();
         
        }
    }
}

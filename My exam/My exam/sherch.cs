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

namespace My_exam
{
    public partial class sherch : Form
    {
        public string myconnection = "datasource=localhost;port=3306;username=root;password=1234;database=pdm";
        public sherch()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(myconnection);
            conn.Open();
            MySqlCommand select = new MySqlCommand("select * from usermessage where username = @username", conn);
            select.Parameters.AddWithValue("@username", textBox1.Text);
            MySqlDataAdapter adap2 = new MySqlDataAdapter(select);
            DataTable dt2 = new DataTable();
            adap2.Fill(dt2);
            dataGridView1.DataSource = dt2;
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            home backtohome = new home();
            backtohome.Show();
            this.Dispose();
        }
    }
}

using MySql.Data.MySqlClient;
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
namespace My_exam
{
    public partial class message : Form
    {
        public string myconnection = "datasource=localhost;port=3306;username=root;password=1234;database=pdm";
        public message()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            home homeForm = new home();
            homeForm.Show();
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            {
                string username = textBox1.Text;
                string password = richTextBox1.Text;



                MySqlConnection conn = new MySqlConnection(myconnection);
                conn.Open();

                MySqlCommand dfskjgfsdh = new MySqlCommand("INSERT INTO usermessage (username, messege, userid, touser) VALUES (@username, @messege, (SELECT username FROM users WHERE username = @username), @touser);", conn);
                dfskjgfsdh.Parameters.AddWithValue("@username", textBox1.Text);
                dfskjgfsdh.Parameters.AddWithValue("@messege", richTextBox1.Text);
                dfskjgfsdh.Parameters.AddWithValue("@touser", textBox2.Text);
                MySqlDataAdapter adapter = new MySqlDataAdapter(dfskjgfsdh);
                DataTable table1 = new DataTable();
                adapter.Fill(table1);




                home backtohome = new home();
                backtohome.Show();
                this.Dispose();

                conn.Close();


                MessageBox.Show("Сообщение отправлено!");

            }

            
        }
    }
}

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
    public partial class singup : Form
    { int a = 0;
        MySqlCommand dfskjgfsdh2;
        public string myconnection = "datasource=localhost;port=3306;username=root;password=1111;database=pdm";

        public singup()
        {
            InitializeComponent();
        }

        private void Singup_Load(object sender, EventArgs e)
        {

        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = sdfasdfa.Text;
                string password = fasfasf.Text;
                string fullname = asdcvasdc.Text;
                string age = sadfafsaf.Text;
                string gender = sdvasfsadfsa.Text;




                MySqlConnection conn = new MySqlConnection(myconnection);
                conn.Open();

                MySqlCommand dfskjgfsdh = new MySqlCommand("INSERT INTO users(username,password,fullname,age,gender ) VALUES (@username,@password,@fullname,@age,@gender )", conn);
                dfskjgfsdh.Parameters.AddWithValue("@username", sdfasdfa.Text);
                dfskjgfsdh.Parameters.AddWithValue("@password", fasfasf.Text);
                dfskjgfsdh.Parameters.AddWithValue("@fullname", asdcvasdc.Text);
                dfskjgfsdh.Parameters.AddWithValue("@age", (sadfafsaf.Text));
                dfskjgfsdh.Parameters.AddWithValue("@gender", sdvasfsadfsa.Text);
                MySqlDataAdapter adapter = new MySqlDataAdapter(dfskjgfsdh);
                DataTable table1 = new DataTable();
                adapter.Fill(table1);




                login backtoLogin = new login();
                backtoLogin.Show();
                this.Dispose();

                conn.Close();
                MessageBox.Show("Success Registration !");

            }
            catch
            {
                MessageBox.Show("INVALID REGISTRATION OR WRONG FIELD TYPE !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            login regisForm = new login();
            regisForm.Show();
            this.Dispose();
        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            delete delete = new delete();
            delete.Show();
            this.Close();
        }
    }
    }


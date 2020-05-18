using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace kurs
{
    public partial class MainForm : Form
    {
        private OracleConnection con = new OracleConnection();
        private StreamWriter sv;
        public MainForm()
        {
            InitializeComponent();
            sv = new StreamWriter("log.txt");
            
        }

        private void connectDatabaseButton_Click(object sender, EventArgs e)
        {
            try
            {
                string conString = "User Id=c##katya; password=123;" +
                                "Data Source=localhost:1521;";

                OracleConnection con = new OracleConnection();
                con.ConnectionString = conString;
                con.Open();

                string sql = "Select count(*) from users where name = '" + name.Text + 
                    "' and password=" + "(select DBMS_CRYPTO.HASH(rawtohex('" +
                    pass.Text+ "') ,2) as md5 from dual)";
                OracleCommand cmdIC = con.CreateCommand();
                cmdIC.CommandText = sql;
                int result = Convert.ToInt32(cmdIC.ExecuteScalar());
                
                if (result!=0)
                {
                
                    UserScreen userScreen = new UserScreen(con, sv);

                    MessageBox.Show("Вход выполнен!");
                    userScreen.Show();
                }
                else
                {
                    throw new Exception("Wrong name or password");
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            con.Close();
        }
    }
}

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
    public partial class BalancesScreen : Form
    {
        private OracleConnection con;
        private DataSet balanceSet;
        private OracleDataAdapter selectBalance;
        private StreamWriter sv;

        public BalancesScreen(OracleConnection conn, StreamWriter svv)
        {
            this.con = conn;
            this.sv = svv;
            InitializeComponent();
            Show_Balances();
        }

        private void Show_Balances()
        {
            balanceSet = new DataSet();
            String selectBalanceRequest = "SELECT * FROM balance";

            selectBalance = new OracleDataAdapter(selectBalanceRequest, 
                "User Id=c##katya; password=123;Data Source=localhost:1521;");

            selectBalance.Fill(balanceSet, "Balance");
            dataGrid1.SetDataBinding(balanceSet, "Balance");
        }

        private void createBalance_Click(object sender, EventArgs e)
        {
            var cmd1 = new OracleCommand("createBalance", con);
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            cmd1.Parameters.Add("@begin_date", beginDate.Text);
            cmd1.Parameters.Add("@end_date", endDate.Text);
            try
            {
                cmd1.ExecuteNonQuery();
                sv.WriteLine("Balance from "+ beginDate.Text+" to "+ endDate.Text+" create");
                sv.Flush();
                MessageBox.Show("Баланс создан!");
                beginDate.Text = "";
                endDate.Text = "";
                Show_Balances();
            }
            catch (OracleException exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }
        private void delBalance_Click(object sender, EventArgs e)

        {
            var cmd1 = new OracleCommand("deleteBalance", con);
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            cmd1.Parameters.Add("@bal_id", number.Text);
            try
            {
                cmd1.ExecuteNonQuery();
                sv.WriteLine("Balance with id " + number.Text + " delete");

                MessageBox.Show("Баланс удалён!");
                number.Text = "";
                Show_Balances();
            }
            catch (OracleException exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }
    }
}

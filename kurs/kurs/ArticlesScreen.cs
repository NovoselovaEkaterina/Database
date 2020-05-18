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
    public partial class ArticlesScreen : Form
    {
        private OracleConnection con;
        private DataSet articleSet;
        private OracleDataAdapter selectArticle;
        private StreamWriter sv;

        public ArticlesScreen(OracleConnection conn, StreamWriter svv)
        {
            this.con = conn;
            this.sv = svv;
            InitializeComponent();
            Show_Articles();
        }

        private void Show_Articles()
        {
            articleSet = new DataSet();
            String selectArticleRequest = "SELECT * FROM articles";
            selectArticle = new OracleDataAdapter(selectArticleRequest, 
                "User Id=c##katya; password=123;Data Source=localhost:1521;");

            selectArticle.Fill(articleSet, "Articles");
            dataGrid1.SetDataBinding(articleSet, "Articles");
        }

        private void Add_Click(object sender, EventArgs e)
        {
            String sql = "insert into articles(name) values('" +
                nameArt.Text +
                "')";
            OracleCommand cmdIC = con.CreateCommand();
            cmdIC.CommandText = sql;
            
            try
            {
                cmdIC.ExecuteNonQuery();
                MessageBox.Show("Запись добавлена!");
                nameArt.Text = "";
                Show_Articles();
            }
            catch (OracleException exc)
            {
                MessageBox.Show(exc.ToString());
            }
            
        }

        private void Modify_Click(object sender, EventArgs e)
        {
            String sql = "update articles set name = '" +
                nameArt.Text + "' where id = " + IdArt.Text;
            OracleCommand cmdIC = con.CreateCommand();
            cmdIC.CommandText = sql;
            try
            {
                cmdIC.ExecuteNonQuery();
                MessageBox.Show("Запись изменена!");
                IdArt.Text = "";
                nameArt.Text = "";
                Show_Articles();
            }
            catch (OracleException exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void Del_Click(object sender, EventArgs e)
        {
            var cmd1 = new OracleCommand("delArticle", con);
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            cmd1.Parameters.Add("@art_id", IdArt.Text);
            try
            {
                cmd1.ExecuteNonQuery();
                sv.WriteLine("Article with id " + IdArt.Text + " delete");
                sv.Flush();
                MessageBox.Show("Запись удалена!");
                IdArt.Text = "";
                Show_Articles();
            }
            catch (OracleException exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }
    }
}

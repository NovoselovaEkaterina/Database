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
using System.Windows.Forms.DataVisualization.Charting;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace kurs
{
    public partial class StatisticsScreen : Form
    {
        private OracleConnection con;
        private StreamWriter sv;

        public StatisticsScreen(OracleConnection conn, StreamWriter svv)
        {
            this.con = conn;
            this.sv = svv;
            InitializeComponent();
            chart1.ChartAreas.Add("Show amount");
            Show_Amount();
        }
        private OracleDataAdapter select;
        private void Show_Amount()
        {
            String selectRequest = "select amount,create_date from balance";
            select = new OracleDataAdapter(selectRequest,
                "User Id=c##katya; password=123;Data Source=localhost:1521;");
            var series = new Series("amount");
            series.ChartType = SeriesChartType.Line;
            series.ChartArea = "Show amount";
            OracleCommand cmdIC = con.CreateCommand();
            cmdIC.CommandText = selectRequest;
            try
            {
                var reader = cmdIC.ExecuteReader();
                OracleTimeStamp t = new OracleTimeStamp(2020, 1, 1);
                while (reader.Read())
                {
                    series.Points.AddXY((double)reader.GetOracleTimeStamp(1).GetDaysBetween(t).TotalDays,
                        reader.GetDouble(0));
                }
                chart1.Series.Add(series);
            }
            catch (OracleException exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }
        
        private void streamByTime_Click(object sender, EventArgs e)
        {
            DataSet set;
            set = new DataSet();
            String selectRequest = "select article_id, debit, credit, create_date from operations " +
                "where create_date between '" +
                beginDate.Text+ "' and '" +endDate.Text+
                "' and (article_id =";
            string arts = IdArts.Text;
            int i = 0;
            while(i<arts.Length)
            {
                if(arts[i]>'0' && arts[i] < '9')
                {
                    if(i>0)
                        selectRequest += " or article_id =";
                    string art = "";
                    art+=arts[i];
                    i++;
                    while(i < arts.Length && arts[i] > '0' && arts[i] < '9')
                    {
                        art += arts[i];
                        i++;
                    }
                    selectRequest += art;
                }
                i++;
            }
            selectRequest += ")";
            select = new OracleDataAdapter(selectRequest,
                "User Id=c##katya; password=123;Data Source=localhost:1521;");

            select.Fill(set, "Stream");
            var rows = set.Tables["Stream"].Rows;
            sv.WriteLine("Динамика изменения финансовых потоков");
            sv.WriteLine("Время начала: " + beginDate.Text);
            sv.WriteLine("Время конца: " + endDate.Text);
            sv.WriteLine("Номер статьи | Доход | Расход | Дата совершения операции");
            for (int j = 0; j < rows.Count; j++)
            {
                sv.WriteLine(rows[j].Field<decimal>("article_id") + "              " 
                    + rows[j].Field<decimal>("debit")+"      "+ rows[j].Field<decimal>("credit")+
                    "        " + rows[j].Field<DateTime>("create_date"));
            }
            sv.WriteLine("");
            sv.Flush();

            dataGrid1.SetDataBinding(set, "Stream");
        }

        private void artsByPercent_Click(object sender, EventArgs e)
        {
            DataSet set;
            set = new DataSet();
            String selectRequest = "select article_id, round(sum(" + type.SelectedItem.ToString()
                + ")*100/(select sum(" + type.SelectedItem.ToString() +
                ") from balance where create_date between '" + beginDate.Text + "' and '" + endDate.Text +
                "'),5) as percent from operations " +
                "where balance_id in (select id from balance where create_date between '" +
                beginDate.Text + "' and '" + endDate.Text +
                "') and (article_id =";
            string arts = IdArts.Text;
            int i = 0;
            while (i < arts.Length)
            {
                if (arts[i] > '0' && arts[i] < '9')
                {
                    if (i > 0)
                        selectRequest += " or article_id =";
                    string art = "";
                    art += arts[i];
                    i++;
                    while (i < arts.Length && arts[i] > '0' && arts[i] < '9')
                    {
                        art += arts[i];
                        i++;
                    }
                    selectRequest += art;
                }
                i++;
            }
            selectRequest += ") group by article_id";
            select = new OracleDataAdapter(selectRequest,
                "User Id=c##katya; password=123;Data Source=localhost:1521;");
            select.Fill(set, "Stream");
            var rows = set.Tables["Stream"].Rows;
            sv.WriteLine("Процентное соотношение финансовых потоков ("+ type.SelectedItem.ToString() + ") по статьям");
            sv.WriteLine("Время начала: " + beginDate.Text);
            sv.WriteLine("Время конца: " + endDate.Text);
            sv.WriteLine("Номер статьи | Процент ");
            for (int j = 0; j < rows.Count; j++)
            {
                sv.WriteLine(rows[j].Field<decimal>("article_id") + "              " + rows[j].Field<decimal>("Percent"));
            }
            sv.WriteLine("");
            sv.Flush();

            dataGrid1.SetDataBinding(set, "Stream");
        }
    }
}

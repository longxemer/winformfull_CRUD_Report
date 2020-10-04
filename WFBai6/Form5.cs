using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.Shared;

namespace WFBai6
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        // khai báo chuỗi kết nối 
        static String con = @"Data Source=DESKTOP-C699A2T;Initial Catalog = WFBai6; Integrated Security = True";
        // kết nối SQL
        static SqlConnection cnn = new SqlConnection(con);


        private void Form5_Load(object sender, EventArgs e)
        {
            //String a = MaPhong(comboBox1.Text);
            cnn.Open();
            String sql = "select * from v_soluong";
            SqlCommand cmd = new SqlCommand(sql, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CrystalReport6 rp = new CrystalReport6();
            //crystalReportViewer1.C;
            rp.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rp;
            cnn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
        }
    }
}

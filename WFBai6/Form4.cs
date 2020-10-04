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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        // khai báo chuỗi kết nối 
        static String con = @"Data Source=DESKTOP-C699A2T;Initial Catalog = WFBai6; Integrated Security = True";
        // kết nối SQL
        static SqlConnection cnn = new SqlConnection(con);

        // đưa dữ liệu vào comboBox
        public void loadCbb()
        {
            using (SqlConnection cnn = new SqlConnection(con))
            {
                using (SqlCommand cmd = new SqlCommand("select * from Phong", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        comboBox1.DataSource = tb;
                        comboBox1.DisplayMember = "sTenP";
                        comboBox1.ValueMember = "sMaP";
                    }
                }
            }
        }

        //Kiểm tra tên Phòng
        public String MaPhong(String ten)
        {
            
            cnn.Open();
            String s = null;
            String sql = "select sMaP from Phong where sTenP = '" + ten.Trim() + "'";
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                s += dr["sMaP"];
                break;
            }
            cnn.Close();
            return s;

        }


        private void btnXem_Click(object sender, EventArgs e)
        {
            
            String a = MaPhong(comboBox1.Text);
            cnn.Open();
            String sql = "select * from v_NhanVien where MaP = '"+a+"'";
            SqlCommand cmd = new SqlCommand(sql, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CrystalReport5 rp = new CrystalReport5();
            //crystalReportViewer1.C;
            rp.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rp;
            cnn.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            loadCbb();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            f5.Show();
            //Form4 f4 = new Form4();
            
        }
    }
}

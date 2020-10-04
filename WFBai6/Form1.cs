using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WFBai6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        // khai báo chuỗi kết nối 
        static String con = @"Data Source=DESKTOP-C699A2T;Initial Catalog = WFBai6; Integrated Security = True";
        // kết nối SQL
        static SqlConnection cnn = new SqlConnection(con);
        // hàm đưa dữ liệu lên Gridview



        // hàm kiểm tra mã trùng lặp 
        public bool CheckMa(String ma)
        {
            cnn.Open();
            String sql = "select sMaNV from NhanVien where sMaNV = '" + ma + "'";
            SqlCommand cmd = new SqlCommand(sql, cnn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read() == true)
            {
                cnn.Close();
                return true;
            }
            else
            {
                cnn.Close();
                return false;
            }
        }

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
                        cbPhong.DataSource = tb;
                        cbPhong.DisplayMember = "sTenP";
                        cbPhong.ValueMember = "sMaP";
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
            while(dr.Read())
            {
                s += dr["sMaP"];
                break;
            }
            cnn.Close();
            return s;
            
        }
            
        


        // đưa dữ liệu lên Grid view
        public void LoadData()
        {
            // khai báo kết nối SQL
            
            try
            {
                cnn.Open();
                String sql = "select * from v_NhanVien";
                SqlDataAdapter dt = new SqlDataAdapter(sql, cnn);
                DataTable db = new DataTable();
                dt.Fill(db);
                dataGV1.DataSource = db;
                cnn.Close();
            }catch(Exception e)
            {
                MessageBox.Show("Lỗi :" + e);
            }
            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            loadCbb();
        }




        // thêm dữ liệu vào 
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtMa.Text != "")
                {
                    if (CheckMa(txtMa.Text) == true)
                    {
                        MessageBox.Show("Mã nhân viên này dã tồn tại", "thông báo");

                    }
                    else
                    {
                        if (txtTen.Text == "")
                        {
                            MessageBox.Show("Nhập thiếu Tên !", "thông báo");

                        }
                        else
                        {
                            if (txtDay.Text == "" && txtMonth.Text == "" && txtYear.Text == "")
                            {
                                MessageBox.Show("Nhập thiếu Ngày-tháng-năm !", "Thông báo");

                            }
                            else
                            {

                                String b = "" + txtDay.Text + "-" + txtMonth.Text + "-" + txtYear.Text + "";
                                if (rdNam.Checked == false && rdNu.Checked == false)
                                {
                                    MessageBox.Show("Hãy chọn giới tính !", "Thông báo");
                                }
                                else
                                {
                                    if (txtDiachi.Text == "")
                                    {
                                        MessageBox.Show("Nhập thiếu địa chỉ !", "thông báo");

                                    }
                                    else
                                    {
                                        if (txtLuong.Text == "")
                                        {
                                            MessageBox.Show("Nhập thiếu lương !", "thông báo");

                                        }
                                        else
                                        {
                                            String r = MaPhong(cbPhong.Text);


                                            cnn.Open();
                                            String sex = null;
                                            // kiểm tra giới tính
                                            if (rdNam.Checked == true)
                                            {
                                                sex = "nam";
                                            }
                                            if (rdNu.Checked == true)
                                            {
                                                sex = "nu";
                                            }
                                            String sql = "insert into NhanVien values ('" + txtMa.Text + "','" + txtTen.Text + "','" + b.ToString() + "','" + sex.ToString() + "','" + txtDiachi.Text + "'," + txtLuong.Text + ",'" + r.Trim() + "')";
                                            SqlCommand cmd = new SqlCommand(sql, cnn);
                                            cmd.Parameters.AddWithValue("sMaNV", txtMa.Text);
                                            cmd.Parameters.AddWithValue("sHoten", txtTen.Text);
                                            cmd.Parameters.AddWithValue("dNgaysinh", b.ToString());
                                            cmd.Parameters.AddWithValue("sGioitinh", sex.ToString());
                                            cmd.Parameters.AddWithValue("sDiachi", txtDiachi.Text);
                                            cmd.Parameters.AddWithValue("fLuong", txtLuong.Text);
                                            cmd.Parameters.AddWithValue("sMaP", r.Trim());

                                            int s = (int)cmd.ExecuteNonQuery();
                                            cnn.Close();
                                            if (s > 0)
                                            {
                                                MessageBox.Show("Thêm thành công !", "Thông báo");
                                                LoadData();
                                            }
                                            else
                                            {
                                                MessageBox.Show("Thêm thất bại! ");
                                            }
                                        }

                                    }

                                }

                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Nhập thiếu Mã nhân viên !", "thông báo");

                }
            } catch (Exception ex)

            { 
            cnn.Close();
            MessageBox.Show("Lỗi : " + ex);
                
            }
        }



        // sửa dữ liệu 
        private void button3_Click(object sender, EventArgs e)
        {
           try
            {
                DialogResult thongbao;
                thongbao = MessageBox.Show("Bạn có muốn Sửa dữ liệu không ? ", "thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (thongbao == DialogResult.OK)
                {
                    if (txtMa.Text != "")
                    {
                        if (CheckMa(txtMa.Text) != true)
                        {
                            MessageBox.Show("Mã nhân viên này Không chính xác !", "thông báo");
                        }
                        else
                        {
                            if (txtTen.Text == "")
                            {
                                MessageBox.Show("Nhập thiếu Tên !", "thông báo");

                            }
                            else
                            {
                                if (txtDay.Text == "" && txtMonth.Text == "" && txtYear.Text == "")
                                {
                                    MessageBox.Show("Nhập thiếu Ngày-tháng-năm !", "Thông báo");

                                }
                                else
                                {

                                    String b = "" + txtDay.Text + "-" + txtMonth.Text + "-" + txtYear.Text + "";
                                    if (rdNam.Checked == false && rdNu.Checked == false)
                                    {
                                        MessageBox.Show("Hãy chọn giới tính !", "Thông báo");
                                    }
                                    else
                                    {
                                        if (txtDiachi.Text == "")
                                        {
                                            MessageBox.Show("Nhập thiếu địa chỉ !", "thông báo");

                                        }
                                        else
                                        {
                                            if (txtLuong.Text == "")
                                            {
                                                MessageBox.Show("Nhập thiếu lương !", "thông báo");

                                            }
                                            else
                                            {
                                                String r = MaPhong(cbPhong.Text);


                                                cnn.Open();
                                                String sex = null;
                                                // kiểm tra giới tính
                                                if (rdNam.Checked == true)
                                                {
                                                    sex = "nam";
                                                }
                                                if (rdNu.Checked == true)
                                                {
                                                    sex = "nu";
                                                }

                                                String sql = "update NhanVien set sHoten = '" + txtTen.Text + "',dNgaysinh = '" + b.ToString() + "',sGioitinh = '" + sex.ToString() + "',sDiachi = '" + txtDiachi.Text + "',fLuong = " + txtLuong.Text + ",sMaP = '" + r.Trim() + "' where sMaNV = '" + txtMa.Text + "'";
                                                SqlCommand cmd = new SqlCommand(sql, cnn);
                                                cmd.Parameters.AddWithValue("sMaNV", txtMa.Text);
                                                cmd.Parameters.AddWithValue("sHoten", txtTen.Text);
                                                cmd.Parameters.AddWithValue("dNgaysinh", b.ToString());
                                                cmd.Parameters.AddWithValue("sGioitinh", sex.ToString());
                                                cmd.Parameters.AddWithValue("sDiachi", txtDiachi.Text);
                                                cmd.Parameters.AddWithValue("fLuong", txtLuong.Text);
                                                cmd.Parameters.AddWithValue("sMaP", r.Trim());

                                                int s = (int)cmd.ExecuteNonQuery();
                                                cnn.Close();
                                                if (s > 0)
                                                {
                                                    MessageBox.Show("Sửa thành công !", "Thông báo");
                                                    LoadData();
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Sửa thất bại! ");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Nhập thiếu Mã nhân viên !", "thông báo");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex);
            }
        }











        private void txtMa_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtLuong_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDay_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtMonth_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult thongbao;
            thongbao = MessageBox.Show("Bạn có muốn xóa hay không", "thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (thongbao == DialogResult.OK)
            {
                if (txtMa.Text != "")
                {
                    if (CheckMa(txtMa.Text) == true)
                    {
                        cnn.Open();
                        String sql = "delete from NhanVien where sMaNV = '" + txtMa.Text + "'";
                        SqlCommand cmd = new SqlCommand(sql, cnn);
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                        MessageBox.Show("Đã xóa dữ liệu !");
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Mã không tìm thấy !");
                    }
                    
                }else
                {
                    MessageBox.Show("Chưa nhập mã !");
                }
               
            
            }
        }

        private void dataGV1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMa.Text = dataGV1.CurrentRow.Cells[0].Value.ToString();
            txtTen.Text = dataGV1.CurrentRow.Cells[1].Value.ToString(); 
            txtDiachi.Text = dataGV1.CurrentRow.Cells[4].Value.ToString();
            txtLuong.Text = dataGV1.CurrentRow.Cells[5].Value.ToString();
            if (dataGV1.CurrentRow.Cells[3].Value.ToString() == "nam")
            {
                rdNam.Checked = true;
            }
            else if(dataGV1.CurrentRow.Cells[3].Value.ToString() == "nu")
            {
                rdNu.Checked = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdMaNV.Checked == true)
                {


                    if (txtSearch.Text != "")
                    {
                        if (CheckMa(txtSearch.Text) == true)
                        {
                            cnn.Open();

                            String sql = "select * from NhanVien where sMaNV = '" + txtSearch.Text + "'";
                            SqlDataAdapter dt = new SqlDataAdapter(sql, cnn);
                            DataTable tb = new DataTable();
                            dt.Fill(tb);
                            dataGV1.DataSource = tb;
                            cnn.Close();
                        }
                        else
                        {
                            MessageBox.Show("Mã không tìm thấy !");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chưa nhập mã !");
                    }

                }
                if (rdDiachi.Checked == true)
                {
                    if (txtSearch.Text != "")
                    {
                        
                            cnn.Open();

                            String sql = "select * from NhanVien where sDiachi = '" + txtSearch.Text.Trim() + "'";
                            SqlDataAdapter dt = new SqlDataAdapter(sql, cnn);
                            DataTable tb = new DataTable();
                            dt.Fill(tb);
                            dataGV1.DataSource = tb;
                            cnn.Close();
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Chưa nhập mã !");
                    }

                
            }
            
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối SQL" + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                String sql = " select * from v_soluong ";
                SqlDataAdapter dt = new SqlDataAdapter(sql, cnn);
                DataTable db = new DataTable();
                dt.Fill(db);
                dataGV1.DataSource = db;
                cnn.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                String m = MaPhong(cbPhong.Text);
                String sql = "select * from NhanVien where sMaP = '" + m.ToString() + "'";
                SqlDataAdapter dt = new SqlDataAdapter(sql, cnn);
                DataTable db = new DataTable();
                dt.Fill(db);
                dataGV1.DataSource = db;
                cnn.Close();
            }catch(Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LoadData();
          
        }
    }
}

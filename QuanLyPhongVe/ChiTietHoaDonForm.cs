using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyPhongVe
{
    public partial class ChiTietHoaDonForm : Form
    {
        string connStr = @"Data Source=DESKTOP-TCPNEFN;Initial Catalog=QuanLyPhongVe;Integrated Security=True";
        int currentID = -1;

        public ChiTietHoaDonForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM ChiTietHoaDon", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            currentID = -1;
            txtMaHD.Clear();
            txtTenPhim.Clear();
            txtSoLuong.Clear();
            txtDonGia.Clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "INSERT INTO ChiTietHoaDon (MaHD, TenPhim, SoLuong, DonGia) VALUES (@mahd, @ten, @sl, @dg)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@mahd", txtMaHD.Text);
                cmd.Parameters.AddWithValue("@ten", txtTenPhim.Text);
                cmd.Parameters.AddWithValue("@sl", Convert.ToInt32(txtSoLuong.Text));
                cmd.Parameters.AddWithValue("@dg", Convert.ToDecimal(txtDonGia.Text));
                cmd.ExecuteNonQuery();
            }
            LoadData();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (currentID == -1)
            {
                MessageBox.Show("Chọn chi tiết cần sửa!");
                return;
            }
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "UPDATE ChiTietHoaDon SET MaHD=@mahd, TenPhim=@ten, SoLuong=@sl, DonGia=@dg WHERE MaCTHD=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@mahd", txtMaHD.Text);
                cmd.Parameters.AddWithValue("@ten", txtTenPhim.Text);
                cmd.Parameters.AddWithValue("@sl", Convert.ToInt32(txtSoLuong.Text));
                cmd.Parameters.AddWithValue("@dg", Convert.ToDecimal(txtDonGia.Text));
                cmd.Parameters.AddWithValue("@id", currentID);
                cmd.ExecuteNonQuery();
            }
            LoadData();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (currentID == -1)
            {
                MessageBox.Show("Chọn chi tiết cần xóa!");
                return;
            }
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "DELETE FROM ChiTietHoaDon WHERE MaCTHD=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", currentID);
                cmd.ExecuteNonQuery();
            }
            LoadData();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                currentID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["MaCTHD"].Value);
                txtMaHD.Text = dataGridView1.Rows[e.RowIndex].Cells["MaHD"].Value.ToString();
                txtTenPhim.Text = dataGridView1.Rows[e.RowIndex].Cells["TenPhim"].Value.ToString();
                txtSoLuong.Text = dataGridView1.Rows[e.RowIndex].Cells["SoLuong"].Value.ToString();
                txtDonGia.Text = dataGridView1.Rows[e.RowIndex].Cells["DonGia"].Value.ToString();
            }
        }
    }
}

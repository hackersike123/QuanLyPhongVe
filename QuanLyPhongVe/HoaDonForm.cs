using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyPhongVe
{
    public partial class HoaDonForm : Form
    {
        string connStr = @"Data Source=DESKTOP-TCPNEFN;Initial Catalog=QuanLyPhongVe;Integrated Security=True";
        int currentID = -1;

        public HoaDonForm()
        {
            InitializeComponent();
            LoadKhachHang();
            LoadData();
        }

        private void LoadKhachHang()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaKH, TenKH FROM KhachHang", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cboKhachHang.DataSource = dt;
                cboKhachHang.DisplayMember = "TenKH";
                cboKhachHang.ValueMember = "MaKH";
            }
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM HoaDon", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            currentID = -1;
            txtTongTien.Clear();
            dtpNgayLap.Value = DateTime.Now;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "INSERT INTO HoaDon (MaKH, NgayLap, TongTien) VALUES (@makh, @ngay, @tong)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@makh", cboKhachHang.SelectedValue);
                cmd.Parameters.AddWithValue("@ngay", dtpNgayLap.Value);
                cmd.Parameters.AddWithValue("@tong", Convert.ToDecimal(txtTongTien.Text));
                cmd.ExecuteNonQuery();
            }
            LoadData();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (currentID == -1)
            {
                MessageBox.Show("Chọn hóa đơn cần sửa!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "UPDATE HoaDon SET MaKH=@makh, NgayLap=@ngay, TongTien=@tong WHERE MaHD=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@makh", cboKhachHang.SelectedValue);
                cmd.Parameters.AddWithValue("@ngay", dtpNgayLap.Value);
                cmd.Parameters.AddWithValue("@tong", Convert.ToDecimal(txtTongTien.Text));
                cmd.Parameters.AddWithValue("@id", currentID);
                cmd.ExecuteNonQuery();
            }
            LoadData();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (currentID == -1)
            {
                MessageBox.Show("Chọn hóa đơn cần xóa!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "DELETE FROM HoaDon WHERE MaHD=@id";
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
                currentID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["MaHD"].Value);
                cboKhachHang.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["MaKH"].Value;
                dtpNgayLap.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["NgayLap"].Value);
                txtTongTien.Text = dataGridView1.Rows[e.RowIndex].Cells["TongTien"].Value.ToString();
            }
        }
    }
}

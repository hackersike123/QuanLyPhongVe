using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyPhongVe
{
    public partial class KhachHangForm : Form
    {
        string connStr = @"Data Source=DESKTOP-TCPNEFN;Initial Catalog=QuanLyPhongVe;Integrated Security=True";
        int currentID = -1;

        public KhachHangForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM KhachHang", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            currentID = -1;
            txtTenKH.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "INSERT INTO KhachHang (TenKH, SDT, Email) VALUES (@ten, @sdt, @em)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ten", txtTenKH.Text);
                cmd.Parameters.AddWithValue("@sdt", txtSDT.Text);
                cmd.Parameters.AddWithValue("@em", txtEmail.Text);
                cmd.ExecuteNonQuery();
            }
            LoadData();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (currentID == -1)
            {
                MessageBox.Show("Chọn khách hàng cần sửa!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "UPDATE KhachHang SET TenKH=@ten, SDT=@sdt, Email=@em WHERE MaKH=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ten", txtTenKH.Text);
                cmd.Parameters.AddWithValue("@sdt", txtSDT.Text);
                cmd.Parameters.AddWithValue("@em", txtEmail.Text);
                cmd.Parameters.AddWithValue("@id", currentID);
                cmd.ExecuteNonQuery();
            }
            LoadData();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (currentID == -1)
            {
                MessageBox.Show("Chọn khách hàng cần xóa!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "DELETE FROM KhachHang WHERE MaKH=@id";
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
                currentID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["MaKH"].Value);
                txtTenKH.Text = dataGridView1.Rows[e.RowIndex].Cells["TenKH"].Value.ToString();
                txtSDT.Text = dataGridView1.Rows[e.RowIndex].Cells["SDT"].Value.ToString();
                txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells["Email"].Value.ToString();
            }
        }
    }
}

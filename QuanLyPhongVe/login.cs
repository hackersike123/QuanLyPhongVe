using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyPhongVe
{
    public partial class login : Form
    {
        private string connStr = @"Data Source=DESKTOP-TCPNEFN;Initial Catalog=QuanLyPhongVe;Integrated Security=True";

        public login()
        {
            InitializeComponent();
            button1.Click += Button1_Click; // Đăng nhập
            button2.Click += Button2_Click; // Thoát
            button3.Click += Button3_Click; // Đăng ký
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ!");
                return;
            }

            string encPass = AESHelper.Encrypt(password);

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT * FROM Users WHERE Username=@u AND Password=@p";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@u", username);
                        cmd.Parameters.AddWithValue("@p", encPass);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            Program.CurrentUser = username;
                            Program.CurrentRole = dt.Rows[0]["Role"].ToString();

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Nhập đầy đủ để đăng ký!");
                return;
            }

            string encPass = AESHelper.Encrypt(password);

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "INSERT INTO Users (Username, Password, Role) VALUES (@u, @p, 'Staff')";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@u", username);
                        cmd.Parameters.AddWithValue("@p", encPass);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Đăng ký thành công! Bạn có thể đăng nhập ngay.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng ký: " + ex.Message);
            }
        }
    }
}

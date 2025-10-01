using System;
using System.Windows.Forms;

namespace QuanLyPhongVe
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();

            // Gắn sự kiện cho các nút
            button1.Click += Button1_Click; // Đăng nhập
            button2.Click += Button2_Click; // Thoát
        }

        /// <summary>
        /// Xử lý khi nhấn nút Đăng nhập
        /// </summary>
        private void Button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            // ✅ Demo đăng nhập: admin / 123
            if (username == "admin" && password == "123")
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK; // Trả về OK để Program.cs mở menu
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                textBox2.Clear();
                textBox2.Focus();
            }
        }

        /// <summary>
        /// Xử lý khi nhấn nút Thoát
        /// </summary>
        private void Button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Giúp enter = đăng nhập, esc = thoát
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                Button1_Click(null, null);
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                Button2_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}

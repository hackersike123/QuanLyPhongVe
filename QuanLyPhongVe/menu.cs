using System;
using System.Windows.Forms;

namespace QuanLyPhongVe
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
            ApplyRolePermission();
        }

        private void ApplyRolePermission()
        {
            if (Program.CurrentRole == "Admin")
            {
                quảnLýPhimToolStripMenuItem.Visible = true;
                quảnLýNgườiDùngToolStripMenuItem.Visible = true;
                hệThốngToolStripMenuItem.Visible = true;
            }
            else if (Program.CurrentRole == "Staff")
            {
                // Staff: chưa có chức năng
                quảnLýPhimToolStripMenuItem.Visible = false;
                quảnLýNgườiDùngToolStripMenuItem.Visible = false;
                hệThốngToolStripMenuItem.Visible = false;

                MessageBox.Show("Bạn đăng nhập với vai trò Staff. Hiện tại chưa có chức năng.");
            }
        }

        private void mởToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rapphim frm = new rapphim();
            frm.ShowDialog();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void quảnLýPhimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng quản lý phim (Admin).");
        }

        private void quảnLýNgườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng quản lý người dùng (Admin).");
        }
    }
}

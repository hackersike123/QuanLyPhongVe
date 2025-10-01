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
                kháchHàngToolStripMenuItem.Visible = true;
                hóaĐơnToolStripMenuItem.Visible = true;
                chiTiếtHóaĐơnToolStripMenuItem.Visible = true;
                hệThốngToolStripMenuItem.Visible = true;
            }
            else if (Program.CurrentRole == "Staff")
            {
                quảnLýPhimToolStripMenuItem.Visible = false;
                quảnLýNgườiDùngToolStripMenuItem.Visible = false;
                kháchHàngToolStripMenuItem.Visible = false;
                hóaĐơnToolStripMenuItem.Visible = false;
                chiTiếtHóaĐơnToolStripMenuItem.Visible = false;
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

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KhachHangForm frm = new KhachHangForm();
            frm.ShowDialog();
        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HoaDonForm frm = new HoaDonForm();
            frm.ShowDialog();
        }

        private void chiTiếtHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChiTietHoaDonForm frm = new ChiTietHoaDonForm();
            frm.ShowDialog();
        }
    }
}

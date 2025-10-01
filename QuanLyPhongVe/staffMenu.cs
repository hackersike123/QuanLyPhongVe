using System;
using System.Windows.Forms;

namespace QuanLyPhongVe
{
    public partial class staffMenu : Form
    {
        public staffMenu()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // staffMenu
            // 
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Name = "staffMenu";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Staff - Chưa có chức năng";
            this.Load += new System.EventHandler(this.staffMenu_Load);
            this.ResumeLayout(false);
        }

        private void staffMenu_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn đăng nhập với vai trò Staff. Hiện tại chưa có chức năng.");
        }
    }
}

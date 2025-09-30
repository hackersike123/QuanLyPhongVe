using System;
using System.Windows.Forms;

namespace QuanLyPhongVe
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
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
    }
}

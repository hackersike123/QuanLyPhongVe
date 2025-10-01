using System;
using System.Windows.Forms;

namespace QuanLyPhongVe
{
    internal static class Program
    {
        /// <summary>
        /// Điểm bắt đầu của ứng dụng
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Mở form đăng nhập trước
            using (login frmLogin = new login())
            {
                if (frmLogin.ShowDialog() == DialogResult.OK)
                {
                    // Nếu đăng nhập thành công -> mở menu chính
                    Application.Run(new menu());
                }
                else
                {
                    // Nếu login không thành công hoặc người dùng bấm thoát -> đóng app
                    Application.Exit();
                }
            }
        }
    }
}

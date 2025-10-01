using System;
using System.Windows.Forms;

namespace QuanLyPhongVe
{
    internal static class Program
    {
        public static string CurrentUser { get; set; }
        public static string CurrentRole { get; set; }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (login frmLogin = new login())
            {
                if (frmLogin.ShowDialog() == DialogResult.OK)
                {
                    if (CurrentRole == "Admin")
                    {
                        Application.Run(new menu()); // Admin vào menu đầy đủ
                    }
                    else if (CurrentRole == "Staff")
                    {
                        Application.Run(new staffMenu()); // Staff chỉ vào form trống
                    }
                }
                else
                {
                    Application.Exit();
                }
            }
        }
    }
}

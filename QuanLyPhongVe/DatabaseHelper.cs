using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyPhongVe
{
    public class DatabaseHelper
    {
        private static string connectionString =
            @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyPhongVe;Integrated Security=True";
        // 🔹 Nếu bạn dùng SQL Server Authentication thì đổi sang:
        // @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyPhongVe;User ID=sa;Password=yourpassword";

        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

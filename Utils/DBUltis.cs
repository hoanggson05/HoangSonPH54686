using Microsoft.Data.SqlClient;
using System.Data;

namespace QuanLyBanVeRapPhim.Utils
{
    public static class DBUtil
    {
        static SqlConnection conn =
            new SqlConnection(@"Server=.;Database=QuanLyBanVeRapPhim;Trusted_Connection=True;");

        public static void OpenConnection()
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
        }

        public static void CloseConnection()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }

        public static DataTable ExecuteQueryTable(string sql, params object[] args)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            for (int i = 0; i < args.Length; i++)
                cmd.Parameters.AddWithValue($"@{i}", args[i]);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            da.Fill(tb);
            return tb;
        }

        public static int ExecuteNonQuery(string v, SqlCommand cmd)
        {
            OpenConnection();

            cmd.Connection = conn;
            int result = cmd.ExecuteNonQuery();

            CloseConnection();
            return result;
        }



        public static object ExecuteScalar(string sql, params object[] args)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            for (int i = 0; i < args.Length; i++)
                cmd.Parameters.AddWithValue($"@{i}", args[i]);

            return cmd.ExecuteScalar();
        }
    }
}

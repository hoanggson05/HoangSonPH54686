using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;

namespace QuanLiVeTaiQuay.DAO // Chú ý: Nên đổi cùng namespace với DAO để không bị đỏ
{
    public class DataProvider
    {
        // ĐOẠN NÀY LÀ QUAN TRỌNG NHẤT - PHẢI CÓ THÌ MỚI HẾT LỖI
        private static DataProvider instance;
        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return instance; }
            private set { instance = value; }
        }

        private string connectionStr = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyBanVeRapPhim;Integrated Security=True";

        public DataTable ExecuteQuery(string query)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
                connection.Close();
            }
            return data;
        }

        public int ExecuteNonQuery(string query)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                data = command.ExecuteNonQuery();
                connection.Close();
            }
            return data;
        }
    }
}
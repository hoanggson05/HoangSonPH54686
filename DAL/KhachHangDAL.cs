using Microsoft.Data.SqlClient;
using QuanLyBanVeRapPhim.Utils;
using System.Data;

namespace QuanLyBanVeRapPhim.DAL
{
    public static class KhachHangDAL
    {
        public static DataTable SelectAll()
        {
            DBUtil.OpenConnection();
            DataTable tb = DBUtil.ExecuteQueryTable(
                "SELECT * FROM KhachHang WHERE TrangThai = 1"
            );
            DBUtil.CloseConnection();
            return tb;
        }

        public static int Insert(string hoTen, string sdt, string email)
        {
            SqlCommand cmd = new SqlCommand(
                @"INSERT INTO KhachHang(HoTen, SDT,Email, TrangThai)
                  VALUES(@hoTen, @sdt,@email, 1)"
            );

            cmd.Parameters.AddWithValue("@hoTen", hoTen);
            cmd.Parameters.AddWithValue("@sdt", sdt);
            cmd.Parameters.AddWithValue("@email", email);
            return DBUtil.ExecuteNonQuery("", cmd);
        }

        public static int Update(int maKH, string hoTen, string sdt, string email)
        {
            SqlCommand cmd = new SqlCommand(
                @"UPDATE KhachHang 
                  SET HoTen = @hoTen,
                      SDT = @sdt
                      Email = @email
                  WHERE MaKH = @ma"
            );

            cmd.Parameters.AddWithValue("@hoTen", hoTen);
            cmd.Parameters.AddWithValue("@sdt", sdt);
            cmd.Parameters.AddWithValue("@ma", maKH);
            cmd.Parameters.AddWithValue("@email", email);
            return DBUtil.ExecuteNonQuery("", cmd);
        }

        public static int Delete(int maKH)
        {
            SqlCommand cmd = new SqlCommand(
                "UPDATE KhachHang SET TrangThai = 0 WHERE MaKH = @ma"
            );

            cmd.Parameters.AddWithValue("@ma", maKH);

            return DBUtil.ExecuteNonQuery("", cmd);
        }
    }
}

using QuanLyBanVeRapPhim.Utils;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyBanVeRapPhim.DAL
{
    public static class PhimDAL
    {
        // ===== SELECT =====
        public static DataTable SelectAll()
        {
            DBUtil.OpenConnection();
            DataTable tb = DBUtil.ExecuteQueryTable(
                "SELECT * FROM Phim WHERE TrangThai = 1"
            );
            DBUtil.CloseConnection();
            return tb;
        }

        // ===== INSERT =====
        public static int Insert(string ten, string theLoai, int thoiLuong, int doTuoi)
        {
            SqlCommand cmd = new SqlCommand(
                @"INSERT INTO Phim(TenPhim, TheLoai, ThoiLuong, DoTuoi, TrangThai)
                  VALUES(@ten, @theLoai, @thoiLuong, @doTuoi, 1)"
            );

            cmd.Parameters.AddWithValue("@ten", ten);
            cmd.Parameters.AddWithValue("@theLoai", theLoai);
            cmd.Parameters.AddWithValue("@thoiLuong", thoiLuong);
            cmd.Parameters.AddWithValue("@doTuoi", doTuoi);

            return DBUtil.ExecuteNonQuery("", cmd);
        }

        // ===== UPDATE =====
        public static int Update(int maPhim, string ten, string theLoai, int thoiLuong, int doTuoi)
        {
            SqlCommand cmd = new SqlCommand(
                @"UPDATE Phim 
                  SET TenPhim=@ten, 
                      TheLoai=@theLoai,
                      ThoiLuong=@thoiLuong,
                      DoTuoi=@doTuoi
                  WHERE MaPhim=@ma"
            );

            cmd.Parameters.AddWithValue("@ten", ten);
            cmd.Parameters.AddWithValue("@theLoai", theLoai);
            cmd.Parameters.AddWithValue("@thoiLuong", thoiLuong);
            cmd.Parameters.AddWithValue("@doTuoi", doTuoi);
            cmd.Parameters.AddWithValue("@ma", maPhim);

            return DBUtil.ExecuteNonQuery("", cmd);
        }

        // ===== DELETE (XÓA MỀM) =====
        public static int Delete(int maPhim)
        {
            SqlCommand cmd = new SqlCommand(
                "UPDATE Phim SET TrangThai = 0 WHERE MaPhim = @ma"
            );

            cmd.Parameters.AddWithValue("@ma", maPhim);

            return DBUtil.ExecuteNonQuery("", cmd);
        }
       
    }
}

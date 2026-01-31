using QuanLyBanVeRapPhim.DAL;
using System.Data;

namespace QuanLiVeTaiQuay.BLL
{
    public static class KhachHangBLL
    {
        public static DataTable GetAll()
        {
            return KhachHangDAL.SelectAll();
        }

        public static void Them(string hoTen, string sdt, string email)
        {
            KhachHangDAL.Insert(hoTen, sdt, email);
        }

        public static void Sua(int maKH, string hoTen, string sdt, string email)
        {
            KhachHangDAL.Update(maKH, hoTen, sdt, email);
        }

        public static void Xoa(int maKH)
        {
            KhachHangDAL.Delete(maKH);
        }

    }
}

using QuanLyBanVeRapPhim.DAL;
using System.Data;

namespace QuanLyBanVeRapPhim.BLL
{
    public static class KhachHangBLL
    {
        public static DataTable GetAll()
        {
            return KhachHangDAL.SelectAll();
        }

        public static void Them(string hoTen, string sdt)
        {
            KhachHangDAL.Insert(hoTen, sdt);
        }

        public static void Sua(int maKH, string hoTen, string sdt)
        {
            KhachHangDAL.Update(maKH, hoTen, sdt);
        }

        public static void Xoa(int maKH)
        {
            KhachHangDAL.Delete(maKH);
        }
       
    }
}

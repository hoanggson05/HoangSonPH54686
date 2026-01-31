using QuanLiVeXemPhimTaiQuay.DAO;
using QuanLyBanVeRapPhim.DAL;
using System.Data;

namespace QuanLiVeTaiQuay.BLL
{
    public static class PhimBLL
    {
        public static DataTable GetAll()
        {
            return PhimDAL.SelectAll();
        }

        public static void Them(string ten, string theLoai, int thoiLuong, int doTuoi)
        {
            PhimDAL.Insert(ten, theLoai, thoiLuong, doTuoi);
        }

        public static void Sua(int ma, string ten, string theLoai, int thoiLuong, int doTuoi)
        {
            PhimDAL.Update(ma, ten, theLoai, thoiLuong, doTuoi);
        }

        public static void Xoa(int ma)
        {
            PhimDAL.Delete(ma);
        }

        internal static void Them(Phim phim)
        {
            throw new NotImplementedException();
        }

        internal static void Sua(Phim phim)
        {
            throw new NotImplementedException();
        }
    }
}

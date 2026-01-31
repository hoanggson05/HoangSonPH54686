namespace QuanLiVeXemPhimTaiQuay.DAO
{
    internal class SuatChieu
    {
        public int MaSuat { get; set; }
        public int MaPhim { get; set; }
        public int MaPhong { get; set; }

        public DateTime NgayChieu { get; set; }
        public TimeSpan GioChieu { get; set; }
        public decimal GiaVe { get; set; }
    }
}

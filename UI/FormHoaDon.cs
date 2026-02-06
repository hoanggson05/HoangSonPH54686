using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace QuanLiVeTaiQuay.UI
{
    public partial class FormHoaDon : Form
    {
        private int _maHD;
        private string strConn = @"Data Source=.;Initial Catalog=QuanLyBanVeRapPhim;Integrated Security=True;TrustServerCertificate=True";

        public FormHoaDon(int maHD)
        {
            InitializeComponent(); // Khởi tạo giao diện đẹp phía dưới
            this._maHD = maHD;
            LoadDuLieuHoaDon();
        }

        private void LoadDuLieuHoaDon()
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string sql = @"SELECT HD.MaHD, HD.NgayLap, NV.TenNV, HD.TongTien, 
                                          P.TenPhim, PC.TenPhong, G.TenGhe, V.GiaBan
                                   FROM HoaDon HD 
                                   JOIN NhanVien NV ON HD.MaNV = NV.MaNV
                                   JOIN HoaDonChiTiet CT ON HD.MaHD = CT.MaHD
                                   JOIN Ve V ON CT.MaVe = V.MaVe
                                   JOIN SuatChieu S ON V.MaSuat = S.MaSuat
                                   JOIN Phim P ON S.MaPhim = P.MaPhim
                                   JOIN PhongChieu PC ON S.MaPhong = PC.MaPhong
                                   JOIN GheNgoi G ON V.MaGhe = G.MaGhe
                                   WHERE HD.MaHD = @ma";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.SelectCommand.Parameters.AddWithValue("@ma", _maHD);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        txtMaHD.Text = dt.Rows[0]["MaHD"].ToString();
                        dtpNgayBan.Value = Convert.ToDateTime(dt.Rows[0]["NgayLap"]);
                        txtNhanVien.Text = dt.Rows[0]["TenNV"].ToString();
                        txtTongTien.Text = string.Format("{0:N0}", dt.Rows[0]["TongTien"]);

                        dgvDanhSachVe.DataSource = dt;
                        // Ẩn bớt cột thừa để giống ảnh mẫu
                        string[] hideCols = { "MaHD", "NgayLap", "TenNV", "TongTien" };
                        foreach (string col in hideCols)
                            if (dgvDanhSachVe.Columns.Contains(col)) dgvDanhSachVe.Columns[col].Visible = false;
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }
    }
}
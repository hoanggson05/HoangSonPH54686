using System.Data;
using Microsoft.Data.SqlClient;

namespace QuanLiVeTaiQuay.UI
{
    public partial class FormThongKe : Form
    {
        // Chuỗi kết nối của bạn
        string strConn = @"Data Source=.;Initial Catalog=QuanLyBanVeRapPhim;Integrated Security=True;TrustServerCertificate=True";

        public FormThongKe()
        {
            InitializeComponent();
            LoadComboPhim();
        }

        private void LoadComboPhim()
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT MaPhim, TenPhim FROM Phim", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DataRow dr = dt.NewRow();
                    dr["MaPhim"] = "ALL";
                    dr["TenPhim"] = "-- Tất cả các phim --";
                    dt.Rows.InsertAt(dr, 0);
                    cboPhim.DataSource = dt;
                    cboPhim.DisplayMember = "TenPhim";
                    cboPhim.ValueMember = "MaPhim";
                }
                catch { }
            }
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    // Câu lệnh SQL truy vấn doanh thu
                    string sql = @"SELECT P.TenPhim, COUNT(CT.MaVe) as SoVeDaBan, SUM(CT.GiaBan) as TongDoanhThu 
                                   FROM HoaDon HD 
                                   JOIN HoaDonChiTiet CT ON HD.MaHD = CT.MaHD 
                                   JOIN Ve V ON CT.MaVe = V.MaVe 
                                   JOIN SuatChieu S ON V.MaSuat = S.MaSuat 
                                   JOIN Phim P ON S.MaPhim = P.MaPhim 
                                   WHERE HD.NgayLap BETWEEN @tu AND @den ";

                    if (cboPhim.SelectedValue.ToString() != "ALL")
                        sql += " AND P.MaPhim = @maPhim";

                    sql += " GROUP BY P.TenPhim";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@tu", dtpTuNgay.Value.Date);
                    cmd.Parameters.AddWithValue("@den", dtpDenNgay.Value.Date.AddDays(1));
                    if (cboPhim.SelectedValue.ToString() != "ALL")
                        cmd.Parameters.AddWithValue("@maPhim", cboPhim.SelectedValue);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvThongKe.DataSource = dt;

                    // Vẽ biểu đồ tròn cho Admin
                    chartDoanhThu.Series.Clear();
                    Series s = new Series("Revenue") { ChartType = SeriesChartType.Pie };
                    decimal tongTien = 0;

                    foreach (DataRow r in dt.Rows)
                    {
                        decimal val = Convert.ToDecimal(r["TongDoanhThu"]);
                        tongTien += val;
                        s.Points.AddXY(r["TenPhim"].ToString(), val);
                    }
                    chartDoanhThu.Series.Add(s);
                    lblTongDoanhThu.Text = "Tổng doanh thu: " + tongTien.ToString("N0") + " VNĐ";
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }
    }
}
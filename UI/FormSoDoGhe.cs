using Microsoft.Data.SqlClient;
using QuanLiVeXemPhimTaiQuay.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLiVeTaiQuay.UI
{
    public partial class FormSoDoGhe : Form
    {
        private string strConn = @"Data Source=.;Initial Catalog=QuanLyBanVeRapPhim;Integrated Security=True;TrustServerCertificate=True";
        private int _maSuat;
        private string _tenPhim;
        private decimal _giaVeGoc = 0;
        private int _maKH = -1;
        private List<int> _listMaVeChon = new List<int>();

        // Controls
        private FlowLayoutPanel flpGhe;
        private TextBox txtTongTien, txtGiamGia, txtCanTra, txtSDT, txtTenKH, txtDiemTichLuy;
        private NumericUpDown numDiemDung;
        private GroupBox gbLoaiVe;

        public FormSoDoGhe(int maSuat, string tenPhim)
        {
            this._maSuat = maSuat;
            this._tenPhim = tenPhim;
            this.Text = "Bán Vé - " + _tenPhim;
            this.Size = new Size(1250, 850);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            InitializeUI(); // Khởi tạo giao diện
            LoadGiaVe();    // Lấy giá vé từ DB
            LoadSoDoGhe();  // Hiển thị sơ đồ ghế
        }

        private void InitializeUI()
        {
            // 1. PANEL HEADER (MÀN CHIẾU)
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 100, BackColor = Color.Black };
            Label lblScreen = new Label
            {
                Text = "Màn Chiếu",
                Size = new Size(800, 35),
                Location = new Point(100, 50),
                BackColor = Color.Blue,
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 14, FontStyle.Bold)
            };
            pnlHeader.Controls.Add(lblScreen);
            this.Controls.Add(pnlHeader);

            // 2. PANEL FOOTER (CHI TIẾT THANH TOÁN)
            Panel pnlFooter = new Panel { Dock = DockStyle.Bottom, Height = 250, BackColor = Color.Black, ForeColor = Color.White };

            // --- CỘT 1: THÀNH VIÊN ---
            GroupBox gbKH = new GroupBox { Text = "Thành Viên", ForeColor = Color.White, Location = new Point(10, 10), Size = new Size(400, 220) };

            gbKH.Controls.Add(new Label { Text = "Số ĐT:", Location = new Point(10, 35), AutoSize = true });
            txtSDT = new TextBox { Location = new Point(120, 32), Width = 230 };
            // QUAN TRỌNG: Gán sự kiện SAU KHI khởi tạo txtSDT để tránh lỗi NullReferenceException
            txtSDT.TextChanged += (s, e) => { TimKiemKhachHang(); };

            gbKH.Controls.Add(new Label { Text = "Tên KH:", Location = new Point(10, 75), AutoSize = true });
            txtTenKH = new TextBox { Location = new Point(120, 72), Width = 230, ReadOnly = true };

            gbKH.Controls.Add(new Label { Text = "Điểm hiện có:", Location = new Point(10, 115), AutoSize = true });
            txtDiemTichLuy = new TextBox { Location = new Point(120, 112), Width = 100, ReadOnly = true, Text = "0" };

            gbKH.Controls.Add(new Label { Text = "Dùng điểm:", Location = new Point(10, 155), AutoSize = true });
            numDiemDung = new NumericUpDown { Location = new Point(120, 152), Width = 100, Maximum = 1000000 };
            numDiemDung.ValueChanged += (s, e) => { UpdateTotal(); };

            gbKH.Controls.AddRange(new Control[] { txtSDT, txtTenKH, txtDiemTichLuy, numDiemDung });
            pnlFooter.Controls.Add(gbKH);

            // --- CỘT 2: LOẠI VÉ ---
            gbLoaiVe = new GroupBox { Text = "Loại vé", ForeColor = Color.White, Location = new Point(420, 10), Size = new Size(250, 220) };
            string[] loai = { "Người Lớn (100%)", "Sinh Viên (80%)", "Trẻ Em (50%)" };
            double[] tiLe = { 1.0, 0.8, 0.5 };
            for (int i = 0; i < loai.Length; i++)
            {
                RadioButton rb = new RadioButton { Text = loai[i], Location = new Point(20, 40 + (i * 40)), AutoSize = true, Checked = (i == 0), Tag = tiLe[i] };
                rb.CheckedChanged += (s, e) => { if (rb.Checked) UpdateTotal(); };
                gbLoaiVe.Controls.Add(rb);
            }
            pnlFooter.Controls.Add(gbLoaiVe);

            // --- CỘT 3: TỔNG TIỀN ---
            txtTongTien = new TextBox { Location = new Point(800, 30), Width = 200, ReadOnly = true, Font = new Font("Segoe UI", 12, FontStyle.Bold) };
            txtGiamGia = new TextBox { Location = new Point(800, 75), Width = 200, ReadOnly = true };
            txtCanTra = new TextBox { Location = new Point(800, 120), Width = 200, ReadOnly = true, Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.Red };

            Button btnThanhToan = new Button { Text = "Thanh Toán", Location = new Point(800, 175), Size = new Size(120, 40), BackColor = Color.Yellow, ForeColor = Color.Black, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            btnThanhToan.Click += (s, e) => { XuLyThanhToan(); };

            pnlFooter.Controls.AddRange(new Control[] {
                new Label { Text = "Tổng cộng:", Location = new Point(700, 35) }, txtTongTien,
                new Label { Text = "Giảm giá:", Location = new Point(700, 80) }, txtGiamGia,
                new Label { Text = "Cần trả:", Location = new Point(700, 125) }, txtCanTra, btnThanhToan
            });
            this.Controls.Add(pnlFooter);

            // 3. SƠ ĐỒ GHẾ
            flpGhe = new FlowLayoutPanel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(50), RightToLeft = RightToLeft.Yes };
            this.Controls.Add(flpGhe);
            flpGhe.BringToFront();
        }

        private void TimKiemKhachHang()
        {
            // Khi nhập đủ 10 số điện thoại mới bắt đầu tìm
            if (txtSDT.Text.Length < 10)
            {
                _maKH = -1; txtTenKH.Text = ""; txtDiemTichLuy.Text = "0"; return;
            }
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT MaKH, HoTen, DiemTichLuy FROM KhachHang WHERE SDT = @sdt";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@sdt", txtSDT.Text.Trim());
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    _maKH = (int)dr["MaKH"];
                    txtTenKH.Text = dr["HoTen"].ToString();
                    txtDiemTichLuy.Text = dr["DiemTichLuy"].ToString();
                    numDiemDung.Maximum = Convert.ToDecimal(dr["DiemTichLuy"]);
                }
                else
                {
                    _maKH = -1; txtTenKH.Text = "Khách mới"; txtDiemTichLuy.Text = "0"; numDiemDung.Maximum = 0;
                }
            }
        }

        private void LoadGiaVe()
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT GiaVe FROM SuatChieu WHERE MaSuat = @ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ma", _maSuat);
                object res = cmd.ExecuteScalar();
                if (res != null) _giaVeGoc = Convert.ToDecimal(res);
            }
        }

        private void LoadSoDoGhe()
        {
            flpGhe.Controls.Clear();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                string sql = @"SELECT V.MaVe, G.TenGhe, V.TrangThai 
                               FROM Ve V JOIN GheNgoi G ON V.MaGhe = G.MaGhe 
                               WHERE V.MaSuat = @ma ORDER BY G.TenGhe ASC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ma", _maSuat);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    int maVe = (int)dr["MaVe"];
                    Button btn = new Button
                    {
                        Text = dr["TenGhe"].ToString(),
                        Size = new Size(65, 30),
                        Margin = new Padding(3),
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.LightYellow,
                        Tag = maVe
                    };
                    if ((bool)dr["TrangThai"]) { btn.BackColor = Color.Gray; btn.Enabled = false; }
                    else
                    {
                        btn.Click += (s, e) => {
                            if (btn.BackColor == Color.LightYellow) { btn.BackColor = Color.Yellow; _listMaVeChon.Add(maVe); }
                            else { btn.BackColor = Color.LightYellow; _listMaVeChon.Remove(maVe); }
                            UpdateTotal();
                        };
                    }
                    flpGhe.Controls.Add(btn);
                }
            }
        }

        private void UpdateTotal()
        {
            decimal tileGiam = 1;
            foreach (RadioButton rb in gbLoaiVe.Controls) if (rb.Checked) tileGiam = Convert.ToDecimal(rb.Tag);

            decimal tongChuaGiam = _listMaVeChon.Count * _giaVeGoc;
            decimal tongSauDoiTuong = tongChuaGiam * tileGiam;
            decimal tienDiemDung = numDiemDung.Value * 1000;

            decimal canTra = tongSauDoiTuong - tienDiemDung;
            if (canTra < 0) canTra = 0;

            txtTongTien.Text = tongChuaGiam.ToString("N0");
            txtGiamGia.Text = (tongChuaGiam - canTra).ToString("N0");
            txtCanTra.Text = canTra.ToString("N0");
        }

        private void XuLyThanhToan()
        {
            if (_listMaVeChon.Count == 0) { MessageBox.Show("Vui lòng chọn ít nhất một ghế!"); return; }

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // 1. Tạo Hóa Đơn mới và lấy MaHD vừa sinh
                    decimal tongTien = decimal.Parse(txtCanTra.Text.Replace(",", ""));
                    // Lưu ý: MaNV mặc định là 1 (Quản Trị Viên) theo cấu trúc bảng của bạn
                    string sqlInsertHD = "INSERT INTO HoaDon (MaNV, NgayLap, TongTien) VALUES (1, GETDATE(), @tong); SELECT SCOPE_IDENTITY();";
                    SqlCommand cmdHD = new SqlCommand(sqlInsertHD, conn, trans);
                    cmdHD.Parameters.AddWithValue("@tong", tongTien);
                    int maHDMoi = Convert.ToInt32(cmdHD.ExecuteScalar());

                    foreach (int maVe in _listMaVeChon)
                    {
                        // 2. Cập nhật trạng thái vé sang 'Đã bán' (TrangThai = 1)
                        string sqlUpdateVe = "UPDATE Ve SET TrangThai = 1" + (_maKH != -1 ? ", MaKH = @makh" : "") + " WHERE MaVe = @mave";
                        SqlCommand cmdVe = new SqlCommand(sqlUpdateVe, conn, trans);
                        cmdVe.Parameters.AddWithValue("@mave", maVe);
                        if (_maKH != -1) cmdVe.Parameters.AddWithValue("@makh", _maKH);
                        cmdVe.ExecuteNonQuery();

                        // 3. Thêm vào Hóa Đơn Chi Tiết
                        string sqlInsertCT = "INSERT INTO HoaDonChiTiet (MaHD, MaVe, GiaBan) VALUES (@mahd, @mave, @giaban)";
                        SqlCommand cmdCT = new SqlCommand(sqlInsertCT, conn, trans);
                        cmdCT.Parameters.AddWithValue("@mahd", maHDMoi);
                        cmdCT.Parameters.AddWithValue("@mave", maVe);
                        cmdCT.Parameters.AddWithValue("@giaban", _giaVeGoc);
                        cmdCT.ExecuteNonQuery();
                    }

                    // 4. Cập nhật điểm tích lũy cho khách hàng thành viên
                    if (_maKH != -1)
                    {
                        int diemCong = (int)(tongTien * 0.05m / 1000); // 5% giá trị thanh toán
                        string sqlKH = "UPDATE KhachHang SET DiemTichLuy = DiemTichLuy + @diem - @dung WHERE MaKH = @ma";
                        SqlCommand cmdKH = new SqlCommand(sqlKH, conn, trans);
                        cmdKH.Parameters.AddWithValue("@diem", diemCong);
                        cmdKH.Parameters.AddWithValue("@dung", (int)numDiemDung.Value);
                        cmdKH.Parameters.AddWithValue("@ma", _maKH);
                        cmdKH.ExecuteNonQuery();
                    }

                    trans.Commit();

                    // 5. TỰ ĐỘNG HIỆN TRANG HÓA ĐƠN
                    // Truyền mã hóa đơn vừa tạo vào form để hiển thị thông tin
                    FormHoaDon frm = new FormHoaDon(maHDMoi);
                    frm.ShowDialog();

                    this.Close(); // Đóng sơ đồ ghế để hoàn tất phiên giao dịch
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Lỗi hệ thống: " + ex.Message);
                }
            }
        }

    }
}
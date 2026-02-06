using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace QuanLiVeTaiQuay.UI
{
    public partial class FormSuatChieu : Form
    {
        private string strConn = @"Data Source=.;Initial Catalog=QuanLyBanVeRapPhim;Integrated Security=True;TrustServerCertificate=True";

        // Khai báo biến với tiền tố gạch dưới để tránh lỗi trùng lặp
        private TextBox _txtMaSuat, _txtGiaVe;
        private ComboBox _cbPhim, _cbPhongChieu; // _cbPhim bây giờ dùng để chọn Tên phim
        private DateTimePicker _dtpNgayBD, _dtpGioBD, _dtpNgayKT, _dtpGioKT;
        private DataGridView _dgvSuatChieu;
        private Button _btnThem, _btnXoa, _btnSua;

        public FormSuatChieu()
        {
            // InitializeComponent(); // Xóa hoặc comment nếu bạn dựng UI thủ công hoàn toàn
            SetupManualUI();
            LoadDataPhimVaoCombo();
            LoadDataPhongVaoCombo();
            LoadDataSuatChieu();
        }

        private void SetupManualUI()
        {
            this.Text = "Quản Lý Suất Chiếu";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // 1. Bảng danh sách bên trái
            _dgvSuatChieu = new DataGridView
            {
                Location = new Point(15, 20),
                Size = new Size(640, 600),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true,
                AllowUserToAddRows = false,
                BackgroundColor = Color.White
            };
            _dgvSuatChieu.CellClick += DgvSuatChieu_CellClick;

            // 2. Panel nhập liệu bên phải
            Panel pnl = new Panel
            {
                Location = new Point(670, 20),
                Size = new Size(480, 600),
                BackColor = Color.White
            };
            int curY = 20;

            // Xóa dòng "Phim", đổi "Mã phim" thành "Tên phim"
            AddLine(pnl, "Mã lịch chiếu:", _txtMaSuat = new TextBox { Width = 280, ReadOnly = true }, ref curY);
            AddLine(pnl, "Tên phim:", _cbPhim = new ComboBox { Width = 280, DropDownStyle = ComboBoxStyle.DropDownList }, ref curY);
            AddLine(pnl, "Phòng chiếu:", _cbPhongChieu = new ComboBox { Width = 280, DropDownStyle = ComboBoxStyle.DropDownList }, ref curY);

            // Thời gian chiếu
            curY += 10;
            Label lblBD = new Label { Text = "Thời gian chiếu:", Location = new Point(10, curY), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            _dtpNgayBD = new DateTimePicker { Location = new Point(150, curY), Width = 135, Format = DateTimePickerFormat.Short };
            _dtpGioBD = new DateTimePicker { Location = new Point(295, curY), Width = 135, Format = DateTimePickerFormat.Time, ShowUpDown = true };
            pnl.Controls.AddRange(new Control[] { lblBD, _dtpNgayBD, _dtpGioBD });

            // Thời gian kết thúc
            curY += 45;
            Label lblKT = new Label { Text = "Thời gian kết thúc:", Location = new Point(10, curY), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            _dtpNgayKT = new DateTimePicker { Location = new Point(150, curY), Width = 135, Format = DateTimePickerFormat.Short };
            _dtpGioKT = new DateTimePicker { Location = new Point(295, curY), Width = 135, Format = DateTimePickerFormat.Time, ShowUpDown = true };
            pnl.Controls.AddRange(new Control[] { lblKT, _dtpNgayKT, _dtpGioKT });

            curY += 50;
            AddLine(pnl, "Giá vé:", _txtGiaVe = new TextBox { Width = 280 }, ref curY);

            // 3. Nút bấm đặt dưới cùng bên phải
            curY += 30;
            _btnThem = new Button { Text = "Thêm", Location = new Point(40, curY), Size = new Size(100, 45), BackColor = Color.White };
            _btnXoa = new Button { Text = "Xóa", Location = new Point(160, curY), Size = new Size(100, 45), BackColor = Color.White };
            _btnSua = new Button { Text = "Sửa", Location = new Point(280, curY), Size = new Size(100, 45), BackColor = Color.White };

            _btnThem.Click += (s, e) => ExecuteDb("INSERT");
            _btnSua.Click += (s, e) => ExecuteDb("UPDATE");
            _btnXoa.Click += (s, e) => ExecuteDb("DELETE");

            pnl.Controls.AddRange(new Control[] { _btnThem, _btnXoa, _btnSua });
            this.Controls.AddRange(new Control[] { _dgvSuatChieu, pnl });
        }

        private void AddLine(Control p, string txt, Control c, ref int y)
        {
            p.Controls.Add(new Label { Text = txt, Location = new Point(10, y), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold) });
            c.Location = new Point(150, y - 3);
            p.Controls.Add(c);
            y += 45;
        }

        private void LoadDataSuatChieu()
        {
            using (SqlConnection c = new SqlConnection(strConn))
            {
                string s = "SELECT S.MaSuat, P.TenPhong, F.TenPhim, S.GioBatDau, S.GioKetThuc, S.GiaVe FROM SuatChieu S " +
                           "JOIN Phim F ON S.MaPhim = F.MaPhim JOIN PhongChieu P ON S.MaPhong = P.MaPhong";
                SqlDataAdapter da = new SqlDataAdapter(s, c);
                DataTable dt = new DataTable(); da.Fill(dt);
                _dgvSuatChieu.DataSource = dt;
            }
        }

        private void LoadDataPhimVaoCombo()
        {
            using (SqlConnection c = new SqlConnection(strConn))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaPhim, TenPhim FROM Phim", c);
                DataTable dt = new DataTable(); da.Fill(dt);
                _cbPhim.DataSource = dt;
                _cbPhim.DisplayMember = "TenPhim";
                _cbPhim.ValueMember = "MaPhim";
            }
        }

        private void LoadDataPhongVaoCombo()
        {
            using (SqlConnection c = new SqlConnection(strConn))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaPhong, TenPhong FROM PhongChieu", c);
                DataTable dt = new DataTable(); da.Fill(dt);
                _cbPhongChieu.DataSource = dt;
                _cbPhongChieu.DisplayMember = "TenPhong";
                _cbPhongChieu.ValueMember = "MaPhong";
            }
        }

        private void DgvSuatChieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow r = _dgvSuatChieu.Rows[e.RowIndex];
                _txtMaSuat.Text = r.Cells["MaSuat"].Value?.ToString();
                _cbPhim.Text = r.Cells["TenPhim"].Value?.ToString(); // Tự động chọn Phim theo tên
                _cbPhongChieu.Text = r.Cells["TenPhong"].Value?.ToString();
                _txtGiaVe.Text = r.Cells["GiaVe"].Value != DBNull.Value ? Convert.ToDecimal(r.Cells["GiaVe"].Value).ToString("0") : "0";

                if (r.Cells["GioBatDau"].Value != DBNull.Value)
                {
                    DateTime d = Convert.ToDateTime(r.Cells["GioBatDau"].Value);
                    _dtpNgayBD.Value = d; _dtpGioBD.Value = d;
                }
                if (r.Cells["GioKetThuc"].Value != DBNull.Value)
                {
                    DateTime d2 = Convert.ToDateTime(r.Cells["GioKetThuc"].Value);
                    _dtpNgayKT.Value = d2; _dtpGioKT.Value = d2;
                }
            }
        }

        private void ExecuteDb(string op)
        {
            using (SqlConnection c = new SqlConnection(strConn))
            {
                try
                {
                    c.Open();
                    string sql = "";
                    DateTime start = _dtpNgayBD.Value.Date + _dtpGioBD.Value.TimeOfDay;
                    DateTime end = _dtpNgayKT.Value.Date + _dtpGioKT.Value.TimeOfDay;

                    if (op == "INSERT") sql = "INSERT INTO SuatChieu (MaPhim, MaPhong, GioBatDau, GioKetThuc, GiaVe) VALUES (@p, @ph, @s, @e, @g)";
                    else if (op == "UPDATE") sql = "UPDATE SuatChieu SET MaPhim=@p, MaPhong=@ph, GioBatDau=@s, GioKetThuc=@e, GiaVe=@g WHERE MaSuat=@id";
                    else sql = "DELETE FROM SuatChieu WHERE MaSuat=@id";

                    SqlCommand cmd = new SqlCommand(sql, c);
                    if (op != "INSERT") cmd.Parameters.AddWithValue("@id", _txtMaSuat.Text);
                    if (op != "DELETE")
                    {
                        cmd.Parameters.AddWithValue("@p", _cbPhim.SelectedValue);
                        cmd.Parameters.AddWithValue("@ph", _cbPhongChieu.SelectedValue);
                        cmd.Parameters.AddWithValue("@s", start);
                        cmd.Parameters.AddWithValue("@e", end);
                        cmd.Parameters.AddWithValue("@g", decimal.Parse(_txtGiaVe.Text));
                    }
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thành công!"); LoadDataSuatChieu();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }
    }
}
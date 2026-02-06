using Microsoft.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace QuanLiVeTaiQuay.UI
{
    public partial class FormLịchChiếu : Form
    {
        private string strConn = @"Data Source=.;Initial Catalog=QuanLyBanVeRapPhim;Integrated Security=True;TrustServerCertificate=True";
        private DataGridView dgvSuatChieu;
        private DateTimePicker dtpNgayChieu;
        private ComboBox cbPhim;

        public FormLịchChiếu()
        {
            InitializeUI();
            // Thiết lập ngày mặc định là hôm nay và load dữ liệu theo ngày này
            dtpNgayChieu.Value = DateTime.Now;
            FilterData();
        }

        private void InitializeUI()
        {
            this.BackColor = Color.FromArgb(64, 64, 64);
            this.Size = new Size(1100, 600); // Tăng kích thước để giống ảnh mẫu
            this.Text = "Lịch Chiếu Phim";

            // 1. Header: Lịch Chiếu Phim
            Label lblHeader = new Label
            {
                Text = "Lịch Chiếu Phim",
                Dock = DockStyle.Top,
                Height = 60,
                ForeColor = Color.Yellow,
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Black
            };

            // 2. Panel trái (Bộ lọc và Thông tin)
            Panel pnlLeft = new Panel { Dock = DockStyle.Left, Width = 300, BackColor = Color.Gray, Padding = new Padding(15) };

            Label lblDate = new Label { Text = "Thời Gian:", ForeColor = Color.Red, Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(15, 20), AutoSize = true };
            dtpNgayChieu = new DateTimePicker { Location = new Point(15, 55), Width = 250, Format = DateTimePickerFormat.Short };
            // Sự kiện: Khi đổi ngày trên lịch, bảng bên cạnh tự lọc
            dtpNgayChieu.ValueChanged += (s, e) => FilterData();

            Label lblMovie = new Label { Text = "Phim:", ForeColor = Color.Red, Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(15, 110), AutoSize = true };
            cbPhim = new ComboBox { Location = new Point(15, 145), Width = 250, DropDownStyle = ComboBoxStyle.DropDown }; // Để DropDown để gán Text được

            Button btnChonVe = new Button
            {
                Text = "Chọn vé",
                Location = new Point(70, 450),
                Size = new Size(150, 45),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnChonVe.Click += BtnChonVe_Click;

            pnlLeft.Controls.AddRange(new Control[] { lblDate, dtpNgayChieu, lblMovie, cbPhim, btnChonVe });

            // 3. GridView bên phải
            dgvSuatChieu = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true,
                BackgroundColor = Color.Silver,
                RowHeadersVisible = false
            };
            // Sự kiện: Click vào dòng bất kỳ thì tên phim hiện sang ô bên trái
            dgvSuatChieu.CellClick += DgvSuatChieu_CellClick;

            this.Controls.Add(dgvSuatChieu);
            this.Controls.Add(pnlLeft);
            this.Controls.Add(lblHeader);
        }

        // Logic 1: Khi click vào bảng, load tên phim sang ô bên trái
        private void DgvSuatChieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSuatChieu.Rows[e.RowIndex];
                cbPhim.Text = row.Cells["TenPhim"].Value.ToString();
            }
        }

        // Logic 2: Lọc danh sách suất chiếu theo ngày được chọn
        private void FilterData()
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    // 1. Câu lệnh SQL lấy đúng các trường để hiển thị lên bảng
                    // Lưu ý: MaSuat sẽ được dùng làm MaCaChieu
                    string sql = @"SELECT S.MaSuat as MaCaChieu, 
                                  P.TenPhim, 
                                  S.GioBatDau as ThoiGianChieu, 
                                  PC.TenPhong as MaPhong 
                           FROM SuatChieu S 
                           INNER JOIN Phim P ON S.MaPhim = P.MaPhim 
                           INNER JOIN PhongChieu PC ON S.MaPhong = PC.MaPhong
                           WHERE CAST(S.GioBatDau AS DATE) = @ngay";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    // Lấy ngày từ DateTimePicker bên trái
                    cmd.Parameters.AddWithValue("@ngay", dtpNgayChieu.Value.ToString("yyyy-MM-dd"));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // 2. Xử lý hiển thị lên DataGridView
                    dgvSuatChieu.DataSource = null; // Xóa dữ liệu cũ
                    dgvSuatChieu.AutoGenerateColumns = true; // Để GridView tự tạo cột theo SQL
                    dgvSuatChieu.DataSource = dt;

                    // 3. Kiểm tra nếu không có dữ liệu
                    if (dt.Rows.Count == 0)
                    {
                        // Thử load toàn bộ nếu lọc theo ngày không có (để debug)
                        // MessageBox.Show("Không có suất chiếu nào trong ngày: " + dtpNgayChieu.Value.ToShortDateString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối DB: " + ex.Message);
                }
            }
        }
        private void BtnChonVe_Click(object sender, EventArgs e)
        {
            if (dgvSuatChieu.CurrentRow != null)
            {
                int maSuat = (int)dgvSuatChieu.CurrentRow.Cells["MaCaChieu"].Value;
                string tenPhim = dgvSuatChieu.CurrentRow.Cells["TenPhim"].Value.ToString();

                // Mở form Sơ đồ ghế và truyền dữ liệu để tránh lỗi CS7036
                FormSoDoGhe frm = new FormSoDoGhe(maSuat, tenPhim);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một suất chiếu trên bảng!");
            }
        }
    }
}
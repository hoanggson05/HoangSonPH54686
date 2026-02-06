namespace QuanLiVeTaiQuay.UI
{
    partial class FormHoaDon : Form
    {

        private void InitializeComponent()
        {
            // Cài đặt Form chính
            this.Text = "HÓA ĐƠN CHI TIẾT";
            this.Size = new Size(850, 650);
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Tiêu đề lớn (HÓA ĐƠN THANH TOÁN)
            Label lblTitle = new Label
            {
                Text = "HÓA ĐƠN THANH TOÁN",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.Blue,
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // GroupBox: Thông Tin Hóa Đơn
            GroupBox gbInfo = new GroupBox
            {
                Text = "Thông Tin Hóa Đơn",
                Location = new Point(20, 70),
                Size = new Size(790, 150),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.Blue
            };

            // Thêm các Label và TextBox vào GroupBox này
            Label l1 = new Label { Text = "Mã Hóa Đơn:", Location = new Point(20, 40), AutoSize = true, ForeColor = Color.Blue };
            txtMaHD = new TextBox { Location = new Point(130, 37), Width = 200, ReadOnly = true };

            Label l2 = new Label { Text = "Ngày Bán:", Location = new Point(20, 85), AutoSize = true, ForeColor = Color.Blue };
            dtpNgayBan = new DateTimePicker { Location = new Point(130, 82), Width = 200 };

            Label l3 = new Label { Text = "Nhân Viên:", Location = new Point(400, 40), AutoSize = true, ForeColor = Color.Blue };
            txtNhanVien = new TextBox { Location = new Point(510, 37), Width = 200, ReadOnly = true };

            Label l4 = new Label { Text = "Tổng Tiền (VNĐ):", Location = new Point(400, 85), AutoSize = true, ForeColor = Color.Blue };
            txtTongTien = new TextBox { Location = new Point(510, 82), Width = 200, ForeColor = Color.Red, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

            gbInfo.Controls.AddRange(new Control[] { l1, txtMaHD, l2, dtpNgayBan, l3, txtNhanVien, l4, txtTongTien });

            // Khu vực nút bấm (Dàn hàng ngang như ảnh 2)
            Panel pnlButtons = new Panel { Location = new Point(20, 230), Size = new Size(790, 80) };
            string[] btnNames = { "In Hóa Đơn", "Lưu File", "Đóng" };
            for (int i = 0; i < btnNames.Length; i++)
            {
                Button btn = new Button
                {
                    Text = btnNames[i],
                    Size = new Size(120, 50),
                    Location = new Point(180 + (i * 150), 10),
                    BackColor = Color.LightSteelBlue,
                    FlatStyle = FlatStyle.Flat
                };
                if (btnNames[i] == "Đóng") btn.Click += (s, e) => this.Close();
                pnlButtons.Controls.Add(btn);
            }

            // GroupBox: Danh Sách Vé Đã Chọn
            GroupBox gbList = new GroupBox
            {
                Text = "Danh Sách Chi Tiết Vé",
                Location = new Point(20, 320),
                Size = new Size(790, 250),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.Blue
            };

            dgvDanhSachVe = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BorderStyle = BorderStyle.None
            };
            gbList.Controls.Add(dgvDanhSachVe);

            // Thêm tất cả vào Form
            this.Controls.AddRange(new Control[] { lblTitle, gbInfo, pnlButtons, gbList });
        }

        // Khai báo biến
        private TextBox txtMaHD, txtNhanVien, txtTongTien;
        private DateTimePicker dtpNgayBan;
        private DataGridView dgvDanhSachVe;
    }
}
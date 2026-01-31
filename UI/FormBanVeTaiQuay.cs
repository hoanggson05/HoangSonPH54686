using Microsoft.Data.SqlClient;
using System.Data;

namespace QuanLiVeTaiQuay.UI
{
    public partial class FormBanVeTaiQuay : Form
    {
        // Chuỗi kết nối
        private string strConn = @"Data Source=.;Initial Catalog=QuanLyBanVeRapPhim;Integrated Security=True;TrustServerCertificate=True";

        // Khai báo biến
        private TextBox txtSdt, txtTen, txtSearch;
        private Label lblStatus, lblTotal;
        private DataGridView dgvBill;
        private FlowLayoutPanel flowProduct;
        private Color clrBlueHeader = Color.FromArgb(0, 114, 198);

        public FormBanVeTaiQuay()
        {
            InitializeUI();
            LoadDataFromSQL();
        }

        private void InitializeUI()
        {
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9);

            // --- 1. HEADER CÓ ICON ---
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.White };
            // Giả lập icon bằng nhãn nếu bạn chưa có file ảnh, hoặc dùng picIcon như cũ
            Label lblIcon = new Label { Text = "🎟️", Font = new Font("Segoe UI", 20), Location = new Point(10, 5), AutoSize = true };
            Label lblTitle = new Label { Text = "BÁN VÉ TẠI QUẦY", Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(50, 12), AutoSize = true };
            pnlHeader.Controls.Add(lblIcon);
            pnlHeader.Controls.Add(lblTitle);
            this.Controls.Add(pnlHeader);

            TableLayoutPanel mainLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2 };
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            this.Controls.Add(mainLayout);
            mainLayout.BringToFront();

            // --- 2. CỘT TRÁI: KHÁCH HÀNG & GIỎ HÀNG ---
            Panel pnlLeft = new Panel { Dock = DockStyle.Fill, Padding = new Padding(5) };
            mainLayout.Controls.Add(pnlLeft, 0, 0);

            // Group Khách hàng
            pnlLeft.Controls.Add(CreateHeaderLabel("Thông Tin Khách Hàng"));
            Panel pnlCust = new Panel { Dock = DockStyle.Top, Height = 100, BorderStyle = BorderStyle.FixedSingle };
            txtSdt = new TextBox { Location = new Point(110, 15), Width = 150 };
            txtTen = new TextBox { Location = new Point(110, 45), Width = 150 };
            lblStatus = new Label { Text = "", Location = new Point(110, 75), AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Italic) };
            Button btnCheck = new Button { Text = "XÁC NHẬN", Location = new Point(270, 14), Width = 80, Height = 52, BackColor = Color.DodgerBlue, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnCheck.Click += BtnCheck_Click;

            pnlCust.Controls.Add(new Label { Text = "Số điện thoại:", Location = new Point(10, 18), AutoSize = true });
            pnlCust.Controls.Add(txtSdt);
            pnlCust.Controls.Add(new Label { Text = "Họ tên khách:", Location = new Point(10, 48), AutoSize = true });
            pnlCust.Controls.Add(txtTen);
            pnlCust.Controls.Add(btnCheck);
            pnlCust.Controls.Add(lblStatus);
            pnlLeft.Controls.Add(pnlCust);

            // Group Thanh toán (Nằm dưới cùng bên trái)
            Panel pnlPay = new Panel { Dock = DockStyle.Bottom, Height = 90, BorderStyle = BorderStyle.FixedSingle, BackColor = Color.WhiteSmoke };
            lblTotal = new Label { Text = "Tổng tiền: 0 đ", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.Red, Location = new Point(15, 10), AutoSize = true };
            Button btnPay = new Button { Text = "✔ THANH TOÁN (F1)", Location = new Point(15, 35), Size = new Size(180, 45), BackColor = Color.ForestGreen, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            pnlPay.Controls.Add(lblTotal); pnlPay.Controls.Add(btnPay);
            pnlLeft.Controls.Add(pnlPay);

            // Group Hóa đơn
            pnlLeft.Controls.Add(CreateHeaderLabel("Thông Tin Hóa Đơn"));
            dgvBill = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                EnableHeadersVisualStyles = false,
                ColumnHeadersDefaultCellStyle = { BackColor = clrBlueHeader, ForeColor = Color.White }
            };
            dgvBill.Columns.Add("Ten", "Tên Phim/Vé");
            dgvBill.Columns.Add("Gia", "Đơn Giá");
            dgvBill.Columns.Add("SL", "SL");
            dgvBill.Columns.Add("Tong", "Tổng");
            pnlLeft.Controls.Add(dgvBill);
            dgvBill.BringToFront();

            // --- 3. CỘT PHẢI: TÌM KIẾM & DANH SÁCH ---
            Panel pnlRight = new Panel { Dock = DockStyle.Fill, Padding = new Padding(5) };
            mainLayout.Controls.Add(pnlRight, 1, 0);

            pnlRight.Controls.Add(CreateHeaderLabel("Bộ Lọc"));
            Panel pnlSearch = new Panel { Dock = DockStyle.Top, Height = 60, BorderStyle = BorderStyle.FixedSingle };
            txtSearch = new TextBox { Location = new Point(100, 18), Width = 180 };
            Button btnSearch = new Button { Text = "TÌM KIẾM", Location = new Point(290, 15), Width = 90, Height = 30, BackColor = clrBlueHeader, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSearch.Click += (s, e) => LoadDataFromSQL(txtSearch.Text);
            pnlSearch.Controls.Add(new Label { Text = "Tên phim:", Location = new Point(10, 21), AutoSize = true });
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Controls.Add(btnSearch);
            pnlRight.Controls.Add(pnlSearch);

            pnlRight.Controls.Add(CreateHeaderLabel("Danh Sách Vé Phim"));
            flowProduct = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, BackColor = Color.FromArgb(245, 245, 245) };
            pnlRight.Controls.Add(flowProduct);
            flowProduct.BringToFront();
        }

        private Label CreateHeaderLabel(string text) => new Label { Text = "  " + text, Dock = DockStyle.Top, Height = 28, BackColor = clrBlueHeader, ForeColor = Color.White, TextAlign = ContentAlignment.MiddleLeft, Font = new Font("Segoe UI", 9, FontStyle.Bold) };

        // --- LOGIC KIỂM TRA KHÁCH HÀNG (Dựa trên SQL của bạn: HoTen, SDT) ---
        private void BtnCheck_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSdt.Text)) return;
            try
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();
                    // Khớp tên cột: SDT và HoTen
                    string query = "SELECT HoTen FROM KhachHang WHERE SDT = @sdt";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@sdt", txtSdt.Text);
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        txtTen.Text = result.ToString();
                        lblStatus.Text = "✓ Khách hàng thân thiết";
                        lblStatus.ForeColor = Color.Green;
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(txtTen.Text))
                        {
                            lblStatus.Text = "⚠ Không tồn tại. Nhập tên để thêm!";
                            lblStatus.ForeColor = Color.Red;
                        }
                        else
                        {
                            string insert = "INSERT INTO KhachHang (HoTen, SDT) VALUES (@ten, @sdt)";
                            SqlCommand cmdAdd = new SqlCommand(insert, conn);
                            cmdAdd.Parameters.AddWithValue("@ten", txtTen.Text);
                            cmdAdd.Parameters.AddWithValue("@sdt", txtSdt.Text);
                            cmdAdd.ExecuteNonQuery();
                            lblStatus.Text = "✓ Đã lưu khách hàng mới!";
                            lblStatus.ForeColor = Color.Green;
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi SQL: " + ex.Message); }
        }

        private void LoadDataFromSQL(string key = "")
        {
            flowProduct.Controls.Clear();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                // JOIN Phim và SuatChieu để lấy giá vé
                string sql = "SELECT P.TenPhim, S.GiaVe FROM Phim P JOIN SuatChieu S ON P.MaPhim = S.MaPhim WHERE S.TrangThai = 1";
                if (!string.IsNullOrEmpty(key)) sql += " AND P.TenPhim LIKE @key";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@key", "%" + key + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow r in dt.Rows) AddCard(r["TenPhim"].ToString(), Convert.ToDouble(r["GiaVe"]));
            }
        }

        private void AddCard(string name, double price)
        {
            Panel card = new Panel { Width = 140, Height = 170, BackColor = Color.White, Margin = new Padding(7), BorderStyle = BorderStyle.FixedSingle };
            Label lblN = new Label { Text = name, Dock = DockStyle.Top, Height = 45, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 8, FontStyle.Bold) };
            Label lblP = new Label { Text = $"{price:N0}đ", Dock = DockStyle.Bottom, Height = 25, TextAlign = ContentAlignment.MiddleCenter, ForeColor = Color.OrangeRed };
            Button btn = new Button { Text = "Chọn", Dock = DockStyle.Bottom, Height = 30, BackColor = clrBlueHeader, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btn.Click += (s, e) =>
            {
                dgvBill.Rows.Add(name, price, 1, price);
                double t = 0; foreach (DataGridViewRow r in dgvBill.Rows) t += Convert.ToDouble(r.Cells[3].Value);
                lblTotal.Text = $"Tổng tiền: {t:N0} đ";
            };
            card.Controls.Add(lblN); card.Controls.Add(lblP); card.Controls.Add(btn);
            flowProduct.Controls.Add(card);
        }
    }
}
using Microsoft.Data.SqlClient;
using System.Data;

namespace QuanLiVeTaiQuay.UI
{
    public partial class FormKhachHang : Form
    {
        private string strConn = @"Data Source=.;Initial Catalog=QuanLyBanVeRapPhim;Integrated Security=True;TrustServerCertificate=True";
        private DataGridView dgvKhachHang;
        private TextBox txtMaKH, txtHoTen, txtSDT, txtEmail, txtSearch;
        private DateTimePicker dtpNgaySinh;
        private ComboBox cboHang;
        private Color clrMain = Color.FromArgb(0, 114, 198);

        public FormKhachHang()
        {
            InitializeComponentManual();
            LoadData();
        }

        private void InitializeComponentManual()
        {
            this.Text = "Quản lý khách hàng";
            this.Size = new Size(1000, 600);
            this.BackColor = Color.White;

            // --- Header ---
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 40, BackColor = Color.Firebrick };
            Label lblTitle = new Label { Text = "QUẢN LÝ KHÁCH HÀNG", ForeColor = Color.White, Font = new Font("Segoe UI", 12, FontStyle.Bold), Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter };
            pnlHeader.Controls.Add(lblTitle);
            this.Controls.Add(pnlHeader);

            TableLayoutPanel mainLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            this.Controls.Add(mainLayout);
            mainLayout.BringToFront();

            // --- CỘT TRÁI: NHẬP LIỆU ---
            GroupBox grbInput = new GroupBox { Text = "Thông tin khách hàng", Dock = DockStyle.Fill, Margin = new Padding(10) };
            mainLayout.Controls.Add(grbInput, 0, 0);

            int y = 30;
            txtMaKH = CreateInput(grbInput, "Mã khách hàng:", ref y, true);
            txtHoTen = CreateInput(grbInput, "Họ tên:", ref y);
            txtSDT = CreateInput(grbInput, "Điện thoại:", ref y);
            txtEmail = CreateInput(grbInput, "Email:", ref y);

            Label lblNgaySinh = new Label { Text = "Ngày sinh:", Location = new Point(10, y), AutoSize = true };
            dtpNgaySinh = new DateTimePicker { Location = new Point(110, y), Width = 180, Format = DateTimePickerFormat.Short };
            grbInput.Controls.Add(lblNgaySinh); grbInput.Controls.Add(dtpNgaySinh);
            y += 35;

            Label lblHang = new Label { Text = "Hạng TV:", Location = new Point(10, y), AutoSize = true };
            cboHang = new ComboBox { Location = new Point(110, y), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
            cboHang.Items.AddRange(new string[] { "Đồng", "Bạc", "Vàng", "Kim Cương" });
            grbInput.Controls.Add(lblHang); grbInput.Controls.Add(cboHang);
            y += 50;

            // Nút bấm
            Button btnThem = new Button { Text = "Thêm", Location = new Point(10, y), Width = 65, BackColor = Color.LightGreen, FlatStyle = FlatStyle.Flat };
            Button btnSua = new Button { Text = "Sửa", Location = new Point(80, y), Width = 65, BackColor = Color.LightBlue, FlatStyle = FlatStyle.Flat };
            Button btnXoa = new Button { Text = "Xóa", Location = new Point(150, y), Width = 65, BackColor = Color.LightPink, FlatStyle = FlatStyle.Flat };
            Button btnLamMoi = new Button { Text = "Làm mới", Location = new Point(220, y), Width = 70, FlatStyle = FlatStyle.Flat };

            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += (s, e) => ClearInput();

            grbInput.Controls.AddRange(new Control[] { btnThem, btnSua, btnXoa, btnLamMoi });

            // --- CỘT PHẢI: DANH SÁCH ---
            Panel pnlRight = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
            mainLayout.Controls.Add(pnlRight, 1, 0);

            Panel pnlSearch = new Panel { Dock = DockStyle.Top, Height = 40 };
            txtSearch = new TextBox { Location = new Point(10, 10), Width = 250 };
            Button btnSearch = new Button { Text = "Tìm kiếm", Location = new Point(270, 8), Width = 80 };
            btnSearch.Click += (s, e) => LoadData(txtSearch.Text);
            pnlSearch.Controls.AddRange(new Control[] { txtSearch, btnSearch });
            pnlRight.Controls.Add(pnlSearch);

            dgvKhachHang = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvKhachHang.CellClick += DgvKhachHang_CellClick;
            pnlRight.Controls.Add(dgvKhachHang);
            dgvKhachHang.BringToFront();
        }

        private TextBox CreateInput(GroupBox gb, string label, ref int y, bool readOnly = false)
        {
            Label lbl = new Label { Text = label, Location = new Point(10, y), AutoSize = true };
            TextBox txt = new TextBox { Location = new Point(110, y), Width = 180, ReadOnly = readOnly };
            gb.Controls.Add(lbl); gb.Controls.Add(txt);
            y += 35;
            return txt;
        }

        private void LoadData(string search = "")
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string sql = "SELECT MaKH, HoTen, SDT, Email, NgaySinh, HangThanhVien, DiemTichLuy FROM KhachHang WHERE TrangThai = 1";
                if (!string.IsNullOrEmpty(search)) sql += " AND (HoTen LIKE @key OR SDT LIKE @key)";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@key", "%" + search + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvKhachHang.DataSource = dt;
            }
        }

        private void DgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
                txtMaKH.Text = row.Cells["MaKH"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txtSDT.Text = row.Cells["SDT"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                dtpNgaySinh.Value = row.Cells["NgaySinh"].Value != DBNull.Value ? Convert.ToDateTime(row.Cells["NgaySinh"].Value) : DateTime.Now;
                cboHang.SelectedItem = row.Cells["HangThanhVien"].Value.ToString();
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                string sql = "INSERT INTO KhachHang (HoTen, SDT, Email, NgaySinh, HangThanhVien) VALUES (@ten, @sdt, @mail, @ns, @hang)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ten", txtHoTen.Text);
                cmd.Parameters.AddWithValue("@sdt", txtSDT.Text);
                cmd.Parameters.AddWithValue("@mail", txtEmail.Text);
                cmd.Parameters.AddWithValue("@ns", dtpNgaySinh.Value);
                cmd.Parameters.AddWithValue("@hang", cboHang.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm thành công!");
                LoadData();
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text)) return;
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                string sql = "UPDATE KhachHang SET HoTen=@ten, SDT=@sdt, Email=@mail, NgaySinh=@ns, HangThanhVien=@hang WHERE MaKH=@ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ma", txtMaKH.Text);
                cmd.Parameters.AddWithValue("@ten", txtHoTen.Text);
                cmd.Parameters.AddWithValue("@sdt", txtSDT.Text);
                cmd.Parameters.AddWithValue("@mail", txtEmail.Text);
                cmd.Parameters.AddWithValue("@ns", dtpNgaySinh.Value);
                cmd.Parameters.AddWithValue("@hang", cboHang.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thành công!");
                LoadData();
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text)) return;
            if (MessageBox.Show("Bạn có muốn xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();
                    string sql = "UPDATE KhachHang SET TrangThai = 0 WHERE MaKH = @ma";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ma", txtMaKH.Text);
                    cmd.ExecuteNonQuery();
                    LoadData();
                }
            }
        }

        private void ClearInput()
        {
            txtMaKH.Clear(); txtHoTen.Clear(); txtSDT.Clear(); txtEmail.Clear();
            dtpNgaySinh.Value = DateTime.Now; cboHang.SelectedIndex = -1;
        }
    }
}
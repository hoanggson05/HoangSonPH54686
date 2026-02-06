using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLiVeTaiQuay.UI
{
    public partial class FormMain : Form
    {
        private bool isCollapsed = false;
        private Form currentForm;
        private string userRole;

        private Panel pnlMenu, pnlMain, pnlHeader;
        private Label lblTitle, lblHello;
        private Button btnToggle, btnVe, btnCaChieu, btnPhimMoi, btnTheLoai, btnPhongChieu, btnLichChieu, btnDangXuat;

        public FormMain(string role = "Admin")
        {
            this.userRole = role;
            InitForm();
            InitMenu(); // Khởi tạo menu trước
            InitHeader();
            PhanQuyen(); // Phân quyền sau khi đã có các nút

            // Mở mặc định trang Sơ đồ ghế
            OpenChildForm(new FormLịchChiếu(), "Sơ đồ ghế");
        }

        private void InitForm()
        {
            this.Text = "Hệ thống Quản lý vé";
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitMenu()
        {
            pnlMenu = new Panel { Width = 220, Dock = DockStyle.Left, BackColor = Color.FromArgb(32, 32, 32) };
            pnlMain = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };

            // 1. Khởi tạo nút Đăng xuất
            btnDangXuat = CreateLogoutButton();

            // 2. Khởi tạo các nút chức năng theo ảnh mẫu
            btnVe = CreateButton("🎟️  Vé", (s, e) => OpenChildForm(new FormBanVe(), "Quản lý Vé"));
            btnCaChieu = CreateButton("📅  Ca Chiếu", (s, e) => OpenChildForm(new FormSuatChieu(), "Quản lý Ca Chiếu"));
            btnPhimMoi = CreateButton("🎬  Phim", (s, e) => OpenChildForm(new FormQuanLyPhim(), "Quản lý Phim"));
            btnLichChieu = CreateButton("🪑  Bán vé", (s, e) => OpenChildForm(new FormLịchChiếu(), "Bán vé"));
            btnToggle = CreateButton("☰", BtnToggle_Click);

            // 3. THỨ TỰ THÊM VÀO PANEL (Quyết định vị trí từ dưới lên do DockStyle.Top)
            pnlMenu.Controls.Add(btnDangXuat); // Dock Bottom -> Luôn ở đáy

            pnlMenu.Controls.Add(btnVe);
            pnlMenu.Controls.Add(btnCaChieu);
            pnlMenu.Controls.Add(btnPhimMoi);
            pnlMenu.Controls.Add(btnTheLoai);
            pnlMenu.Controls.Add(btnPhongChieu);
            pnlMenu.Controls.Add(btnLichChieu);
            pnlMenu.Controls.Add(btnToggle); // Nút menu nằm trên cùng

            btnDangXuat.SendToBack();

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlMenu);
        }

        private void InitHeader()
        {
            pnlHeader = new Panel { Height = 60, Dock = DockStyle.Top, BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            lblTitle = new Label { Text = "TRANG CHỦ", Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(20, 15), AutoSize = true };

            // Hiển thị chế độ làm việc hiện tại
            string displayName = (userRole == "Admin") ? "Quản Lý" : "Nhân Viên";
            lblHello = new Label { Text = "Chế độ: " + displayName, Font = new Font("Segoe UI", 10, FontStyle.Italic), AutoSize = true };

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblHello);
            pnlMain.Controls.Add(pnlHeader);

            this.Layout += (s, e) => lblHello.Location = new Point(pnlHeader.Width - 180, 20);
        }

        private Button CreateButton(string text, EventHandler clickAction)
        {
            Button btn = new Button
            {
                Text = text,
                Tag = text,
                Dock = DockStyle.Top,
                Height = 50,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(32, 32, 32),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0),
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 10)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += clickAction;
            btn.Click += (s, e) => ActivateButton(s);
            return btn;
        }

        private Button CreateLogoutButton()
        {
            Button btn = new Button
            {
                Text = "🚪  Đăng xuất",
                Tag = "🚪  Đăng xuất",
                Dock = DockStyle.Bottom,
                Height = 50,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(192, 0, 0),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (s, e) => {
                if (MessageBox.Show("Bạn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Hide();
                    // Quay về Dashboard nếu gốc là Admin, ngược lại về Login
                    if (FormDangNhap.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                        new FormDashboard().ShowDialog();
                    else
                        new FormDangNhap().ShowDialog();
                    this.Close();
                }
            };
            return btn;
        }

        private void PhanQuyen()
{
    // Nếu là Nhân viên -> Ẩn các nút quản lý hệ thống
    if (userRole == "NhanVien")
    {
        // Sử dụng toán tử ?. để tránh lỗi NullReferenceException
        if (btnPhongChieu != null) btnPhongChieu.Visible = false;
        if (btnTheLoai != null) btnTheLoai.Visible = false;
        if (btnCaChieu != null) btnCaChieu.Visible = false;
    }
}

        private void ActivateButton(object sender)
        {
            if (!(sender is Button activeBtn) || activeBtn == btnToggle || activeBtn == btnDangXuat) return;
            foreach (Control ctrl in pnlMenu.Controls)
            {
                if (ctrl is Button b && b != btnToggle && b != btnDangXuat)
                {
                    b.BackColor = Color.FromArgb(32, 32, 32);
                    b.ForeColor = Color.White;
                }
            }
            activeBtn.BackColor = Color.FromArgb(64, 64, 64);
            activeBtn.ForeColor = Color.Yellow;
        }

        private void BtnToggle_Click(object sender, EventArgs e)
        {
            isCollapsed = !isCollapsed;
            pnlMenu.Width = isCollapsed ? 60 : 220;
            foreach (Control ctrl in pnlMenu.Controls)
            {
                if (ctrl is Button btn && btn != btnToggle)
                {
                    btn.Text = isCollapsed ? "" : btn.Tag?.ToString();
                }
            }
        }

        private void OpenChildForm(Form childForm, string title)
        {
            currentForm?.Close();
            currentForm = childForm;
            lblTitle.Text = title.ToUpper();
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
        }
    }
}
namespace QuanLiVeTaiQuay.UI
{
    public partial class FormMain : Form
    {
        // ===== CONTROLS =====

        Button btnBanVeTaiQuay;
        Button btnToggle;
        Button btnBanVe;
        Button btnKhachHang;
        Button btnPhim;
        Button btnPhong;
        Button btnSuatChieu;

        bool isCollapsed = false;

        public FormMain()
        {
            InitializeComponent();
            OpenChildForm(new FormBanVe());
        }

        private void InitializeComponent()
        {
            this.Controls.Add(pnlMenu);   
            this.Controls.Add(pnlMain);   
            this.Text = "FormMain";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;


            pnlMain = new Panel();
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.BackColor = Color.WhiteSmoke;
            this.Controls.Add(pnlMain);

            pnlMenu = new Panel();
            pnlMenu.Width = 220;
            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.BackColor = Color.FromArgb(32, 32, 32);
            this.Controls.Add(pnlMenu);

            btnSuatChieu = CreateMenuButton("⏰  Suất chiếu", 45);
            btnPhong = CreateMenuButton("🏢  Phòng", 45);
            btnPhim = CreateMenuButton("🎬  Phim", 45);
            btnKhachHang = CreateMenuButton("👤  Khách hàng", 45);
            btnBanVe = CreateMenuButton("🎟️  Quản lí vé", 45);
            btnBanVeTaiQuay = CreateMenuButton("🎟️  Bán vé tại quầy", 45);
            btnKhachHang.Click += (s, e) => OpenChildForm(new FormKhachHang());
            btnBanVe.Click += (s, e) => OpenChildForm(new FormBanVe());
            btnBanVeTaiQuay.Click += (s, e) => OpenChildForm(new FormBanVeTaiQuay());

            // ===== TOGGLE BUTTON =====
            btnToggle = CreateMenuButton("☰", 50);
            btnToggle.Click += BtnToggle_Click;


            pnlMenu.Controls.Add(btnSuatChieu);
            pnlMenu.Controls.Add(btnPhong);
            pnlMenu.Controls.Add(btnPhim);
            pnlMenu.Controls.Add(btnKhachHang);
            pnlMenu.Controls.Add(btnBanVe);
            pnlMenu.Controls.Add(btnBanVeTaiQuay);
            pnlMenu.Controls.Add(btnToggle);
        }

  
        private Button CreateMenuButton(string text, int height)
        {
            return new Button
            {
                Text = text,
                Dock = DockStyle.Top,
                Height = height,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(32, 32, 32),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0),
                FlatAppearance = { BorderSize = 0 }
            };
        }

        // ===== TOGGLE MENU =====
        private void BtnToggle_Click(object sender, EventArgs e)
        {
            if (!isCollapsed)
            {
                pnlMenu.Width = 60;

                foreach (Button btn in pnlMenu.Controls.OfType<Button>())
                {
                    if (btn != btnToggle)
                        btn.Text = "";
                }
            }
            else
            {
                pnlMenu.Width = 220;

                btnBanVe.Text = "🎟️  Bán vé";
                btnKhachHang.Text = "👤  Khách hàng";
                btnPhim.Text = "🎬  Phim";
                btnPhong.Text = "🏢  Phòng";
                btnSuatChieu.Text = "⏰  Suất chiếu";
            }

            isCollapsed = !isCollapsed;
        }

        // ===== LOAD FORM CON =====
        private Form currentForm;

        private void OpenChildForm(Form childForm)
        {
            // đóng form cũ nếu có
            if (currentForm != null)
                currentForm.Close();

            currentForm = childForm;

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(childForm);
            pnlMain.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();
        }
    }
}

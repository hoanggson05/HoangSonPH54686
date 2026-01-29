using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLiVeXemPhimTaiQuay.UI
{
    public partial class FormMenu : Form
    {
        Panel panelMenu, panelTop, panelMain;
        Button btnToggle, btnKhachHang, btnPhim, btnPhong, btnSuatChieu, btnBanVe;

        bool isCollapsed = false;

        public FormMenu()
        {

            InitUI();
        }

        void InitUI()
        {
            // ===== FORM =====
            this.Text = "QUẢN LÝ BÁN VÉ RẠP PHIM";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.White;

            // ===== MENU TRÁI =====
            panelMenu = new Panel()
            {
                Width = 220,
                Dock = DockStyle.Left,
                BackColor = Color.FromArgb(30, 30, 30)
            };

            // ===== NÚT TOGGLE =====
            btnToggle = new Button()
            {
                Text = "☰",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Height = 55,
                Dock = DockStyle.Top
            };
            btnToggle.FlatAppearance.BorderSize = 0;
            btnToggle.Click += ToggleMenu;

            // ===== NÚT MENU =====
            btnBanVe = CreateMenuButton("🎟️  Bán vé");
            btnKhachHang = CreateMenuButton("👤  Khách hàng");
            btnPhim = CreateMenuButton("🎬  Phim");
            btnPhong = CreateMenuButton("🏢  Phòng");
            btnSuatChieu = CreateMenuButton("⏰  Suất chiếu");

            // ===== CLICK MENU =====
            btnBanVe.Click += (s, e) => LoadForm(new FormBanVe());
            btnKhachHang.Click += (s, e) => LoadForm(new FormKhachHang());
            btnPhim.Click += (s, e) => LoadForm(new FormQuanLyPhim());
            btnPhong.Click += (s, e) => LoadForm(new FormPhongChieu());
            btnSuatChieu.Click += (s, e) => LoadForm(new FormSuatChieu());

            // ⚠️ QUAN TRỌNG: ADD THEO ĐÚNG THỨ TỰ (Dock.Top)
            panelMenu.Controls.Add(btnSuatChieu);
            panelMenu.Controls.Add(btnPhong);
            panelMenu.Controls.Add(btnPhim);
            panelMenu.Controls.Add(btnKhachHang);
            panelMenu.Controls.Add(btnBanVe);
            panelMenu.Controls.Add(btnToggle);

            // ===== TOP BAR =====
            panelTop = new Panel()
            {
                Height = 60,
                Dock = DockStyle.Top,
                BackColor = Color.White
            };

            Label lblTitle = new Label()
            {
                Text = "Chào mừng đến hệ thống bán vé 🎬",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Left = 20,
                Top = 18
            };
            panelTop.Controls.Add(lblTitle);

            // ===== PANEL MAIN =====
            panelMain = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            // ===== ADD VÀO FORM =====
            this.Controls.Add(panelMenu);
            this.Controls.Add(panelTop);
            this.Controls.Add(panelMain);



        }

        // ===== TẠO NÚT MENU =====
        Button CreateMenuButton(string text)
        {
            Button btn = new Button()
            {
                Text = text,
                Dock = DockStyle.Top,
                Height = 50,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 50, 50);
            return btn;
        }

        // ===== LOAD FORM CON VÀO PANEL PHẢI =====
        void LoadForm(Form frm)
        {
            panelMain.Controls.Clear();

            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            panelMain.Controls.Add(frm);
            frm.Show();
        }

        // ===== THÒ / THỤT MENU =====
        void ToggleMenu(object sender, EventArgs e)
        {
            if (!isCollapsed)
            {
                panelMenu.Width = 60;

                foreach (Control c in panelMenu.Controls)
                {
                    if (c is Button btn && btn != btnToggle)
                        btn.Text = "";
                }
            }
            else
            {
                panelMenu.Width = 220;

                btnBanVe.Text = "🎟️  Bán vé";
                btnKhachHang.Text = "👤  Khách hàng";
                btnPhim.Text = "🎬  Phim";
                btnPhong.Text = "🏢  Phòng";
                btnSuatChieu.Text = "⏰  Suất chiếu";
            }

            isCollapsed = !isCollapsed;
        }
    }
}

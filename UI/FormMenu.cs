using System;
using System.Windows.Forms;

namespace QuanLiVeTaiQuay.UI
{
    public partial class FormMenu : Form
    {
        MenuStrip menu;

        ToolStripMenuItem mnuHeThong, mnuThoat;
        ToolStripMenuItem mnuQuanLy, mnuPhim, mnuPhong, mnuSuatChieu, mnuKhachHang, mnuNhanVien;
        ToolStripMenuItem mnuNghiepVu, mnuHoaDon;

        public FormMenu()
        {
            InitUI();
        }

        void InitUI()
        {
            // ===== FORM =====
            this.Text = "QUẢN LÝ BÁN VÉ RẠP PHIM";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Width = 1000;
            this.Height = 600;

            // ===== MENU STRIP =====
            menu = new MenuStrip();

            // ===== HỆ THỐNG =====
            mnuHeThong = new ToolStripMenuItem("Hệ thống");
            mnuThoat = new ToolStripMenuItem("Thoát");

            mnuThoat.Click += (s, e) =>
            {
                Application.Exit();
            };

            mnuHeThong.DropDownItems.Add(mnuThoat);

            // ===== QUẢN LÝ =====
            mnuQuanLy = new ToolStripMenuItem("Quản lý");

            mnuPhim = new ToolStripMenuItem("Phim");
            mnuPhong = new ToolStripMenuItem("Phòng");
            mnuSuatChieu = new ToolStripMenuItem("Suất chiếu");
            mnuKhachHang = new ToolStripMenuItem("Khách hàng");
            mnuNhanVien = new ToolStripMenuItem("Nhân viên");

            mnuPhim.Click += (s, e) => new FormQuanLyPhim().ShowDialog();
            mnuPhong.Click += (s, e) => new FormPhongChieu().ShowDialog();
            mnuSuatChieu.Click += (s, e) => new FormSuatChieu().ShowDialog();
            mnuKhachHang.Click += (s, e) => new FormKhachHang().ShowDialog();
            mnuNhanVien.Click += (s, e) => new FormNhanVien().ShowDialog();

            mnuQuanLy.DropDownItems.AddRange(new ToolStripItem[]
            {
                mnuPhim,
                mnuPhong,
                mnuSuatChieu,
                mnuKhachHang,
                mnuNhanVien
            });

            // ===== NGHIỆP VỤ =====
            mnuNghiepVu = new ToolStripMenuItem("Nghiệp vụ");
            mnuHoaDon = new ToolStripMenuItem("Hóa đơn");

            mnuHoaDon.Click += (s, e) => new FormHoaDon().ShowDialog();

            mnuNghiepVu.DropDownItems.Add(mnuHoaDon);

            // ===== ADD MENU =====
            menu.Items.AddRange(new ToolStripItem[]
            {
                mnuHeThong,
                mnuQuanLy,
                mnuNghiepVu
            });

            this.MainMenuStrip = menu;
            this.Controls.Add(menu);
        }
    }
}

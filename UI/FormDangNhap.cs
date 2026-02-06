using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLiVeTaiQuay.UI
{
    public partial class FormDangNhap : Form
    {
        private string strConn = @"Data Source=.;Initial Catalog=QuanLyBanVeRapPhim;Integrated Security=True;TrustServerCertificate=True";
        private TextBox txtUser, txtPass;

        // Biến tĩnh để lưu thông tin người dùng sau khi đăng nhập thành công
        public static string Role = "";
        public static string FullName = "";

        public FormDangNhap()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Size = new Size(400, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None; // Xóa viền giống mẫu
            this.BackColor = Color.FromArgb(245, 247, 250);

            // Logo giả lập (Viên kim cương - f5e3fc)
            Label lblLogo = new Label
            {
                Text = "💎",
                Font = new Font("Segoe UI", 50),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 150
            };
            Label lblBrand = new Label
            {
                Text = "RJCODE LOGIN",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 40,
                ForeColor = Color.FromArgb(70, 70, 70)
            };

            // Ô nhập User
            txtUser = new TextBox { Width = 280, Font = new Font("Segoe UI", 12), BorderStyle = BorderStyle.None };
            Panel line1 = new Panel { Height = 2, Width = 280, BackColor = Color.BlueViolet };

            // Ô nhập Pass
            txtPass = new TextBox { Width = 280, Font = new Font("Segoe UI", 12), BorderStyle = BorderStyle.None, UseSystemPasswordChar = true };
            Panel line2 = new Panel { Height = 2, Width = 280, BackColor = Color.BlueViolet };

            // Nút đăng nhập
            Button btnLogin = new Button
            {
                Text = "Log In",
                Size = new Size(280, 45),
                BackColor = Color.BlueViolet,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click;

            // Xếp vị trí
            int startY = 200;
            txtUser.Location = new Point(60, startY); line1.Location = new Point(60, startY + 25);
            txtPass.Location = new Point(60, startY + 60); line2.Location = new Point(60, startY + 85);
            btnLogin.Location = new Point(60, startY + 120);

            this.Controls.AddRange(new Control[] { lblLogo, lblBrand, txtUser, line1, txtPass, line2, btnLogin });
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                string query = "SELECT TenNV, VaiTro FROM NhanVien WHERE TenDangNhap=@u AND MatKhau=@p";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@u", txtUser.Text);
                cmd.Parameters.AddWithValue("@p", txtPass.Text);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    FullName = dr["TenNV"].ToString();
                    Role = dr["VaiTro"].ToString();

                    MessageBox.Show($"Chào mừng {FullName}!");
                    this.Hide();

                    // KIỂM TRA QUYỀN ADMIN
                    if (Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        // Nếu là Admin -> Hiện Form trung gian để chọn
                        FormDashboard fDashboard = new FormDashboard();
                        fDashboard.ShowDialog();
                    }
                    else
                    {
                        // Nếu là Nhân viên -> Vào thẳng FormMain với quyền Nhân viên
                        FormMain fMain = new FormMain("NhanVien");
                        fMain.ShowDialog();
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
                }
            }
        }
    }
}
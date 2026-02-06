using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLiVeTaiQuay.UI
{
    public partial class FormDashboard : Form
    {
        public FormDashboard()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Size = new Size(400, 300);
            this.Text = "Chọn chế độ làm việc";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            Label lblWelcome = new Label
            {
                Text = "Chào mừng Admin!",
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                BackColor = Color.DeepSkyBlue,
                ForeColor = Color.Yellow
            };

            Button btnAdmin = new Button
            {
                Text = "Quản Lý",
                Size = new Size(200, 60),
                Location = new Point(100, 90),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnAdmin.Click += (s, e) => OpenMain("Admin");

            Button btnStaff = new Button
            {
                Text = "Nhân Viên",
                Size = new Size(200, 60),
                Location = new Point(100, 170),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnStaff.Click += (s, e) => OpenMain("NhanVien");

            this.Controls.AddRange(new Control[] { lblWelcome, btnAdmin, btnStaff });
        }

        private void OpenMain(string role)
        {
            this.Hide();
            FormMain fMain = new FormMain(role); // Truyền quyền vào FormMain
            fMain.ShowDialog();
            this.Close();
        }
    }
}
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLiVeXemPhimTaiQuay.UI
{
    public partial class FormBanVe : Form
    {
        // ===== PANELS =====
        Panel panelLeft, panelCenter, panelRight;

        // ===== CONTROLS =====
        ComboBox cboPhim, cboSuat;
        FlowLayoutPanel flpGhe;
        Label lblTongTien;
        Button btnThanhToan;

        string connStr = @"Server=.;Database=QuanLyBanVeRapPhim;Trusted_Connection=True;";
        decimal tongTien = 0;

        public FormBanVe()
        {
            InitUI();
            LoadPhim();
        }

        // ================= UI =================
        void InitUI()
        {
            this.Text = "BÁN VÉ TẠI QUẦY";
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            // ===== LEFT =====
            panelLeft = new Panel()
            {
                Dock = DockStyle.Left,
                Width = 300,
                BackColor = Color.FromArgb(245, 245, 245)
            };

            Label lblPhim = new Label()
            {
                Text = "🎬 Phim",
                Dock = DockStyle.Top,
                Height = 30
            };

            cboPhim = new ComboBox()
            {
                Dock = DockStyle.Top,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboPhim.SelectedIndexChanged += CboPhim_SelectedIndexChanged;

            Label lblSuat = new Label()
            {
                Text = "⏰ Suất chiếu",
                Dock = DockStyle.Top,
                Height = 30
            };

            cboSuat = new ComboBox()
            {
                Dock = DockStyle.Top,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboSuat.SelectedIndexChanged += CboSuat_SelectedIndexChanged;

            panelLeft.Controls.Add(cboSuat);
            panelLeft.Controls.Add(lblSuat);
            panelLeft.Controls.Add(cboPhim);
            panelLeft.Controls.Add(lblPhim);

            // ===== CENTER =====
            panelCenter = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            Label lblGhe = new Label()
            {
                Text = "💺 Chọn ghế",
                Dock = DockStyle.Top,
                Height = 30
            };

            flpGhe = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };

            panelCenter.Controls.Add(flpGhe);
            panelCenter.Controls.Add(lblGhe);

            // ===== RIGHT =====
            panelRight = new Panel()
            {
                Dock = DockStyle.Right,
                Width = 300,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            lblTongTien = new Label()
            {
                Text = "Tổng tiền: 0 đ",
                Dock = DockStyle.Top,
                Height = 60,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            btnThanhToan = new Button()
            {
                Text = "💳 THANH TOÁN",
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.Green,
                ForeColor = Color.White
            };

            panelRight.Controls.Add(btnThanhToan);
            panelRight.Controls.Add(lblTongTien);

            // ===== ADD FORM =====
            this.Controls.Add(panelCenter);
            this.Controls.Add(panelRight);
            this.Controls.Add(panelLeft);
        }

        // ================= DATA =================
        void LoadPhim()
        {
             SqlConnection conn = new SqlConnection(connStr);
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT MaPhim, TenPhim FROM Phim WHERE TrangThai = 1", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cboPhim.DataSource = dt;
            cboPhim.DisplayMember = "TenPhim";
            cboPhim.ValueMember = "MaPhim";
        }

        void CboPhim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPhim.SelectedValue == null) return;

             SqlConnection conn = new SqlConnection(connStr);
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT MaSuat, GioBatDau, GiaVe FROM SuatChieu WHERE MaPhim=@ma",
                conn);
            da.SelectCommand.Parameters.AddWithValue("@ma", cboPhim.SelectedValue);

            DataTable dt = new DataTable();
            da.Fill(dt);

            cboSuat.DataSource = dt;
            cboSuat.DisplayMember = "GioBatDau";
            cboSuat.ValueMember = "MaSuat";
        }

        void CboSuat_SelectedIndexChanged(object sender, EventArgs e)
        {
            flpGhe.Controls.Clear();
            tongTien = 0;
            UpdateTongTien();

             SqlConnection conn = new SqlConnection(connStr);
            SqlDataAdapter da = new SqlDataAdapter(
                @"SELECT g.MaGhe, g.TenGhe 
                  FROM GheNgoi g 
                  JOIN SuatChieu s ON g.MaPhong = s.MaPhong
                  WHERE s.MaSuat = @ms",
                conn);
            da.SelectCommand.Parameters.AddWithValue("@ms", cboSuat.SelectedValue);

            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                Button btn = new Button()
                {
                    Text = row["TenGhe"].ToString(),
                    Width = 60,
                    Height = 40,
                    BackColor = Color.LightGray,
                    Tag = row["MaGhe"]
                };

                btn.Click += ChonGhe;
                flpGhe.Controls.Add(btn);
            }
        }

        void ChonGhe(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.BackColor == Color.LightGray)
            {
                btn.BackColor = Color.Green;
                tongTien += 80000;
            }
            else
            {
                btn.BackColor = Color.LightGray;
                tongTien -= 80000;
            }

            UpdateTongTien();
        }

        void UpdateTongTien()
        {
            lblTongTien.Text = $"Tổng tiền: {tongTien:N0} đ";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace QuanLiVeTaiQuay.UI
{
    public partial class FormQuanLyPhim : Form
    {
        private string strConn = @"Data Source=.;Initial Catalog=QuanLyBanVeRapPhim;Integrated Security=True;TrustServerCertificate=True";

        private TextBox txtMaPhim, txtTenPhim, txtMoTa, txtThoiLuong, txtQuocGia, txtDaoDien, txtGioiHanTuoi, txtNamSX;
        private DateTimePicker dtpNgayKC, dtpNgayKT;
        private CheckedListBox clbTheLoai;
        private DataGridView dgvPhim;
        private Button btnThem, btnXoa, btnSua;

        public FormQuanLyPhim()
        {
            this.Text = "Quản Lý Phim";
            this.Size = new Size(1150, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 245, 245);
            InitializeComponentCustom();
            LoadDataPhim();
        }

        private void InitializeComponentCustom()
        {
            Panel pnlInput = new Panel { Dock = DockStyle.Top, Height = 320, BorderStyle = BorderStyle.FixedSingle };

            // Cột 1
            AddLabelAndControl(pnlInput, "Mã phim:", txtMaPhim = new TextBox { Width = 200, ReadOnly = true }, 20, 20);
            AddLabelAndControl(pnlInput, "Tên phim:", txtTenPhim = new TextBox { Width = 200 }, 20, 55);
            AddLabelAndControl(pnlInput, "Mô tả:", txtMoTa = new TextBox { Width = 200 }, 20, 90);

            Label lblTL = new Label { Text = "Thể loại:", Location = new Point(20, 125), AutoSize = true };
            clbTheLoai = new CheckedListBox { Location = new Point(120, 125), Size = new Size(150, 130), CheckOnClick = true };
            clbTheLoai.Items.AddRange(new string[] { "Hành Động", "Gợi cảm", "Hoạt Hình", "Hài", "Viễn Tưởng", "Phiêu lưu", "Gia đình", "Tình Cảm", "Tâm Lý", "Kinh Dị" });
            pnlInput.Controls.AddRange(new Control[] { lblTL, clbTheLoai });

            // Cột 2
            AddLabelAndControl(pnlInput, "Thời lượng:", txtThoiLuong = new TextBox { Width = 200 }, 450, 20);
            AddLabelAndControl(pnlInput, "Ngày KC:", dtpNgayKC = new DateTimePicker { Width = 200, Format = DateTimePickerFormat.Short }, 450, 55);
            AddLabelAndControl(pnlInput, "Ngày KT:", dtpNgayKT = new DateTimePicker { Width = 200, Format = DateTimePickerFormat.Short }, 450, 90);
            AddLabelAndControl(pnlInput, "Quốc Gia:", txtQuocGia = new TextBox { Width = 200 }, 450, 125);
            AddLabelAndControl(pnlInput, "Đạo Diễn:", txtDaoDien = new TextBox { Width = 200 }, 450, 160);
            AddLabelAndControl(pnlInput, "Giới Hạn Tuổi:", txtGioiHanTuoi = new TextBox { Width = 200 }, 450, 195);
            AddLabelAndControl(pnlInput, "Năm SX:", txtNamSX = new TextBox { Width = 200 }, 450, 230);

            btnThem = new Button { Text = "Thêm", Location = new Point(20, 275), Size = new Size(100, 35) };
            btnXoa = new Button { Text = "Xóa", Location = new Point(130, 275), Size = new Size(100, 35) };
            btnSua = new Button { Text = "Sửa", Location = new Point(240, 275), Size = new Size(100, 35) };

            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;

            pnlInput.Controls.AddRange(new Control[] { btnThem, btnXoa, btnSua });
            this.Controls.Add(pnlInput);

            dgvPhim = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true
            };
            dgvPhim.CellClick += DgvPhim_CellClick;
            this.Controls.Add(dgvPhim);
            dgvPhim.BringToFront();
        }

        private void AddLabelAndControl(Control parent, string lblText, Control ctrl, int x, int y)
        {
            Label lbl = new Label { Text = lblText, Location = new Point(x, y), AutoSize = true };
            ctrl.Location = new Point(x + 120, y - 3);
            parent.Controls.Add(lbl);
            parent.Controls.Add(ctrl);
        }

        private void LoadDataPhim()
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    string sql = "SELECT MaPhim, TenPhim, MoTa, TheLoai, ThoiLuong, NgayKhoiChieu, NgayKetThuc, QuocGia, DaoDien, DoTuoiQuyDinh as GiớiHạnTuổi, NamSX FROM Phim";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvPhim.DataSource = dt;
                }
                catch (Exception ex) { MessageBox.Show("Lỗi SQL: " + ex.Message); }
            }
        }

        // KHẮC PHỤC LỖI DBNULL TẠI ĐÂY
        private void DgvPhim_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPhim.Rows[e.RowIndex];

                txtMaPhim.Text = row.Cells["MaPhim"].Value?.ToString();
                txtTenPhim.Text = row.Cells["TenPhim"].Value?.ToString();
                txtMoTa.Text = row.Cells["MoTa"].Value?.ToString();
                txtThoiLuong.Text = row.Cells["ThoiLuong"].Value?.ToString();
                txtQuocGia.Text = row.Cells["QuocGia"].Value?.ToString();
                txtDaoDien.Text = row.Cells["DaoDien"].Value?.ToString();
                txtGioiHanTuoi.Text = row.Cells["GiớiHạnTuổi"].Value?.ToString();
                txtNamSX.Text = row.Cells["NamSX"].Value?.ToString();

                // Kiểm tra NULL cho DateTime
                if (row.Cells["NgayKhoiChieu"].Value != DBNull.Value && row.Cells["NgayKhoiChieu"].Value != null)
                    dtpNgayKC.Value = Convert.ToDateTime(row.Cells["NgayKhoiChieu"].Value);

                if (row.Cells["NgayKetThuc"].Value != DBNull.Value && row.Cells["NgayKetThuc"].Value != null)
                    dtpNgayKT.Value = Convert.ToDateTime(row.Cells["NgayKetThuc"].Value);

                // Xử lý CheckedListBox
                for (int i = 0; i < clbTheLoai.Items.Count; i++) clbTheLoai.SetItemChecked(i, false);
                string valTheLoai = row.Cells["TheLoai"].Value?.ToString();
                if (!string.IsNullOrEmpty(valTheLoai))
                {
                    string[] categories = valTheLoai.Split(new[] { ", " }, StringSplitOptions.None);
                    foreach (var cat in categories)
                    {
                        int index = clbTheLoai.Items.IndexOf(cat.Trim());
                        if (index >= 0) clbTheLoai.SetItemChecked(index, true);
                    }
                }
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            ExecuteQuery("INSERT");
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            ExecuteQuery("UPDATE");
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaPhim.Text)) return;
            if (MessageBox.Show("Xác nhận xóa?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Phim WHERE MaPhim=@ma", conn);
                    cmd.Parameters.AddWithValue("@ma", txtMaPhim.Text);
                    cmd.ExecuteNonQuery();
                    LoadDataPhim();
                }
            }
        }

        private void ExecuteQuery(string type)
        {
            List<string> selected = new List<string>();
            foreach (var item in clbTheLoai.CheckedItems) selected.Add(item.ToString());
            string theLoaiStr = string.Join(", ", selected);

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string sql = "";
                    if (type == "INSERT")
                        sql = @"INSERT INTO Phim (TenPhim, MoTa, TheLoai, ThoiLuong, NgayKhoiChieu, NgayKetThuc, QuocGia, DaoDien, DoTuoiQuyDinh, NamSX) 
                                VALUES (@ten, @mota, @theloai, @thoiluong, @ngaykc, @ngaykt, @quocgia, @daodien, @tuoi, @namsx)";
                    else
                        sql = @"UPDATE Phim SET TenPhim=@ten, MoTa=@mota, TheLoai=@theloai, ThoiLuong=@thoiluong, 
                                NgayKhoiChieu=@ngaykc, NgayKetThuc=@ngaykt, QuocGia=@quocgia, DaoDien=@daodien, 
                                DoTuoiQuyDinh=@tuoi, NamSX=@namsx WHERE MaPhim=@ma";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    if (type == "UPDATE") cmd.Parameters.AddWithValue("@ma", txtMaPhim.Text);

                    cmd.Parameters.AddWithValue("@ten", txtTenPhim.Text);
                    cmd.Parameters.AddWithValue("@mota", txtMoTa.Text);
                    cmd.Parameters.AddWithValue("@theloai", theLoaiStr);
                    cmd.Parameters.AddWithValue("@thoiluong", decimal.TryParse(txtThoiLuong.Text, out decimal tl) ? tl : 0);
                    cmd.Parameters.AddWithValue("@ngaykc", dtpNgayKC.Value);
                    cmd.Parameters.AddWithValue("@ngaykt", dtpNgayKT.Value);
                    cmd.Parameters.AddWithValue("@quocgia", txtQuocGia.Text);
                    cmd.Parameters.AddWithValue("@daodien", txtDaoDien.Text);
                    cmd.Parameters.AddWithValue("@tuoi", int.TryParse(txtGioiHanTuoi.Text, out int t) ? t : 0);
                    cmd.Parameters.AddWithValue("@namsx", int.TryParse(txtNamSX.Text, out int n) ? n : 0);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show(type + " thành công!");
                    LoadDataPhim();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }
    }
}
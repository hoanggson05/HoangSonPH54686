using Microsoft.Data.SqlClient;
using System.Data;

namespace QuanLiVeTaiQuay.UI
{
    public partial class FormBanVe : Form
    {
        // Chuỗi kết nối Database
        private string strConn = @"Data Source=.;Initial Catalog=QuanLyBanVeRapPhim;Integrated Security=True;TrustServerCertificate=True";

        // Controls
        private TabControl tabControlMain;
        private TabPage tabList, tabEdit;
        private DataGridView dgvVe;
        private TextBox txtSearch, txtMaVe, txtTenPhim, txtGiaBan;
        private PictureBox picPhim;
        private DataGridView dgvXemVe;

        public FormBanVe()
        {
            InitializeUI();
            LoadData();
        }

        private void InitializeUI()
        {
            this.BackColor = Color.White;

            // ===== TAB CONTROL (Ẩn tiêu đề) =====
            tabControlMain = new TabControl { Dock = DockStyle.Fill, Appearance = TabAppearance.FlatButtons, ItemSize = new Size(0, 1) };
            tabList = new TabPage { BackColor = Color.White };
            tabEdit = new TabPage { BackColor = Color.White };
            tabControlMain.TabPages.AddRange(new[] { tabList, tabEdit });
            this.Controls.Add(tabControlMain);

            // --- GIAO DIỆN TRANG DANH SÁCH (Ảnh 1) ---
            Label lblTitle = new Label { Text = "🎟 Tickets", Font = new Font("Segoe UI", 18, FontStyle.Bold), Location = new Point(20, 10), AutoSize = true };
            Label lblSearch = new Label { Text = "Search ticket:", Location = new Point(20, 55), AutoSize = true };
            txtSearch = new TextBox { Location = new Point(20, 75), Width = 400 };
            Button btnSearch = new Button { Text = "Search", Location = new Point(430, 73), BackColor = Color.Orange, FlatStyle = FlatStyle.Flat };
            btnSearch.Click += (s, e) => LoadData(txtSearch.Text);

            dgvVe = new DataGridView
            {
                Location = new Point(20, 110),
                Size = new Size(650, 400),
                BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true
            };

            Button btnEdit = new Button { Text = "Edit", Location = new Point(680, 150), Size = new Size(80, 30), BackColor = Color.CornflowerBlue, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnEdit.Click += BtnEdit_Click;

            tabList.Controls.AddRange(new Control[] { lblTitle, lblSearch, txtSearch, btnSearch, dgvVe, btnEdit });

            // --- GIAO DIỆN TRANG CHỈNH SỬA (Ảnh 2) ---
            Label lblEditTitle = new Label { Text = "🐾 Edit Ticket", Font = new Font("Segoe UI", 18, FontStyle.Bold), Location = new Point(20, 10), AutoSize = true };
            picPhim = new PictureBox { Location = new Point(30, 70), Size = new Size(150, 180), BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.StretchImage };

            // Các trường nhập liệu
            txtMaVe = CreateInput(tabEdit, "Ticket ID:", 240, 70, true);
            txtTenPhim = CreateInput(tabEdit, "Movie Name:", 240, 130, true);
            txtGiaBan = CreateInput(tabEdit, "Price:", 240, 190, false);

            Button btnSave = new Button { Text = "Save", Location = new Point(240, 260), Size = new Size(100, 35), BackColor = Color.MediumOrchid, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSave.Click += BtnSave_Click;

            Button btnCancel = new Button { Text = "Cancel", Location = new Point(350, 260), Size = new Size(100, 35), BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnCancel.Click += (s, e) => tabControlMain.SelectedTab = tabList;

            tabEdit.Controls.AddRange(new Control[] { lblEditTitle, picPhim, btnSave, btnCancel });
        }

        private TextBox CreateInput(TabPage page, string labelText, int x, int y, bool readOnly)
        {
            Label lbl = new Label { Text = labelText, Location = new Point(x, y), AutoSize = true };
            TextBox txt = new TextBox { Location = new Point(x, y + 20), Width = 200, ReadOnly = readOnly };
            page.Controls.Add(lbl);
            page.Controls.Add(txt);
            return txt;
        }

        // ===== XỬ LÝ DỮ LIỆU =====

        private void LoadData(string search = "")
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string sql = @"SELECT V.MaVe, P.TenPhim, G.TenGhe, V.GiaBan 
                             FROM Ve V 
                             JOIN SuatChieu S ON V.MaSuat = S.MaSuat 
                             JOIN Phim P ON S.MaPhim = P.MaPhim
                             JOIN GheNgoi G ON V.MaGhe = G.MaGhe";
                if (!string.IsNullOrEmpty(search))
                    sql += " WHERE P.TenPhim LIKE @s OR V.MaVe LIKE @s";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@s", "%" + search + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvVe.DataSource = dt;
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvVe.CurrentRow != null)
            {
                txtMaVe.Text = dgvVe.CurrentRow.Cells["MaVe"].Value.ToString();
                txtTenPhim.Text = dgvVe.CurrentRow.Cells["TenPhim"].Value.ToString();
                txtGiaBan.Text = dgvVe.CurrentRow.Cells["GiaBan"].Value.ToString();
                // Load ảnh mặc định hoặc theo phim nếu có path trong DB
                tabControlMain.SelectedTab = tabEdit;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                string sql = "UPDATE Ve SET GiaBan = @gia WHERE MaVe = @ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@gia", decimal.Parse(txtGiaBan.Text));
                cmd.Parameters.AddWithValue("@ma", txtMaVe.Text);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Cập nhật thành công!");
                    tabControlMain.SelectedTab = tabList;
                    LoadData();
                }
            }
        }
    }
}
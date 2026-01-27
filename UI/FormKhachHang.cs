using QuanLiVeTaiQuay.DAO;
using QuanLyBanVeRapPhim.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiVeTaiQuay.UI
{
    public partial class FormKhachHang : Form
    {
        TextBox txtMaKh, txtTenKh, txtSDT;
        NumericUpDown numThoiLuong;
        Button btnThem, btnSua, btnXoa, btnLuu;
        DataGridView dgvKhachHang;
        bool isEdit = false;
        void ClearForm()
        {
            txtMaKh.Clear();
            txtTenKh.Clear();
            txtSDT.Clear();

            txtTenKh.ReadOnly = false;
            txtSDT.ReadOnly = false;

            txtTenKh.Focus();
        }

        public FormKhachHang()
        {
            FKhachHang();
            LoadData();
        }
        void FKhachHang()
        {
            this.Text = "Quản lý khách hàng";
            this.Width = 800;
            this.Height = 500;
            Label lblMa = new Label() { Text = "Mã phim", Left = 20, Top = 20 };
            txtMaKh = new TextBox() { Left = 120, Top = 18, Width = 200 };

            Label lblTen = new Label() { Text = "Tên phim", Left = 20, Top = 50 };
            txtTenKh = new TextBox() { Left = 120, Top = 48, Width = 200 };

            Label lblSdt = new Label() { Text = "Thể loại", Left = 20, Top = 80 };
            txtSDT = new TextBox() { Left = 120, Top = 78, Width = 200 };

            Label lblThoiLuong = new Label() { Text = "Thời lượng", Left = 20, Top = 110 };
            numThoiLuong = new NumericUpDown()
            {
                Left = 120,
                Top = 108,
                Width = 100,
                Minimum = 30,
                Maximum = 300
            };
            void BtnThem_Click(object sender, EventArgs e)
            {
                KhachHangBLL.Them(txtTenKh.Text, txtSDT.Text);
                MessageBox.Show("Thêm khách hàng thành công");
                LoadData();
            }

            void BtnSua_Click(object sender, EventArgs e)
            {

                if (!isEdit)
                {
                    isEdit = true;
                    btnSua.Text = "Xác nhận";
                    txtTenKh.ReadOnly = false;
                    txtSDT.ReadOnly = false;
                }
                else
                {
                    KhachHangBLL.Sua(
                        int.Parse(txtMaKh.Text),
                        txtMaKh.Text,
                        txtSDT.Text
                    );

                    isEdit = false;
                    btnSua.Text = "Sửa";
                    txtTenKh.ReadOnly = true;
                    txtSDT.ReadOnly = true;

                    LoadData();
                    ClearForm();
                }
            }

            void BtnXoa_Click(object sender, EventArgs e)
            {
                KhachHangBLL.Xoa(int.Parse(txtMaKh.Text));
                LoadData();
            }
            void Dgv_SelectionChanged(object sender, EventArgs e)
            {
                if (dgvKhachHang.CurrentRow == null) return;

                DataRowView row = dgvKhachHang.CurrentRow.DataBoundItem as DataRowView;
                if (row == null) return;

                txtMaKh.Text = row["MaKH"].ToString();
                txtTenKh.Text = row["HoTen"].ToString();
                txtSDT.Text = row["SDT"].ToString();
            }

            btnThem = new Button() { Text = "Thêm", Left = 350, Top = 20 };
            btnSua = new Button() { Text = "Sửa", Left = 350, Top = 50 };
            btnXoa = new Button() { Text = "Xóa", Left = 350, Top = 80 };

            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;

            dgvKhachHang = new DataGridView()
            {
                Left = 20,
                Top = 160,
                Width = 740,
                Height = 280,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvKhachHang.SelectionChanged += Dgv_SelectionChanged;

            this.Controls.AddRange(new Control[]
            {
                lblMa, txtMaKh,
                lblTen, txtTenKh,
                lblSdt, txtSDT,
                lblThoiLuong, numThoiLuong,
                btnThem, btnSua, btnXoa,
                dgvKhachHang
            });
        }
        void LoadData()
        {
            dgvKhachHang.DataSource = KhachHangBLL.GetAll();
        }
        private void FormKhachHang_Load(object sender, EventArgs e)
        {

        }
    }
}

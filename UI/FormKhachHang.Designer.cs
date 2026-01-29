namespace QuanLiVeXemPhimTaiQuay.UI
{
    partial class FormKhachHang
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.DataGridView dgvKhachHang;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.TextBox txtTenKH;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.Label lblSDT;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelContainer = new System.Windows.Forms.Panel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.txtTenKH = new System.Windows.Forms.TextBox();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.lblTen = new System.Windows.Forms.Label();
            this.lblSDT = new System.Windows.Forms.Label();
            this.dgvKhachHang = new System.Windows.Forms.DataGridView();

            this.panelContainer.SuspendLayout();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).BeginInit();
            this.SuspendLayout();

            // panelContainer
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Controls.Add(this.dgvKhachHang);
            this.panelContainer.Controls.Add(this.panelTop);

            // panelTop
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 110;
            this.panelTop.Controls.Add(this.lblTen);
            this.panelTop.Controls.Add(this.txtTenKH);
            this.panelTop.Controls.Add(this.lblSDT);
            this.panelTop.Controls.Add(this.txtSDT);
            this.panelTop.Controls.Add(this.btnThem);
            this.panelTop.Controls.Add(this.btnSua);
            this.panelTop.Controls.Add(this.btnXoa);

            // lblTen
            this.lblTen.Text = "Họ tên:";
            this.lblTen.Location = new System.Drawing.Point(20, 20);

            // txtTenKH
            this.txtTenKH.Location = new System.Drawing.Point(80, 18);
            this.txtTenKH.Width = 200;

            // lblSDT
            this.lblSDT.Text = "SĐT:";
            this.lblSDT.Location = new System.Drawing.Point(320, 20);

            // txtSDT
            this.txtSDT.Location = new System.Drawing.Point(370, 18);
            this.txtSDT.Width = 150;

            // btnThem
            this.btnThem.Text = "Thêm";
            this.btnThem.Location = new System.Drawing.Point(550, 15);

            // btnSua
            this.btnSua.Text = "Sửa";
            this.btnSua.Location = new System.Drawing.Point(630, 15);

            // btnXoa
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Location = new System.Drawing.Point(710, 15);

            // dgvKhachHang
            this.dgvKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKhachHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKhachHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            // FormKhachHang
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panelContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "FormKhachHang";

            this.panelContainer.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).EndInit();
            this.ResumeLayout(false);
        }
    }
}

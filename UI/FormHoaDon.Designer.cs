namespace QuanLiVeTaiQuay.UI
{
    partial class FormHoaDon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.txtTenPhim = new System.Windows.Forms.TextBox();
            this.lblTenPhim = new System.Windows.Forms.Label();
            this.txtMaPhim = new System.Windows.Forms.TextBox();
            this.lblMaPhim = new System.Windows.Forms.Label();
            this.dgvPhim = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhim)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(579, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(168, 22);
            this.textBox1.TabIndex = 63;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(481, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 62;
            this.label1.Text = "Ngày Lập";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(579, 81);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(168, 22);
            this.textBox3.TabIndex = 61;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(481, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 16);
            this.label3.TabIndex = 60;
            this.label3.Text = "Trang Thái";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(662, 156);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(126, 30);
            this.btnLamMoi.TabIndex = 59;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(477, 156);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(126, 30);
            this.btnXoa.TabIndex = 58;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(257, 156);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(126, 30);
            this.btnSua.TabIndex = 57;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(45, 156);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(126, 30);
            this.btnThem.TabIndex = 56;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            // 
            // txtTenPhim
            // 
            this.txtTenPhim.Location = new System.Drawing.Point(148, 81);
            this.txtTenPhim.Name = "txtTenPhim";
            this.txtTenPhim.Size = new System.Drawing.Size(168, 22);
            this.txtTenPhim.TabIndex = 53;
            // 
            // lblTenPhim
            // 
            this.lblTenPhim.AutoSize = true;
            this.lblTenPhim.Location = new System.Drawing.Point(50, 81);
            this.lblTenPhim.Name = "lblTenPhim";
            this.lblTenPhim.Size = new System.Drawing.Size(44, 16);
            this.lblTenPhim.TabIndex = 52;
            this.lblTenPhim.Text = "Khách";
            // 
            // txtMaPhim
            // 
            this.txtMaPhim.Location = new System.Drawing.Point(148, 27);
            this.txtMaPhim.Name = "txtMaPhim";
            this.txtMaPhim.Size = new System.Drawing.Size(168, 22);
            this.txtMaPhim.TabIndex = 51;
            // 
            // lblMaPhim
            // 
            this.lblMaPhim.AutoSize = true;
            this.lblMaPhim.Location = new System.Drawing.Point(50, 31);
            this.lblMaPhim.Name = "lblMaPhim";
            this.lblMaPhim.Size = new System.Drawing.Size(82, 16);
            this.lblMaPhim.TabIndex = 50;
            this.lblMaPhim.Text = "Mã Hóa Đơn";
            // 
            // dgvPhim
            // 
            this.dgvPhim.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhim.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhim.Location = new System.Drawing.Point(2, 209);
            this.dgvPhim.Name = "dgvPhim";
            this.dgvPhim.RowHeadersWidth = 51;
            this.dgvPhim.RowTemplate.Height = 24;
            this.dgvPhim.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect;
            this.dgvPhim.Size = new System.Drawing.Size(796, 214);
            this.dgvPhim.TabIndex = 49;
            // 
            // FormHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.txtTenPhim);
            this.Controls.Add(this.lblTenPhim);
            this.Controls.Add(this.txtMaPhim);
            this.Controls.Add(this.lblMaPhim);
            this.Controls.Add(this.dgvPhim);
            this.Name = "FormHoaDon";
            this.Text = "FormHoaDon";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhim)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.TextBox txtTenPhim;
        private System.Windows.Forms.Label lblTenPhim;
        private System.Windows.Forms.TextBox txtMaPhim;
        private System.Windows.Forms.Label lblMaPhim;
        private System.Windows.Forms.DataGridView dgvPhim;
    }
}
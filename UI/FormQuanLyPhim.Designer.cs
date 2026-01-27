namespace QuanLiVeTaiQuay.UI
{
    partial class FormQuanLyPhim
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
            this.dgvPhim = new System.Windows.Forms.DataGridView();
            this.lblMaPhim = new System.Windows.Forms.Label();
            this.txtMaPhim = new System.Windows.Forms.TextBox();
            this.txtTenPhim = new System.Windows.Forms.TextBox();
            this.lblTenPhim = new System.Windows.Forms.Label();
            this.txtTheLoai = new System.Windows.Forms.TextBox();
            this.lblTheLoai = new System.Windows.Forms.Label();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhim)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPhim
            // 
            this.dgvPhim.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhim.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhim.Location = new System.Drawing.Point(0, 177);
            this.dgvPhim.Name = "dgvPhim";
            this.dgvPhim.RowHeadersWidth = 51;
            this.dgvPhim.RowTemplate.Height = 24;
            this.dgvPhim.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect;
            this.dgvPhim.Size = new System.Drawing.Size(540, 276);
            this.dgvPhim.TabIndex = 0;
            this.dgvPhim.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // lblMaPhim
            // 
            this.lblMaPhim.AutoSize = true;
            this.lblMaPhim.Location = new System.Drawing.Point(9, 32);
            this.lblMaPhim.Name = "lblMaPhim";
            this.lblMaPhim.Size = new System.Drawing.Size(59, 16);
            this.lblMaPhim.TabIndex = 1;
            this.lblMaPhim.Text = "Mã Phim";
            // 
            // txtMaPhim
            // 
            this.txtMaPhim.Location = new System.Drawing.Point(74, 29);
            this.txtMaPhim.Name = "txtMaPhim";
            this.txtMaPhim.Size = new System.Drawing.Size(168, 22);
            this.txtMaPhim.TabIndex = 2;
            // 
            // txtTenPhim
            // 
            this.txtTenPhim.Location = new System.Drawing.Point(74, 82);
            this.txtTenPhim.Name = "txtTenPhim";
            this.txtTenPhim.Size = new System.Drawing.Size(168, 22);
            this.txtTenPhim.TabIndex = 4;
            // 
            // lblTenPhim
            // 
            this.lblTenPhim.AutoSize = true;
            this.lblTenPhim.Location = new System.Drawing.Point(9, 82);
            this.lblTenPhim.Name = "lblTenPhim";
            this.lblTenPhim.Size = new System.Drawing.Size(64, 16);
            this.lblTenPhim.TabIndex = 3;
            this.lblTenPhim.Text = "Tên Phim";
            // 
            // txtTheLoai
            // 
            this.txtTheLoai.Location = new System.Drawing.Point(74, 132);
            this.txtTheLoai.Name = "txtTheLoai";
            this.txtTheLoai.Size = new System.Drawing.Size(168, 22);
            this.txtTheLoai.TabIndex = 6;
            // 
            // lblTheLoai
            // 
            this.lblTheLoai.AutoSize = true;
            this.lblTheLoai.Location = new System.Drawing.Point(9, 132);
            this.lblTheLoai.Name = "lblTheLoai";
            this.lblTheLoai.Size = new System.Drawing.Size(60, 16);
            this.lblTheLoai.TabIndex = 5;
            this.lblTheLoai.Text = "Thể Loại";
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(609, 192);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(126, 30);
            this.btnThem.TabIndex = 7;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(609, 254);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(126, 30);
            this.btnSua.TabIndex = 8;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(609, 320);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(126, 30);
            this.btnXoa.TabIndex = 9;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(609, 393);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(126, 30);
            this.btnLamMoi.TabIndex = 10;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(538, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(168, 22);
            this.textBox1.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(383, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "Thời Lượng";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(538, 79);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(168, 22);
            this.textBox2.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(383, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "Độ Tuổi Quy Định";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(538, 132);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(168, 22);
            this.textBox3.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(383, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 16);
            this.label3.TabIndex = 15;
            this.label3.Text = "Trang Thái";
            // 
            // FormQuanLyPhim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.txtTheLoai);
            this.Controls.Add(this.lblTheLoai);
            this.Controls.Add(this.txtTenPhim);
            this.Controls.Add(this.lblTenPhim);
            this.Controls.Add(this.txtMaPhim);
            this.Controls.Add(this.lblMaPhim);
            this.Controls.Add(this.dgvPhim);
            this.Name = "FormQuanLyPhim";
            this.Text = "FormQuanLyPhim";
            this.Load += new System.EventHandler(this.FormQuanLyPhim_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhim)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPhim;
        private System.Windows.Forms.Label lblMaPhim;
        private System.Windows.Forms.TextBox txtMaPhim;
        private System.Windows.Forms.TextBox txtTenPhim;
        private System.Windows.Forms.Label lblTenPhim;
        private System.Windows.Forms.TextBox txtTheLoai;
        private System.Windows.Forms.Label lblTheLoai;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
    }
}
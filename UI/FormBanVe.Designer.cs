namespace QuanLiVeXemPhimTaiQuay.UI
{
    partial class FormBanVe
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.TableLayoutPanel infoLayout;
        private System.Windows.Forms.FlowLayoutPanel flpGhe;
        private System.Windows.Forms.DataGridView dgvVe;
        private System.Windows.Forms.FlowLayoutPanel actionLayout;

        private System.Windows.Forms.ComboBox cbPhim;
        private System.Windows.Forms.ComboBox cbSuat;
        private System.Windows.Forms.ComboBox cbKhachHang;
        private System.Windows.Forms.TextBox txtPhong;
        private System.Windows.Forms.TextBox txtGiaVe;

        private System.Windows.Forms.Button btnBanVe;
        private System.Windows.Forms.Button btnLamMoi;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormBanVe
            // 
            this.ClientSize = new System.Drawing.Size(898, 455);
            this.Name = "FormBanVe";
            this.ResumeLayout(false);

        }
    }
}

namespace QuanLiVeTaiQuay.UI
{
    partial class FormThongKe
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.cboPhim = new System.Windows.Forms.ComboBox();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.dgvThongKe = new System.Windows.Forms.DataGridView();
            this.chartDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblTongDoanhThu = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).BeginInit();
            this.SuspendLayout();

            // Thiết lập vị trí (Dựa trên ảnh yêu cầu của bạn)
            this.cboPhim.Location = new System.Drawing.Point(180, 20);
            this.cboPhim.Size = new System.Drawing.Size(250, 24);

            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(100, 60);

            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(320, 60);

            this.btnThongKe.Location = new System.Drawing.Point(450, 58);
            this.btnThongKe.Text = "Thống kê";
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);

            this.dgvThongKe.Location = new System.Drawing.Point(20, 110);
            this.dgvThongKe.Size = new System.Drawing.Size(460, 380);

            chartArea1.Name = "ChartArea1";
            this.chartDoanhThu.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartDoanhThu.Legends.Add(legend1);
            this.chartDoanhThu.Location = new System.Drawing.Point(500, 110);
            this.chartDoanhThu.Size = new System.Drawing.Size(400, 380);

            this.lblTongDoanhThu.AutoSize = true;
            this.lblTongDoanhThu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblTongDoanhThu.Location = new System.Drawing.Point(550, 510);
            this.lblTongDoanhThu.Text = "Tổng doanh thu: 0 VNĐ";

            // Add Controls
            this.Controls.Add(this.lblTongDoanhThu);
            this.Controls.Add(this.chartDoanhThu);
            this.Controls.Add(this.dgvThongKe);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.cboPhim);
            this.Controls.Add(this.dtpDenNgay);
            this.Controls.Add(this.dtpTuNgay);

            this.Size = new System.Drawing.Size(950, 600);
            this.Text = "Thống Kê Doanh Thu";
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).EndInit();
            this.ResumeLayout(false);
        }

        // Khai báo các biến (Phải trùng khớp hoàn toàn với file .cs)
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.ComboBox cboPhim;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.DataGridView dgvThongKe;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThu;
        private System.Windows.Forms.Label lblTongDoanhThu;
    }
}
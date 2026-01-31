using QuanLyBanVeRapPhim.BLL;

namespace QuanLiVeXemPhimTaiQuay.UI
{
    public partial class FormQuanLyPhim : Form
    {
        public FormQuanLyPhim()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormQuanLyPhim_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            dgvPhim.DataSource = PhimBLL.GetAll();
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiVeXemPhimTaiQuay.UI
{
    public partial class test : Form
    {
        public test()
        {
            InitializeForm();
        }
        void InitializeForm()
        {
            this.Text = "Quản lý khách hàng";
            this.Width = 800;
            this.Height = 500;

            Label lblMa = new Label() { Text = "Mã KH", Left = 20, Top = 20 };
            TextBox txtMa = new TextBox() { Left = 120, Top = 18 };

            this.Controls.Add(lblMa);
            this.Controls.Add(txtMa);
        }
    }
}

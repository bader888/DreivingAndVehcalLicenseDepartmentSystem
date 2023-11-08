using DVDL_Business;
using System;
using System.Windows.Forms;

namespace DVLD.Drivers
{
    public partial class frmManageDrivers : Form
    {
        public frmManageDrivers()
        {
            InitializeComponent();
        }

        private void frmManageDrivers_Load(object sender, EventArgs e)
        {
            dgvDrivers.DataSource = clsDrivers.GetAllDrivers();
        }
    }
}

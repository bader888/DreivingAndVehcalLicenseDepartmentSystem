using DVDL_Business;
using DVLD.Tests;
using System.Windows.Forms;

namespace DVLD.ManageApplications
{
    public partial class frmLocalDrivingLicenseApplications : Form
    {
        public frmLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void _ShowAllL_D_Lapps()
        {
            dataGridView1.DataSource = clsLocalDrivingLicenseApplications.GetAllLocalDrivingLicenseApplications();

        }

        private void frmLocalDrivingLicenseApplications_Load(object sender, System.EventArgs e)
        {
            _ShowAllL_D_Lapps();
        }

        private void AddnewL_D_Lapp_Click(object sender, System.EventArgs e)
        {
            frmNewLocalLicense frm = new frmNewLocalLicense();
            frm.DataBack += _ShowAllL_D_Lapps; // Subscribe to the event
            frm.ShowDialog();
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            int ApplicationID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            frmListTestAppointments frm = new frmListTestAppointments(ApplicationID);
            frm.ShowDialog();
        }
    }
}

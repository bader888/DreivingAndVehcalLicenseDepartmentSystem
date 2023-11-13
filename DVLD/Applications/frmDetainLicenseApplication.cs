using DVDL_Business;
using DVLD.License;
using DVLD.License.DetainReleaseLicenses;
using System;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class frmDetainLicenseApplication : Form
    {
        public frmDetainLicenseApplication()
        {
            InitializeComponent();
        }
        private void loadAllDetainLicenses()
        {
            dgvDetainedLicenses.DataSource = clsDetainedLicenses.GetAllDetainedLicenses();
            lblTotalRecords.Text = clsGlobal.RecordCount("DetainedLicenses").ToString();

        }
        private void frmDetainLicenseApplication_Load(object sender, EventArgs e)
        {
            loadAllDetainLicenses();
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string PersonName = dgvDetainedLicenses.SelectedCells[7].Value.ToString();
            int PersonID = clsPerson.GetPersonIDbyHisName(PersonName);
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = int.Parse(dgvDetainedLicenses.SelectedCells[1].Value.ToString());
            frmShowLicense frm = new frmShowLicense();
            frm.ShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void cmsApplications_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //check the license is release ot not 
            int LicenseID = int.Parse(dgvDetainedLicenses.SelectedCells[1].Value.ToString());
            if (clsDetainedLicenses.IsLicenseDetain(LicenseID))
                releaseDetainedLicenseToolStripMenuItem.Enabled = true;
            else
                releaseDetainedLicenseToolStripMenuItem.Enabled = false;
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string PersonName = dgvDetainedLicenses.SelectedCells[7].Value.ToString();
            int PersonID = clsPerson.GetPersonIDbyHisName(PersonName);
            int DriverID = clsDrivers.GetDriverIDByHisName(PersonName);
            frmPersonLicenseshistory frm = new frmPersonLicenseshistory(DriverID, PersonID);
            frm.ShowDialog();

        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = int.Parse(dgvDetainedLicenses.SelectedCells[1].Value.ToString());
            frmReleaseDetainLicense frm = new frmReleaseDetainLicense(LicenseID);
            frm.ShowDialog();
            loadAllDetainLicenses();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
            loadAllDetainLicenses();
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainLicense frm = new frmReleaseDetainLicense();
            frm.ShowDialog();
            loadAllDetainLicenses();
        }
    }
}

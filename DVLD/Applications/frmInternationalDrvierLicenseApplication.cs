using DVDL_Business;
using DVLD.License;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class frmInternationalDrvierLicenseApplication : Form
    {
        public frmInternationalDrvierLicenseApplication()
        {
            InitializeComponent();
        }


        private void _LoadAllInternationalLicenses()
        {
            dgvInternationalLicenses.DataSource = clsInternationalLicenses.GetAllInternationalLicenses();

        }
        private void frmInternationalDrvierLicenseApplication_Load(object sender, System.EventArgs e)
        {
            _LoadAllInternationalLicenses();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            int InternationID = int.Parse(dgvInternationalLicenses.SelectedCells[0].Value.ToString());
            frmShowDriverInternationalLicenseInfo frm = new frmShowDriverInternationalLicenseInfo(InternationID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            string PersonFullName = dgvInternationalLicenses.SelectedCells[8].Value.ToString();
            int DriverID = int.Parse(dgvInternationalLicenses.SelectedCells[2].Value.ToString());
            int PersonID = clsPerson.GetPersonIDbyHisName(PersonFullName);
            frmPersonLicenseshistory frm = new frmPersonLicenseshistory(DriverID, PersonID);
            frm.ShowDialog();
        }

        private void btnIssueInternationalLicense_Click(object sender, System.EventArgs e)
        {
            frmIssueInternationalLicense frm = new frmIssueInternationalLicense();
            frm.ShowDialog();
            _LoadAllInternationalLicenses();

        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            string PersonFullName = dgvInternationalLicenses.SelectedCells[8].Value.ToString();
            int PersonID = clsPerson.GetPersonIDbyHisName(PersonFullName);
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }
    }
}

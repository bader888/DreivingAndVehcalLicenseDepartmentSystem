using DVDL_Business;
using System.Windows.Forms;

namespace DVLD.License
{
    public partial class frmShowLicense : Form
    {
        int LicenseID = -1;
        public frmShowLicense()
        {
            InitializeComponent();
        }

        private void frmShowLicense_Load(object sender, System.EventArgs e)
        {
            ctrlShowLicenseInfo1.ShowDrivingLicenseInfo(clsGlobal.L_DappID);
        }

        public void ShowLicenseInfo(int LicenseID)
        {
            ctrlShowLicenseInfo1.ShowLicenseInfo(LicenseID);
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}

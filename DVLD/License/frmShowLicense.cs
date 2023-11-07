using DVDL_Business;
using System.Windows.Forms;

namespace DVLD.License
{
    public partial class frmShowLicense : Form
    {
        public frmShowLicense()
        {
            InitializeComponent();
        }

        private void frmShowLicense_Load(object sender, System.EventArgs e)
        {
            ctrlShowLicenseInfo1.ShowDrivingLicenseInfo(clsGlobal.L_DappID);
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}

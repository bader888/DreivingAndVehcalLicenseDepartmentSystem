using System.Windows.Forms;

namespace DVLD.License
{
    public partial class frmShowDriverInternationalLicenseInfo : Form
    {
        int _InternationalID = -1;
        public frmShowDriverInternationalLicenseInfo(int InternationalID)
        {
            InitializeComponent();
            _InternationalID = InternationalID;
        }

        private void frmShowDriverInternationalLicenseInfo_Load(object sender, System.EventArgs e)
        {
            ctrlDriverInternationalLicenseInfo1.ShowInternationalDriverLicenseInfo(_InternationalID);
        }
    }
}

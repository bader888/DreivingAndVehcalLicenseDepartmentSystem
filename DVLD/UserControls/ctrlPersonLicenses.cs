using DVDL_Business;
using System.Windows.Forms;

namespace DVLD.UserControls
{
    public partial class ctrlPersonLicenses : UserControl
    {
        public ctrlPersonLicenses()
        {
            InitializeComponent();
        }

        public void ShowPersonlocalLicenses()
        {
            dgvLocalLicensesHistory.DataSource = clsLocalDrivingLicenseApplications.GetPersonLocalDrivingLicenseApplications(clsGlobal.L_DappID);
        }
    }
}

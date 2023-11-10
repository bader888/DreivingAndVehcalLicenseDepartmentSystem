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

        public void ShowPersonLocalLicenseByDriverID(int DriverID)
        {
            dgvLocalLicensesHistory.DataSource = clsLocalDrivingLicenseApplications.GetPersonLocalDrivingLicenseApplicationsbyDriverID(DriverID);

        }

        public void ShowPersonInternationalLicenseByDriverID(int DriverID)
        {
            dgvInternationalLicensesHistory.DataSource = clsInternationalLicenses.GetAllInternationalLicensesbyDriverID(DriverID);

        }

    }
}

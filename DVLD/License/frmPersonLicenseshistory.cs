using System;
using System.Windows.Forms;

namespace DVLD.License
{
    public partial class frmPersonLicenseshistory : Form
    {
        int _DriverID = -1;
        int _PersonID = -1;

        public frmPersonLicenseshistory(int DriverID, int PersonID)
        {
            InitializeComponent();
            _DriverID = DriverID;
            _PersonID = PersonID;

        }

        private void frmPersonLicenseshistory_Load(object sender, EventArgs e)
        {
            ctrlPersonLicenses1.ShowPersonLocalLicenseByDriverID(_DriverID);
            ctrlPersonLicenses1.ShowPersonInternationalLicenseByDriverID(_DriverID);
            ctrlPersonInfo1.ShowPersonInfo(_PersonID);
        }
    }
}

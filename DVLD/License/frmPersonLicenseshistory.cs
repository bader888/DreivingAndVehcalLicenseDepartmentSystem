using DVDL_Business;
using System;
using System.Windows.Forms;

namespace DVLD.License
{
    public partial class frmPersonLicenseshistory : Form
    {
        public frmPersonLicenseshistory()
        {
            InitializeComponent();
        }

        private void frmPersonLicenseshistory_Load(object sender, EventArgs e)
        {
            int PersonID = clsPerson.GetPersonIDByL_DappID(clsGlobal.L_DappID);
            ctrlPersonLicenses1.ShowPersonlocalLicenses();
            ctrlPersonInfo1.ShowPersonInfo(PersonID);
        }
    }
}

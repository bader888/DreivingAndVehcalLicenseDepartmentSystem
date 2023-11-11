using DVDL_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class frmRenewLocalLicense : Form
    {

        int _OldLicenseID = -1;
        public frmRenewLocalLicense()
        {
            InitializeComponent();


        }

        private void _ShowRenewLocalLicenseInfo()
        {
            decimal ApplicationFees = clsApplicationType.GetApplicationTypeFeesbyName("Renew");
            decimal LicenseFees = clsLicenseClasses.GetLicenseClassFeesByName(ctrlDriverLicenseInfoWithFilter1.LicenseClass);
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblExpirationDate.Text = DateTime.Now.AddYears(10).ToShortDateString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblOldLicenseID.Text = _OldLicenseID.ToString();
            lblApplicationFees.Text = ApplicationFees.ToString();
            lblLicenseFees.Text = LicenseFees.ToString();
            lblTotalFees.Text = (ApplicationFees + LicenseFees).ToString();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseFound(int obj)
        {

            //show Renew License Application info 
            _OldLicenseID = obj;
            _ShowRenewLocalLicenseInfo();
            llShowLicenseInfo.Enabled = true;

            //if the license not expiered --> you cannot renew it
            DataTable dt = clsLicense.GetLicenseInfobyID(_OldLicenseID);


        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlDriverLicenseInfoWithFilter1.PersonID;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();

        }
    }
}

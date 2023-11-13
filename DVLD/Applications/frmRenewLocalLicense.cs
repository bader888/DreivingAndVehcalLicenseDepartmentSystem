using DVDL_Business;
using DVLD.License;
using System;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class frmRenewLocalLicense : Form
    {

        int _OldLicenseID = -1;
        clsLicense _OldLicense = new clsLicense();

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

        private bool IsLicenseExpired()
        {
            DateTime CurrentDate = DateTime.Now;
            return CurrentDate > _OldLicense.ExpirationDate;
        }

        private void DeactivateOldLicense()
        {
            clsLicense.DeactivateLicense(_OldLicenseID);
        }

        private clsApplications CreateNewApplication()
        {
            clsApplications application = new clsApplications();
            application.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            application.ApplicantPersonID = ctrlDriverLicenseInfoWithFilter1.PersonID;
            application.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationStatus = 3;//completed
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = clsApplicationType.GetApplicationTypeFeesbyName("Renew Driving");
            application.ApplicationTypeID = clsApplicationType.GetApplicationTypeIDbyName("Renew Driving");
            return application;
        }

        private clsLicense CreateNewLicense()
        {
            clsLicense License = new clsLicense();
            License.DriverID = ctrlDriverLicenseInfoWithFilter1.DriverID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(10);
            License.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            License.LicenseClass = clsLicenseClasses.GetLicenseClassIDbyName(ctrlDriverLicenseInfoWithFilter1.LicenseClass);
            License.PaidFees = clsLicenseClasses.GetLicenseClassFeesByName(ctrlDriverLicenseInfoWithFilter1.LicenseClass);
            License.Notes = txtNotes.Text;
            License.IsActive = true;
            License.IssueReason = 2; //renew 
            return License;
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseFound(int obj)
        {

            //show Renew License Application info 
            _OldLicenseID = obj;
            _ShowRenewLocalLicenseInfo();
            llShowLicenseInfo.Enabled = true;
            llShowLicenseHistory.Enabled = true;
            _OldLicense = clsLicense.Find(_OldLicenseID);
            if (!IsLicenseExpired())
            {
                MessageBox.Show($"Your license is valid. Whith Expieration Date = {_OldLicense.ExpirationDate.ToShortDateString()}", "License Valid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            btnRenewLicense.Enabled = true;
        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            //Deactivate the OldLicense 
            DeactivateOldLicense();

            //create and save new application
            clsApplications application = CreateNewApplication();
            if (application.Save())
            {
                //create And Save new license To this person 
                clsLicense License = CreateNewLicense();
                License.ApplicationID = application.ApplicationID;
                if (License.Save())
                {
                    lblRenewedLicenseID.Text = License.LicenseID.ToString();
                    lblApplicationID.Text = application.ApplicationID.ToString();
                    MessageBox.Show($"License renewed successfully With ID = {License.LicenseID}! Thank you for renewing.", "License Renewed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                    MessageBox.Show("Faild To Save The License");
            }
            else
                MessageBox.Show("Faild To Save The Application");

        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlDriverLicenseInfoWithFilter1.PersonID;
            int DriverID = ctrlDriverLicenseInfoWithFilter1.DriverID;
            frmPersonLicenseshistory frm = new frmPersonLicenseshistory(DriverID, PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlDriverLicenseInfoWithFilter1.PersonID;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }
    }
}

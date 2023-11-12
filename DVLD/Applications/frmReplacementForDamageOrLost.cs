using DVDL_Business;
using DVLD.License;
using System;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class frmReplacementForDamageOrLost : Form
    {

        int _OldLicense = -1;
        public frmReplacementForDamageOrLost()
        {
            InitializeComponent();
        }

        private void ShowReplacementApplicationInfo()
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblOldLicenseID.Text = _OldLicense.ToString();
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
            application.PaidFees = decimal.Parse(lblApplicationFees.Text);
            application.ApplicationTypeID =
                rbDamagedLicense.Checked ?
                clsApplicationType.GetApplicationTypeIDbyName("Replacement for a Damaged Driving License") :
                clsApplicationType.GetApplicationTypeIDbyName("Replacement for a Lost Driving License");
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
            License.Notes = "";
            License.IsActive = true;
            License.IssueReason = byte.Parse(((rbDamagedLicense.Checked) ? 3 : 4).ToString()); //renew 
            return License;
        }


        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseFound(int obj)
        {

            _OldLicense = obj;
            llShowLicenseHistory.Enabled = true;
            clsLicense license = clsLicense.Find(_OldLicense);
            if (!license.IsActive)
                MessageBox.Show("You can't replacement this license because it is deactivate!");
            else
                btnIssueReplacement.Enabled = true;
            ShowReplacementApplicationInfo();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblApplicationFees.Text = clsApplicationType.GetApplicationTypeFeesbyName("Replacement for a Damaged Driving License").ToString();
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblApplicationFees.Text = clsApplicationType.GetApplicationTypeFeesbyName("Replacement for a Lost Driving License").ToString();

        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            llShowLicenseInfo.Enabled = true;
            clsLicense.DeactivateLicense(_OldLicense);
            clsApplications application = CreateNewApplication();
            if (application.Save())
            {
                //create And Save new license To this person 
                clsLicense License = CreateNewLicense();
                License.ApplicationID = application.ApplicationID;
                if (License.Save())
                {
                    MessageBox.Show($"License Replacement successfully With ID = {License.LicenseID}! Thank you for Replacement.", "License Replacement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblRreplacedLicenseID.Text = License.LicenseID.ToString();
                    lblApplicationID.Text = application.ApplicationID.ToString();
                    llShowLicenseInfo.Enabled = true;

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
            int NewLicenseID = int.Parse(lblRreplacedLicenseID.Text);
            frmShowLicense frm = new frmShowLicense();
            frm.ShowLicenseInfo(NewLicenseID);
            frm.ShowDialog();
        }
    }
}

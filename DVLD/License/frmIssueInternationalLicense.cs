using DVDL_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD.License
{
    public partial class frmIssueInternationalLicense : Form
    {
        public frmIssueInternationalLicense()
        {
            InitializeComponent();
        }
        int LicenseID = -1;


        private DataRow GetLicenseInfo()
        {

            DataTable dtLicense = clsLicense.GetLicenseInfobyID(LicenseID);
            DataRow row = dtLicense.Rows[0];

            return row;

        }

        private bool CanIssueNewInternationalLicense()
        {
            DataRow LicenseInfo = GetLicenseInfo();
            bool HasActiveLocalLicense = (bool)LicenseInfo["IsActive"];
            bool HasOrdinaryDrivinglicense = (int)LicenseInfo["LicenseClass"] == 3 ? true : false;
            bool HasActiveInternationalLicense = clsDrivers.HasActiveInternationalLicense(ctrlDriverLicenseInfoWithFilter1.DriverID);

            if (!HasActiveLocalLicense)
            {
                MessageBox.Show("You do not have an Active Local license, so you cannot issue an International license.", "License Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }

            if (!HasOrdinaryDrivinglicense)
            {
                MessageBox.Show("You do not have a Class 3 - Ordinary driving license, so you cannot issue an International license.", "License Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (HasActiveInternationalLicense)
            {
                MessageBox.Show("You have an Active International License, so you cannot issue a new International Driving License.", "License Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;


        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseFound(int obj)
        {
            LicenseID = obj;
            lblLocalLicenseID.Text = LicenseID.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName.ToString();
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblFees.Text = "51";
            btnIssueLicense.Enabled = true;
            llShowLicenseHistory.Enabled = true;

        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int DriverID = ctrlDriverLicenseInfoWithFilter1.DriverID;
            int PersonID = ctrlDriverLicenseInfoWithFilter1.PersonID;
            frmPersonLicenseshistory frm = new frmPersonLicenseshistory(DriverID, PersonID);
            frm.ShowDialog();

        }

        private clsApplications _CreateNewApplication()
        {
            clsApplications applications = new clsApplications();
            applications.ApplicantPersonID = ctrlDriverLicenseInfoWithFilter1.PersonID;
            applications.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            applications.ApplicationDate = DateTime.Parse(lblApplicationDate.Text);
            applications.LastStatusDate = DateTime.Now;
            applications.PaidFees = 50;
            applications.ApplicationTypeID = clsApplicationType.GetApplicationTypeIDbyName("New International License");
            return applications;

        }

        private clsInternationalLicenses _CreatrInternationalLicense()
        {
            clsInternationalLicenses internationalLicenses = new clsInternationalLicenses();
            internationalLicenses.CreatedByUserID = clsGlobal.CurrentUser.UserID; ;
            internationalLicenses.DriverID = ctrlDriverLicenseInfoWithFilter1.DriverID;
            internationalLicenses.IssuedUsingLocalLicenseID = LicenseID;
            internationalLicenses.IssueDate = DateTime.Now;
            internationalLicenses.ExpirationDate = DateTime.Now.AddYears(1);
            internationalLicenses.IsActive = true;
            return internationalLicenses;
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {

            if (CanIssueNewInternationalLicense())
            {
                //create new application 
                clsApplications applications = _CreateNewApplication();
                if (applications.Save())
                {
                    //create international License
                    clsInternationalLicenses internationalLicenses = _CreatrInternationalLicense();
                    internationalLicenses.ApplicationID = applications.ApplicationID;

                    if (internationalLicenses.Save())
                    {
                        MessageBox.Show($"International License issued successfully with ID = {internationalLicenses.InternationalLicenseID}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblInternationalLicenseID.Text = internationalLicenses.InternationalLicenseID.ToString();
                        lblApplicationID.Text = applications.ApplicationID.ToString();
                        llShowLicenseInfo.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Failed to save the International License. Please check your information and try again.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                    MessageBox.Show("Failed to save the application. Please review the provided information and try again.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }

        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int InternationalID = int.Parse(lblInternationalLicenseID.Text);
            frmShowDriverInternationalLicenseInfo frm = new frmShowDriverInternationalLicenseInfo(InternationalID);
            frm.ShowDialog();
        }

        private void frmIssueInternationalLicense_Load(object sender, EventArgs e)
        {

        }
    }
}

using DVDL_Business;
using DVDL_Business.Global;
using System;
using System.Windows.Forms;

namespace DVLD.License.DetainReleaseLicenses
{


    public partial class frmDetainLicense : Form
    {
        int LicenseID = -1;

        public frmDetainLicense()
        {
            InitializeComponent();
        }

        private void ShowDetainLicenseInfo()
        {
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblLicenseID.Text = LicenseID.ToString();
        }


        private clsDetainedLicenses CreateNewDetainLicense()
        {
            clsDetainedLicenses detainedLicense = new clsDetainedLicenses();
            detainedLicense.LicenseID = LicenseID;
            detainedLicense.IsReleased = false;
            detainedLicense.DetainDate = DateTime.Now;
            detainedLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            detainedLicense.ReleaseApplicationID = -1;
            detainedLicense.ReleasedByUserID = -1;
            detainedLicense.ReleaseDate = DateTime.MinValue;
            detainedLicense.FineFees = int.Parse(txtFineFees.Text);
            return detainedLicense;
        }
        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseFound(int obj)
        {
            LicenseID = obj;
            ShowDetainLicenseInfo();
            llShowLicenseHistory.Enabled = true;
            llShowLicenseInfo.Enabled = true;

            if (clsDetainedLicenses.IsLicenseDetain(LicenseID))
            {
                MessageBox.Show("The license is already Detained. You cannot Detained it again.", "License Already Obtained", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            btnDetain.Enabled = true;
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFineFees.Text))
            {
                MessageBox.Show("Fees failed. Cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!ValidationHelper.StringValidator.ContainsNumbersOnly(txtFineFees.Text))
            {
                MessageBox.Show("Fees must contain numbers only.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsDetainedLicenses detainedLicenses = CreateNewDetainLicense();
            if (detainedLicenses.Save())
            {
                MessageBox.Show("License Detained successfully!", "License Detained", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblDetainID.Text = detainedLicenses.DetainID.ToString();
                ctrlDriverLicenseInfoWithFilter1.EnableFilter(false);
                btnDetain.Enabled = false;

            }
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicense frm = new frmShowLicense();
            frm.ShowLicenseInfo(LicenseID);
            frm.ShowDialog();

        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlDriverLicenseInfoWithFilter1.PersonID;
            int DriverID = ctrlDriverLicenseInfoWithFilter1.DriverID;
            frmPersonLicenseshistory frm = new frmPersonLicenseshistory(DriverID, PersonID);
            frm.ShowDialog();
        }
    }
}



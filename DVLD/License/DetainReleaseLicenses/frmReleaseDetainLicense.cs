using DVDL_Business;
using System;
using System.Windows.Forms;

namespace DVLD.License.DetainReleaseLicenses
{
    public partial class frmReleaseDetainLicense : Form
    {
        int LicenseID = -1;
        clsDetainedLicenses detainedLicense;
        clsApplications application;
        public frmReleaseDetainLicense()
        {
            InitializeComponent();
        }
        public frmReleaseDetainLicense(int LicenseID)
        {
            InitializeComponent();
            this.LicenseID = LicenseID;
        }

        private void ReleaseDetainLicense()
        {
            detainedLicense.ReleaseDate = DateTime.Now;
            detainedLicense.ReleasedByUserID = clsGlobal.CurrentUser.UserID;
            detainedLicense.ReleaseApplicationID = application.ApplicationID;
            detainedLicense.IsReleased = true;
            detainedLicense.Save();

        }

        private clsApplications CreateReleaseApplication()
        {
            application = new clsApplications();
            application.ApplicantPersonID = ctrlDriverLicenseInfoWithFilter1.PersonID;
            application.ApplicationStatus = 3; //completed
            application.ApplicationTypeID = clsApplicationType.GetApplicationTypeIDbyName("Release Detained Driving Licsense");
            application.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            application.LastStatusDate = DateTime.Now;
            application.ApplicationDate = DateTime.Now;
            application.PaidFees = decimal.Parse(lblTotalFees.Text);
            return application;
        }


        private void ShowDetainInfo()
        {
            decimal ApplicationFees = clsApplicationType.GetApplicationTypeFeesbyName("Release Detained Driving Licsense");
            detainedLicense = clsDetainedLicenses.FindByLicenseID(LicenseID);
            if (detainedLicense != null)
            {
                lblFineFees.Text = detainedLicense.FineFees.ToString();
                lblDetainDate.Text = detainedLicense.DetainDate.ToString();
                lblDetainID.Text = detainedLicense.DetainID.ToString();
                lblLicenseID.Text = detainedLicense.LicenseID.ToString();
                lblTotalFees.Text = (detainedLicense.FineFees + ApplicationFees).ToString();
                lblApplicationFees.Text = ApplicationFees.ToString();
                clsUsers user = clsUsers.Find(detainedLicense.CreatedByUserID);
                lblCreatedByUser.Text = user.UserName;
            }
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseFound(int obj)
        {
            LicenseID = obj;
            llShowLicenseHistory.Enabled = true;
            llShowLicenseInfo.Enabled = true;
            ShowDetainInfo();

            if (clsDetainedLicenses.IsLicenseDetain(LicenseID))
                btnRelease.Enabled = true;
            else
            {
                MessageBox.Show("You cannot release undetained license.", "Release Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRelease.Enabled = false;

            }
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            application = CreateReleaseApplication();
            if (application.Save())
            {
                ReleaseDetainLicense();
                MessageBox.Show("License released successfully!", "Release Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblApplicationID.Text = application.ApplicationID.ToString();
                ctrlDriverLicenseInfoWithFilter1.EnableFilter(false);
                btnRelease.Enabled = false;
            }
        }

        private void frmReleaseDetainLicense_Load(object sender, EventArgs e)
        {
            if (LicenseID != -1)
            {
                ctrlDriverLicenseInfoWithFilter1.ShowDriverLicenseInfo(LicenseID);
                ctrlDriverLicenseInfoWithFilter1.EnableFilter(false);
                btnRelease.Enabled = true;
                ShowDetainInfo();
            }
        }
    }
}

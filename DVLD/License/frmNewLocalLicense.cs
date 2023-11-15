using DVDL_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmNewLocalLicense : Form
    {
        public delegate void DataBackEventHandler();

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        public frmNewLocalLicense()
        {
            InitializeComponent();
        }

        enum enApplicationStatus
        {
            New = 1,
            Complete = 2,
            Cancelled = 3
        }

        private clsApplications CreateNewApplication()
        {
            clsApplications application = new clsApplications();
            application.ApplicantPersonID = ctrlPersonInfoWithFilter1.PersonID;
            application.ApplicationStatus = 1; //New
            application.ApplicationTypeID = clsApplicationType.GetApplicationTypeIDbyName("New Local Driving License Service");
            application.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            application.LastStatusDate = DateTime.Now;
            application.ApplicationDate = DateTime.Now;
            application.PaidFees = decimal.Parse(lblFees.Text);
            return application;
        }

        private clsLocalDrivingLicenseApplications CreateNewLocalDrivingLicenseApplication()
        {
            string licenseClass = cbLicenseClass.SelectedItem.ToString();
            clsLocalDrivingLicenseApplications LocalDrivingLicenseApplications = new clsLocalDrivingLicenseApplications();
            LocalDrivingLicenseApplications.LicenseClassID = clsLicenseClasses.GetLicenseClassIDbyName(licenseClass);
            return LocalDrivingLicenseApplications;
        }

        private void _showLicenseClasses()
        {
            DataTable dtLicenseClasses = clsLicenseClasses.GetAllLicenseClasses();
            foreach (DataRow row in dtLicenseClasses.Rows)
            {
                cbLicenseClass.Items.Add(row["ClassName"]);
            }
            cbLicenseClass.SelectedItem = cbLicenseClass.Items[2].ToString();
        }

        private void _ShowLicenseInfo()
        {
            _showLicenseClasses();
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblFees.Text = clsApplicationType.GetApplicationTypeFeesbyName("New Local Driving License Service").ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void _SaveApplication()
        {
            clsApplications application = CreateNewApplication();
            clsLocalDrivingLicenseApplications localDrivingLicenseApplications = CreateNewLocalDrivingLicenseApplication();
            if (clsApplications.isApplicationExist(application.ApplicantPersonID, localDrivingLicenseApplications.LicenseClassID))
            {
                MessageBox.Show("Failed to save application. Another incomplete application with the same class exists.", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (application.Save())
            {
                localDrivingLicenseApplications.ApplicationID = application.ApplicationID;
                localDrivingLicenseApplications.Save();
                lblLocalDrivingLicebseApplicationID.Text = localDrivingLicenseApplications.ApplicationID.ToString();
                MessageBox.Show("Your local driving license application has been saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke();
            }
        }

        private void frmNewLocalLicense_Load(object sender, EventArgs e)
        {
            _ShowLicenseInfo();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _SaveApplication();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApplicationInfoNext_Click(object sender, EventArgs e)
        {
            tcApplicationInfo.SelectedTab = tpApplicationInfo;
            btnSave.Enabled = true;
        }
    }
}

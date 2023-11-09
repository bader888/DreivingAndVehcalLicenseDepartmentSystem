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

        const int PaidFees = 15;

        static int LocalDrivingLicenseID = clsApplicationType.GetApplicationTypeIDbyName("new Local");

        clsApplicationType applicationType = clsApplicationType.Find(LocalDrivingLicenseID);

        clsApplications application = new clsApplications();

        clsLocalDrivingLicenseApplications drivingLicenseApplications = new clsLocalDrivingLicenseApplications();

        private void _ShowLicenseInfo()
        {
            _showLicenseClasses();
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblFees.Text = applicationType.ApplicationFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
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

        private void _SaveL_D_LApp(int LicenseClassID)
        {
            drivingLicenseApplications.ApplicationID = application.ApplicationID;
            drivingLicenseApplications.LicenseClassID = LicenseClassID;
            if (drivingLicenseApplications.Save())
            {
                MessageBox.Show("application Save Successfully!");

                lblLocalDrivingLicebseApplicationID.Text = application.ApplicationID.ToString();

                DataBack?.Invoke();
            }
        }

        private void _SaveApplication()
        {
            application.ApplicantPersonID = ctrlPersonInfoWithFilter1.PersonID;
            application.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationStatus = (int)enApplicationStatus.New;
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = PaidFees;
            application.ApplicationTypeID = clsApplicationType.GetApplicationTypeIDbyName("New local");

            int LicenseClassID = clsLicenseClasses.GetLicenseClassIDbyName(cbLicenseClass.SelectedItem.ToString());

            if (clsApplications.isApplicationExist(application.ApplicantPersonID, LicenseClassID))
            {
                MessageBox.Show("Application Save Faild", "you have app with the same class not completed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (application.Save())
            {
                _SaveL_D_LApp(LicenseClassID);
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

        private void cbLicenseClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;

        }

    }
}

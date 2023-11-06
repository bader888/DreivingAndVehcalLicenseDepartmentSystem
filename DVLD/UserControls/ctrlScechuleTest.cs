using DVDL_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD.UserControls
{
    public partial class ctrlScechuleTest : UserControl
    {
        enum enApplicationStatus
        {
            New = 1,
            Complete = 2,
            Cancelled = 3
        }
        public ctrlScechuleTest()
        {
            InitializeComponent();
        }

        clsApplications application = new clsApplications();
        clsTestAppointments testAppointments = new clsTestAppointments();
        clsLocalDrivingLicenseApplications localDrivingLicenseApplications = new clsLocalDrivingLicenseApplications();

        public bool RetakeTest { get; set; }

        public int TestappointmentID { get; set; }

        public bool UpdateMode { get; set; }

        public string Title
        {
            set
            {
                lblTitle.Text = value;
            }
            get
            {
                return clsGlobal.TestType;
            }
        }

        private void _ShowRetakeTestInfo()
        {
            decimal AppFees = decimal.Parse(lblFees.Text);
            decimal RetakeTest = 5;
            gbRetakeTestInfo.Enabled = true;
            lblRetakeTestAppID.Text = "???";
            lblRetakeAppFees.Text = RetakeTest.ToString();
            lblTotalFees.Text = (AppFees + RetakeTest).ToString();
        }

        private bool _isAppointmentlocked()
        {
            DataTable dt = clsTestAppointments.GetTestAppointmentByID(this.TestappointmentID);
            DataRow row = dt.Rows[0];
            bool IsLocked = (bool)row["IsLocked"];
            lblFees.Text = row["PaidFees"].ToString();
            if (IsLocked)
            {
                lblUserMessage.Visible = true;
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return true;
            }
            return false;
        }

        private void _ShowDrivingLicenseAppInfo()
        {
            DataTable dt = clsCtrlDrivingLicenseBasicInfo.GetDrivingLicenseAppInfo(clsGlobal.L_DappID);
            DataRow row = dt.Rows[0];
            lblDrivingClass.Text = row["ClassName"].ToString();
            lblFullName.Text = row["FullName"].ToString();
            lblLocalDrivingLicenseAppID.Text = row["LocalDrivingLicenseApplicationID"].ToString();
            lblTrial.Text = "0";
            lblFees.Text = clsTestType.GetTestTypeFeesByTitle(clsGlobal.TestType).ToString();
        }

        public void ShowScheduleTestInfo()
        {
            _ShowDrivingLicenseAppInfo();

            if (RetakeTest)
                _ShowRetakeTestInfo();

            if (UpdateMode)
            {
                if (!_isAppointmentlocked())
                    testAppointments = clsTestAppointments.Find(this.TestappointmentID);
            }

        }

        private void _CreateNewTestAppointment()
        {
            int TestTypeID = clsTestType.GetTestTypeIDbyTestTitle(clsGlobal.TestType);
            testAppointments.IsLocked = false;
            testAppointments.LocalDrivingLicenseApplicationID = int.Parse(lblLocalDrivingLicenseAppID.Text);
            testAppointments.PaidFees = decimal.Parse(lblFees.Text);
            testAppointments.TestTypeID = TestTypeID;
            testAppointments.AppointmentDate = dtpTestDate.Value;
            testAppointments.CreatedByUserID = clsGlobal.CurrentUser.UserID;
        }

        private void _CreateNewApplication()
        {
            int ApplicantPersonID = clsPerson.GetPersonIDbyHisName(lblFullName.Text);
            int ApplicationTypeID = clsApplicationType.GetApplicationTypeIDbyName("Renew Driving License Service");

            application.ApplicantPersonID = ApplicantPersonID;
            application.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationStatus = (int)enApplicationStatus.New;
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = 5;
            application.ApplicationTypeID = ApplicationTypeID;
        }

        private void _CreateNewLocalDrivingLicenseApp()
        {
            localDrivingLicenseApplications.ApplicationID = application.ApplicationID;
            localDrivingLicenseApplications.LicenseClassID = clsLicenseClasses.GetLicenseClassIDbyName(lblDrivingClass.Text);
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            _CreateNewTestAppointment();

            if (RetakeTest)
            {
                _CreateNewApplication();
                if (application.Save())
                {
                    _CreateNewLocalDrivingLicenseApp();
                    localDrivingLicenseApplications.Save();
                }
            }
            if (testAppointments.Save())
                MessageBox.Show("Test Appointment Save Sccessfully");
            else
                MessageBox.Show("faild");

        }

    }
}


using DVDL_Business;
using System.Data;
using System.Windows.Forms;

namespace DVLD.UserControls
{
    public partial class ctrlScechuleTest : UserControl
    {


        public ctrlScechuleTest()
        {
            InitializeComponent();
        }

        clsTestAppointments testAppointments = new clsTestAppointments();

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

        private void _isAppointmentBlocked()
        {
            DataTable dt = clsTestAppointments.GetTestAppointmentByID(this.TestappointmentID);
            DataRow row = dt.Rows[0];
            bool IsLocked = (bool)row["IsLocked"];
            if (IsLocked)
            {
                lblUserMessage.Visible = true;
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;

            }
            lblFees.Text = row["PaidFees"].ToString();
        }

        public void ShowScheduleTestInfo()
        {

            DataTable dt = clsCtrlDrivingLicenseBasicInfo.GetDrivingLicenseAppInfo(clsGlobal.L_DappID);
            DataRow row = dt.Rows[0];
            lblDrivingClass.Text = row["ClassName"].ToString();
            lblFullName.Text = row["FullName"].ToString();
            lblLocalDrivingLicenseAppID.Text = row["LocalDrivingLicenseApplicationID"].ToString();
            lblTrial.Text = "0";
            lblFees.Text = clsTestType.GetTestTypeFeesByTitle(clsGlobal.TestType).ToString();

            if (UpdateMode)
                _isAppointmentBlocked();

        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {

            if (UpdateMode)
            {
                testAppointments = clsTestAppointments.Find(this.TestappointmentID);
            }
            else
            {
                if (clsTestAppointments.IsPersonHaveActiveTest(clsGlobal.L_DappID))
                {
                    MessageBox.Show("The Person Have Active Test");
                    return;
                }
            }
            testAppointments.IsLocked = false;
            testAppointments.LocalDrivingLicenseApplicationID = int.Parse(lblLocalDrivingLicenseAppID.Text);
            testAppointments.PaidFees = decimal.Parse(lblFees.Text);
            testAppointments.TestTypeID = 1;
            testAppointments.AppointmentDate = dtpTestDate.Value;
            testAppointments.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (testAppointments.Save())
                MessageBox.Show("Done");
            else
                MessageBox.Show("faild");
        }

    }
}


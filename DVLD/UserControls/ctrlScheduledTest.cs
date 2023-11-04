using DVDL_Business.UserControlLogic;
using System.Data;
using System.Windows.Forms;

namespace DVLD.UserControls
{
    public partial class ctrlScheduledTest : UserControl
    {
        public ctrlScheduledTest()
        {
            InitializeComponent();
        }

        public int TestAppointmentID { get; set; }

        public void ShowScheduleTestInfo(int TestAppointmentID)
        {
            DataTable dt = clsCtrlSchduledTest.GetSchduledTestInfo(TestAppointmentID);
            DataRow row = dt.Rows[0];
            lblDate.Text = row["AppointmentDate"].ToString();
            lblDrivingClass.Text = row["ClassName"].ToString();
            lblFees.Text = row["PaidFees"].ToString();
            lblLocalDrivingLicenseAppID.Text = row["LocalDrivingLicenseApplicationID"].ToString();
            lblFullName.Text = row["FullName"].ToString();
            lblTrial.Text = "0";

        }
    }
}

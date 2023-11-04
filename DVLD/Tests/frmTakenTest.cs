using System.Windows.Forms;

namespace DVLD.Tests
{
    public partial class frmTakenTest : Form
    {
        public frmTakenTest()
        {
            InitializeComponent();
        }

        public int TestAppointmentID { get; set; }


        private void _LockedTheTest()
        {
            //clsTestAppointments testAppointments = clsTestAppointments.Find()
        }


        private void frmTakenTest_Load(object sender, System.EventArgs e)
        {
            ctrlScheduledTest1.ShowScheduleTestInfo(TestAppointmentID);
        }



        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            //TestID
            //TestAppointmentID
            //TestResult
            //Notes
            //CreatedByUserID

            //clsTests Test = new clsTests();
            //Test.TestAppointmentID = this.TestAppointmentID;
            //Test.Notes = txtNotes.Text;
            //Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            //Test.TestResult = rbPass.Checked ? true : false;
            //if (Test.Save())
            //{
            //    MessageBox.Show("Test Save Successfully!");

            //}
            //else
            //    MessageBox.Show("Test Save  Faild!");


        }
    }
}

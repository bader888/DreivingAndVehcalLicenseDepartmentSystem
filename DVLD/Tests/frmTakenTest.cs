using DVDL_Business;
using System.Windows.Forms;

namespace DVLD.Tests
{
    public partial class frmTakenTest : Form
    {
        public delegate void DataBackEventHandler();
        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;
        public frmTakenTest()
        {
            InitializeComponent();
        }

        public int TestAppointmentID { get; set; }

        private void _LockedTheTest()
        {
            clsTestAppointments testAppointment = clsTestAppointments.Find(this.TestAppointmentID);
            testAppointment.IsLocked = true;
            testAppointment.Save();
        }

        private clsTests CreateNewTest()
        {
            clsTests Test = new clsTests();
            Test.TestAppointmentID = this.TestAppointmentID;
            Test.Notes = txtNotes.Text;
            Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            Test.TestResult = rbPass.Checked ? true : false;
            return Test;
        }

        private void SaveTest()
        {
            clsTests Test = CreateNewTest();
            if (Test.Save())
            {
                MessageBox.Show("Test Save Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _LockedTheTest();
                DataBack?.Invoke();
            }
            else
                MessageBox.Show($"Test Save Failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmTakenTest_Load(object sender, System.EventArgs e)
        {
            ctrlScheduledTest1.ShowScheduleTestInfo(TestAppointmentID);
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click_1(object sender, System.EventArgs e)
        {
            SaveTest();
        }
    }
}

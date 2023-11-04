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

        private void frmTakenTest_Load(object sender, System.EventArgs e)
        {
            ctrlScheduledTest1.ShowScheduleTestInfo(TestAppointmentID);
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}

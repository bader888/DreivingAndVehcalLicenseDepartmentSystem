using DVDL_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD.Tests
{
    public partial class frmListTestAppointments : Form
    {

        public frmListTestAppointments()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            if (clsTestAppointments.IsPersonHaveActiveTest(clsGlobal.L_DappID))
            {
                MessageBox.Show("The Person Have Active Test you can't create new appointment");
                return;
            }

            DataTable dt = clsTestAppointments.GetTestAppointmentForSpecificTest(clsGlobal.L_DappID, clsGlobal.TestType);
            foreach (DataRow row in dt.Rows)
            {
                int TestAppointmentID = (int)row["TestAppointmentID"];
                if (clsTests.IsTestPass(TestAppointmentID))
                {
                    MessageBox.Show("The Test Pass you can't create new appointment");
                    return;
                }
            }

            frmScheduleTest frm = new frmScheduleTest();
            if (dt.Rows.Count > 0)
            {
                frm.ReTakeTest = true;
            }

            frm.UpdateMode = false;
            frm.ShowDialog();

        }

        private void _ShowTestAppointment()
        {
            ctrlDrivingLicenseApplicationInfo1._ShowDrivingLicenseApplicationInfo(clsGlobal.L_DappID);
            dgvLicenseTestAppointments.DataSource = clsTestAppointments.GetTestAppointmentForSpecificTest(clsGlobal.L_DappID, clsGlobal.TestType);
        }

        private void ctrlDrivingLicenseApplicationInfo1_Load(object sender, EventArgs e)
        {
            lblTitle.Text = clsGlobal.TestType + " test" + " appointment";
            _ShowTestAppointment();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmScheduleTest frm = new frmScheduleTest();
            frm.TestAppointmentID = int.Parse(dgvLicenseTestAppointments.SelectedCells[0].Value.ToString());
            frm.UpdateMode = true;
            frm.DataBack += _ShowTestAppointment;
            frm.ShowDialog();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTakenTest frm = new frmTakenTest();
            frm.TestAppointmentID = int.Parse(dgvLicenseTestAppointments.SelectedCells[0].Value.ToString());
            frm.DataBack += _ShowTestAppointment;
            frm.ShowDialog();

        }
    }
}

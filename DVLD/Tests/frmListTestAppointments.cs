using DVDL_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD.Tests
{
    public partial class frmListTestAppointments : Form
    {
        public delegate void DataBackEventHandler();

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        bool IsRetakeTest = false;
        public frmListTestAppointments()
        {
            InitializeComponent();
        }


        private void ShowTestTypeImage()
        {
            switch (clsGlobal.TestType)
            {
                case "vision":
                    pbTestTypeImage.Load(clsGlobal.clsImages.Vision);
                    break;

                case "Written":
                    pbTestTypeImage.Load(clsGlobal.clsImages.Written);
                    break;

                case "practical":
                    pbTestTypeImage.Load(clsGlobal.clsImages.Parctical);
                    break;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            DataBack.Invoke();

        }

        private bool IsPersonHaveActiveTest()
        {
            if (clsTestAppointments.IsPersonHaveActiveTest(clsGlobal.L_DappID))
            {
                MessageBox.Show("The Person Has an Active Test; You Can't Create a New Appointment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            return false;
        }

        private bool IsTestPassed()
        {
            DataTable dt = clsTestAppointments.GetTestAppointmentForSpecificTest(clsGlobal.L_DappID, clsGlobal.TestType);
            IsRetakeTest = dt.Rows.Count > 0;
            int TestAppointmentID;
            foreach (DataRow row in dt.Rows)
            {
                TestAppointmentID = (int)row["TestAppointmentID"];
                if (clsTests.IsTestPass(TestAppointmentID))
                {
                    MessageBox.Show("The Test Passed; You Can't Create a New Appointment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return true;
                }
            }
            return false;
        }

        private void OpenSchudleTestForm()
        {
            frmScheduleTest frm = new frmScheduleTest();
            frm.ReTakeTest = IsRetakeTest;
            frm.UpdateMode = false;
            frm.DataBack += ShowTestAppointment;
            frm.ShowDialog();
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            if (!IsPersonHaveActiveTest() && !IsTestPassed())
            {
                OpenSchudleTestForm();
            }
        }

        private void ShowTestAppointment()
        {
            lblTitle.Text = clsGlobal.TestType + " test" + " appointment";
            ctrlDrivingLicenseApplicationInfo1._ShowDrivingLicenseApplicationInfo(clsGlobal.L_DappID);
            dgvLicenseTestAppointments.DataSource = clsTestAppointments.GetTestAppointmentForSpecificTest(clsGlobal.L_DappID, clsGlobal.TestType);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmScheduleTest frm = new frmScheduleTest();
            frm.TestAppointmentID = int.Parse(dgvLicenseTestAppointments.SelectedCells[0].Value.ToString());
            frm.UpdateMode = true;
            frm.DataBack += ShowTestAppointment;
            frm.ShowDialog();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTakenTest frm = new frmTakenTest();
            frm.TestAppointmentID = int.Parse(dgvLicenseTestAppointments.SelectedCells[0].Value.ToString());
            frm.DataBack += ShowTestAppointment;
            frm.ShowDialog();

        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {

            ShowTestTypeImage();
            ShowTestAppointment();
        }

        private void cmsApplications_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if the test locked enable the strip menu items
            bool IsLocked = dgvLicenseTestAppointments.SelectedCells[3].Value.ToString().ToLower() == "true" ? true : false;
            if (IsLocked)
            {
                takeTestToolStripMenuItem.Enabled = false;
                editToolStripMenuItem.Enabled = false;
            }
            else
            {
                takeTestToolStripMenuItem.Enabled = true;
                editToolStripMenuItem.Enabled = true;
            }
        }
    }
}

using DVDL_Business;
using DVLD.License;
using DVLD.Tests;
using System.Windows.Forms;

namespace DVLD.ManageApplications
{
    public partial class frmLocalDrivingLicenseApplications : Form
    {
        enum enPassedTest
        {
            vision = 1,
            Written = 2,
            Partical = 3
        }
        enPassedTest PassedTest;
        public frmLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void _ShowAllL_D_Lapps()
        {
            dataGridView1.DataSource = clsLocalDrivingLicenseApplications.GetAllLocalDrivingLicenseApplications();

        }

        private void frmLocalDrivingLicenseApplications_Load(object sender, System.EventArgs e)
        {
            _ShowAllL_D_Lapps();
        }

        private void AddnewL_D_Lapp_Click(object sender, System.EventArgs e)
        {
            frmNewLocalLicense frm = new frmNewLocalLicense();
            frm.DataBack += _ShowAllL_D_Lapps; // Subscribe to the event
            frm.ShowDialog();
        }

        private void _ShowScheduleTestForm(int L_DappID, string TestType)
        {
            frmListTestAppointments frm = new frmListTestAppointments();
            clsGlobal.TestType = TestType;
            frm.ShowDialog();
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            clsGlobal.L_DappID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            frmListTestAppointments frm = new frmListTestAppointments();
            clsGlobal.TestType = "vision";
            frm.ShowDialog();
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            clsGlobal.L_DappID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            frmListTestAppointments frm = new frmListTestAppointments();
            clsGlobal.TestType = "Written";
            frm.ShowDialog();
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            clsGlobal.L_DappID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            frmListTestAppointments frm = new frmListTestAppointments();
            clsGlobal.TestType = "practical";
            frm.ShowDialog();
        }

        //not completed
        private void cmsApplications_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int PassedTestCount = int.Parse(dataGridView1.SelectedCells[5].Value.ToString());
            scheduleVisionTestToolStripMenuItem.Enabled = true;
            scheduleWrittenTestToolStripMenuItem.Enabled = true;
            ScheduleTestsMenue.Enabled = true;
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;

            switch (PassedTestCount)
            {
                case (int)enPassedTest.vision:
                    {
                        scheduleVisionTestToolStripMenuItem.Enabled = false;
                        break;
                    }
                case (int)enPassedTest.Written:
                    {
                        scheduleVisionTestToolStripMenuItem.Enabled = false;
                        scheduleWrittenTestToolStripMenuItem.Enabled = false;
                        break;
                    }
                case (int)enPassedTest.Partical:
                    {
                        ScheduleTestsMenue.Enabled = false;
                        issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void ScheduleTestsMenue_Click(object sender, System.EventArgs e)
        {

        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            clsGlobal.L_DappID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            frmIssueLocalLicenseFirstTime frm = new frmIssueLocalLicenseFirstTime();
            frm.ShowDialog();


        }
    }
}

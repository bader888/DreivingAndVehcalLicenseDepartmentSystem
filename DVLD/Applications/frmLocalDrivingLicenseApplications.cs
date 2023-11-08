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

        public frmLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void _ShowAllMenuItem()
        {
            scheduleVisionTestToolStripMenuItem.Enabled = true;
            scheduleWrittenTestToolStripMenuItem.Enabled = true;
            ScheduleTestsMenue.Enabled = true;
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;
            DeleteApplicationToolStripMenuItem.Enabled = true;
            CancelApplicaitonToolStripMenuItem.Enabled = true;
            editToolStripMenuItem.Enabled = true;
            ScheduleTestsMenue.Enabled = true;
        }

        private void _DesableMenuItem()
        {
            scheduleVisionTestToolStripMenuItem.Enabled = false;
            scheduleWrittenTestToolStripMenuItem.Enabled = false;
            ScheduleTestsMenue.Enabled = false;
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
            DeleteApplicationToolStripMenuItem.Enabled = false;
            CancelApplicaitonToolStripMenuItem.Enabled = false;
            editToolStripMenuItem.Enabled = false;
        }

        private void _DesableScheduleTestMenuItems(int PassedTestCount)
        {
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
            frm.DataBack += _ShowAllL_D_Lapps;
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

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            clsGlobal.L_DappID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            frmIssueLocalLicenseFirstTime frm = new frmIssueLocalLicenseFirstTime();
            frm.ShowDialog();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            clsGlobal.L_DappID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            frmShowLicense frm = new frmShowLicense();
            frm.Show();
        }

        private void DeleteApplicationToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            clsGlobal.L_DappID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            DialogResult result = MessageBox.Show("Are you sure you want to delete this application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (clsLocalDrivingLicenseApplications.DeleteLocalDrivingLicenseApplications(clsGlobal.L_DappID))
                {
                    MessageBox.Show("Delete Done");
                    _ShowAllL_D_Lapps();
                }
                else
                    MessageBox.Show("You Can't Delete This Application!");
            }
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            clsGlobal.L_DappID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            frmPersonLicenseshistory frm = new frmPersonLicenseshistory();
            frm.ShowDialog();
        }


        //not completed

        private void cmsApplications_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _ShowAllMenuItem();

            string Status = dataGridView1.SelectedCells[6].Value.ToString();

            if (Status == "New")
            {
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                showLicenseToolStripMenuItem.Enabled = false;
            }
            if (Status == "Cancelled")
            {
                _DesableMenuItem();

            }
            if (Status == "Completed")
            {
                _DesableMenuItem();
            }
            else
            {
                int PassedTestCount = int.Parse(dataGridView1.SelectedCells[5].Value.ToString());
                _DesableScheduleTestMenuItems(PassedTestCount);
            }
        }

        private void CancelApplicaitonToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

            clsGlobal.L_DappID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            DialogResult result = MessageBox.Show("Are you sure you want to cancel this application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                //update the status of application ==> Cancelled
                clsApplications.UpdateApplicationStatus(clsGlobal.L_DappID, 2);
                _ShowAllL_D_Lapps();
            }
        }
    }

}

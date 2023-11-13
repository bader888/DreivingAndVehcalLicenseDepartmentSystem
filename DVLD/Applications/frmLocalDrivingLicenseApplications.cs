using DVDL_Business;
using DVLD.License;
using DVLD.Tests;
using System.Data;
using System.Windows.Forms;

namespace DVLD.ManageApplications
{
    public partial class frmLocalDrivingLicenseApplications : Form
    {
        enum enPassedTest
        {
            NoTestPass = 0,
            vision = 1,
            Written = 2,
            Partical = 3
        }

        DataTable dtAllL_Dapps;

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
            scheduleVisionTestToolStripMenuItem.Enabled = false;
            scheduleWrittenTestToolStripMenuItem.Enabled = false;
            scheduleStreetTestToolStripMenuItem.Enabled = false;
            switch (PassedTestCount)
            {
                case (int)enPassedTest.NoTestPass:
                    {
                        scheduleVisionTestToolStripMenuItem.Enabled = true;
                        break;
                    }
                case (int)enPassedTest.vision:
                    {
                        scheduleWrittenTestToolStripMenuItem.Enabled = true;
                        break;
                    }
                case (int)enPassedTest.Written:
                    {

                        scheduleStreetTestToolStripMenuItem.Enabled = true;
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
            dtAllL_Dapps = clsLocalDrivingLicenseApplications.GetAllLocalDrivingLicenseApplications();
            dataGridView1.DataSource = dtAllL_Dapps;
            cbFilterBy.SelectedIndex = 3; //FullName
            lblRecordsCount.Text = clsGlobal.RecordCount("LocalDrivingLicenseApplications").ToString();
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

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            clsGlobal.L_DappID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            frmListTestAppointments frm = new frmListTestAppointments();
            clsGlobal.TestType = "vision";
            frm.DataBack += _ShowAllL_D_Lapps;
            frm.ShowDialog();

        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            clsGlobal.L_DappID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            frmListTestAppointments frm = new frmListTestAppointments();
            clsGlobal.TestType = "Written";
            frm.DataBack += _ShowAllL_D_Lapps;
            frm.ShowDialog();
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            clsGlobal.L_DappID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            frmListTestAppointments frm = new frmListTestAppointments();
            clsGlobal.TestType = "practical";
            frm.DataBack += _ShowAllL_D_Lapps;
            frm.ShowDialog();
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            clsGlobal.L_DappID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            frmIssueLocalLicenseFirstTime frm = new frmIssueLocalLicenseFirstTime();
            frm.DataBack += _ShowAllL_D_Lapps;
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
            string FullName = dataGridView1.SelectedCells[3].Value.ToString();
            int DriverID = clsDrivers.GetDriverIDByHisName(FullName);
            int PersonID = clsPerson.GetPersonIDbyHisName(FullName);
            frmPersonLicenseshistory frm = new frmPersonLicenseshistory(DriverID, PersonID);
            frm.ShowDialog();
        }

        private void cmsApplications_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _ShowAllMenuItem();

            string Status = dataGridView1.SelectedCells[6].Value.ToString();

            if (Status == "New")
            {
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                showLicenseToolStripMenuItem.Enabled = false;
            }
            else if (Status == "Cancelled")
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

        private void txtFilterValue_TextChanged(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterValue.Text))
            {
                _ShowAllL_D_Lapps();
                return;
            }
            string filterText = txtFilterValue.Text;
            if (dtAllL_Dapps != null)
            {
                string filterExpression = $"FullName like '%{filterText}%'";
                // Apply the filter to the DefaultView of the DataTable
                dtAllL_Dapps.DefaultView.RowFilter = filterExpression;
                // Refresh the DataGridView to display the filtered results
                dataGridView1.Refresh();
            }
        }
    }

}

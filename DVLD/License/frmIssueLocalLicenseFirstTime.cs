using DVDL_Business;
using System;
using System.Windows.Forms;

namespace DVLD.License
{
    public partial class frmIssueLocalLicenseFirstTime : Form
    {
        public frmIssueLocalLicenseFirstTime()
        {
            InitializeComponent();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            //create new driver
            clsDrivers driver = new clsDrivers();
            driver.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            driver.CreatedDate = DateTime.Now;
            driver.PersonID = clsPerson.GetPersonIDbyHisName(ctrlDrivingLicenseApplicationInfo1.ApplicantFullName);
            if (driver.Save())
                MessageBox.Show("Driver Save");
            else
                MessageBox.Show("Driver Faild");



            //create new license for the driver

        }

        private void frmIssueLocalLicenseFirstTime_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseApplicationInfo1._ShowDrivingLicenseApplicationInfo(clsGlobal.L_DappID);
        }
    }
}

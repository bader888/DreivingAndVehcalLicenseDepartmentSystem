using DVDL_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD.License
{
    public partial class frmIssueLocalLicenseFirstTime : Form
    {

        int PersonID = -1;
        public frmIssueLocalLicenseFirstTime()
        {
            InitializeComponent();
        }

        private bool _IsPersonDriver()
        {
            PersonID = clsPerson.GetPersonIDbyHisName(ctrlDrivingLicenseApplicationInfo1.ApplicantFullName);
            return clsDrivers.IsPersonAsDriver(PersonID);
        }

        private DataRow _GetInfoForNewLicens()
        {
            DataTable dt = clsLicense.GetInfoForNewLicensebyL_DappID(clsGlobal.L_DappID);
            DataRow row = dt.Rows[0];
            return row;
        }

        private clsDrivers _CreateNewDriver()
        {
            clsDrivers driver = new clsDrivers();
            driver.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            driver.CreatedDate = DateTime.Now;
            driver.PersonID = PersonID;
            return driver;

        }

        private clsLicense _CreateNewLicense(int DriverID)
        {
            DataRow rNewLicenseInfo = _GetInfoForNewLicens();

            clsLicense license = new clsLicense();
            license.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            license.IssueDate = DateTime.Now;
            license.IssueReason = 1; //1 = first time
            license.Notes = txtNotes.Text;
            license.IsActive = true;
            license.DriverID = DriverID;
            license.ApplicationID = (int)rNewLicenseInfo["applicationid"];
            license.PaidFees = (decimal)rNewLicenseInfo["classfees"];
            license.LicenseClass = (int)rNewLicenseInfo["licenseclassid"];
            license.ExpirationDate = license.IssueDate.AddYears((byte)rNewLicenseInfo["DefaultValidityLength"]);

            return license;
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {

            if (_IsPersonDriver())
            {
                MessageBox.Show("person as Driver Exists you can't add it to driver table!");

            }
            else
            {
                clsDrivers driver = _CreateNewDriver();

                if (driver.Save())
                {
                    clsLicense license = _CreateNewLicense(driver.DriverID);

                    if (license.Save())
                    {
                        MessageBox.Show("the License Add Successfully with ID = " + license.LicenseID);
                        //update the status of application ==> Completed
                        clsApplications.UpdateApplicationStatus(clsGlobal.L_DappID, 3);

                    }
                    else
                        MessageBox.Show("Faild To Add New License");
                }
            }
        }

        private void frmIssueLocalLicenseFirstTime_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseApplicationInfo1._ShowDrivingLicenseApplicationInfo(clsGlobal.L_DappID);
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

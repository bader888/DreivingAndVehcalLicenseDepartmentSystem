using DVDL_Business;
using System.Data;
using System.Windows.Forms;

namespace DVLD.UserControls
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }
        public void _ShowDrivingLicenseApplicationInfo(int L_D_appID)
        {
            DataTable dt = clsCtrlDrivingLicenseBasicInfo.GetDrivingLicenseAppInfo(L_D_appID);
            DataRow row = dt.Rows[0];
            lblAppliedFor.Text = row["ClassName"].ToString();
            lblLocalDrivingLicenseApplicationID.Text = L_D_appID.ToString();
            lblPassedTests.Text = $"3/{row["PassedTestCount"].ToString()}";
            ctrlApplicationBasicInfo1._ShowBasicApplicationInfo(L_D_appID);

        }

    }
}

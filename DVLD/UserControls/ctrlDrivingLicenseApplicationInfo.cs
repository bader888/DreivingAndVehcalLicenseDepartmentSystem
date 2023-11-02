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
            DataTable dt = clsLocalDrivingLicenseApplications.GetDrivingLicenseAppInfo(L_D_appID);
            foreach (DataRow row in dt.Rows)
            {
                lblAppliedFor.Text = row["ClassName"].ToString();
                lblLocalDrivingLicenseApplicationID.Text = L_D_appID.ToString();

            }
            ctrlApplicationBasicInfo1._ShowBasicApplicationInfo(dt);

        }

    }
}

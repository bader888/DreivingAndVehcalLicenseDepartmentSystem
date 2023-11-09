using DVDL_Business.Global;
using System;
using System.Windows.Forms;

namespace DVLD.UserControls
{
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {
        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        public int DriverID { get; set; }
        public int PersonID { get; set; }



        // Define a custom event handler delegate with parameters
        public event Action<int> OnLicenseFound;
        // Create a protected method to raise the event with a parameter
        protected virtual void LicenseFound(int LicenseID)
        {
            Action<int> handler = OnLicenseFound;
            if (handler != null)
            {
                handler(LicenseID); // Raise the event with the parameter
            }
        }


        private void btnFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLicenseID.Text))
            {
                return;
            }


            if (ValidationHelper.StringValidator.ContainsNumbersOnly(txtLicenseID.Text))
            {
                int LicenseID = int.Parse(txtLicenseID.Text);
                if (ctrlShowLicenseInfo1.ShowLicenseInfo(LicenseID))
                {

                    this.PersonID = ctrlShowLicenseInfo1.PersonID;
                    this.DriverID = ctrlShowLicenseInfo1.DriverID;
                    if (OnLicenseFound != null)
                        // Raise the event with a parameter
                        OnLicenseFound(LicenseID);
                }
            }
        }
    }
}

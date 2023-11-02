using System;
using System.Windows.Forms;

namespace DVLD.Tests
{
    public partial class frmListTestAppointments : Form
    {
        int _L_D_LappID = -1;
        public frmListTestAppointments(int L_D_LappID)
        {
            InitializeComponent();

            _L_D_LappID = L_D_LappID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {

        }

        private void ctrlDrivingLicenseApplicationInfo1_Load(object sender, EventArgs e)
        {

        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {

            ctrlDrivingLicenseApplicationInfo1._ShowDrivingLicenseApplicationInfo(_L_D_LappID);
        }
    }
}

using DVDL_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD.UserControls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }
        int _PersonID = -1;

        public void _ShowBasicApplicationInfo(DataTable dt)
        {

            foreach (DataRow row in dt.Rows)
            {
                lblApplicant.Text = row["FullName"].ToString();
                lblFees.Text = row["PaidFees"].ToString();
                lblDate.Text = DateTime.Parse(row["ApplicationDate"].ToString()).ToShortDateString();
                lblStatusDate.Text = DateTime.Parse(row["LastStatusDate"].ToString()).ToShortDateString();
                lblStatus.Text = row["Status"].ToString();
                lblType.Text = row["ApplicationTypeTitle"].ToString();
                lblApplicationID.Text = row["ApplicationID"].ToString();
                lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

                _PersonID = int.Parse(row["PersonID"].ToString());
            }
        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(_PersonID);
            frm.ShowDialog();
        }
    }
}

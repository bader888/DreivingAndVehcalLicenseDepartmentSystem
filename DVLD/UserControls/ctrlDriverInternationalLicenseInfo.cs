using DVDL_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD.UserControls
{
    public partial class ctrlDriverInternationalLicenseInfo : UserControl
    {
        public ctrlDriverInternationalLicenseInfo()
        {

            InitializeComponent();

        }

        public void ShowInternationalDriverLicenseInfo(int InternationalID)
        {
            DataTable dt = clsInternationalLicenses.GetInternationalLicenseInfo(InternationalID);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                DateTime DateOfBirth = (DateTime)row["DateOfBirth"];
                DateTime ExpirationDate = (DateTime)row["ExpirationDate"];
                DateTime IssueDate = (DateTime)row["IssueDate"];
                string imgPath = row["ImagePath"].ToString();

                lblApplicationID.Text = row["ApplicationID"].ToString();
                lblFullName.Text = row["FullName"].ToString();
                lblGendor.Text = row["Gender"].ToString();
                lblInternationalLicenseID.Text = row["InternationalLicenseID"].ToString();
                lblIsActive.Text = row["IsActive"].ToString();
                lblDriverID.Text = row["DriverID"].ToString();
                lblNationalNo.Text = row["NationalNo"].ToString();
                lblIssueDate.Text = IssueDate.ToShortDateString();
                lblDateOfBirth.Text = DateOfBirth.ToShortDateString();
                lblExpirationDate.Text = ExpirationDate.ToShortDateString();
                pbPersonImage.Load(imgPath);
            }


        }

    }

}

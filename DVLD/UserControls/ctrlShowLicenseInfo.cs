using DVDL_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ctrlShowLicenseInfo : UserControl
    {
        public ctrlShowLicenseInfo()
        {
            InitializeComponent();
        }

        public int DriverID { get; set; }

        public int PersonID { get; set; }

        public string LicenseClass
        {
            get
            {
                return lblClass.Text;

            }
        }

        public int LicenseID { get; set; }

        private string GetIssueResone(string ReasonNumber)
        {
            switch (ReasonNumber)
            {
                case "1":
                    return "First Time";

                case "2":
                    return "Renew";
                case "3":
                    return "Replacement for Damaged";
                case "4":
                    return "Replacement for Lost";
            }
            return "???";
        }

        public void ShowDrivingLicenseInfo(int L_DappID)
        {
            DataTable dt = clsLicense.GetLicenseInfobyL_DappID(L_DappID);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                DateTime ExpirationDate = (DateTime)row["ExpirationDate"];
                DateTime DateOfBirth = (DateTime)row["DateOfBirth"];
                DateTime IssueDate = (DateTime)row["IssueDate"];
                lblNotes.Text = string.IsNullOrEmpty(row["Notes"].ToString()) ? "no Note" : row["Notes"].ToString();
                lblIsActive.Text = ((bool)row["IsActive"]) == true ? "Yes" : "No";
                lblIssueReason.Text = GetIssueResone(row["IssueReason"].ToString());
                lblGendor.Text = row["Gendor"].ToString() == "1" ? "Male" : "Female";
                lblExpirationDate.Text = ExpirationDate.ToShortDateString();
                lblDateOfBirth.Text = DateOfBirth.ToShortDateString();
                lblIssueDate.Text = IssueDate.ToShortDateString();
                lblNationalNo.Text = row["NationalNo"].ToString();
                lblLicenseID.Text = row["LicenseID"].ToString();
                pbPersonImage.Load(row["ImagePath"].ToString());
                lblFullName.Text = row["FullName"].ToString();
                lblDriverID.Text = row["DriverID"].ToString();
                lblClass.Text = row["ClassName"].ToString();
                if (lblGendor.Text == "Male")
                    pbGendor.Load(clsGlobal.MaleImagePath);
                else
                    pbGendor.Load(clsGlobal.FemaleImagePath);
            }
        }

        public bool ShowLicenseInfo(int LicenseID)
        {
            clsLicense License = clsLicense.Find(LicenseID);
            if (License == null)
            {
                MessageBox.Show("License not found. Please contact support for assistance.", "License Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
            DataTable dt = clsLicense.GetLicenseInfobyID(LicenseID);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                DateTime ExpirationDate = (DateTime)row["ExpirationDate"];
                DateTime DateOfBirth = (DateTime)row["DateOfBirth"];
                DateTime IssueDate = (DateTime)row["IssueDate"];
                lblNotes.Text = string.IsNullOrEmpty(row["Notes"].ToString()) ? "no Note" : row["Notes"].ToString();
                lblIsActive.Text = ((bool)row["IsActive"]) == true ? "Yes" : "No";
                lblIssueReason.Text = GetIssueResone(row["IssueReason"].ToString());
                lblGendor.Text = row["Gendor"].ToString() == "1" ? "Male" : "Female";
                lblExpirationDate.Text = ExpirationDate.ToShortDateString();
                lblDateOfBirth.Text = DateOfBirth.ToShortDateString();
                lblIssueDate.Text = IssueDate.ToShortDateString();
                lblNationalNo.Text = row["NationalNo"].ToString();
                lblLicenseID.Text = row["LicenseID"].ToString();
                pbPersonImage.Load(row["ImagePath"].ToString());
                lblFullName.Text = row["FullName"].ToString();
                lblDriverID.Text = row["DriverID"].ToString();
                lblClass.Text = row["ClassName"].ToString();
                lblIsDetained.Text = clsDetainedLicenses.IsLicenseDetain(LicenseID) ? "Yes" : "No";
                if (lblGendor.Text == "Male")
                    pbGendor.Load(clsGlobal.MaleImagePath);
                else
                    pbGendor.Load(clsGlobal.FemaleImagePath);
                this.DriverID = (int)row["DriverID"];
                this.PersonID = (int)row["PersonID"];
                this.LicenseID = LicenseID;

                return true;
            }

            return false;

        }
    }
}

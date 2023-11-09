using DVDL_DataAccess;
using System;
using System.Data;

namespace DVDL_Business
{
    public class clsInternationalLicenses
    {
        public enum enMode { AddNew = 0, Update = 1 };

        public enMode Mode = enMode.AddNew;

        public int InternationalLicenseID { set; get; }

        public int ApplicationID { set; get; }

        public int DriverID { set; get; }

        public int IssuedUsingLocalLicenseID { set; get; }

        public DateTime IssueDate { set; get; }

        public DateTime ExpirationDate { set; get; }

        public bool IsActive { set; get; }

        public int CreatedByUserID { set; get; }

        private clsInternationalLicenses(int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
            Mode = enMode.Update;
        }

        public clsInternationalLicenses()
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = false;
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }

        public static clsInternationalLicenses Find(int InternationalLicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;

            if (clsInternationalLicensesData.GetInternationalLicensesInfoByID(
             ref InternationalLicenseID,
            ref ApplicationID,
            ref DriverID,
            ref IssuedUsingLocalLicenseID,
            ref IssueDate,
            ref ExpirationDate,
            ref IsActive,
            ref CreatedByUserID
             ))
                return new clsInternationalLicenses(
                InternationalLicenseID,
                ApplicationID,
                DriverID,
                IssuedUsingLocalLicenseID,
                IssueDate,
                ExpirationDate,
                IsActive,
                CreatedByUserID
                );
            else
                return null;
        }
        private bool _AddNewInternationalLicenses()
        {
            this.InternationalLicenseID = clsInternationalLicensesData.AddNewInternationalLicenses(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
            return (this.InternationalLicenseID != -1);
        }

        private bool _UpdateInternationalLicenses()
        {
            return clsInternationalLicensesData.UpdateInternationalLicenses(this.InternationalLicenseID, this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
        }

        public static bool DeleteInternationalLicenses(int ID)
        {
            return clsInternationalLicensesData.DeleteInternationalLicenses(ID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicenses())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateInternationalLicenses();

            }
            return false;
        }

        public static bool isInternationalLicensesExist(int ID)
        {
            return clsInternationalLicensesData.IsInternationalLicensesExist(ID);
        }

        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicensesData.GetAllInternationalLicenses();
        }
    }
}

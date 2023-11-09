using DVDL_DataAccess;
using System;
using System.Data;

namespace DVDL_Business
{
    public class clsLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int LicenseID { set; get; }
        public int ApplicationID { set; get; }
        public int DriverID { set; get; }
        public int LicenseClass { set; get; }
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public string Notes { set; get; }
        public decimal PaidFees { set; get; }
        public bool IsActive { set; get; }
        public byte IssueReason { set; get; }
        public int CreatedByUserID { set; get; }
        private clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;
            Mode = enMode.Update;
        }
        public clsLicense()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = " ";
            this.PaidFees = 0;
            this.IsActive = false;
            this.IssueReason = 0;
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }
        public static clsLicense Find(int LicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees = 0;
            bool IsActive = false;
            byte IssueReason = 0;
            int CreatedByUserID = -1;

            if (clsLicenseData.GetLicensesInfoByID(
             ref LicenseID,
            ref ApplicationID,
            ref DriverID,
            ref LicenseClass,
            ref IssueDate,
            ref ExpirationDate,
            ref Notes,
            ref PaidFees,
            ref IsActive,
            ref IssueReason,
            ref CreatedByUserID
             ))
                return new clsLicense(
                LicenseID,
                ApplicationID,
                DriverID,
                LicenseClass,
                IssueDate,
                ExpirationDate,
                Notes,
                PaidFees,
                IsActive,
                IssueReason,
                CreatedByUserID
                );
            else
                return null;
        }
        private bool _AddNewLicenses()
        {
            this.LicenseID = clsLicenseData.AddNewLicenses(this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID);
            return (this.LicenseID != -1);
        }
        private bool _UpdateLicenses()
        {
            return clsLicenseData.UpdateLicenses(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID);
        }
        public static bool DeleteLicenses(int ID)
        {
            return clsLicenseData.DeleteLicenses(ID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicenses())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else { return false; }
                case enMode.Update:
                    return _UpdateLicenses();

            }
            return false;
        }
        public static bool isLicensesExist(int ID)
        {
            return clsLicenseData.IsLicensesExist(ID);
        }
        public DataTable GetAllLicenses()
        {
            return clsLicenseData.GetAllLicenses();
        }

        //new
        public static DataTable GetInfoForNewLicensebyL_DappID(int L_DappID)
        {
            return clsLicenseData.GetInfoForNewLicensebyL_DappID(L_DappID);
        }

        public static DataTable GetLicenseInfobyL_DappID(int L_DappID)
        {
            return clsLicenseData.GetLicenseInfobyL_DappID(L_DappID);
        }

        public static DataTable GetLicenseInfobyID(int LicenseID)
        {
            return clsLicenseData.GetLicenseInfobyID(LicenseID);
        }

    }
}

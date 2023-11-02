using DVDL_DataAccess;
using System;
using System.Data;

namespace DVDL_Business
{
    public class clsApplications
    {
        public enum enMode { AddNew = 0, Update = 1 };

        public enMode Mode = enMode.AddNew;
        public int ApplicationID { set; get; }
        public int ApplicantPersonID { set; get; }
        public DateTime ApplicationDate { set; get; }
        public int ApplicationTypeID { set; get; }
        public byte ApplicationStatus { set; get; }
        public DateTime LastStatusDate { set; get; }
        public decimal PaidFees { set; get; }
        public int CreatedByUserID { set; get; }
        private clsApplications(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            Mode = enMode.Update;
        }

        public clsApplications()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = 1;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }

        public static clsApplications Find(int ApplicationsID)
        {
            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.Now;
            int ApplicationTypeID = -1;
            byte ApplicationStatus = 1;
            DateTime LastStatusDate = DateTime.Now;
            decimal PaidFees = -1;
            int CreatedByUserID = -1;

            if (clsApplicationsData.GetApplicationsInfoByID(
             ref ApplicationsID,
            ref ApplicantPersonID,
            ref ApplicationDate,
            ref ApplicationTypeID,
            ref ApplicationStatus,
            ref LastStatusDate,
            ref PaidFees,
            ref CreatedByUserID
             ))
                return new clsApplications(
                ApplicationsID,
                ApplicantPersonID,
                ApplicationDate,
                ApplicationTypeID,
                ApplicationStatus,
                LastStatusDate,
                PaidFees,
                CreatedByUserID
                );
            else
                return null;
        }

        private bool _AddNewApplications()
        {
            this.ApplicationID = clsApplicationsData.AddNewApplications(this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
            return (this.ApplicationID != -1);
        }

        static public clsApplications Find(int ApplicantPersonID, int ApplicationTypeID)
        {
            int ApplicationsID = -1;
            DateTime ApplicationDate = DateTime.Now;
            byte ApplicationStatus = 1;
            DateTime LastStatusDate = DateTime.Now;
            decimal PaidFees = -1;
            int CreatedByUserID = -1;

            if (clsApplicationsData.GetAppByIDandAppTypeID(
             ref ApplicationsID,
            ref ApplicantPersonID,
            ref ApplicationDate,
            ref ApplicationTypeID,
            ref ApplicationStatus,
            ref LastStatusDate,
            ref PaidFees,
            ref CreatedByUserID
             ))
                return new clsApplications(
                ApplicationsID,
                ApplicantPersonID,
                ApplicationDate,
                ApplicationTypeID,
                ApplicationStatus,
                LastStatusDate,
                PaidFees,
                CreatedByUserID
                );
            else
                return null;

        }

        private bool _UpdateApplications()
        {
            return clsApplicationsData.UpdateApplications(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }

        public static bool DeleteApplications(int ID)
        {
            return clsApplicationsData.DeleteApplications(ID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplications())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateApplications();

            }
            return false;
        }

        public static bool isApplicationsExist(int ID)
        {
            return clsApplicationsData.IsApplicationsExist(ID);
        }

        public static bool isApplicationExist(int ApplicantPersonID, int LicenseClassID)
        {
            return clsApplicationsData.IsApplicationExist(ApplicantPersonID, LicenseClassID);
        }

        static public DataTable GetAllApplications()
        {
            return clsApplicationsData.GetAllApplications();
        }
    }
}

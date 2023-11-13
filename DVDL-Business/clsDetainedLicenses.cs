using DVDL_DataAccess;
using System;
using System.Data;

namespace DVDL_Business
{
    public class clsDetainedLicenses
    {
        public enum enMode { AddNew = 0, Update = 1 };

        public enMode Mode = enMode.AddNew;

        public int DetainID { set; get; }

        public int LicenseID { set; get; }

        public DateTime DetainDate { set; get; }

        public decimal FineFees { set; get; }

        public int CreatedByUserID { set; get; }

        public bool IsReleased { set; get; }

        public DateTime ReleaseDate { set; get; }

        public int ReleasedByUserID { set; get; }

        public int ReleaseApplicationID { set; get; }

        private clsDetainedLicenses(int DetainID, int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
            Mode = enMode.Update;
        }

        public clsDetainedLicenses()
        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ReleaseDate = DateTime.Now;
            this.ReleasedByUserID = -1;
            this.ReleaseApplicationID = -1;
            Mode = enMode.AddNew;
        }

        public static clsDetainedLicenses Find(int DetainID)
        {
            int LicenseID = -1;
            DateTime DetainDate = DateTime.Now;
            decimal FineFees = -1;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime ReleaseDate = DateTime.Now;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;

            if (clsDetainedLicensesData.GetDetainedLicenseInfoByID(
             ref DetainID,
            ref LicenseID,
            ref DetainDate,
            ref FineFees,
            ref CreatedByUserID,
            ref IsReleased,
            ref ReleaseDate,
            ref ReleasedByUserID,
            ref ReleaseApplicationID
             ))
                return new clsDetainedLicenses(
                DetainID,
                LicenseID,
                DetainDate,
                FineFees,
                CreatedByUserID,
                IsReleased,
                ReleaseDate,
                ReleasedByUserID,
                ReleaseApplicationID
                );
            else
                return null;
        }

        public static clsDetainedLicenses FindByLicenseID(int LicenseID)
        {
            int DetainID = -1;
            DateTime DetainDate = DateTime.Now;
            decimal FineFees = -1;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime ReleaseDate = DateTime.Now;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;

            if (clsDetainedLicensesData.GetDetainedLicenseInfoByLicenseID(
             ref DetainID,
            ref LicenseID,
            ref DetainDate,
            ref FineFees,
            ref CreatedByUserID,
            ref IsReleased,
            ref ReleaseDate,
            ref ReleasedByUserID,
            ref ReleaseApplicationID
             ))
                return new clsDetainedLicenses(
                DetainID,
                LicenseID,
                DetainDate,
                FineFees,
                CreatedByUserID,
                IsReleased,
                ReleaseDate,
                ReleasedByUserID,
                ReleaseApplicationID
                );
            else
                return null;
        }




        private bool _AddNewDetainedLicenses()
        {
            this.DetainID = clsDetainedLicensesData.AddNewDetainedLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);
            return (this.DetainID != -1);
        }

        private bool _UpdateDetainedLicenses()
        {
            return clsDetainedLicensesData.UpdateDetainedLicense(this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);
        }

        public static bool DeleteDetainedLicenses(int ID)
        {
            return clsDetainedLicensesData.DeleteDetainedLicenses(ID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainedLicenses())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateDetainedLicenses();

            }
            return false;
        }

        public static bool isDetainedLicensesExist(int ID)
        {
            return clsDetainedLicensesData.IsDetainedLicenseExist(ID);
        }

        static public DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicensesData.GetAllDetainedLicense();
        }

        public static bool IsLicenseDetain(int LicenseID)
        {
            return clsDetainedLicensesData.IsLicenseDetain(LicenseID);
        }

        public static bool DetainLicense(int LicenseID)
        {
            return clsDetainedLicensesData.DetainLicense(LicenseID);
        }

        public static bool ReleaseLicense(int LicenseID)
        {
            return clsDetainedLicensesData.ReleaseLicense(LicenseID);
        }

    }
}

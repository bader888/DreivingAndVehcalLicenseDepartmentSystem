using DVDL_DataAccess;
using System.Data;

namespace DVDL_Business
{
    public class clsLocalDrivingLicenseApplications
    {
        public enum enMode { AddNew = 0, Update = 1 };

        public enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { set; get; }

        public int ApplicationID { set; get; }

        public int LicenseClassID { set; get; }

        private clsLocalDrivingLicenseApplications(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;
            Mode = enMode.Update;
        }

        public clsLocalDrivingLicenseApplications()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.ApplicationID = -1;
            this.LicenseClassID = -1;
            Mode = enMode.AddNew;
        }

        public static clsLocalDrivingLicenseApplications Find(int LocalDrivingLicenseApplicationsID)
        {
            int ApplicationID = -1;
            int LicenseClassID = -1;

            if (clsLocalDrivingLicenseApplicationsData.GetLocalDrivingLicenseApplicationsInfoByID(
             ref LocalDrivingLicenseApplicationsID,
            ref ApplicationID,
            ref LicenseClassID
             ))
                return new clsLocalDrivingLicenseApplications(
                LocalDrivingLicenseApplicationsID,
                ApplicationID,
                LicenseClassID
                );
            else
                return null;
        }

        public static clsLocalDrivingLicenseApplications GetLocalDrivingLicenseApplicationsbyAppID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationsID = -1;
            int LicenseClassID = -1;

            if (clsLocalDrivingLicenseApplicationsData.GetLocalDrivingLicenseApplicationsInfoByID(
             ref LocalDrivingLicenseApplicationsID,
            ref ApplicationID,
            ref LicenseClassID
             ))
                return new clsLocalDrivingLicenseApplications(
                LocalDrivingLicenseApplicationsID,
                ApplicationID,
                LicenseClassID
                );
            else
                return null;
        }


        private bool _AddNewLocalDrivingLicenseApplications()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationsData.AddNewLocalDrivingLicenseApplications(this.LicenseClassID, this.ApplicationID);
            return (this.LocalDrivingLicenseApplicationID != -1);
        }

        private bool _UpdateLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationsData.UpdateLocalDrivingLicenseApplications(this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
        }

        public static bool DeleteLocalDrivingLicenseApplications(int ID)
        {
            return clsLocalDrivingLicenseApplicationsData.DeleteLocalDrivingLicenseApplications(ID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplications())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else { return false; }
                case enMode.Update:
                    return _UpdateLocalDrivingLicenseApplications();

            }
            return false;
        }

        public static bool isLocalDrivingLicenseApplicationsExist(int ID)
        {
            return clsLocalDrivingLicenseApplicationsData.IsLocalDrivingLicenseApplicationsExist(ID);
        }

        static public DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationsData.GetAllLocalDrivingLicenseApplications();
        }

    }

}

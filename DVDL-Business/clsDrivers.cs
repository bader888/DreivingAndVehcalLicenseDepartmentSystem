using DVDL_DataAccess;
using System;
using System.Data;

namespace DVDL_Business
{
    public class clsDrivers
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int DriverID { set; get; }
        public int PersonID { set; get; }
        public int CreatedByUserID { set; get; }
        public DateTime CreatedDate { set; get; }
        private clsDrivers(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            Mode = enMode.Update;
        }
        public clsDrivers()
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
            Mode = enMode.AddNew;
        }
        public static clsDrivers Find(int DriverID)
        {
            int PersonID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (clsDriversData.GetDriversInfoByID(
             ref DriverID,
            ref PersonID,
            ref CreatedByUserID,
            ref CreatedDate
             ))
                return new clsDrivers(
                DriverID,
                PersonID,
                CreatedByUserID,
                CreatedDate
                );
            else
                return null;
        }
        private bool _AddNewDrivers()
        {
            this.DriverID = clsDriversData.AddNewDrivers(this.PersonID, this.CreatedByUserID, this.CreatedDate);
            return (this.DriverID != -1);
        }
        private bool _UpdateDrivers()
        {
            return clsDriversData.UpdateDrivers(this.DriverID, this.PersonID, this.CreatedByUserID, this.CreatedDate);
        }
        public static bool DeleteDrivers(int ID)
        {
            return clsDriversData.DeleteDrivers(ID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDrivers())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateDrivers();

            }
            return false;
        }
        public static bool IsPersonAsDriver(int PersonID)
        {
            return clsDriversData.IsPersonAsDriver(PersonID);
        }
        public DataTable GetAllDrivers()
        {
            return clsDriversData.GetAllDrivers();
        }


        public static int GetDriverIDByHisName(string DriverName)
        {
            return clsDriversData.GetDriverIDByHisName(DriverName);
        }
    }
}

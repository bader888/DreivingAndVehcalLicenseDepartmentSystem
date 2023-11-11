using DVDL_DataAccess;
using System.Data;

namespace DVDL_Business
{
    public class clsApplicationType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int ApplicationTypeID { set; get; }
        public string ApplicationTypeTitle { set; get; }
        public decimal ApplicationFees { set; get; }
        private clsApplicationType(int ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicationFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
            Mode = enMode.Update;
        }
        public clsApplicationType()
        {
            this.ApplicationTypeID = -1;
            this.ApplicationTypeTitle = " ";
            this.ApplicationFees = 0;

        }
        public static clsApplicationType Find(int ApplicationTypesID)
        {
            string ApplicationTypeTitle = " ";
            decimal ApplicationFees = 0;

            if (clsApplicationTypeData.GetApplicationTypesInfoByID(
             ref ApplicationTypesID,
            ref ApplicationTypeTitle,
            ref ApplicationFees
             ))
                return new clsApplicationType(
                ApplicationTypesID,
                ApplicationTypeTitle,
                ApplicationFees
                );
            else
                return null;
        }

        private bool _UpdateApplicationTypes()
        {
            return clsApplicationTypeData.UpdateApplicationTypes(this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationFees);
        }

        static public int GetApplicationTypeIDbyName(string ApplicationTypeName)
        {
            return clsApplicationTypeData.GetApplicationTypeIDbyName(ApplicationTypeName);
        }

        static public decimal GetApplicationTypeFeesbyName(string ApplicationTypeName)
        {
            return clsApplicationTypeData.GetApplicationTypeFeesbyName(ApplicationTypeName);
        }
        public bool Save()
        {
            return _UpdateApplicationTypes();


        }
        static public DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }
    }
}

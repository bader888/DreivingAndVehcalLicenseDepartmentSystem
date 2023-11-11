using DVDL_DataAccess;
using System.Data;

namespace DVDL_Business
{
    public class clsLicenseClasses
    {

        static public DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassesData.GetAllLicenseClasses();
        }

        static public int GetLicenseClassIDbyName(string LicenseClassName)
        {
            return clsLicenseClassesData.GetLicenseClassIDbyName(LicenseClassName);
        }
        static public decimal GetLicenseClassFeesByName(string LicenseClassName)
        {
            return clsLicenseClassesData.GetLicenseClassFeesByName(LicenseClassName);
        }

    }
}

using DVDL_DataAccess;
using System.Data;

namespace DVDL_Business
{
    public class clsCtrlDrivingLicenseBasicInfo
    {
        static public DataTable GetDrivingLicenseAppInfo(int L_D_LappID)
        {
            return clsCtrlDrivingLicenseBasicInfoData.GetDrivingLicenseAppInfo(L_D_LappID);
        }

        static public DataTable GetApplicationBasicInfo(int L_D_LappID)
        {
            return clsCtrlDrivingLicenseBasicInfoData.GetApplicationBasicInfo(L_D_LappID);
        }


    }
}

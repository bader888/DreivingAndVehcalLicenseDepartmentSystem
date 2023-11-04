using DVDL_DataAccess.UserControlData;
using System.Data;

namespace DVDL_Business.UserControlLogic
{
    public class clsCtrlSchduledTest
    {
        static public DataTable GetSchduledTestInfo(int TestAppointmentID)
        {
            return clsCtrlSchduledTestData.GetSchduledTestInfo(TestAppointmentID);
        }
    }
}

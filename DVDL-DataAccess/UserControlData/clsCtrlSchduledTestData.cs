using System.Data;

namespace DVDL_DataAccess.UserControlData
{
    public class clsCtrlSchduledTestData
    {
        static public DataTable GetSchduledTestInfo(int TestAppointment)
        {
            return clsTestAppointmentsData.GetTestAppointmentByID(TestAppointment);
        }
    }
}

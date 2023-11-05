using DVDL_DataAccess;
using System.Data;

namespace DVDL_Business
{
    public class clsTests
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int TestID { set; get; }
        public int TestAppointmentID { set; get; }
        public bool TestResult { set; get; }
        public string Notes { set; get; }
        public int CreatedByUserID { set; get; }
        private clsTests(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
            Mode = enMode.Update;
        }

        public clsTests()
        {
            this.TestID = 1;
            this.TestAppointmentID = 1;
            this.TestResult = false;
            this.Notes = "";
            this.CreatedByUserID = 1;
            Mode = enMode.AddNew;
        }

        public static clsTests Find(int TestID)
        {
            int TestAppointmentID = 1;
            bool TestResult = false;
            string Notes = "";
            int CreatedByUserID = 1;

            if (clsTestsData.GettestsInfoByID(
             ref TestID,
            ref TestAppointmentID,
            ref TestResult,
            ref Notes,
            ref CreatedByUserID
             ))
                return new clsTests(
                TestID,
                TestAppointmentID,
                TestResult,
                Notes,
                CreatedByUserID
                );
            else
                return null;
        }

        private bool _AddNewtests()
        {
            this.TestID = clsTestsData.AddNewtests(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
            return (this.TestID != -1);
        }

        private bool _Updatetests()
        {
            return clsTestsData.Updatetests(this.TestID, this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
        }

        public static bool Deletetests(int ID)
        {
            return clsTestsData.Deletetests(ID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewtests())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else { return false; }
                case enMode.Update:
                    return _Updatetests();

            }
            return false;
        }

        public static bool istestsExist(int ID)
        {
            return clsTestsData.IstestsExist(ID);
        }

        public DataTable GetAlltests()
        {
            return clsTestsData.GetAlltests();
        }

        public static bool IsTestPass(int TestAppointmentID)
        {
            return clsTestsData.IsTestPass(TestAppointmentID);
        }
    }
}

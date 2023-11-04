using DVDL_DataAccess;
using System;
using System.Data;

namespace DVDL_Business
{
    public class clsTestAppointments
    {
        public enum enMode { AddNew = 0, Update = 1 };

        public enMode Mode = enMode.AddNew;

        public int TestAppointmentID { set; get; }

        public int TestTypeID { set; get; }

        public int LocalDrivingLicenseApplicationID { set; get; }

        public DateTime AppointmentDate { set; get; }

        public decimal PaidFees { set; get; }

        public int CreatedByUserID { set; get; }

        public bool IsLocked { set; get; }

        private clsTestAppointments(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            Mode = enMode.Update;
        }

        public clsTestAppointments()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = -1;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.IsLocked = false;
            Mode = enMode.AddNew;
        }

        public static clsTestAppointments Find(int LocalDrivingLicenseApplicationID)
        {
            int TestAppointmentsID = -1;
            int TestTypeID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = 0;
            int CreatedByUserID = -1;
            bool IsLocked = false;

            if (clsTestAppointmentsData.GetTestAppointmentInfoID(
             ref TestAppointmentsID,
            ref TestTypeID,
            ref LocalDrivingLicenseApplicationID,
            ref AppointmentDate,
            ref PaidFees,
            ref CreatedByUserID,
            ref IsLocked
             ))
                return new clsTestAppointments(
                TestAppointmentsID,
                TestTypeID,
                LocalDrivingLicenseApplicationID,
                AppointmentDate,
                PaidFees,
                CreatedByUserID,
                IsLocked
                );
            else
                return null;
        }

        private bool _AddNewTestAppointments()
        {
            this.TestAppointmentID = clsTestAppointmentsData.AddNewTestAppointments(this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked);
            return (this.TestAppointmentID != -1);
        }

        private bool _UpdateTestAppointments()
        {
            return clsTestAppointmentsData.UpdateTestAppointments(
                this.TestAppointmentID,
                this.TestTypeID,
                this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate,
                this.PaidFees,
                this.CreatedByUserID,
                this.IsLocked
                );
        }

        public static bool DeleteTestAppointments(int ID)
        {
            return clsTestAppointmentsData.DeleteTestAppointments(ID);
        }

        static public bool IsPersonHaveActiveTest(int PersonID)
        {
            return clsTestAppointmentsData.IsPersonHaveActiveTest(PersonID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNewTestAppointments())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateTestAppointments();

            }
            return false;
        }

        public static bool isTestAppointmentsExist(int ID)
        {
            return clsTestAppointmentsData.IsTestAppointmentsExist(ID);
        }

        static public DataTable GetTestAppointmentForSpecificTest(int L_DappID, string TestTypeTitle)
        {
            return clsTestAppointmentsData.GetTestAppointmentForSpecificTest(L_DappID, TestTypeTitle);
        }

        static public DataTable GetTestAppointmentByID(int TestAppointmentID)
        {
            return clsTestAppointmentsData.GetTestAppointmentByID(TestAppointmentID);
        }


    }
}

using DVDL_DataAccess;
using System.Data;

namespace DVDL_Business
{
    public class clsTestType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int TestTypeID { set; get; }
        public string TestTypeTitle { set; get; }
        public string TestTypeDescription { set; get; }
        public decimal TestTypeFees { set; get; }
        private clsTestType(int TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;

        }
        public clsTestType()
        {
            this.TestTypeID = -1;
            this.TestTypeTitle = "";
            this.TestTypeDescription = "";
            this.TestTypeFees = 0;

        }
        public static clsTestType Find(int TestTypesID)
        {
            string TestTypeTitle = "";
            string TestTypeDescription = "";
            decimal TestTypeFees = 0;

            if (clsTestTypeData.GetTestTypesInfoByID(
             ref TestTypesID,
            ref TestTypeTitle,
            ref TestTypeDescription,
            ref TestTypeFees
             ))
                return new clsTestType(
                TestTypesID,
                TestTypeTitle,
                TestTypeDescription,
                TestTypeFees
                );
            else
                return null;
        }
        private bool _UpdateTestTypes()
        {
            return clsTestTypeData.UpdateTestTypes(this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);
        }
        public bool Save()
        {
            return _UpdateTestTypes();
        }
        static public DataTable GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTestTypes();
        }

        public static decimal GetTestTypeFeesByTitle(string Title)
        {
            return clsTestTypeData.GetTestTypeFeesByTitle(Title);
        }

    }
}

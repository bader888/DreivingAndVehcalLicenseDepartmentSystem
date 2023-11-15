using DVDL_DataAccess;

namespace DVDL_Business
{
    public class clsGlobal
    {
        static public string filePath = @"C:\Users\lenovo\source\repos\DreivingAndVehcalLicenseDepartmentSystem\UserAuthentication.txt"; // Path to the file containing user credentials
        static public string FemaleImagePath = "C:\\DVLDImages\\Female 512.png";
        static public string MaleImagePath = "C:\\DVLDImages\\Male 512.png";
        static public string DrivePath = @"C:\DVLDImages";

        public class clsImages
        {
            static public string Vision
            {
                get
                {
                    return @"C:\Users\lenovo\source\repos\DreivingAndVehcalLicenseDepartmentSystem\Icons\Vision 512.png";
                }
            }

            static public string Written
            {
                get
                {
                    return @"C:\Users\lenovo\source\repos\DreivingAndVehcalLicenseDepartmentSystem\Icons\Written Test 512.png";
                }
            }

            static public string Parctical
            {
                get
                {
                    return @"C:\Users\lenovo\source\repos\DreivingAndVehcalLicenseDepartmentSystem\Icons\driving-test 512.png";
                }
            }

            static public string Male
            {
                get
                {
                    return @"C:\Users\lenovo\source\repos\DreivingAndVehcalLicenseDepartmentSystem\Icons\Man 32.png";
                }
            }

            static public string Female
            {
                get
                {
                    return @"C:\Users\lenovo\source\repos\DreivingAndVehcalLicenseDepartmentSystem\Icons\Woman 32.png";
                }
            }
        }

        static public string TestType { get; set; }

        static public clsUsers CurrentUser { get; set; }

        static public int L_DappID { get; set; }

        static public int RecordCount(string TableName)
        {
            return clsRecordCount.CountRecords(TableName);
        }

    }
}

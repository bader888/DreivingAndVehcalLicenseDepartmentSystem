namespace DVDL_Business
{
    public class clsGlobal
    {
        static public string MaleImagePath = "C:\\DVLDImages\\Male 512.png";
        static public string FemaleImagePath = "C:\\DVLDImages\\Female 512.png";
        static public string DrivePath = @"C:\DVLDImages";
        static public string filePath = @"C:\Users\lenovo\source\repos\DreivingAndVehcalLicenseDepartmentSystem\UserAuthentication.txt"; // Path to the file containing user credentials

        static public clsUsers CurrentUser { get; set; }

    }
}

using DVDL_DataAccess;
using System.Data;

namespace DVLD.People
{
    public class clsCountries
    {
        public static int GetCountryIDbyName(string CountryName)
        {
            return clsCountriesData.GetCountryIDbyName(CountryName);
        }
        public static string GetCountryNameByID(int CountryID)
        {
            return clsCountriesData.GetCountryNameByID(CountryID);
        }
        static public DataTable GetAllCountries()
        {
            return clsCountriesData.GetAllCountries();
        }
    }
}

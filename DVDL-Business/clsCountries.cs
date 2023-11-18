using DVDL_DataAccess;
using System.Data;

namespace DVLD.People
{
    public class clsCountries
    {
        public int ID { set; get; }

        public string CountryName { set; get; }

        public clsCountries()

        {
            this.ID = -1;
            this.CountryName = "";

        }

        private clsCountries(int ID, string CountryName)

        {
            this.ID = ID;
            this.CountryName = CountryName;
        }

        public static clsCountries Find(int ID)
        {
            string CountryName = "";

            if (clsCountriesData.GetCountryInfoByID(ID, ref CountryName))

                return new clsCountries(ID, CountryName);
            else
                return null;

        }

        public static clsCountries Find(string CountryName)
        {

            int ID = -1;

            if (clsCountriesData.GetCountryInfoByName(CountryName, ref ID))

                return new clsCountries(ID, CountryName);
            else
                return null;

        }

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

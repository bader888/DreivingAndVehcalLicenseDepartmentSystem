using System;
using System.Data;
using System.Data.SqlClient;

namespace DVDL_DataAccess
{
    public class clsLicenseClassesData
    {
        public static DataTable GetAllLicenseClasses()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = "SELECT * FROM LicenseClasses";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static int GetLicenseClassIDbyName(string LicenseClassName)
        {
            int LicenseClassID = -1; // Default value in case the row name is not found

            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = @"select LicenseClasses.LicenseClassID from LicenseClasses where LicenseClasses.ClassName =  @LicenseClassName ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassName", LicenseClassName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    LicenseClassID = (int)reader["LicenseClassID"];
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return LicenseClassID;
        }

        static public decimal GetLicenseClassFeesByName(string LicenseClassName)
        {
            decimal LicenseClassFees = -1; // Default value in case the row name is not found

            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = @"select ClassFees from LicenseClasses where LicenseClasses.ClassName =  @LicenseClassName ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassName", LicenseClassName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    LicenseClassFees = (decimal)reader["ClassFees"];
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return LicenseClassFees;
        }
    }
}

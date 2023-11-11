using System;
using System.Data;
using System.Data.SqlClient;

namespace DVDL_DataAccess
{
    public class clsApplicationTypeData
    {
        public static int GetApplicationTypeIDbyName(string ApplicationTypeName)
        {
            int ApplicationTypeID = -1; // Default value in case the row name is not found

            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = "SELECT ApplicationTypeID FROM ApplicationTypes WHERE ApplicationTypeTitle LIKE @ApplicationTypeName;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeName", "%" + ApplicationTypeName + "%");

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
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

            return ApplicationTypeID;
        }

        public static DataTable GetAllApplicationTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = "SELECT * FROM ApplicationTypes";
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

        public static bool GetApplicationTypesInfoByID(ref int ApplicationTypeID, ref string ApplicationTypeTitle, ref decimal ApplicationFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypesID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypesID", ApplicationTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                    ApplicationFees = (decimal)reader["ApplicationFees"];

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool UpdateApplicationTypes(int ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicationFees)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = "Update ApplicationTypes set ApplicationTypeTitle = @ApplicationTypeTitle, ApplicationFees = @ApplicationFees  WHERE ApplicationTypeID = @ApplicationTypeID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }


        static public decimal GetApplicationTypeFeesbyName(string ApplicationTypeName)
        {

            decimal ApplicationTypeID = -1; // Default value in case the row name is not found

            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = @"select ApplicationFees FROM ApplicationTypes WHERE ApplicationTypeTitle LIKE @ApplicationTypeName;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeName", "%" + ApplicationTypeName + "%");

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ApplicationTypeID = (decimal)reader["ApplicationFees"];
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

            return ApplicationTypeID;
        }


    }
}




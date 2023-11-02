using System;
using System.Data;
using System.Data.SqlClient;

namespace DVDL_DataAccess
{
    public class clsLocalDrivingLicenseApplicationsData
    {
        public static DataTable GetDrivingLicenseAppInfo(int L_D_LAppID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = @"SELECT *
                            FROM (
                                SELECT
                                    LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID AS L_D_LappID,
                                    PaidFees,
                                    LastStatusDate,
                                    LocalDrivingLicenseApplications.ApplicationID,
                                    LicenseClasses.ClassName,
                                    People.NationalNo,
                                    People.FirstName + ' ' +
                                    People.SecondName + ' ' +
                                    People.ThirdName + ' ' +
                                    People.LastName AS FullName,
                                    People.PersonID,
                                    Applications.ApplicationDate,
                                    CASE
                                        WHEN Applications.ApplicationStatus = 1 THEN 'new'
                                        WHEN Applications.ApplicationStatus = 2 THEN 'completed'
                                        WHEN Applications.ApplicationStatus = 3 THEN 'cancelled'
                                        ELSE 'unknown' -- Handle other values, if necessary
                                    END AS Status,
                                    applicationTypes.ApplicationTypeTitle
                                FROM Applications
                                INNER JOIN LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                                INNER JOIN LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID
                                INNER JOIN People ON Applications.ApplicantPersonID = People.PersonID
                                LEFT JOIN applicationTypes ON Applications.ApplicationTypeID = applicationTypes.ApplicationTypeID
                            ) Result
                            WHERE L_D_LappID = @L_D_LappID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@L_D_LAppID", L_D_LAppID);

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

        public static int AddNewLocalDrivingLicenseApplications(int LicenseClassID, int ApplicationID)
        {
            int LocalDrivingLicenseApplicationsID = -1;
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = "INSERT INTO LocalDrivingLicenseApplications (ApplicationID, LicenseClassID ) VALUES (@ApplicationID, @LicenseClassID ); SELECT SCOPE_IDENTITY();"
            ; SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LocalDrivingLicenseApplicationsID = insertedID;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return LocalDrivingLicenseApplicationsID;
        }

        public static bool DeleteLocalDrivingLicenseApplications(int LocalDrivingLicenseApplicationID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = @"Delete LocalDrivingLicenseApplications where LocalDrivingLicenseApplicationID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", LocalDrivingLicenseApplicationID);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0);
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = @"
                            SELECT LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID as L_D_LappID,  
                            LicenseClasses.ClassName,
                            People.NationalNo, 
                            People.FirstName + ' '+ 
                            People.SecondName  + ' '+ 
                            People.ThirdName + ' '+
                            People.LastName as FullName, 
                            Applications.ApplicationDate,  
                            CASE
                              WHEN Applications.ApplicationStatus = 1 THEN 'new'
                              WHEN Applications.ApplicationStatus = 2 THEN 'completed'
                              WHEN Applications.ApplicationStatus = 3 THEN 'cancelled'
                              ELSE 'unknown' -- Handle other values, if necessary
                            END AS Status 
                            FROM Applications INNER JOIN
                            LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID INNER JOIN
                            LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID INNER JOIN
                            People ON Applications.ApplicantPersonID = People.PersonID";
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

        public static bool IsLocalDrivingLicenseApplicationsExist(int LocalDrivingLicenseApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = "SELECT Found=1 FROM  LocalDrivingLicenseApplications  where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetLocalDrivingLicenseApplicationsInfoByID(ref int LocalDrivingLicenseApplicationID, ref int ApplicationID, ref int LicenseClassID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = "select * from LocalDrivingLicenseApplications  where ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];

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

        public static bool UpdateLocalDrivingLicenseApplications(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = "Update LocalDrivingLicenseApplications set ApplicationID = @ApplicationID, LicenseClassID = @LicenseClassID  WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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

    }
}

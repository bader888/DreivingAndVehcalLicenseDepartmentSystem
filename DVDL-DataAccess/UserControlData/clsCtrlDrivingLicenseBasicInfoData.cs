using System;
using System.Data;
using System.Data.SqlClient;

namespace DVDL_DataAccess
{
    public class clsCtrlDrivingLicenseBasicInfoData
    {
        public static DataTable GetDrivingLicenseAppInfo(int LocalDrivingLicenseApplicationID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = @"select * from LocalDrivingLicenseApplications_View where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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

        public static DataTable GetApplicationBasicInfo(int L_DappID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = @"
 SELECT *
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
                            WHERE L_D_LappID = @L_DappID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@L_DappID", L_DappID);

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

    }
}

using System;
using System.Data;
using System.Data.SqlClient;

namespace DVDL_DataAccess
{
    public class clsTestAppointmentsData
    {
        public static int AddNewTestAppointments(int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked)
        {
            int TestAppointmentsID = -1;
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = "INSERT INTO TestAppointments (TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked ) VALUES (@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate, @PaidFees, @CreatedByUserID, @IsLocked ); SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestAppointmentsID = insertedID;
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
            return TestAppointmentsID;
        }

        public static bool DeleteTestAppointments(int TestAppointmentID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = @"Delete TestAppointments where TestAppointmentID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", TestAppointmentID);
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

        public static bool IsTestAppointmentsExist(int TestAppointmentID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = "SELECT Found=1 FROM  TestAppointments  where TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

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

        public static bool IsPersonHaveActiveTest(int L_DappID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = @"select Found = 1 from TestAppointments_View where LocalDrivingLicenseApplicationID = @L_DappID and IsLocked = 0";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@L_DappID", L_DappID);

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

        public static bool GetTestAppointmentInfoID(ref int TestAppointmentID, ref int TestTypeID, ref int LocalDrivingLicenseApplicationID, ref DateTime AppointmentDate, ref decimal PaidFees, ref int CreatedByUserID, ref bool IsLocked)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = (decimal)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = reader.GetBoolean(reader.GetOrdinal("IsLocked"));
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

        public static bool UpdateTestAppointments(
            int TestAppointmentID,
            int TestTypeID,
            int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate,
            decimal PaidFees,
            int CreatedByUserID,
            bool IsLocked
            )
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = @"Update TestAppointments 
                            set 
                                TestTypeID = @TestTypeID, 
                                LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID, 
                                AppointmentDate = @AppointmentDate, 
                                PaidFees = @PaidFees, 
                                CreatedByUserID = @CreatedByUserID, 
                                IsLocked = @IsLocked  
                             WHERE 
                                TestAppointmentID = @TestAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

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


        public static DataTable GetTestAppointmentByID(int TestAppointmentID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);

            string query = @"select * from TestAppointments_View where TestAppointmentID = @TestAppointmentID ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
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

        public static DataTable GetTestAppointmentForSpecificTest(int L_DappID, string TestTypeTitle)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = @" 
                            select 
                            TestAppointmentID,
                            AppointmentDate,
                            PaidFees, 
                            IsLocked 
                            from 
                            TestAppointments_View 
                            where 
                            LocalDrivingLicenseApplicationID = @L_DappID and TestTypeTitle like  '%' + @TestTypeTitle + '%' ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@L_DappID", L_DappID);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);

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

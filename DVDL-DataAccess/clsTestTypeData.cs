using System;
using System.Data;
using System.Data.SqlClient;

namespace DVDL_DataAccess
{
    public class clsTestTypeData
    {


        public static DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = "SELECT * FROM TestTypes";
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

        public static bool GetTestTypesInfoByID(ref int TestTypeID, ref string TestTypeTitle, ref string TestTypeDescription, ref decimal TestTypeFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = "SELECT * FROM TestTypes WHERE TestTypeID = @TestTypesID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypesID", TestTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    TestTypeTitle = (string)reader["TestTypeTitle"];
                    TestTypeDescription = (string)reader["TestTypeDescription"];
                    TestTypeFees = (decimal)reader["TestTypeFees"];

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

        public static bool UpdateTestTypes(int TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = "Update TestTypes set TestTypeTitle = @TestTypeTitle, TestTypeDescription = @TestTypeDescription, TestTypeFees = @TestTypeFees  WHERE TestTypeID = @TestTypeID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);

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

        public static decimal GetTestTypeFeesByTitle(string Title)
        {
            decimal TestTypeFees = -1;
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = @"select TestTypeFees from TestTypes where TestTypeTitle like   '%' + @Title + '%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Title", Title);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && decimal.TryParse(result.ToString(), out decimal Fess))
                {
                    TestTypeFees = Fess;
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
            return TestTypeFees;
        }

        public static int GetTestTypeIDbyTestTitle(string TestTitle)
        {
            int TestTypeFees = -1;
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = @"select TestTypeID from TestTypes where TestTypeTitle like   '%' + @TestTitle + '%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTitle", TestTitle);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int Fess))
                {
                    TestTypeFees = Fess;
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
            return TestTypeFees;
        }


    }
}

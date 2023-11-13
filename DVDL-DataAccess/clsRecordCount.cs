using System;
using System.Data.SqlClient;

namespace DVDL_DataAccess
{
    public class clsRecordCount
    {
        public static int CountRecords(string tableName)
        {
            int recordCount = 0;

            using (SqlConnection connection = new SqlConnection(clsConnectionString.connectionString))
            {
                connection.Open();

                string query = $"SELECT COUNT(*) FROM {tableName}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // ExecuteScalar is used for retrieving a single value (in this case, the count)
                    object result = command.ExecuteScalar();

                    // Check if the result is not null before converting to int
                    if (result != null && int.TryParse(result.ToString(), out recordCount))
                    {
                        // Successfully retrieved the record count
                    }
                    else
                    {
                        // Handle the case where the count couldn't be retrieved
                        Console.WriteLine("Error retrieving record count.");
                    }
                }
            }

            return recordCount;
        }
    }
}

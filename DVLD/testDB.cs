using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DVLD
{
    public partial class testDB : Form
    {
        public testDB()
        {
            InitializeComponent();

        }

        static string connectionString = "Server =.;" + "Database = DVLD;" + "User Id =sa;" + "PassWord=sa123456;";

        public static bool GetDetainedLicenseInfoByID(int DetainID, int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM DetainedLicenses WHERE DetainID = @DetainedLicensesID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainedLicensesID", DetainID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    LicenseID = (int)reader["LicenseID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = (decimal)reader["FineFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];
                    ReleaseDate = (reader["ReleaseDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)reader["ReleaseDate"];
                    ReleasedByUserID = (reader["ReleasedByUserID"] == DBNull.Value) ? -1 : (int)reader["ReleasedByUserID"];
                    ReleaseApplicationID = (reader["ReleaseApplicationID"] == DBNull.Value) ? -1 : (int)reader["ReleaseApplicationID"];

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
                MessageBox.Show(ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        private void testDB_Load(object sender, System.EventArgs e)
        {

        }
    }
}

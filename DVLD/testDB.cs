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

        public static decimal GetTestTypeFeesByTitle(string Title)
        {
            decimal TestTypeFees = -1;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"select TestTypeFees from TestTypes where TestTypeTitle like   '%' + @Title + '%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Title", Title);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                MessageBox.Show(result.ToString());
                if (result != null && decimal.TryParse(result.ToString(), out decimal fess))
                {
                    TestTypeFees = fess;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return TestTypeFees;
        }
        private void testDB_Load(object sender, System.EventArgs e)
        {
            MessageBox.Show(GetTestTypeFeesByTitle("vision").ToString());
        }
    }
}

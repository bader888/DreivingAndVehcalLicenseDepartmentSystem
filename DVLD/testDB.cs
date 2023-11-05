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

        public static int GetPersonIDbyHisName(string PersonName)
        {
            int PersonID = -1;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"select PersonID from
                            (
                             select  PersonID ,
                             FirstName +' '+
                             SecondName +' '+
                             ThirdName + ''+ 
                             LastName as fullname 
                             from People  
                           )result 
                             where fullname = @PersonName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonName", PersonName);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                //MessageBox.Show(result.ToString());
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    PersonID = insertedID;
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
            return PersonID;
        }
        private void testDB_Load(object sender, System.EventArgs e)
        {
            MessageBox.Show(GetPersonIDbyHisName("q q qq").ToString());
            //MessageBox.Show(clsPerson.GetPersonIDbyHisName("q q q q").ToString());
        }
    }
}

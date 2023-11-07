using DVDL_Business;
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

        private void testDB_Load(object sender, System.EventArgs e)
        {
            MessageBox.Show(clsApplications.UpdateApplicationStatus(63, 1).ToString());
        }
    }
}

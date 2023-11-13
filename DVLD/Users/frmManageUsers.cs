using DVDL_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmManageUsers : Form
    {
        public frmManageUsers()
        {
            InitializeComponent();
        }
        DataTable _AllUsers;
        private void _ShowFilterItem(DataTable AllUsers)
        {
            comboBoxFilters.Items.Clear();
            foreach (DataColumn column in AllUsers.Columns)
            {
                comboBoxFilters.Items.Add(column.ToString());
                comboBoxFilters.SelectedItem = comboBoxFilters.Items[0];
            }
            comboBoxFilters.SelectedIndex = 3;//UserName

        }

        private void _ShowAllUser()
        {
            _AllUsers = clsUsers.GetAllusers();
            dataGridView1.DataSource = _AllUsers;
            _ShowFilterItem(_AllUsers);
            lblRecords.Text = clsGlobal.RecordCount("Users").ToString();
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _ShowAllUser();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddNewUser addNewUser = new frmAddNewUser();
            addNewUser.DataBack += _ShowAllUser; // Subscribe to the event 
            addNewUser.ShowDialog();

        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int UserID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            frmUserInfo userInfo = new frmUserInfo(UserID);
            userInfo.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewUser addNewUser = new frmAddNewUser();
            addNewUser.DataBack += _ShowAllUser; // Subscribe to the event
            addNewUser.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            frmAddNewUser addNewUser = new frmAddNewUser(UserID);
            addNewUser.DataBack += _ShowAllUser; // Subscribe to the event 
            addNewUser.ShowDialog();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this user?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int UserID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());

                clsUsers.Deleteusers(UserID);

                _ShowAllUser();
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            frmChangePassword changePassword = new frmChangePassword(UserID);
            changePassword.ShowDialog();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                _ShowAllUser();
                return;
            }

            if (_AllUsers != null)
            {
                // Get the filter criteria from a TextBox or another control
                string filterCriteria = txtSearch.Text;

                // Apply the filter to the DataTable
                _AllUsers.DefaultView.RowFilter = $"UserName LIKE '%{filterCriteria}%'";

                // Refresh the DataGridView to display the filtered results
                dataGridView1.Refresh();
            }
        }
    }
}

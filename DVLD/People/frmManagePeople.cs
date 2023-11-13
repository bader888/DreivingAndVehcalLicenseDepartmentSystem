using DVDL_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmManagePeople : Form
    {
        public frmManagePeople()
        {
            InitializeComponent();
        }
        DataTable dtPeople;

        private void _LoadPeopleInfo()
        {
            //get all people from db
            dtPeople = clsPerson.GetAllPeople();
            //show the people
            dataGridView1.DataSource = dtPeople;
            //show number of records
            lblRecords.Text = clsGlobal.RecordCount("People").ToString();

            _ShowFilterItem();
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _LoadPeopleInfo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddEditePerson addEditePerson = new frmAddEditePerson();
            addEditePerson.ShowDialog();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());

            frmShowPersonInfo showPersonInfo = new frmShowPersonInfo(PersonID);

            showPersonInfo.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditePerson addEditePerson = new frmAddEditePerson();
            addEditePerson.DataBack += _LoadPeopleInfo; // Subscribe to the event
            addEditePerson.ShowDialog();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            // Ask the user if they are sure they want to delete the person
            DialogResult result = MessageBox.Show("Are you sure you want to delete this person?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (clsPerson.DeletePerson(PersonID))
                {
                    MessageBox.Show("Delete Person Don");
                    _LoadPeopleInfo();
                }
                else
                    MessageBox.Show("Delete Faild the user link With Data");
            }

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            frmAddEditePerson addEditePerson = new frmAddEditePerson(PersonID);
            addEditePerson.DataBack += _LoadPeopleInfo;// Subscribe to the event
            addEditePerson.ShowDialog();

        }

        private void _ShowFilterItem()
        {
            comboBoxFilters.Items.Clear();
            foreach (DataColumn column in dtPeople.Columns)
            {
                comboBoxFilters.Items.Add(column.ToString());
                comboBoxFilters.SelectedItem = comboBoxFilters.Items[0];
            }
            comboBoxFilters.SelectedIndex = 0; //PersonID

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilter.Text))
            {
                _LoadPeopleInfo();
                return;
            }
            string filterText = txtFilter.Text;
            if (dtPeople != null)
            {
                string filterExpression = $"PersonID = {filterText}";

                // Apply the filter to the DefaultView of the DataTable
                dtPeople.DefaultView.RowFilter = filterExpression;


                // Refresh the DataGridView to display the filtered results
                dataGridView1.Refresh();
            }
        }
    }
}

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

        private int GetNumberOfRecords(DataTable dtPeople)
        {
            int CountRecord = 0;
            foreach (DataRow row in dtPeople.Rows)
            {
                CountRecord++;
            }

            return CountRecord;
        }

        private void _LoadPeopleInfo()
        {
            //get all people from db
            DataTable dtPeople = clsPerson.GetAllPeople();
            //show the people
            dataGridView1.DataSource = dtPeople;
            //show number of records
            lblRecords.Text = GetNumberOfRecords(dtPeople).ToString();
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
    }
}

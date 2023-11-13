using DVDL_Business;
using DVLD.License;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD.Drivers
{
    public partial class frmManageDrivers : Form
    {
        public frmManageDrivers()
        {
            InitializeComponent();
        }

        DataTable dtDrivers;

        private void _ShowAllDrivers()
        {
            dtDrivers = clsDrivers.GetAllDrivers();
            dgvDrivers.DataSource = dtDrivers;
            _ShowFilterItem();
            lblRecordsCount.Text = clsGlobal.RecordCount("Drivers").ToString();
        }

        private void frmManageDrivers_Load(object sender, EventArgs e)
        {
            _ShowAllDrivers();

        }

        private void _ShowFilterItem()
        {
            cbFilterBy.Items.Clear();
            foreach (DataColumn column in dtDrivers.Columns)
            {
                cbFilterBy.Items.Add(column.ToString());
                cbFilterBy.SelectedItem = cbFilterBy.Items[0];
            }
            cbFilterBy.SelectedIndex = 0; //DriverID

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtFilterValue.Text))
            {
                _ShowAllDrivers();
                return;
            }
            string filterText = txtFilterValue.Text;
            if (dtDrivers != null)
            {
                string filterExpression = $"DriverID = {filterText}";

                // Apply the filter to the DefaultView of the DataTable
                dtDrivers.DefaultView.RowFilter = filterExpression;

                // Refresh the DataGridView to display the filtered results
                dgvDrivers.Refresh();
            }

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = int.Parse(dgvDrivers.SelectedCells[1].Value.ToString());
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }


        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = int.Parse(dgvDrivers.SelectedCells[0].Value.ToString());
            int PersonID = int.Parse(dgvDrivers.SelectedCells[1].Value.ToString());
            frmPersonLicenseshistory frm = new frmPersonLicenseshistory(DriverID, PersonID);
            frm.ShowDialog();

        }
    }
}

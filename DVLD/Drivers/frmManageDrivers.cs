using DVDL_Business;
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
    }
}

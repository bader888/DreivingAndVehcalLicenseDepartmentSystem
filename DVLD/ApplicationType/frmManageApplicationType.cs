using DVDL_Business;
using DVLD.ManageApplicationType;
using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmManageApplicationType : Form
    {
        public frmManageApplicationType()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _ShowAllApplicationType()
        {
            dataGridView1.DataSource = clsApplicationType.GetAllApplicationTypes();
            lblRecords.Text = clsGlobal.RecordCount("ApplicationTypes").ToString();

        }
        private void frmManageApplicationType_Load(object sender, EventArgs e)
        {
            _ShowAllApplicationType();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationTypeID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            frmEditApplicationType editApplicationType = new frmEditApplicationType(ApplicationTypeID);
            editApplicationType.DataBack += _ShowAllApplicationType; // Subscribe to the event 
            editApplicationType.ShowDialog();
        }
    }
}

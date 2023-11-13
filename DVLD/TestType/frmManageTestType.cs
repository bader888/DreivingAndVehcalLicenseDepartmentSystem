using DVDL_Business;
using System;
using System.Windows.Forms;

namespace DVLD.ManageTestType
{
    public partial class frmManageTestType : Form
    {
        public frmManageTestType()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _ShowTestTypes()
        {
            dataGridView2.DataSource = clsTestType.GetAllTestTypes();
            lblRecords.Text = clsGlobal.RecordCount("TestTypes").ToString();

        }
        private void frmManageTestType_Load(object sender, EventArgs e)
        {
            _ShowTestTypes();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestTypeID = int.Parse(dataGridView2.SelectedCells[0].Value.ToString());
            frmEditTestType editTestType = new frmEditTestType(TestTypeID);
            editTestType.DataBack += _ShowTestTypes;// Subscribe to the event
            editTestType.ShowDialog();

        }
    }
}

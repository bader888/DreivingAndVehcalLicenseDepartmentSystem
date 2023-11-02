using DVDL_Business;
using System;
using System.Windows.Forms;

namespace DVLD.ManageTestType
{
    public partial class frmEditTestType : Form
    {
        public delegate void DataBackEventHandler();

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        clsTestType testType = new clsTestType();
        int _TestTypeID = -1;
        public frmEditTestType(int TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;

        }

        private void ShowTestTypeInfo()
        {
            testType = clsTestType.Find(_TestTypeID);
            lblappID.Text = testType.TestTypeID.ToString();
            txtTestTitle.Text = testType.TestTypeTitle;
            txtTestFess.Text = testType.TestTypeFees.ToString();
            txtTestDescription.Text = testType.TestTypeDescription;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            ShowTestTypeInfo();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            testType.TestTypeTitle = txtTestTitle.Text;
            testType.TestTypeDescription = txtTestDescription.Text;
            testType.TestTypeFees = decimal.Parse(txtTestFess.Text);

            if (testType.Save())
            {
                DataBack?.Invoke();
                MessageBox.Show("Save Done!");
            }
            else
                MessageBox.Show("Save Faild!");
        }
    }
}

using DVDL_Business;
using System;
using System.Windows.Forms;

namespace DVLD.ManageApplicationType
{
    public partial class frmEditApplicationType : Form
    {
        public delegate void DataBackEventHandler();

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;


        int _ApplicationTypeID = 1;
        clsApplicationType applicationType = new clsApplicationType();
        public frmEditApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
        }

        private void _ShowApplicationInfo()
        {
            applicationType = clsApplicationType.Find(_ApplicationTypeID);
            txtApplicationFess.Text = applicationType.ApplicationFees.ToString();
            txtApplicationType.Text = applicationType.ApplicationTypeTitle;
            lblappID.Text = applicationType.ApplicationTypeID.ToString();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            applicationType.ApplicationTypeTitle = txtApplicationType.Text;
            applicationType.ApplicationFees = decimal.Parse(txtApplicationFess.Text);

            if (applicationType.Save())
            {
                MessageBox.Show("Save Done!");
                DataBack?.Invoke();
            }
            else
                MessageBox.Show("Save Faild!");

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            _ShowApplicationInfo();
        }
    }
}

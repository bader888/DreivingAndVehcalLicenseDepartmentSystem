using System.Windows.Forms;

namespace DVLD
{
    public partial class frmShowPersonInfo : Form
    {
        private int _PersonID = -1;

        public frmShowPersonInfo(int PersonID)
        {
            InitializeComponent();

            _PersonID = PersonID;
        }

        private void frmShowPersonInfo_Load(object sender, System.EventArgs e)
        {
            ctrlPersonInfo1.ShowPersonInfo(_PersonID);
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void ctrlPersonInfo1_Load(object sender, System.EventArgs e)
        {

        }
    }
}

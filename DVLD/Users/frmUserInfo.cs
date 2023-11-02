using System;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmUserInfo : Form
    {
        int _UserID = -1;
        public frmUserInfo(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            ctrlUserInfo1.ShowUserInfo(_UserID);
        }
    }
}

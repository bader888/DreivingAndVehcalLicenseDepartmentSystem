using DVDL_Business;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ctrlUserInfo : UserControl
    {
        public ctrlUserInfo()
        {
            InitializeComponent();
        }

        public void ShowUserInfo(int UserID)
        {
            clsUsers user = clsUsers.Find(UserID);
            lblUserID.Text = user.UserID.ToString();
            lblUserName.Text = user.UserName.ToString();
            lblIsActive.Text = user.IsActive == true ? "yes" : "No";
            ctrlPersonInfo1.ShowPersonInfo(user.PersonID);
        }
    }
}

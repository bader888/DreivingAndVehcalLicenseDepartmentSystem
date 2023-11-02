using DVDL_Business;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmChangePassword : Form
    {

        clsUsers User = new clsUsers();

        public frmChangePassword(int UserID)
        {
            InitializeComponent();

            User = clsUsers.Find(UserID);
        }

        private void frmChangePassword_Load(object sender, System.EventArgs e)
        {
            ctrlUserInfo1.ShowUserInfo(User.UserID);
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void txtCurrentPassword_TextChanged(object sender, System.EventArgs e)
        {

            if (txtCurrentPassword.Text != User.Password)
                errorProvider1.SetError(txtCurrentPassword, "wrong password!");
            else
                errorProvider1.Clear();

        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text) || string.IsNullOrWhiteSpace(txtNewPassword.Text) || string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
                return;

            if (txtNewPassword.Text != txtConfirmPassword.Text)
                return;

            User.Password = txtNewPassword.Text;
            if (User.Save())
                MessageBox.Show("Password Change Successfully");
        }

        private void txtConfirmPassword_TextChanged(object sender, System.EventArgs e)
        {

            if (txtNewPassword.Text != txtConfirmPassword.Text)
                errorProvider1.SetError(txtConfirmPassword, "Not Match the new password");
            else
                errorProvider1.Clear();

        }

    }
}

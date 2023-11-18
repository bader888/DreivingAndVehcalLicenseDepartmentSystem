using DVDL_Business;
using DVDL_Business.Lib;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmLogin : Form
    {

        //password 111
        //username c200
        public frmLogin()
        {
            InitializeComponent();

        }

        private bool isPasswordVisible = false;

        private void OpenMainScreen()
        {
            Main main = new Main(this);
            this.Hide();
            main.ShowDialog();
        }

        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            string UserName = txtUserName.Text, EnterdPassword = txtPassword.Text;//entierd password 
            //find the user by the name  
            clsUsers User = clsUsers.Find(UserName);

            if (User == null)
            {

                MessageBox.Show("User Not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!User.IsActive)
            {

                MessageBox.Show("User Not Active!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Focus();
                return;
            }

            //  return true if the stored password = entred password
            if (clsEncrypter.PasswordHasher.VerifyPassword(EnterdPassword, User.Password))
            {

                if (checkBoxRemeberMe.Checked)
                    clsFileOperations.RememberUsernameAndPassword(UserName, EnterdPassword);
                else
                    clsFileOperations.RememberUsernameAndPassword(" ", " ");


                clsGlobal.CurrentUser = User;
                OpenMainScreen();
            }
            else
                MessageBox.Show("Wrong Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmLogin_Load(object sender, System.EventArgs e)
        {
            string username = "", password = "";
            if (clsFileOperations.GetStoredCredential(ref username, ref password))
            {
                txtUserName.Text = username.Trim();
                txtPassword.Text = password.Trim();
                checkBoxRemeberMe.Checked = true;
            }
            else
                checkBoxRemeberMe.Checked = false;

        }

        private void imgShowHidePassword_Click(object sender, System.EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            if (isPasswordVisible)
            {
                txtPassword.UseSystemPasswordChar = false;
                imgShowHidePassword.Image = Image.FromFile("C:\\Users\\lenovo\\source\\repos\\BankProjectDB\\Icon\\show.png");
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                imgShowHidePassword.Image = Image.FromFile("C:\\Users\\lenovo\\source\\repos\\BankProjectDB\\Icon\\hide.png");
            }

        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}

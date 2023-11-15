using DVDL_Business;
using DVDL_Business.Lib;
using DVLD.Users;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();

        }

        private bool isPasswordVisible = false;

        private void _OpenMainScreen()
        {
            Main main = new Main();
            main.ShowDialog();
        }

        private void Login()
        {
            string UserName = txtUserName.Text;
            string EnterdPassword = txtPassword.Text;//entierd password 
            clsUsers User = clsUsers.Find(UserName);

            if (User == null)
            {
                MessageBox.Show("User Not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!User.IsActive)
            {
                MessageBox.Show("User Not Active!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //  User.Password == stored password
            if (clsEncrypter.PasswordHasher.VerifyPassword(EnterdPassword, User.Password))
            {
                if (checkBoxRemeberMe.Checked)
                    UserAuthentication.SetUserCredentials(UserName, EnterdPassword);
                else
                    clsFileOperations.ClearFile(clsGlobal.filePath);
                //global
                clsGlobal.CurrentUser = User;
                _OpenMainScreen();
            }
            else
                MessageBox.Show("Wrong Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowRememberUserInfo()
        {
            if (!clsFileOperations.IsFileEmpty(clsGlobal.filePath))
            {
                var (username, password) = UserAuthentication.GetUserCredentials();
                txtUserName.Text = username;
                txtPassword.Text = password;
                checkBoxRemeberMe.Checked = true;
            }
        }

        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            Login();
        }

        private void frmLogin_Load(object sender, System.EventArgs e)
        {
            ShowRememberUserInfo();

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

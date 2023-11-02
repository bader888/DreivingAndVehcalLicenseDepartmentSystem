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
        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        private void _OpenMainScreen()
        {
            Main main = new Main();
            main.ShowDialog();
        }

        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            string UserName = txtUserName.Text;
            string Password = txtPassword.Text;
            clsUsers User = clsUsers.Find(UserName, Password);

            if (User != null)
            {
                //global
                clsGlobal.CurrentUser = User;
                if (User.Password == Password && User.UserName == UserName && User.IsActive)
                {
                    if (checkBoxRemeberMe.Checked)
                        UserAuthentication.SetUserCredentials(UserName, Password);
                    else
                        clsFileOperations.ClearFile(clsGlobal.filePath);
                    _OpenMainScreen();
                }
                else
                    MessageBox.Show("User Not Active!");
            }
            else
                MessageBox.Show("username/password wrong!");
        }

        private void frmLogin_Load(object sender, System.EventArgs e)
        {
            if (!clsFileOperations.IsFileEmpty(clsGlobal.filePath))
            {
                var (username, password) = UserAuthentication.GetUserCredentials();
                txtUserName.Text = username;
                txtPassword.Text = password;
                checkBoxRemeberMe.Checked = true;
            }
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
    }
}

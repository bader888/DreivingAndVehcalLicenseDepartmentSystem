using DVDL_Business;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmAddNewUser : Form
    {
        public delegate void DataBackEventHandler();
        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;


        clsUsers _user = new clsUsers();
        bool _UpdateMode = false;
        int _UserID = -1;

        public frmAddNewUser()
        {
            InitializeComponent();
        }

        public frmAddNewUser(int UserId)
        {
            InitializeComponent();
            _UserID = UserId;
            _UpdateMode = true;

        }

        private void frmAddNewUser_Load(object sender, System.EventArgs e)
        {
            if (_UpdateMode)
            {
                _SwitchToUpdateMode();
            }
        }


        private bool _IsUserDataValid()
        {

            if (string.IsNullOrWhiteSpace(txtUserName.Text))
                return false;

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
                return false;

            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
                return false;


            return true;
        }

        private void _ShowUserInfo()
        {
            txtUserName.Text = _user.UserName;
            txtPassword.Text = _user.Password;
            txtConfirmPassword.Text = _user.Password;
            lblUserID.Text = _user.UserID.ToString();
            ctrlPersonInfoWithFilter1.ShowPersonInfo(_user.PersonID);
        }

        private void _SwitchToUpdateMode()
        {
            _user = clsUsers.Find(_UserID);
            if (_user != null)
            {
                lblHeader.Text = "Update User";
                _ShowUserInfo();
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            if (_IsUserDataValid() && errorProvider1.GetError(txtConfirmPassword) == string.Empty)
            {
                _user.PersonID = ctrlPersonInfoWithFilter1.PersonID;
                _user.Password = txtPassword.Text;
                _user.UserName = txtUserName.Text;
                _user.IsActive = checkboxIsActive.Checked ? true : false;
                if (_user.Save())
                {
                    MessageBox.Show("Data Save Seccessfully");
                    lblUserID.Text = _user.UserID.ToString();
                    DataBack?.Invoke();


                }
                else
                {
                    MessageBox.Show("hiu");

                }

            }
        }

        private void txtConfirmPassword_TextChanged(object sender, System.EventArgs e)
        {
            if (txtConfirmPassword.Text != txtPassword.Text)
                errorProvider1.SetError(txtConfirmPassword, "not match!");
            else
                errorProvider1.Clear();

        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            tabControl1.SelectedTab = tabLoginInfo;
        }

        private void ctrlPersonInfoWithFilter1_Load(object sender, System.EventArgs e)
        {
            frmAddEditePerson addEditePerson = new frmAddEditePerson();
            addEditePerson.ShowDialog();

        }
    }
}

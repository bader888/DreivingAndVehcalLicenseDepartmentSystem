using DVDL_Business;
using DVDL_Business.Global;
using DVLD.People;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmAddEditePerson : Form
    {
        public delegate void DataBackEventHandler();
        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        // Define a delegate for sending the PersonID
        public delegate void SendPersonIDHandler(int personID);
        public event SendPersonIDHandler OnSendPersonID;

        clsPerson person = new clsPerson();
        int _PersonID = -1;
        bool UpdateMode = false;
        bool PersonHaveImage = false;
        public frmAddEditePerson()
        {
            InitializeComponent();
        }

        public frmAddEditePerson(int personID)
        {
            InitializeComponent();
            _PersonID = personID;
            UpdateMode = true;
        }

        private void _CopyImageToCdDrive(string sourceImagePath)
        {
            string destinationFolder = clsGlobal.DrivePath;
            string newFileName = Guid.NewGuid().ToString() + ".png";

            try
            {
                string destinationPath = Path.Combine(destinationFolder, newFileName);

                File.Copy(sourceImagePath, destinationPath);

                person.ImagePath = destinationPath;
                PersonHaveImage = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values
            _FillCountriesInComoboBox();

            if (!UpdateMode)
            {
                lblHeader.Text = "Add New Person";
                person = new clsPerson();
            }
            else
            {
                lblHeader.Text = "Update Person";
            }

            //set default image for the person.
            if (radioMale.Checked)
                imgPerson.Image = Image.FromFile(clsGlobal.MaleImagePath);
            else
                imgPerson.Image = Image.FromFile(clsGlobal.FemaleImagePath);

            //hide/show the remove linke incase there is no image for the person.
            linkRemoveImage.Visible = (imgPerson.ImageLocation != null);

            //we set the max date to 18 years from today, and set the default value the same.
            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);
            dateTimePicker1.Value = dateTimePicker1.MaxDate;

            //should not allow adding age more than 100 years
            dateTimePicker1.MinDate = DateTime.Now.AddYears(-100);

            //this will set default country to jordan.
            comboBoxCountries.SelectedIndex = comboBoxCountries.FindString("Jordan");

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNumber.Text = "";
            radioMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
        }
        private void _FillCountriesInComoboBox()
        {
            DataTable dtCountries = clsCountries.GetAllCountries();
            foreach (DataRow row in dtCountries.Rows)
            {
                comboBoxCountries.Items.Add(row["CountryName"]);
            }

            comboBoxCountries.SelectedIndex = comboBoxCountries.FindString("iraq");
        }

        private void _ShowPersonInfo()
        {
            person = clsPerson.Find(_PersonID);
            if (person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblPersonID.Text = person.PersonID.ToString();
            lblNationalNumber.Text = person.NationalNo;
            txtFirstName.Text = person.FirstName;
            txtSecondName.Text = person.SecondName;
            txtThirdName.Text = person.ThirdName;
            txtLastName.Text = person.LastName;
            txtAddress.Text = person.Address;
            txtEmail.Text = person.Email;
            txtPhone.Text = person.Phone;
            txtNationalNumber.Text = person.NationalNo;
            dateTimePicker1.Value = person.DateOfBirth;
            comboBoxCountries.SelectedIndex = comboBoxCountries.FindString(person.CountryInfo.CountryName);

            //handle the nullable value 
            if (person.ImagePath != "")
                imgPerson.Load(person.ImagePath);
            else
            {
                if (radioFamle.Checked)
                    person.ImagePath = clsGlobal.FemaleImagePath;
                else
                    person.ImagePath = clsGlobal.MaleImagePath;
            }
            PersonHaveImage = true;
            linkRemoveImage.Visible = true;
            if (person.Gendor == 0)
                radioFamle.Checked = true;
            else
                radioMale.Checked = true;

        }

        private void _SetPersonImage()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filters to allow specific image file types
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.ico|All Files|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file's path
                string selectedImagePath = openFileDialog1.FileName;

                _CopyImageToCdDrive(selectedImagePath);

                // Display the selected image in the PictureBox
                imgPerson.Image = Image.FromFile(selectedImagePath);
                linkRemoveImage.Visible = true;

            }
        }

        private bool _IsPersonInfoValid()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                //set error provider to txtFirstName
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSecondName.Text))
            {
                //set error provider to txtFirstName
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtThirdName.Text))
            {
                //set error provider to txtFirstName
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                //set error provider to txtFirstName
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                //set error provider to txtFirstName
                return false;
            }
            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //int personID = int.Parse(lblPersonID.Text);
            DataBack?.Invoke();

            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_IsPersonInfoValid())
            {
                person.FirstName = txtFirstName.Text;
                person.SecondName = txtSecondName.Text;
                person.LastName = txtLastName.Text;
                person.ThirdName = txtThirdName.Text;
                person.Email = txtEmail.Text;
                person.Phone = txtPhone.Text;
                person.NationalityCountryID = 1;
                person.NationalNo = txtNationalNumber.Text;
                person.DateOfBirth = dateTimePicker1.Value;
                person.Address = txtAddress.Text;

                person.NationalityCountryID = clsCountries.Find(comboBoxCountries.Text).ID;

                //if the person has no Image --set the defult image
                if (string.IsNullOrWhiteSpace(person.ImagePath))
                {
                    if (radioFamle.Checked)
                        person.ImagePath = clsGlobal.FemaleImagePath;
                    else
                        person.ImagePath = clsGlobal.MaleImagePath;

                    linkRemoveImage.Visible = false;
                }

                if (radioMale.Checked)
                    person.Gendor = 1;
                if (radioFamle.Checked)
                    person.Gendor = 0;

                if (person.Save())
                {
                    MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    lblHeader.Text = "Update Person";
                    lblPersonID.Text = person.PersonID.ToString();
                    lblNationalNumber.Text = person.NationalNo;
                    UpdateMode = true;

                    OnSendPersonID.Invoke(person.PersonID);
                }
                else
                    MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void linkSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _SetPersonImage();
        }

        private void linkRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            person.ImagePath = clsGlobal.MaleImagePath;
            imgPerson.Image = Image.FromFile(clsGlobal.MaleImagePath);
            PersonHaveImage = false;
            linkRemoveImage.Visible = false;
        }

        private void radioMale_CheckedChanged(object sender, EventArgs e)
        {
            if (!PersonHaveImage)
            {
                imgPerson.Image = Image.FromFile(clsGlobal.MaleImagePath);
                person.ImagePath = clsGlobal.MaleImagePath;
            }
            person.Gendor = 1;

        }

        private void radioFamle_CheckedChanged(object sender, EventArgs e)
        {
            if (!PersonHaveImage)
            {
                imgPerson.Image = Image.FromFile(clsGlobal.FemaleImagePath);
                person.ImagePath = clsGlobal.FemaleImagePath;
            }
            person.Gendor = 0;
        }

        private void frmAddEditePerson_Load(object sender, EventArgs e)
        {
            radioMale.Checked = true;

            //load countries to comboBoxCountries
            _FillCountriesInComoboBox();

            if (UpdateMode)
            {
                lblHeader.Text = "Update Person";
                _ShowPersonInfo();
            }

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (!ValidationHelper.StringValidator.IsEmail(txtEmail.Text))
                errorProvider1.SetError(txtEmail, "email@example.com");
            else
                errorProvider1.Clear();

        }
    }
}

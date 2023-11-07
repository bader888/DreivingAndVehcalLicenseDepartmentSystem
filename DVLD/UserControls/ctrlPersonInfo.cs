using DVDL_Business;
using DVLD.People;
using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ctrlPersonInfo : UserControl
    {
        public ctrlPersonInfo()
        {
            InitializeComponent();
        }

        clsPerson person;

        public void ShowPersonInfo(int PersonID)
        {
            person = clsPerson.Find(PersonID);

            if (person == null)
            {
                MessageBox.Show("Person Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblPersonName.Text = $"{person.FirstName} {person.LastName} {person.ThirdName} {person.LastName}";
            lblNationalNumber.Text = person.NationalNo;
            lblPersonID.Text = person.PersonID.ToString();
            lblAddress.Text = person.Address;
            lblDateBirth.Text = person.DateOfBirth.ToShortDateString();
            lblEnail.Text = person.Email;
            lblPhone.Text = person.Phone;
            lblNationalNumber.Text = person.NationalityCountryID.ToString();
            try
            {
                imgPerson.Load(person.ImagePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            lblCountry.Text = clsCountries.GetCountryNameByID(person.NationalityCountryID);
            if (person.Gendor == 0)
                lblGender.Text = "Female";
            else
                lblGender.Text = "Male";
        }

        private void linkEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditePerson addEditePerson = new frmAddEditePerson(person.PersonID);
            addEditePerson.ShowDialog();
        }
    }
}

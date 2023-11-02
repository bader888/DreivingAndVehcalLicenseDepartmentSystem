using DVDL_Business;
using DVDL_Business.Global;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ctrlPersonInfoWithFilter : UserControl
    {
        public ctrlPersonInfoWithFilter()
        {
            InitializeComponent();
        }
        public int PersonID { get; set; }
        private void _ShowFilterItem()
        {
            DataTable People = clsPerson.GetAllPeople();
            comboBoxFilters.Items.Add(People.Columns["PersonID"].ToString());
            comboBoxFilters.Items.Add(People.Columns["NationalNo"].ToString());

            comboBoxFilters.SelectedItem = comboBoxFilters.Items[0];

        }

        private void ctrlPersonInfoWithFilter_Load(object sender, EventArgs e)
        {
            _ShowFilterItem();
        }

        public void ShowPersonInfo(int PersonID)
        {
            this.PersonID = PersonID;
            ctrlPersonInfo1.ShowPersonInfo(PersonID);
        }

        private void btnSearchPerson_Click(object sender, EventArgs e)
        {
            if (ValidationHelper.StringValidator.ContainsNumbersOnly(txtFind.Text))
            {
                PersonID = int.Parse(txtFind.Text);

                ctrlPersonInfo1.ShowPersonInfo(PersonID);
            }
        }
    }
}

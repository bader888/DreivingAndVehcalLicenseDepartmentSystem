﻿using DVDL_Business;
using System.Windows.Forms;

namespace DVLD.Tests
{
    public partial class frmScheduleTest : Form
    {

        public frmScheduleTest()
        {
            InitializeComponent();

        }

        public bool UpdateMode { get; set; }

        public int TestAppointmentID { get; set; }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void frmScheduleTest_Load(object sender, System.EventArgs e)
        {

            ctrlScechuleTest1.UpdateMode = this.UpdateMode;
            ctrlScechuleTest1.TestappointmentID = this.TestAppointmentID;
            ctrlScechuleTest1.Title = "Schedule " + clsGlobal.TestType + " Test";
            ctrlScechuleTest1.ShowScheduleTestInfo();
        }


    }
}

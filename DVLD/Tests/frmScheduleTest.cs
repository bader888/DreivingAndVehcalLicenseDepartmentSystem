﻿using DVDL_Business;
using System.Windows.Forms;

namespace DVLD.Tests
{
    public partial class frmScheduleTest : Form
    {
        public delegate void DataBackEventHandler();
        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        public frmScheduleTest()
        {
            InitializeComponent();
        }

        public bool UpdateMode { get; set; }

        public bool ReTakeTest { get; set; }

        public int TestAppointmentID { get; set; }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void ShowSchuledTestInfo()
        {
            ctrlScechuleTest1.RetakeTest = this.ReTakeTest;
            ctrlScechuleTest1.UpdateMode = this.UpdateMode;
            ctrlScechuleTest1.TestappointmentID = this.TestAppointmentID;
            ctrlScechuleTest1.Title = "Schedule " + clsGlobal.TestType + " Test";
            ctrlScechuleTest1.ShowTestTypeImage();
            ctrlScechuleTest1.ShowScheduleTestInfo();
        }

        private void frmScheduleTest_Load(object sender, System.EventArgs e)
        {
            ShowSchuledTestInfo();
        }

        private void ctrlScechuleTest1_OnAppointmentSave(int obj)
        {
            DataBack?.Invoke();

        }
    }
}

﻿using DVDL_Business;
using DVLD.Applications;
using DVLD.Drivers;
using DVLD.License;
using DVLD.License.DetainReleaseLicenses;
using DVLD.ManageApplications;
using DVLD.ManageTestType;
using DVLD.Users;
using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class Main : Form
    {
        frmLogin Login;
        public Main(frmLogin frmLogin)
        {
            InitializeComponent();
            Login = frmLogin;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmManagePeople managePeople = new frmManagePeople();
            managePeople.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            frmManageUsers manageUsers = new frmManageUsers();
            manageUsers.ShowDialog();
        }

        private void currentUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo userInfo = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            userInfo.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword changePassword = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            changePassword.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login.Show();
            clsGlobal.CurrentUser = null;
            this.Close();
        }


        private void manageTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestType manageTestType = new frmManageTestType();
            manageTestType.ShowDialog();
        }

        private void localToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewLocalLicense frm = new frmNewLocalLicense();
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplications frm = new frmLocalDrivingLicenseApplications();
            frm.ShowDialog();

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            frmManageDrivers frm = new frmManageDrivers();
            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueInternationalLicense frm = new frmIssueInternationalLicense();
            frm.ShowDialog();

        }

        private void internationalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalDrvierLicenseApplication frm = new frmInternationalDrvierLicenseApplication();
            frm.ShowDialog();

        }

        private void renewLocalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalLicense frm = new frmRenewLocalLicense();
            frm.ShowDialog();

        }

        private void replacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplacementForDamageOrLost frm = new frmReplacementForDamageOrLost();
            frm.ShowDialog();

        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainLicense frm = new frmReleaseDetainLicense();
            frm.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
        }

        private void manageApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmManageApplicationType manageApplicationType = new frmManageApplicationType();
            manageApplicationType.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplications frm = new frmLocalDrivingLicenseApplications();
            frm.ShowDialog();
        }
    }
}

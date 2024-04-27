//using DVLD.Applications;
//using DVLD.Applications.Detain_License;
//using DVLD.Applications.International_License;
//using DVLD.Applications.ReplaceLostOrDamagedLicense;
//using DVLD.Applications.Rlease_Detained_License;
using DVLD.Classes;
//using DVLD.Drivers;
//using DVLD.Licenses;
//using DVLD.Licenses.International_License;
using DVLD.Login;
using DVLD.People;
//using DVLD.Tests;
using DVLD.ManageUsers;
using System;
using System.Drawing;
using System.Windows.Forms;
using DVLD.Users;
using DVLD.ManageApplicationTypes;
using DVLD.Tests;
using DVLD.Applications.LocalDrivingLicense;
using DVLD.Applications.International_License;
using DVLD.Applications.RenewLocalLicense;
using DVLD.Applications.ReleaseDetainedLicense;
using DVLD.Drivers;
using DVLD.Applications.ReplaceLostOrDamagedLicense;
using DVLD.Licenses.Detain_License;


namespace DVLD
{

    public partial class frmMain : Form
    {
        frmLogin _frmLogin;

        public frmMain(frmLogin frm)
        {
            InitializeComponent();
            _frmLogin= frm;

        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListPeople();
            frm.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            lblLoggedInUser.Text = "LoggedIn User: " + clsGlobal.CurrentUser.UserName;
            this.Refresh();

        }

      

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();
        }

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListUsers frm = new frmListUsers();
            frm.ShowDialog();   
        }

       

     

        private void manageApplicationTypesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmListApplicationTypes frm = new frmListApplicationTypes();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmListTestTypes frm = new frmListTestTypes();
            frm.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();

        }

        private void manageLocalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicenseApplication frm = new frmListLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateNewLocalDrivingLicense frm = new frmAddUpdateNewLocalDrivingLicense();
            frm.ShowDialog();   
        }

        private void retakeTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicenseApplication frm = new frmListLocalDrivingLicenseApplication();
               frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
                frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicenseApplication frm = new frmRenewLocalDrivingLicenseApplication();
                frm.ShowDialog();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
                frm.ShowDialog();
        }

        private void ManageInternationaDrivingLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmListInternationalLicenseApplications frm = new frmListInternationalLicenseApplications();
                frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDrivers frm = new frmListDrivers();
                frm.ShowDialog();

        }

        private void ReplacementLostOrDamagedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
             frmReplaceLostorDamagedLicense frm = new frmReplaceLostorDamagedLicense();
                frm.ShowDialog();
        }

        private void ManageDetainedLicensestoolStripMenuItem1_Click(object sender, EventArgs e)
        {
                frmListDetainedLicenses frm = new frmListDetainedLicenses();
                frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainedLicenseApplication frm = new frmDetainedLicenseApplication();
              frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
                frm.ShowDialog();
        }

        //private void vehiclesLicensesServicesToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}

    }
}

using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Controls
{
    public partial class ctrlLocalDrivingLicenseApplicationInfo : UserControl
    {
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplicationInfo;

        private int _LocalDrivingLicenseApplicationID = -1;

        private int _LicenseID;

        public int LocalDrivingLicenseApplicationID
        {
            get
            {
                return _LocalDrivingLicenseApplicationID;
            }
        }
        public ctrlLocalDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }
        public void LoadApplicationInfoByLocalDrivingAppID(int LocalDrivingLicenseAppID)
        {
            _LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseAppID);

            if (_LocalDrivingLicenseApplicationInfo == null)
            {
                _ResetLocalDrivingApplicationInfo();

                MessageBox.Show("No Application with ApplicationID = " + LocalDrivingLicenseApplicationID, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _FillLocalDrivingApplicationInfo();
        }

        public void LoadApplicationInfoByApplicationID(int ApplicationID)
        {
            _LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByApplicationID(ApplicationID);

            if (_LocalDrivingLicenseApplicationInfo == null)
            {
                _ResetLocalDrivingApplicationInfo();

                MessageBox.Show("No Application with ApplicationID = " + LocalDrivingLicenseApplicationID.ToString(), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillLocalDrivingApplicationInfo();
        }
        private void _FillLocalDrivingApplicationInfo()
        {
            _LicenseID = _LocalDrivingLicenseApplicationInfo.GetActiveLicenseID();

            llShowLisenceInfo.Enabled = (_LicenseID != -1);

            lblDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplicationInfo.LocalDrivingLicenseApplicationID.ToString();


            lblLicenseClassType.Text = clsLicenseClass.Find(_LocalDrivingLicenseApplicationInfo.LicenseClassID).ClassName;

            lblPassedTests.Text = _LocalDrivingLicenseApplicationInfo.GetPassedTestCount().ToString();

            ctrlApplicationBasicInfo1.LoadApplicationInfo(_LocalDrivingLicenseApplicationInfo.ApplicationID); ;
        }

        private void _ResetLocalDrivingApplicationInfo()
        {
            _LocalDrivingLicenseApplicationID = -1;
            ctrlApplicationBasicInfo1.ResetApplicationInfo();
            lblDrivingLicenseApplicationID.Text = "???";
            lblPassedTests.Text = "???";
            lblLicenseClassType.Text = "???";   
        }

        private void llShowLisenceInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }
    }
}

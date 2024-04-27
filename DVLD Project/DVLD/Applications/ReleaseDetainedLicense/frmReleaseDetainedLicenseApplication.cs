using DVLD.Classes;
using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses;
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

namespace DVLD.Applications.ReleaseDetainedLicense
{
    public partial class frmReleaseDetainedLicenseApplication : Form
    {
        private int _SelectedLicenseID = -1;

        public frmReleaseDetainedLicenseApplication()
        {
            InitializeComponent();
        }
        public frmReleaseDetainedLicenseApplication(int LicenseID)
        {
            InitializeComponent();
            _SelectedLicenseID = LicenseID; 

            ctrlDriverLicenseInfowithFilter1.LoadLicenseInfo(_SelectedLicenseID);

            ctrlDriverLicenseInfowithFilter1.FilterEnabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlDriverLicenseInfowithFilter1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;

            lblLicenseID.Text = _SelectedLicenseID.ToString();

            llShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);

            if (_SelectedLicenseID == -1) 
            {
                return;
            }

            if (!ctrlDriverLicenseInfowithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License ID is not detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).Fees.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;

            lblDetainID.Text = ctrlDriverLicenseInfowithFilter1.SelectedLicenseInfo.DetainedInfo.DetainID.ToString();
            lblLicenseID.Text = ctrlDriverLicenseInfowithFilter1.SelectedLicenseInfo.DetainedInfo.LicenseID.ToString();

            lblCreatedBy.Text = ctrlDriverLicenseInfowithFilter1.SelectedLicenseInfo.DetainedInfo.CreatedByUserInfo.UserName;

            lblDetainDate.Text = clsFormat.DateToShort(ctrlDriverLicenseInfowithFilter1.SelectedLicenseInfo.DetainedInfo.DetainDate);

            lblFineFees.Text = ctrlDriverLicenseInfowithFilter1.SelectedLicenseInfo.DetainedInfo.FineFees.ToString();

            lblTotalFees.Text = ((Convert.ToSingle(lblApplicationFees.Text)) + (Convert.ToSingle(lblFineFees.Text))).ToString();

            btnRelease.Enabled = true;

        }

        private void frmReleaseDetainedLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfowithFilter1.txtLicenseIDFocus();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_SelectedLicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensePersonHistory frm = new frmShowLicensePersonHistory(ctrlDriverLicenseInfowithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to release this detained  license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int ApplicationID = -1;

            bool IsReleased = ctrlDriverLicenseInfowithFilter1.SelectedLicenseInfo.ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID);

            lblApplicationID.Text = ApplicationID.ToString();

            if (!IsReleased ) 
            {
                MessageBox.Show("Faild to to release the Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            MessageBox.Show("Detained License released Successfully ", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRelease.Enabled = false;
            ctrlDriverLicenseInfowithFilter1.FilterEnabled = false;
            llShowLicenseHistory.Enabled = true;


        }
    }
}

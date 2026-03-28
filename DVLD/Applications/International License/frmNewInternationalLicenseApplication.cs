using DVLD.Classes;
using DVLD.Licenses;
using DVLD.Licenses.International_License;
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

namespace DVLD.Applications.International_License
{
    public partial class frmNewInternationalLicenseApplication : Form
    {
        private int _InterationalLicenseID = -1;

        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));

            lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).Fees.ToString();

            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        
        }

        private void ctrlDriverLicenseInfowithFilter1_OnLicenseSelected(int obj)
        {
          int SelectedLicenseID =    obj;

            lblLocalLicenseID.Text = SelectedLicenseID.ToString();

            llShowLicenseInfo.Enabled = (SelectedLicenseID != -1);
            
            if (SelectedLicenseID == -1)
            {
                return;
            }

            if (ctrlDriverLicenseInfowithFilter1.SelectedLicenseInfo.LicenseClass != 3)
            {
                MessageBox.Show("Selected License should be Class 3, select another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int ActiverInternationalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(ctrlDriverLicenseInfowithFilter1.SelectedLicenseInfo.DriverInfo.DriverID);


            if (ActiverInternationalLicenseID != -1)
            {
                MessageBox.Show("Person already has an active international license with ID = " + ActiverInternationalLicenseID.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowLicenseInfo.Enabled = true;
                _InterationalLicenseID = ActiverInternationalLicenseID;
                btnIssue.Enabled = false;
                return;
            }
            btnIssue.Enabled = true ;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsInternationalLicense internationalLicense = new clsInternationalLicense();

            internationalLicense.ApplicantPersonID = ctrlDriverLicenseInfowithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;

            internationalLicense.ApplicationDate = DateTime.Now;

            internationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed ;

            internationalLicense.LastStatusDate = DateTime.Now ;

            internationalLicense.IssueDate = DateTime.Now ;

            internationalLicense.ExpirationDate = DateTime.Now.AddYears(1);

            internationalLicense.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).Fees;

            internationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            internationalLicense.DriverID = ctrlDriverLicenseInfowithFilter1.SelectedLicenseInfo.DriverID;

            internationalLicense.IssuedUsingLocalLicenseID = ctrlDriverLicenseInfowithFilter1.SelectedLicenseInfo.LicenseID;

            if (!internationalLicense.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }


            _InterationalLicenseID = internationalLicense.InternationalLicenseID;

            lblInternationalLicenseID.Text = internationalLicense.InternationalLicenseID.ToString();

            lblApplicationID.Text = internationalLicense.ApplicationID.ToString();

            MessageBox.Show("International License Issued Successfully with ID=" + internationalLicense.InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssue.Enabled = false;
            ctrlDriverLicenseInfowithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;









        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(_InterationalLicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensePersonHistory frm = new frmShowLicensePersonHistory(ctrlDriverLicenseInfowithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);

            frm.ShowDialog();   
        }

        private void frmNewInternationalLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfowithFilter1.txtLicenseIDFocus();
        }
    }
}

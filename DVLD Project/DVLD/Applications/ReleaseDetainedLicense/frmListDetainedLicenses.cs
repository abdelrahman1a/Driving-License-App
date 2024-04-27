using DVLD.Licenses;
using DVLD.Licenses.Detain_License;
using DVLD.Licenses.Local_Licenses;
using DVLD.People;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.ReleaseDetainedLicense
{
    public partial class frmListDetainedLicenses : Form
    {

        private DataTable _dtAllDetainedLicenses;

        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;

            _dtAllDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();   
            dgvListDetaincedLicenses.DataSource = _dtAllDetainedLicenses;   
            lblRecordsCount.Text = dgvListDetaincedLicenses.Rows.Count.ToString();  

            if (dgvListDetaincedLicenses.Rows.Count > 0 ) 
            {
                dgvListDetaincedLicenses.Columns[0].HeaderText = "D.ID";
                dgvListDetaincedLicenses.Columns[0].Width = 90;

                dgvListDetaincedLicenses.Columns[1].HeaderText = "L.ID";
                dgvListDetaincedLicenses.Columns[1].Width = 90;

                dgvListDetaincedLicenses.Columns[2].HeaderText = "D.Date";
                dgvListDetaincedLicenses.Columns[2].Width = 160;

                dgvListDetaincedLicenses.Columns[3].HeaderText = "Is Released";
                dgvListDetaincedLicenses.Columns[3].Width = 110;

                dgvListDetaincedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvListDetaincedLicenses.Columns[4].Width = 110;

                dgvListDetaincedLicenses.Columns[5].HeaderText = "Release Date";
                dgvListDetaincedLicenses.Columns[5].Width = 160;

                dgvListDetaincedLicenses.Columns[6].HeaderText = "N.No";
                dgvListDetaincedLicenses.Columns[6].Width = 90;

                dgvListDetaincedLicenses.Columns[7].HeaderText = "Full Name";
                dgvListDetaincedLicenses.Columns[7].Width = 330;

                dgvListDetaincedLicenses.Columns[8].HeaderText = "Rlease App.ID";
                dgvListDetaincedLicenses.Columns[8].Width = 150;



            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Released")
            {
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0;
            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsReleased.Visible = false;
                if (txtFilterValue.Text == "None")
                {
                    txtFilterValue.Enabled = false;
                }
                else
                {
                    txtFilterValue.Enabled = true;
                }

                txtFilterValue.Text = "";
                txtFilterValue.Focus(); 

            }


        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch(cbFilterBy.Text)
            {
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;
                case "Is Released":
                    FilterColumn = "IsReleased";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Release Application ID":
                    FilterColumn = "ReleaseApplicationID";
                    break;
                default:
                    FilterColumn = "None";
                    break;

            }
            // Reset Filters if Nothing Selected
            if (txtFilterValue.Text.Trim() == "" || txtFilterValue.Text.Trim() =="None")
            {
                _dtAllDetainedLicenses.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtAllDetainedLicenses.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "DetainID" || FilterColumn == "ReleaseApplicationID")
            {
                _dtAllDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text);
            }
            else
            {
                _dtAllDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'", FilterColumn, txtFilterValue.Text);

            }
            lblRecordsCount.Text = dgvListDetaincedLicenses.Rows.Count.ToString();

        }

        private void btnClsoe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsReleased";
            string FilterValue = cbIsReleased.Text;

            switch(FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }

            if (FilterValue == "All")
                _dtAllDetainedLicenses.DefaultView.RowFilter = "";
            else
                _dtAllDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}" , FilterColumn , FilterValue);

            lblRecordsCount.Text = _dtAllDetainedLicenses.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Detain ID" || cbFilterBy.Text == "Release Application ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvListDetaincedLicenses.CurrentRow.Cells[1].Value;
            int PersonID = clsLicense.Find(LicenseID).DriverInfo.PersonID;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvListDetaincedLicenses.CurrentRow.Cells[1].Value;

            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvListDetaincedLicenses.CurrentRow.Cells[1].Value;
            int PersonID = clsLicense.Find(LicenseID).DriverInfo.PersonID;
            frmShowLicensePersonHistory frm = new frmShowLicensePersonHistory(PersonID);
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvListDetaincedLicenses.CurrentRow.Cells[1].Value;
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication(LicenseID);
            frm.ShowDialog();
            frmListDetainedLicenses_Load(null, null);

        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvListDetaincedLicenses.CurrentRow.Cells[1].Value;
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication(LicenseID);
            frm.ShowDialog();
            frmListDetainedLicenses_Load(null, null);
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            frmDetainedLicenseApplication frm = new frmDetainedLicenseApplication();
            frm.ShowDialog();
            frmListDetainedLicenses_Load(null, null);

        }

        private void CMSListDetainedLicenses_Opening(object sender, CancelEventArgs e)
        {
            releaseDetainedLicenseToolStripMenuItem.Enabled = !(bool)dgvListDetaincedLicenses.CurrentRow.Cells[3].Value;
        }
    }
}

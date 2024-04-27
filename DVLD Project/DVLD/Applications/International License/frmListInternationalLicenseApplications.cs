using DVLD.Licenses;
using DVLD.Licenses.International_License;
using DVLD.People;
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
    public partial class frmListInternationalLicenseApplications : Form
    {
        private DataTable _dtAllInternationalLicenses;
        public frmListInternationalLicenseApplications()
        {
            InitializeComponent();
        }

        private void frmListInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            _dtAllInternationalLicenses = clsInternationalLicense.GetAllInternationalLicenses();

            cbFilterBy.SelectedIndex = 0;

            dgvInternationalLicenseApplicaions.DataSource = _dtAllInternationalLicenses;    
            lblCountRecords.Text = dgvInternationalLicenseApplicaions.Rows.Count.ToString();

            if (dgvInternationalLicenseApplicaions.Rows.Count > 0)
            {
                dgvInternationalLicenseApplicaions.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenseApplicaions.Columns[0].Width = 160;

                dgvInternationalLicenseApplicaions.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenseApplicaions.Columns[1].Width = 150;


                dgvInternationalLicenseApplicaions.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenseApplicaions.Columns[2].Width = 130;


                dgvInternationalLicenseApplicaions.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicenseApplicaions.Columns[3].Width = 130;


                dgvInternationalLicenseApplicaions.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenseApplicaions.Columns[4].Width = 180;


                dgvInternationalLicenseApplicaions.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicenseApplicaions.Columns[5].Width = 180;


                dgvInternationalLicenseApplicaions.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenseApplicaions.Columns[6].Width = 120;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Activer")
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

                if (cbFilterBy.Text == "None")
                    txtFilterValue.Enabled = false;
                else
                    txtFilterValue.Enabled = true;

                txtFilterValue.Text = "";
                txtFilterValue.Focus();

            }

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn;

            switch(cbFilterBy.Text) 
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;

                case "Application ID":
                    FilterColumn = "ApplicationID";
                    break;

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                case "Is Active":
                    FilterColumn = "IsActive";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            if(txtFilterValue.Text.Trim() == "" || txtFilterValue.Text.Trim() == "None")
            {
                _dtAllInternationalLicenses.DefaultView.RowFilter = "";
                lblCountRecords.Text = dgvInternationalLicenseApplicaions.Rows.Count.ToString();
                return; 
            }

            _dtAllInternationalLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            lblCountRecords.Text = dgvInternationalLicenseApplicaions.Rows.Count.ToString();


        }

      

        private void btnAddNewInternationalLicenses_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
            frmListInternationalLicenseApplications_Load(null, null);
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterValue = cbIsReleased.Text;
            string FilterColumn = "IsActive";

            switch (FilterValue)
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
                _dtAllInternationalLicenses.DefaultView.RowFilter = "";
            else
                _dtAllInternationalLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);
       
            lblCountRecords.Text = dgvInternationalLicenseApplicaions.Rows.Count.ToString();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID =(int) dgvInternationalLicenseApplicaions.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.FindByDriverID(DriverID).PersonID;

            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();   
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID =  (int)dgvInternationalLicenseApplicaions.CurrentRow.Cells[0].Value;

            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(InternationalLicenseID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternationalLicenseApplicaions.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.FindByDriverID(DriverID).PersonID;
            frmShowLicensePersonHistory frm = new frmShowLicensePersonHistory(PersonID);
            frm.ShowDialog();
        }
    }
}

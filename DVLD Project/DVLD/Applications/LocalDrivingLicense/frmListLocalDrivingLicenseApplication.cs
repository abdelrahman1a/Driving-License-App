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

namespace DVLD.Applications.LocalDrivingLicense
{
    public partial class frmListLocalDrivingLicenseApplication : Form
    {
        private DataTable _dtAllLocalDrivingLicenseApplciation;

        public frmListLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }

        private void btnAddNewLocalDrinvingLicenseApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateNewLocalDrivingLicense frm = new frmAddUpdateNewLocalDrivingLicense();
            frm.ShowDialog();
            frmListLocalDrivingLicenseApplication_Load(null, null);
        }

        private void frmListLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _dtAllLocalDrivingLicenseApplciation = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvLocalDrivingLicenseApplication.DataSource = _dtAllLocalDrivingLicenseApplciation;

            lblCountRecords.Text = dgvLocalDrivingLicenseApplication.Rows.Count.ToString();

            if (dgvLocalDrivingLicenseApplication.Columns.Count > 0)
            {
                dgvLocalDrivingLicenseApplication.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplication.Columns[0].Width = 120;

                dgvLocalDrivingLicenseApplication.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplication.Columns[1].Width = 120;

                dgvLocalDrivingLicenseApplication.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplication.Columns[2].Width = 120;

                dgvLocalDrivingLicenseApplication.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplication.Columns[3].Width = 120;

                dgvLocalDrivingLicenseApplication.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplication.Columns[4].Width = 120;

                dgvLocalDrivingLicenseApplication.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplication.Columns[5].Width = 120;
            }
            cbFilterBy.SelectedIndex = 0;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = (cbFilterBy.Text != "None");

            if (txtFilter.Visible)
            {
                txtFilter.Text = "";
                txtFilter.Focus();
            }

            _dtAllLocalDrivingLicenseApplciation.DefaultView.RowFilter = "";
            lblCountRecords.Text = _dtAllLocalDrivingLicenseApplciation.Rows.Count.ToString();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Status":
                    FilterColumn = "Status";
                    break;
                default:
                    FilterColumn = "None";
                    break;


            }

            if (txtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllLocalDrivingLicenseApplciation.DefaultView.RowFilter = "";
                lblCountRecords.Text = _dtAllLocalDrivingLicenseApplciation.Rows.Count.ToString();
                return;
            }
            if (FilterColumn == "LocalDrivingLicenseApplicationID")
            {
                // you are dealing with integers
                _dtAllLocalDrivingLicenseApplciation.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());

            }
            else
                _dtAllLocalDrivingLicenseApplciation.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'", FilterColumn, txtFilter.Text.Trim());

            lblCountRecords.Text = _dtAllLocalDrivingLicenseApplciation.Rows.Count.ToString();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingApplicationID = (int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value;
            frmAddUpdateNewLocalDrivingLicense frm = new frmAddUpdateNewLocalDrivingLicense(LocalDrivingApplicationID);
            frm.ShowDialog();

            frmListLocalDrivingLicenseApplication_Load(null, null);
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "L.D.L.AppID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value;

            int LicenseID = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID).GetActiveLicenseID();

            if (LicenseID != -1)
            {
                // to implement show Licenseinfo

            }
            else
            {
                MessageBox.Show("No License Found!", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to cancel this application?", "Caption", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            if (localDrivingLicenseApplication != null)
            {
                if (localDrivingLicenseApplication.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    frmListLocalDrivingLicenseApplication_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not cancel applicatoin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to delete this application?", "Caption", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            if (localDrivingLicenseApplication != null)
            {
                if (localDrivingLicenseApplication.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    frmListLocalDrivingLicenseApplication_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not delete applicatoin , Other Data Depends on it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            //frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(localDrivingLicenseApplication.ApplicantPersonID);
            //frm.ShowDialog();
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplicationInfo frm = new frmLocalDrivingLicenseApplicationInfo((int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListLocalDrivingLicenseApplication_Load(null, null);
        }

        private void CMSApplications_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalDrivingLienseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            int TotalTests =(int) dgvLocalDrivingLicenseApplication.CurrentRow.Cells[5].Value;

            bool LicenseExists = LocalDrivingLienseApplication.IsLicenseIssued();

            issueDrivingLicenseToolStripMenuItem.Enabled = (TotalTests == 3) && !LicenseExists;

            showLicenseToolStripMenuItem.Enabled = LicenseExists;

            editToolStripMenuItem.Enabled = !LicenseExists && (LocalDrivingLienseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

            scheduleTestMenu.Enabled = !LicenseExists;

            cancelApplicationToolStripMenuItem.Enabled = (LocalDrivingLienseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

            deleteApplicationToolStripMenuItem.Enabled = (LocalDrivingLienseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

            bool PassedVisionTest = LocalDrivingLienseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest);

            bool PassedWrittenTest = LocalDrivingLienseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest);

            bool PassedStreetTest = LocalDrivingLienseApplication.DoesPassTestType(clsTestType.enTestType.StreetTest);

            scheduleTestMenu.Enabled = !PassedVisionTest || !PassedWrittenTest || !PassedStreetTest;    
            if (scheduleTestMenu.Enabled)
            {
                visionTestToolStripMenuItem.Enabled = !PassedVisionTest;
                writtenTestToolStripMenuItem.Enabled = PassedVisionTest && !PassedWrittenTest;
                streetTestToolStripMenuItem.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;
        
            }
        }
    }
}

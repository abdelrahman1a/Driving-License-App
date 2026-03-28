using DVLD.Licenses;
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

namespace DVLD.Drivers
{
    public partial class frmListDrivers : Form
    {

        private DataTable _dtAllDrivers;
        public frmListDrivers()
        {
            InitializeComponent();
        }

        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            CBFilterBy.SelectedIndex = 0;
            _dtAllDrivers = clsDriver.GetAllDrivers();
            dgvManageDrivers.DataSource = _dtAllDrivers;
            lblCountRecords.Text = dgvManageDrivers.Rows.Count.ToString();

            if (dgvManageDrivers.Rows.Count > 0)
            {
                dgvManageDrivers.Columns[0].HeaderText = "Driver ID";
                dgvManageDrivers.Columns[0].Width = 120;

                dgvManageDrivers.Columns[1].HeaderText = "Person ID";
                dgvManageDrivers.Columns[1].Width = 120;

                dgvManageDrivers.Columns[2].HeaderText = "National No.";
                dgvManageDrivers.Columns[2].Width = 140;

                dgvManageDrivers.Columns[3].HeaderText = "Full Name";
                dgvManageDrivers.Columns[3].Width = 320;

                dgvManageDrivers.Columns[4].HeaderText = "Date";
                dgvManageDrivers.Columns[4].Width = 170;


                dgvManageDrivers.Columns[5].HeaderText = "Active Licenses";
                dgvManageDrivers.Columns[5].Width = 150;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CBFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (CBFilterBy.Text != "None");

            if (CBFilterBy.Text == "None")
                txtFilterValue.Enabled = false;
            else
                txtFilterValue.Enabled = true;

            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (CBFilterBy.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                default:
                    FilterColumn = "None";
                    break;

            }
            
            if (txtFilterValue.Text.Trim() == "" || txtFilterValue.Text.Trim() == "None")
            {
                _dtAllDrivers.DefaultView.RowFilter = "";
                lblCountRecords.Text = dgvManageDrivers.Rows.Count.ToString();
                return;
            }
            // Filter for Numbers
            if (FilterColumn != "FullName" && FilterColumn != "NationalNo")
            {
                _dtAllDrivers.DefaultView.RowFilter = String.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());

            }
            else
                _dtAllDrivers.DefaultView.RowFilter = String.Format("[{0}] Like '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblCountRecords.Text = dgvManageDrivers.Rows.Count.ToString();

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CBFilterBy.Text == "Driver ID" || CBFilterBy.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvManageDrivers.CurrentRow.Cells[1].Value;

            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
            frmListDrivers_Load(null, null);
        }

       

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvManageDrivers.CurrentRow.Cells[1].Value;
            frmShowLicensePersonHistory frm = new frmShowLicensePersonHistory(PersonID);
            frm.ShowDialog();

        }

        private void lblCountRecords_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dgvManageDrivers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

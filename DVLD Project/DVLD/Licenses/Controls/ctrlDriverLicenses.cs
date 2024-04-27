using DVLD.Licenses.Local_Licenses;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.Controls
{
    public partial class ctrlDriverLicenses : UserControl
    {
        private int _DriverID;
        private clsDriver _Driver;
        private DataTable _dtDriverLocalLicenseHistory;
        private DataTable _dtDriverInternationalLicenseHistory;
        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        private void _LoadLocalLicenseInfo()
        {
            _dtDriverLocalLicenseHistory = clsDriver.GetLicenses(_DriverID);
            lblLocalRecordsCount.Text = dgvLocalLicenseHistory.Rows.Count.ToString();

            if (dgvLocalLicenseHistory.Rows.Count > 0)
            {
                dgvLocalLicenseHistory.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicenseHistory.Columns[0].Width = 110;

                dgvLocalLicenseHistory.Columns[1].HeaderText = "App.ID";
                dgvLocalLicenseHistory.Columns[1].Width = 110;

                dgvLocalLicenseHistory.Columns[2].HeaderText = "Class Name";
                dgvLocalLicenseHistory.Columns[2].Width = 270;

                dgvLocalLicenseHistory.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicenseHistory.Columns[3].Width = 110;

                dgvLocalLicenseHistory.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicenseHistory.Columns[4].Width = 170;

                dgvLocalLicenseHistory.Columns[5].HeaderText = "Is Active";
                dgvLocalLicenseHistory.Columns[5].Width = 110;

            }
        }


        private void _LoadInternationalLicenseInfo()
        {
            _dtDriverInternationalLicenseHistory = clsDriver.GetInternationalLicenses(_DriverID);
            lblLocalRecordsCount.Text = dgvInternationalLicenseHistory.Rows.Count.ToString();

            if (dgvInternationalLicenseHistory.Rows.Count > 0)
            {
                dgvInternationalLicenseHistory.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenseHistory.Columns[0].Width = 160;

                dgvInternationalLicenseHistory.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenseHistory.Columns[1].Width = 130;

                dgvInternationalLicenseHistory.Columns[2].HeaderText = "L.License ID";
                dgvInternationalLicenseHistory.Columns[2].Width = 130;

                dgvInternationalLicenseHistory.Columns[3].HeaderText = "Issue Date";
                dgvInternationalLicenseHistory.Columns[3].Width = 180;

                dgvInternationalLicenseHistory.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicenseHistory.Columns[4].Width = 180;

                dgvInternationalLicenseHistory.Columns[5].HeaderText = "Is Active";
                dgvInternationalLicenseHistory.Columns[5].Width = 120;

            }
        }

        public void LoadInfo(int DriverID)
        {
            _DriverID = DriverID;
            _Driver = clsDriver.FindByDriverID(_DriverID);

            if (_Driver == null )
            {
                MessageBox.Show("No Driver With DriverID = " + _DriverID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return; 
            }

            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();
        }

        public void LoadByPersonID(int PersonID)
        {
            _Driver = clsDriver.FindByPersonID(PersonID);

            if (_Driver == null)
            {
                MessageBox.Show("No Driver Linked with Person ID = " + PersonID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _DriverID = _Driver.DriverID;


            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();

        }



        public void Clear()
        {
            _dtDriverLocalLicenseHistory.Clear();
            _dtDriverInternationalLicenseHistory.Clear();
        }

        private void showLocalLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvLocalLicenseHistory.CurrentRow.Cells[0].Value;
            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void showInternationalLicenseInfoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = (int)dgvInternationalLicenseHistory.CurrentRow.Cells[0].Value;
            frmShowLicensePersonHistory frm = new frmShowLicensePersonHistory(InternationalLicenseID);
            frm.ShowDialog();
        }
    }
}
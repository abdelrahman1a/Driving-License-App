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

namespace DVLD.Tests
{
    public partial class frmListTestTypes : Form
    {
        private DataTable _dtAllTestTypes;
        public frmListTestTypes()
        {
            InitializeComponent();
        }
        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            _dtAllTestTypes = clsTestType.GetAllTestTypes();
            dgvManageTestTypes.DataSource = _dtAllTestTypes;
            lblRecordsCount.Text = dgvManageTestTypes.Rows.Count.ToString();

            if (dgvManageTestTypes.Rows.Count > 0)
            {
                dgvManageTestTypes.Columns[0].HeaderText = "ID";
                dgvManageTestTypes.Columns[0].Width = 110;

                dgvManageTestTypes.Columns[1].HeaderText = "Title";
                dgvManageTestTypes.Columns[1].Width = 210;

                dgvManageTestTypes.Columns[2].HeaderText = "Description";
                dgvManageTestTypes.Columns[2].Width = 500;

                dgvManageTestTypes.Columns[3].HeaderText = "Fees";
                dgvManageTestTypes.Columns[3].Width = 160;

            }
         


        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTestTypes frm = new frmEditTestTypes((clsTestType.enTestType)dgvManageTestTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListTestTypes_Load(null, null);
        }
    }
}

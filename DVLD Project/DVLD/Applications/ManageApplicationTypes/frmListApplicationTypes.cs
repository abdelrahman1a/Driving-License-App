using DVLD.Applications.ManageApplicationTypes;
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

namespace DVLD.ManageApplicationTypes
{
    public partial class frmListApplicationTypes : Form
    {
        private int _ApplicationTypes = -1;
        private clsApplicationType _ApplicationType;

        private DataTable _dtAllApplicationTypes;
        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _dtAllApplicationTypes = clsApplicationType.GetAllApplicationTypes();
            dgvManageApplicationTypes.DataSource = _dtAllApplicationTypes;
            lblRecordsCount.Text = dgvManageApplicationTypes.Rows.Count.ToString();

            if (dgvManageApplicationTypes.Rows.Count > 0)
            {
                dgvManageApplicationTypes.Columns[0].HeaderText = "ID";
                dgvManageApplicationTypes.Columns[0].Width = 110;

                dgvManageApplicationTypes.Columns[1].HeaderText = "Title";
                dgvManageApplicationTypes.Columns[1].Width = 400;

                dgvManageApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvManageApplicationTypes.Columns[2].Width = 250;
            }
       
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditApplicationTypes frm = new frmEditApplicationTypes((int)dgvManageApplicationTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListApplicationTypes_Load(null, null);
        }
    }
}

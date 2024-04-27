using DVLD.People;
using DVLD.Users;
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

namespace DVLD.ManageUsers
{
    public partial class frmListUsers : Form
    {
        private static DataTable _dtAllUsers;
        public frmListUsers()
        {
            InitializeComponent();
        }
        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _dtAllUsers = clsUser.GetAllUsers();
            dgvManageUsers.DataSource = _dtAllUsers;
            CbFilterBy.SelectedIndex = 0;
            lblcountRecords.Text = dgvManageUsers.Rows.Count.ToString();

            dgvManageUsers.Columns[0].HeaderText = "UserID";
            dgvManageUsers.Columns[0].Width = 110;

            dgvManageUsers.Columns[1].HeaderText = "PersonID";
            dgvManageUsers.Columns[1].Width = 120;

            dgvManageUsers.Columns[2].HeaderText = "Full Name";
            dgvManageUsers.Columns[2].Width = 350;

            dgvManageUsers.Columns[3].HeaderText = "User Name";
            dgvManageUsers.Columns[3].Width = 120;

            dgvManageUsers.Columns[4].HeaderText = "Is Active";
            dgvManageUsers.Columns[4].Width = 120;


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                CbISActive.Visible = true;
                CbISActive.Focus();
                CbISActive.SelectedIndex = 0;
            }
            else
            {
                txtFilterValue.Visible = (CbFilterBy.Text != "None");
                CbISActive.Visible = false;

                if (CbFilterBy.Text == "None")
                {
                    txtFilterValue.Enabled = false;
                }
                else
                    txtFilterValue.Enabled = true;

                txtFilterValue.Text = "";
                txtFilterValue.Focus();

            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (CbFilterBy.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "UserName":
                    FilterColumn = "UserName";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text == "" || txtFilterValue.Text == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblcountRecords.Text = dgvManageUsers.Rows.Count.ToString();
                return;
            }
            // dealing with Numbers
            if (FilterColumn != "FullName" && FilterColumn != "UserName")
            {
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            
            }
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblcountRecords.Text = _dtAllUsers.Rows.Count.ToString();
        }

        private void CbISActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = CbISActive.Text;
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
            {
                _dtAllUsers.DefaultView.RowFilter = "";
            }
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);
           
            lblcountRecords.Text = _dtAllUsers.Rows.Count.ToString();

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm1 = new frmAddUpdateUser();
            frm1.ShowDialog();
            frmListUsers_Load(null, null); // -> for refreshing data 
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CbFilterBy.Text == "User ID" || CbFilterBy.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvManageUsers.CurrentRow.Cells[0].Value;
            if (clsUser.DeleteUser(UserID))
            {
                MessageBox.Show("User Has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                frmListUsers_Load(null, null);
            }
            else
            {
                MessageBox.Show("User is not deleted due to it's connectivity with another one", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm1 = new frmAddUpdateUser((int)dgvManageUsers.CurrentRow.Cells[0].Value);
            frm1.ShowDialog();
            frmListUsers_Load(null, null);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm1 = new frmUserInfo((int)dgvManageUsers.CurrentRow.Cells[0].Value);
            frm1.ShowDialog();

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvManageUsers.CurrentRow.Cells[0].Value;
            frmChangePassword frm1 = new frmChangePassword(UserID);
            frm1.ShowDialog();  
         
        }

        private void dgvManageUsers_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmUserInfo frm1 = new frmUserInfo((int)dgvManageUsers.CurrentRow.Cells[0].Value);
            frm1.ShowDialog();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm1 = new frmAddUpdateUser();
            frm1.ShowDialog();
            frmListUsers_Load(null, null);
        }
    }
}

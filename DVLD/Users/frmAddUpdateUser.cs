using DVLD.Classes;
using DVLD_Buisness;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmAddUpdateUser : Form
    {
        public enum enMode { AddNew = 0 , Update = 1};
        private enMode _Mode ;
        private int _UserID = -1;
        private clsUser _User;
        public frmAddUpdateUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _UserID = UserID;   
        }

        private void _ReseteDefaultValues()
        {
            if (_Mode == enMode.AddNew) 
            {
                lblTitle.Text = "Add New User";
                this.Text = "Add New User";
                _User = new clsUser();
                tbLoginInfo.Enabled = false;

                ctrlPersonCardWithFilter1.FilterFocus();
            }
            else
            {
                lblTitle.Text = "Update User";
                this.Text = "Update User";
                tbLoginInfo.Enabled = true;
                btnSave.Enabled = true;

            }

            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = true;
        }

        private void _LoadData()
        {
            _User = clsUser.FindByUserID(_UserID);
            ctrlPersonCardWithFilter1.Enabled = false;

            if (_User == null ) 
            {
                MessageBox.Show("No User with ID = " +_UserID, "User Not Found" ,MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName.ToString();   
            txtPassword.Text = _User.Password.ToString();   
            txtConfirmPassword.Text = _User.Password.ToString();   
            chkIsActive.Checked = _User.IsActive;
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);

        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _ReseteDefaultValues(); 
            if(_Mode == enMode.Update)
                _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren()) // check for Error providers
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _User.UserName = txtUserName.Text.Trim();
            _User.Password =clsUtil.ComputeHash( txtPassword.Text.Trim());   
            _User.IsActive = chkIsActive.Checked;   

            if (_User.Save())
            {
                lblUserID.Text = _User.UserID.ToString();
                // change mode to Update
                lblTitle.Text = "Update User";
                this.Text = "Update User";

                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text != txtConfirmPassword.Text) 
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password confirmed doesn't match password ");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text)) 
            {
                e.Cancel= true;
                errorProvider1.SetError(txtPassword, "password can't be blank");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);

            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "UserName can't be blank");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);

            }
            if (_Mode == enMode.AddNew)
            {
                if (clsUser.isUserExist(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "UserName is User by Another User");
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);

                }
            }
            else
            {
                if (_User.UserName != txtUserName.Text.Trim()) 
                {
                    if (clsUser.isUserExist(txtUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUserName, "UserName is User by Another User");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txtUserName, null); 
                    }

                    
                
                }
            }
        }

        private void btnPersonInfoNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update) 
            {
                btnSave.Enabled = true;
                tbLoginInfo.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tbLoginInfo"];
                return;
            }
            // incase Add New
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                if (clsUser.isUserExistForPersonID(ctrlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Person already has a User , choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                }
                else
                {
                    btnSave.Enabled=true;
                    tbLoginInfo.Enabled=true;
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tbLoginInfo"];
                }
            }
            else
            {
                MessageBox.Show("Please Select a Person ", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void frmAddUpdateUser_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

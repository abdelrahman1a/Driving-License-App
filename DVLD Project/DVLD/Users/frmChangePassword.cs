using DVLD.Classes;
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

namespace DVLD.Users
{
    public partial class frmChangePassword : Form
    {
        private int _UserID;
        private clsUser _User;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;   
        }

        private void _ResetDefaultValues()
        {
            txtConfirmPassword.Text = "";
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Focus() ;
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            _User = clsUser.FindByUserID(_UserID);
            if (_User == null ) 
            {
                MessageBox.Show("Couldn't find User with id = " + _UserID , "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            // if found
            ctrlUserCard1._LoadUserInfo(_UserID);
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim())) 
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Password can't be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            }
            if (_User.Password != clsUtil.ComputeHash(txtCurrentPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current Password is wrong");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);

            }

        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "New Password can't be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNewPassword, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Confirmation Password not equal New Password");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren()) // check if the form isn't valid
            {
                MessageBox.Show("some fields aren't valid" , "validationError" , 
                    MessageBoxButtons.OK , MessageBoxIcon.Error);
                return;
            }
            _User.Password =clsUtil.ComputeHash( txtNewPassword.Text);
            
            if (_User.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ResetDefaultValues();
            }
            else
            {
                MessageBox.Show("An Error Occurred , Password didn't change", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

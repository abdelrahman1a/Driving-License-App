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

namespace DVLD.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser User = clsUser.FindByUsernameAndPassword(txtUserName.Text.Trim(),clsUtil.ComputeHash( txtPassword.Text.Trim()));
            if (User != null)
            {

                if (chkRemember.Checked)
                {
                    // store username and password
                    clsGlobal.RememberUsernameAndPassword(txtUserName.Text.Trim(),txtPassword.Text.Trim());
                }
                else
                {
                    clsGlobal.RememberUsernameAndPassword("" , "");

                }

                if (!User.IsActive)
                {
                    txtUserName.Focus();
                    MessageBox.Show("Your accound is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                clsGlobal.CurrentUser = User;
               
                frmMain frm = new frmMain(this);
                frm.ShowDialog();   


            }
            else
            {
                txtUserName.Focus ();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         
         
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";
            if (clsGlobal.GetStoredCredential(ref UserName , ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                chkRemember.Checked = true;

            }
            else
            {
                chkRemember.Checked = false;
            }
        }
    }
}

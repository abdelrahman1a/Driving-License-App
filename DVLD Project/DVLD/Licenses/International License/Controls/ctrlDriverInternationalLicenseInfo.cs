using DVLD.Classes;
using DVLD.Properties;
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
using System.IO;


namespace DVLD.Licenses.International_License.Controls
{
    public partial class ctrlDriverInternationalLicenseInfo : UserControl
    {
      
        private int _internationalLicenseID;
        private clsInternationalLicense _internationalLicense;

        public ctrlDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        public int InternationalLicenseID
        {
            get { return _internationalLicenseID; }   
        }

        private void _LoadPersonImage()
        {
            if (_internationalLicense.DriverInfo.PersonInfo.Gendor == 0)
                pbDriverImage.Image = Resources.Male_512;
            else
                pbDriverImage.Image = Resources.Female_512;

            string ImagePath = _internationalLicense.DriverInfo.PersonInfo.ImagePath;
            if (ImagePath != "")
            {
                if (File.Exists(ImagePath))
                    pbDriverImage.Load(ImagePath);

                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        public void LoadInfo(int internationalLicenseID)
        {
            _internationalLicenseID = internationalLicenseID;

            _internationalLicense = clsInternationalLicense.Find(_internationalLicenseID);

            if (_internationalLicense == null )
            {
                MessageBox.Show("Could not find Internationa License ID = " + _internationalLicenseID.ToString(),
                 "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _internationalLicenseID = -1;
                return;
            }

            lblInternationalLicenseID.Text = _internationalLicense.InternationalLicenseID.ToString();

            lblApplicationID.Text = _internationalLicense.ApplicationID.ToString();

            lblNationalNo.Text = _internationalLicense.DriverInfo.PersonInfo.NationalNo.ToString();

            lblLicenseID.Text = _internationalLicense.IssuedUsingLocalLicenseID.ToString();

            lblGender.Text = _internationalLicense.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";

            lblIssueDate.Text = clsFormat.DateToShort(_internationalLicense.IssueDate); 

            lblIsActive.Text = _internationalLicense.IsActive ? "Yes" : "No";

            lblDateOfBirth.Text = clsFormat.DateToShort(_internationalLicense.DriverInfo.PersonInfo.DateOfBirth);

            lblDriverID.Text = _internationalLicense.DriverInfo.DriverID.ToString();    

            lblExpirationDate.Text = clsFormat.DateToShort(_internationalLicense.ExpirationDate);

            lblFullName.Text = _internationalLicense.DriverInfo.PersonInfo.FullName;

            _LoadPersonImage();
        }

   
    }
}

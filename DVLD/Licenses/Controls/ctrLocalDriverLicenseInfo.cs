using DVLD.Properties;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class ctrLocalDriverLicenseInfo : UserControl
    {
        int _LicenseID;
        public ctrLocalDriverLicenseInfo()
        {
            InitializeComponent();
        }
        clsLicense License;
        public clsLicense LicenseInfo
        {
            get
            {
                return License;
            }
        }
        public int LicenseID
        {
            get
            {
                return _LicenseID;

            }
            set
            {
                _LicenseID = value;
                 
            }
        }
      
        public void LoadInfo (int LicenseID)
        {
            _LicenseID = LicenseID;   
            License = clsLicense.Find(LicenseID);
            if (License == null )
            {
                MessageBox.Show("There is no License with id = "+LicenseID, "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _LicenseID = -1;
                return; 
            }
            lblClassName.Text = License.LicenseClassInfo.ClassName;
            lblName.Text = License.DriverInfo.PersonInfo.FullName;
            lblLicenseID.Text = License.LicenseID.ToString();
            lblNationalNo.Text= License.DriverInfo.PersonInfo.NationalNo.ToString();
            lblGendor.Text = License.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";
            lblIssueDate.Text = License.IssueDate.ToString("dd/MMM/yyyy");
            lblIssueReason.Text = License.IssueReason.ToString();   
            lblNotes.Text = License.Notes.ToString();
            lblIsActive.Text = License.IsActive == true ? "Yes" : "No";
            lblDateOfBirth.Text = License.DriverInfo.PersonInfo.DateOfBirth.ToString("dd/MMM/yyyy");
            lblExpirationDate.Text = License.ExpirationDate.ToString("dd/MMM/yyyy");
            lblIsDetained.Text = License.IsDetained ? "Yes" : "No"  ;
            lblDriverID.Text = License.DriverID.ToString();
            HandleReasonType();
            HandelImage();
        }
        void HandleReasonType ()
        {
            switch(License.IssueReason)
            {
                case clsLicense.enIssueReason.FirstTime:
                    lblIssueReason.Text = "First Time";
                    break;
                case clsLicense.enIssueReason.ReplacementForDamaged:
                    lblIssueReason.Text = "Replacemnt For Damaged";
                    break;
                case clsLicense.enIssueReason.ReplacementForLost:
                    lblIssueReason.Text = "Replacemnt For Lost";
                    break;
                case clsLicense.enIssueReason.Renew:
                    lblIssueReason.Text = "Renew";
                    break;
                
            }
        }
        void HandelImage()
        {
            if(License.DriverInfo.PersonInfo.Gendor == 0)
            {
                pbPersonImage.Image = Resources.person_boy;
            }
            else
            {
                pbPersonImage.Image = Resources.person_girl;

            }
            string ImageBath = License.DriverInfo.PersonInfo.ImageBath;
            if (ImageBath != "")
            {
                if (File.Exists(ImageBath))
                {
                    pbPersonImage.ImageLocation = ImageBath;
                }
                else
                {
                    MessageBox.Show("The File with path [ " + ImageBath +" ] Does Not Exist ");
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

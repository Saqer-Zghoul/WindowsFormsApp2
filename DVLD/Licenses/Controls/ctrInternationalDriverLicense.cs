using DVLD.Properties;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace DVLD.Licenses.Controls
{
    public partial class ctrInternationalDriverLicense : UserControl
    {
        public ctrInternationalDriverLicense()
        {
            InitializeComponent();
        }
private int  _InternationalLicenseID =1 ;
        public int InternationalLicenseID
        {
            get { return _InternationalLicenseID; }
        }
        clsInternationalLicense _LicenseInfo;
        public clsInternationalLicense LicenseInfo
        {
            get { return _LicenseInfo; }
        }
        public void LoadInternationalLicense(int InternationalLicenseID)
        {
            _LicenseInfo = clsInternationalLicense.Find(InternationalLicenseID);
            if (_LicenseInfo == null)
            {
                MessageBox.Show("License with ID = " +InternationalLicenseID +" Cannot be Found ! ", "Error In Find ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _InternationalLicenseID = InternationalLicenseID;
            lblDriverID.Text = _LicenseInfo.DriverID.ToString();
            lblExpirationDate.Text = _LicenseInfo.ExpirationDate.ToString("dd/MMM/yyyy");
            lblGendor.Text  = (_LicenseInfo.ApplicationInfo.PersonInfo.Gendor == 0 ? "Male" : "Female");
            lblApplicationID.Text = _LicenseInfo.ApplicationID.ToString();
            lblDateOfBirth.Text = _LicenseInfo.ApplicationInfo.PersonInfo.DateOfBirth.ToString("dd/MMM/yyyy");
            lblInternationalLicenseID.Text = _LicenseInfo.InternationalLicenseID.ToString();
            lblIsActive.Text =(_LicenseInfo.IsActive ? "Yes" : "No");
            lblName.Text = _LicenseInfo.ApplicationInfo.PersonInfo.FullName.ToString();
            lblNationalNo.Text = _LicenseInfo.ApplicationInfo.PersonInfo.NationalNo.ToString();
            lblIssueDate.Text = _LicenseInfo.IssueDate.ToString("dd/MMM/yyyy");
            lblLicenseID.Text = _LicenseInfo.IssuedUsingLocalLicenseID.ToString();

            HandelImage();


        }
        void HandelImage()
        {
            if (_LicenseInfo.ApplicationInfo.PersonInfo.Gendor == 0)
            {
                pbPersonImage.Image = Resources.person_boy;
            }
            else
            {
                pbPersonImage.Image = Resources.person_girl;

            }
            string ImageBath = _LicenseInfo.ApplicationInfo.PersonInfo.ImageBath;
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
    }
}

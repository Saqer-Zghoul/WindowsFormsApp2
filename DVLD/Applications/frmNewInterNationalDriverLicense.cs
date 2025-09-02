using DVLD.Global_Class;
using DVLD.Licenses;
using DVLD.Licenses.InterNational_Licenses;
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

namespace DVLD.Applications.New_Driver_License
{
    public partial class frmNewInterNationalDriverLicense : Form
    {
        public frmNewInterNationalDriverLicense()
        {
            InitializeComponent();
        }

        private void frmNewInterNationalDriverLicense_Shown(object sender, EventArgs e)
        {
            ctrDriverLicenseInfoWithFilter1.FilterFocus();
        }
        int _NewLicenseID = -1;
        private void ctrDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            if (ctrDriverLicenseInfoWithFilter1.SelectedLicenseInfo==null )
            {
                llShowLicenseInfo.Enabled = false;
                btnIssue.Enabled = false;
                llShowLicenseHistory.Enabled = false;

                return;

            }
            if (ctrDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassInfo.LicenseClassID != 3 )
            {
                MessageBox.Show("Selected License is not contain with class 3-Ordinary driving License",
                    "Not Allowed ",MessageBoxButtons.OK,MessageBoxIcon.Error);
                llShowLicenseInfo.Enabled = false;
                llShowLicenseHistory.Enabled = true;

                btnIssue.Enabled = false;

                return;
            }
            if (clsInternationalLicense.IsThereInternationalLicenseForThisDriver(ctrDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID))
            {
                MessageBox.Show("The Driver already has International License Before , cannot add new International License ", "Not Allowed ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                llShowLicenseHistory.Enabled = true;

                llShowLicenseHistory.Enabled = true;

                return;
            }
            if (!ctrDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("The License with id = "+ ctrDriverLicenseInfoWithFilter1.LicenseID +" Is not Active",
                    "Activation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowLicenseInfo.Enabled = false;
                llShowLicenseHistory.Enabled = true;

                btnIssue.Enabled = false;

                return;
            }    
            if (DateTime.Compare(ctrDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ExpirationDate,DateTime.Now) < 0 )
            {
                MessageBox.Show("Please Renew Your License , it Expired on " + ctrDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ExpirationDate
                    , "Expairation", MessageBoxButtons.OK, MessageBoxIcon.Error);        
                btnIssue.Enabled = false;
                llShowLicenseHistory.Enabled = true;

                llShowLicenseInfo.Enabled = false;
                return;
            }
            llShowLicenseHistory.Enabled = true;
            
            btnIssue.Enabled = true;
            lblLocalLicenseID.Text = ctrDriverLicenseInfoWithFilter1.LicenseID.ToString();

        }

        private void ctrDriverLicenseInfoWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void frmNewInterNationalDriverLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblFees.Text = clsApplicationTypes.Find((int)clsApplicationTypes.enApplicationType.NewInternationalLicense).ApplicationFees.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToString("dd/MMM/yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName.ToString();
            btnIssue.Enabled = false;
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled=false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private int CreateNewApplicationForInternationalLicence()
        {
            return 1;
        }
        private void btnIssue_Click(object sender, EventArgs e)
        {
            clsLicense IssuedByLicense = ctrDriverLicenseInfoWithFilter1.SelectedLicenseInfo;
            clsApplication Application = new clsApplication();
            Application.ApplicationTypeID =clsApplicationTypes.Find((int)clsApplicationTypes.enApplicationType.NewInternationalLicense).ApplicationTypeID;
            Application.Status= clsApplication.enStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.ApplicantPersonID = IssuedByLicense.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            Application.PaidFees = clsApplicationTypes.Find((int)clsApplicationTypes.enApplicationType.NewInternationalLicense).ApplicationFees;
            if (!Application.Save())
            {
                MessageBox.Show("Error in save Application , Please Double Check on it ", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            clsInternationalLicense InternationalLicense = new clsInternationalLicense();
            InternationalLicense.ApplicationID = Application.ApplicationID;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            InternationalLicense.DriverID = IssuedByLicense.DriverID;
            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            InternationalLicense.IsActive =true;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.IssuedUsingLocalLicenseID = IssuedByLicense.LicenseID;
            if (InternationalLicense.Save())
            {
                MessageBox.Show("Saved Successfuly", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnIssue.Enabled = false;
                llShowLicenseInfo.Enabled = true;
                _NewLicenseID =InternationalLicense.InternationalLicenseID;
            }
            else
            {
                MessageBox.Show("Saved Failed  ", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowLicenseInfo.Enabled = false ;
            }
            
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInternationalDriverLicenseInfo frm = new frmInternationalDriverLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseHistory frm = new frmShowLicenseHistory(ctrDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();

        }
    }
}

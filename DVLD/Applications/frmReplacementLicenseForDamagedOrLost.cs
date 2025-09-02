using DVLD.Global_Class;
using DVLD.Licenses;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class frmReplacementLicenseForDamagedOrLost : Form
    {
        public frmReplacementLicenseForDamagedOrLost()
        {
            InitializeComponent();
        }
        float ApplicationFees;
        enum enMode { Addnew , Update }
        enMode Mode;
        clsLicense.enIssueReason Reason;
        clsLicense _OldLicense;
        clsLicense _ReplacedLicense;
       
        private void frmReplacementLicenseForDamagedOrLost_Load(object sender, EventArgs e)
        {
            Reason= clsLicense.enIssueReason.ReplacementForDamaged;
            Mode = enMode.Addnew;
            btnIssue.Enabled = false;
    //        ApplicationFees = clsApplicationTypes.Find((int)clsApplicationTypes.enApplicationType.ReplacementDamagedDrivingLicense).ApplicationFees;
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblCreatedByUserName.Text = clsGlobal.CurrentUser.UserName;
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplicationTypes.enApplicationType.ReplacementDamagedDrivingLicense).ApplicationFees.ToString();
        }

        private void ctrDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;

            llShowLicenseInfo.Enabled = false;
                lblOldLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = (SelectedLicenseID!= -1);
          
            if (SelectedLicenseID == -1) { btnIssue.Enabled = false ; return; }
            _OldLicense = ctrDriverLicenseInfoWithFilter1.SelectedLicenseInfo;
            if (!ctrDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("License Is not Active , You cannot Replace it ", "Not Allowed ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }
            btnIssue.Enabled = true;
        
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want issue a replacement for the License ? ","Confirm " , MessageBoxButtons.OKCancel ,MessageBoxIcon.Question ) != DialogResult.OK)
            {
                return;
            }
            _ReplacedLicense = ctrDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Replace(Reason, clsGlobal.CurrentUser.UserID);
            if (_ReplacedLicense != null)
            {
                MessageBox.Show("Replaced Successfuly with new License id = "+_ReplacedLicense.LicenseID, "Saved ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblRepacedLicenseID.Text = _ReplacedLicense.LicenseID.ToString();
                lblReplaceApplicationID.Text =_ReplacedLicense.ApplicationID.ToString();
                btnIssue.Enabled = false;
                ctrDriverLicenseInfoWithFilter1.FilterEnable = false;
                llShowLicenseInfo.Enabled = true;
                gbReplacementFor.Enabled =false;
            }
            else
            {
                MessageBox.Show("Replacement Faild , please Try again ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            Reason = clsLicense.enIssueReason.ReplacementForDamaged;
            lblTitle.Text = "Replacment License For Damaged";
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplicationTypes.enApplicationType.ReplacementDamagedDrivingLicense).ApplicationFees.ToString();

        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            Reason = clsLicense.enIssueReason.ReplacementForLost;
            lblTitle.Text = "Replacement License For Lost ";
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplicationTypes.enApplicationType.ReplacementLostDrivingLicense ).ApplicationFees.ToString();


        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_ReplacedLicense.LicenseID);
            frm.ShowDialog();

        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseHistory frm = new frmShowLicenseHistory(_OldLicense.DriverInfo.PersonID);
            frm.ShowDialog();

        }
    }
}

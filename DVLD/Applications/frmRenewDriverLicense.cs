using DVLD.Global_Class;
using DVLD.Licenses;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class frmRenewDriverLicense : Form
    {
        public frmRenewDriverLicense()
        {
            InitializeComponent();
        }
        int _OldLicenseID = -1;
        int _NewLicesneID = -1;
        clsLicense _OldLicenseInfo;
        clsLicense _NewLicenseInfo;

        private void ctrDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
             if (ctrDriverLicenseInfoWithFilter1.SelectedLicenseInfo == null)
            {
                llShowLicenseInfo.Enabled = false;
                llShowLicenseHistory.Enabled = false;
                btnIssue.Enabled = false;

                return;
            }
            _OldLicenseInfo = ctrDriverLicenseInfoWithFilter1.SelectedLicenseInfo;
            _OldLicenseID = ctrDriverLicenseInfoWithFilter1.LicenseID;
            if (_OldLicenseInfo.ExpirationDate > DateTime.Now)
            {
                
                    MessageBox.Show("Selected License is not yet expired , it will expire on :" + ctrDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ExpirationDate.ToString("dd/MMM/yyyy"), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                llShowLicenseHistory.Enabled = true;

                return;
                
            }
             if (!_OldLicenseInfo.IsActive)
            {
                MessageBox.Show("Your License is not Active ", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnIssue.Enabled = false ;
                llShowLicenseHistory.Enabled = true;


                return;
            }
            btnIssue.Enabled = true;
            llShowLicenseHistory.Enabled = true;
            lblOldLicenseID.Text=_OldLicenseID.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(10).ToString("dd/MMM/yyyy");
            lblLicenseFees.Text = _OldLicenseInfo.LicenseClassInfo.ClassFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblLicenseFees.Text) + Convert.ToSingle(lblApplicationFees.Text)).ToString();

        }

        private void frmRenewDriverLicense_Load(object sender, EventArgs e)
        {
            lblCreatedByUser.Text= clsGlobal.CurrentUser.UserName.ToString();
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblIssueDate.Text= DateTime .Now.ToString("dd/MMM/yyyy");
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplicationTypes.enApplicationType.RenewDrivingLicense).ApplicationFees.ToString(); 

            btnIssue.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            llShowLicenseHistory.Enabled = false;
        }

        private void frmRenewDriverLicense_Shown(object sender, EventArgs e)
        {

            ctrDriverLicenseInfoWithFilter1.FilterFocus();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {

            clsApplication Application = new clsApplication();
            Application.ApplicantPersonID = _OldLicenseInfo.DriverInfo.PersonID;
            Application.ApplicationDate= DateTime.Now;
            Application.Status = clsApplication.enStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            Application.ApplicationTypeID = clsApplicationTypes.Find((int)clsApplicationTypes.enApplicationType.RenewDrivingLicense).ApplicationTypeID;
            Application.PaidFees = clsApplicationTypes.Find((int)clsApplicationTypes.enApplicationType.RenewDrivingLicense).ApplicationFees;
            if (!Application.Save())
            {
                MessageBox.Show("Error in Save Application ", "Save failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            _NewLicenseInfo = new clsLicense();
            _NewLicenseInfo.DriverID = _OldLicenseInfo.DriverID;
            _NewLicenseInfo.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _NewLicenseInfo.ApplicationID =Application.ApplicationID;
            _NewLicenseInfo.IssueDate = DateTime.Now;
            _NewLicenseInfo.IssueReason = clsLicense.enIssueReason.Renew;
            _NewLicenseInfo.ExpirationDate = DateTime.Now.AddYears(_OldLicenseInfo.LicenseClassInfo.DefaultValidityLength);
            _NewLicenseInfo.IsActive = true;
            _OldLicenseInfo.IsActive  = false;
            _OldLicenseInfo.Save();

            _NewLicenseInfo.LicenseClassID = _OldLicenseInfo.LicenseClassID;
            _NewLicenseInfo.PaidFees = _OldLicenseInfo.LicenseClassInfo.ClassFees;
            
            if (txtNotes.Text.Trim() != "")
                _NewLicenseInfo.Notes = txtNotes.Text.Trim();
            _NewLicenseInfo.Notes ="";

            if (_NewLicenseInfo.Save())
            {
                MessageBox.Show("Saved Successfuly , your new license its id = " +_NewLicenseInfo.LicenseID, "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                llShowLicenseInfo.Enabled = true;
                btnIssue.Enabled = false;
                _NewLicesneID= _NewLicenseInfo.LicenseID;
                lblRenewApplicationID.Text  =Application.ApplicationID.ToString();
                lblRenewedLicenseID.Text= _NewLicenseInfo.LicenseID.ToString();

            }
            else
            {
                MessageBox.Show("Saved Failed :-(", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowLicenseInfo.Enabled = false;
            }

        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseHistory frm = new frmShowLicenseHistory(_OldLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicesneID);
            frm.ShowDialog();

        }
    }
}

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

namespace DVLD.Detain_Licenses
{
    public partial class frmDetainLicense : Form
    {
        public frmDetainLicense()
        {
            InitializeComponent();
        }
        int _LicenseID = -1;
        clsLicense CurrentLicense;
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            llShowLicenseInfo.Enabled = false;
            if (ctrDriverLicenseInfoWithFilter1.SelectedLicenseInfo == null)
            {
                // llShowLicenseHistory.Enabled = false ;
                return;

            }
            _LicenseID = ctrDriverLicenseInfoWithFilter1.LicenseID;
            CurrentLicense  = clsLicense.Find(_LicenseID);

            if (ctrDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License is already Detained , choose another one .", "Not Allowed ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDetain.Enabled = false;
                lblLicenseID.Text = _LicenseID.ToString();
                llShowLicenseHistory.Enabled = true;
                lblDetainID.Text = clsDetainLicense.FindByLicenseID(_LicenseID).DetainID.ToString();
                txtFineFees.Text = clsDetainLicense.FindByLicenseID(_LicenseID).FineFees.ToString();
                lblApplicationDate.Text = clsDetainLicense.FindByLicenseID(_LicenseID).DetainDate.ToString("dd/MMM/yyyy");

                txtFineFees.Enabled = false;
                return;
            }
            txtFineFees.Enabled = true;
            txtFineFees.Text ="";
            lblDetainID.Text = "[???]";
            lblLicenseID.Text = _LicenseID.ToString();
            btnDetain.Enabled = true;
            llShowLicenseHistory.Enabled = true;

        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseHistory frm = new frmShowLicenseHistory(CurrentLicense.DriverInfo.PersonID);
            frm.ShowDialog();

        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_LicenseID);
            frm.ShowDialog();


        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            btnDetain.Enabled = false;
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblCreatedByUserName.Text = clsGlobal.CurrentUser.UserName.ToString();
        
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("There is some field Empty ", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            int DetainID  = ctrDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Detain(Convert.ToSingle(txtFineFees.Text), clsGlobal.CurrentUser.UserID);
        if (DetainID != -1 )
            {
                MessageBox.Show("Detained License Successfuly , DetainID = " + DetainID, "Detain", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnDetain.Enabled = false;
                ctrDriverLicenseInfoWithFilter1.FilterEnable = false;
                llShowLicenseInfo.Enabled = true;
                lblDetainID.Text = DetainID.ToString(); 
            }
        else
            {
                MessageBox.Show("Detained Faild , there is an error in saved ,\n Please contact with your Admin ", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnDetain.Enabled = true;
                ctrDriverLicenseInfoWithFilter1.FilterEnable = true;
                llShowLicenseInfo.Enabled = true;

            }

        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // منع الإدخال
            }
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty( txtFineFees.Text.Trim()))
            {
                errorProvider1.SetError(txtFineFees, "This is Must be a value ");
                e.Cancel = true;
            }
            else
            {
               errorProvider1.SetError(txtFineFees, null);

            }
        }

       
    }
}

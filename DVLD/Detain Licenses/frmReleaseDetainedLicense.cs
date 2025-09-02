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
    public partial class frmReleaseDetainedLicense : Form
    {
        public frmReleaseDetainedLicense()
        {
            InitializeComponent();

            Mode = enMode.New;

        }
        enum enMode { New , Update }
        enMode Mode;
        public frmReleaseDetainedLicense(int licenseID)
        {
            InitializeComponent();
            _LicenseID=licenseID;
            Mode = enMode.Update;
        }
        int _LicenseID;
        clsDetainLicense _DetainedLicense;
        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
            if (Mode == enMode.Update)
            {
                _DetainedLicense = clsDetainLicense.FindByLicenseID(_LicenseID);
                PassValues();
                btnRealese.Enabled = true;
                ctrDriverLicenseInfoWithFilter1.FilterEnable = false ;
                llShowLicenseHistory.Enabled = true;
                llShowLicenseInfo.Enabled = false;
                ctrDriverLicenseInfoWithFilter1.FilterEnable = false;
                ctrDriverLicenseInfoWithFilter1.LoadLicenseInfo(_LicenseID);
            }
            else
            {
                btnRealese.Enabled= false;
                llShowLicenseHistory.Enabled = false;
                llShowLicenseInfo.Enabled = false;
            }

        }
        
        void ResetValues()
        {
            lblApplicationFees.Text = "[$$$]";
            lblTotalFees.Text = "[$$$]";
            lblReleaseApplicationID.Text = "[???]";
            lblLicenseID.Text = "[???]";
            lblFineFees.Text = "[$$$]";
            lblDetainDate.Text = "[dd/mm/yyyy]";
          
        }
        void PassValues()
        {
            lblFineFees.Text = _DetainedLicense.FineFees.ToString();
            lblDetainID.Text = _DetainedLicense.DetainID.ToString();
            lblLicenseID.Text = _LicenseID.ToString();
            lblDetainDate.Text = _DetainedLicense.DetainDate.ToString("dd/MMM/yyyy");
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplicationTypes.enApplicationType.ReleaseDetainedDrivingLicense).ApplicationFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblFineFees.Text) + Convert.ToSingle(lblApplicationFees.Text)).ToString();
            lblReleaseApplicationID.Text ="[???]";
            lblCreatedByUserName.Text = clsGlobal.CurrentUser.UserName.ToString();


        }
        private void ctrDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            llShowLicenseInfo.Enabled = false;
            if (ctrDriverLicenseInfoWithFilter1.SelectedLicenseInfo == null)
            {
                btnRealese.Enabled = false;
               
                return;

            }
            _LicenseID = obj;
             //= ctrDriverLicenseInfoWithFilter1.SelectedLicenseInfo;
            if (!ctrDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                ResetValues();
                btnRealese.Enabled = false;
                MessageBox.Show("Selected License is not Detained ", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                

            }
            else
            {
                _DetainedLicense = clsDetainLicense.FindByLicenseID(_LicenseID);

                PassValues();
                btnRealese.Enabled = true;

            }
            llShowLicenseHistory.Enabled = true;
        }

        private void btnRealese_Click(object sender, EventArgs e)
        {
            int ReleaseID =  ctrDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Release(clsGlobal.CurrentUser.UserID);
       if (ReleaseID != -1 )
            {
                MessageBox.Show("Released Successfuly , with Release Application ID = "+ ReleaseID, "Saved Successfuly", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblReleaseApplicationID.Text= ReleaseID.ToString();
                btnRealese.Enabled = false;
                llShowLicenseInfo.Enabled = true;
                

                    ctrDriverLicenseInfoWithFilter1.FilterEnable = false;
            }
       else
            {
                MessageBox.Show("Released Faild ,try again", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnRealese.Enabled = true;
                llShowLicenseInfo.Enabled = false ;
            }
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseHistory frm = new frmShowLicenseHistory(ctrDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_LicenseID);
            frm.ShowDialog();

        }
    }
}

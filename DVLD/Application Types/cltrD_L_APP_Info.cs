using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Application_Types
{
    public partial class cltrD_L_APP_Info : UserControl
    {
        public cltrD_L_APP_Info()
        {
            InitializeComponent();
        }
        int _DrivingLicenseApplicationID =0;
        clsLocalDrivingLicenseApplication _DrivingLicanse;
        private void cltrD_L_APP_Info_Load(object sender, EventArgs e)
        {

        }
        public void LoadInfo(int DrivingLicenseApplicationID )
        {
        _DrivingLicanse=    clsLocalDrivingLicenseApplication.Find(DrivingLicenseApplicationID);
            if (_DrivingLicanse == null)
            {
                MessageBox.Show("The Driving License Application Cannot be found ! ID =  "+ DrivingLicenseApplicationID, "Error in Find", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblDrivingLicenseApplicationID.Text = _DrivingLicanse.LocalDrivingLicenseApplicationID.ToString();
            lblClassLicense.Text = _DrivingLicanse.LicenseClass.ClassName.ToString();
            lblPassedTests.Text = _DrivingLicanse.GetPassedTestsCount()+"/3";
            lblApplicationID.Text = _DrivingLicanse.ApplicationInfo.ApplicationID.ToString();
            lblApplicationStatus.Text = _DrivingLicanse.ApplicationInfo.Status.ToString();
            lblApplicationPaidFees.Text = _DrivingLicanse.ApplicationInfo.PaidFees.ToString();
            lblApplicationType.Text = _DrivingLicanse.ApplicationInfo.ApplicationType.ApplicationTypeTitle.ToString();  
            lblPersonApplicant.Text = _DrivingLicanse.ApplicationInfo.PersonInfo.FullName.ToString();
            lblApplicationDate.Text = _DrivingLicanse.ApplicationInfo.ApplicationDate.ToString();
            lblLastStatusDate.Text = _DrivingLicanse.ApplicationInfo.LastStatusDate.ToString();
            lblCreatedBy.Text = _DrivingLicanse.ApplicationInfo.UserInfo.UserName.ToString();


        
        
        }

        private void lblPassedTests_Click(object sender, EventArgs e)
        {

        }

        private void lblApplicationID_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonInfo frm = new frmPersonInfo(_DrivingLicanse.ApplicationInfo.PersonInfo.PersonID);
            frm.ShowDialog();

        }
    }
}

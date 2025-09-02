using DVLD.Application_Types;
using DVLD.Applications;
using DVLD.Applications.New_Driver_License;
using DVLD.Detain_Licenses;
using DVLD.Drivers;
using DVLD.Global_Class;
using DVLD.Login_Screen;
using DVLD.ManageApplications;
using DVLD.Test_Types;
using DVLD.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmMain2 : Form
    {
        frmLoginScreen _LoginForm;
        public frmMain2(frmLoginScreen LoginForm)
        {
            InitializeComponent();
            _LoginForm = LoginForm;

        }

        private void accountSettinfToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmManagePeople();
          
            frm.Show();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmManageUsers();

            frm.Show();
        }

        private void sineOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser=null;

            _LoginForm.Show();
            this.Close();

        }

        private void currentUserInfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowDetails frm = new frmShowDetails(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm =new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void ManageApplicationTypes_Click(object sender, EventArgs e)
        {
            frmManageApplicationTypes frm = new frmManageApplicationTypes();
            frm.ShowDialog();
        }

        private void mangeTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestTypes frm = new frmManageTestTypes();
            frm.ShowDialog();   
        }

        private void localLincesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frm = new frmNewLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void manageApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void localDrivingLicenseApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplications frm = new frmLocalDrivingLicenseApplications();
            frm.ShowDialog();
        }

        private void driverLicenseServecesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDriversList frm = new frmDriversList();
            frm.ShowDialog();
        }

        private void renewDrivingLincensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewDriverLicense frm = new frmRenewDriverLicense();
            frm.ShowDialog();
        }

        private void frmMain2_Load(object sender, EventArgs e)
        {

        }

        private void internationalLincensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInterNationalDriverLicense frm = new frmNewInterNationalDriverLicense();
            frm.ShowDialog();
        }

        private void internationalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIntrnationalDrivingLicensesApplication frm = new frmIntrnationalDrivingLicensesApplication();
            frm.ShowDialog();

        }

        private void replacementLicenseForDamagedOrLostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplacementLicenseForDamagedOrLost frm = new frmReplacementLicenseForDamagedOrLost();
            frm.ShowDialog();
        }

        private void manageDetainLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageDetainedDrivingLicenses frm = new frmManageDetainedDrivingLicenses();
            frm.ShowDialog();

        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();   
        }

        private void releaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplications frm = new frmLocalDrivingLicenseApplications();
            frm.ShowDialog();


        }
    }
}

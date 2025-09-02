using DVLD.Global_Class;
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

namespace DVLD.Issue_Driver_License
{
    public partial class frmIssueDriverLicense_FirstTime : Form
    {
        public frmIssueDriverLicense_FirstTime(int DrivingLicensApplicationID)
        {
            InitializeComponent();
            _DrivingLicensApplicationID = DrivingLicensApplicationID;

        }

        int _DrivingLicensApplicationID;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseAppliction;
        private void frmIssueDriverLicense_FirstTime_Load(object sender, EventArgs e)
        {
            cltrD_L_APP_Info1.LoadInfo(_DrivingLicensApplicationID) ;
            
          
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _LocalDrivingLicenseAppliction = clsLocalDrivingLicenseApplication.Find(_DrivingLicensApplicationID);
           int LicenseID =  _LocalDrivingLicenseAppliction.IssueDrivringLicenseFirstTime(txtNotes.Text.Trim(), clsGlobal.CurrentUser.UserID);
            if (LicenseID == -1 )
            {
                if (MessageBox.Show("Error in save " , "Saved" , MessageBoxButtons.OK,MessageBoxIcon.Error ) == DialogResult.OK)
                {
                    this.Close();
                }
                return;


            }

            else
            {
                if (MessageBox.Show("Saver Successfuly , with License ID = " + LicenseID,"Saved",MessageBoxButtons.OK,MessageBoxIcon.Information) == DialogResult.OK)
                {
                    this.Close();

                }
                    
            }
        }
    }
}

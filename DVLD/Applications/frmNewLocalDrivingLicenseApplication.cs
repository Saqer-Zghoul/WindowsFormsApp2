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

namespace DVLD
{
    public partial class frmNewLocalDrivingLicenseApplication : Form
    {
        public frmNewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }
        clsApplicationTypes CurrentApplicationType = clsApplicationTypes.Find(1);
        clsApplication Application = new clsApplication();

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (cltrPersonCardWithFilter1.PersonID == -1 )
            {
                MessageBox.Show("Please Selecte a Person ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            tabControl1.SelectedTab = tpApplicationInfo;
        }

        private void tpApplicationInfo_Click(object sender, EventArgs e)
        {

        }

        private void frmNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = clsLicenseClass.GetAllLicenseClasses();
            
            foreach(DataRow Row in dt.Rows)
            {
                cbLicensClass.Items.Add(Row["ClassName"]);
            }
            lblApplicationDate.Text = DateTime.Now.ToString();
            lblApplicationFees.Text = CurrentApplicationType.ApplicationFees.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName.ToString();
            cltrPersonCardWithFilter1.FilterFocus();

        }

       clsLocalDrivingLicenseApplication NewLocalDrivingLicense = new clsLocalDrivingLicenseApplication();
        private void btnSave_Click(object sender, EventArgs e)
        {
    
            int IsApplicationAlreadyExist = clsApplication.IsPerosnHasApplicationWithTheSameClassAndStatus(cltrPersonCardWithFilter1.PersonID, clsLicenseClass.Find(cbLicensClass.SelectedItem.ToString()).LicenseClassID);

            if (IsApplicationAlreadyExist !=-1)
            {
                MessageBox.Show("The Person Has Application with ID = "+ IsApplicationAlreadyExist +" ,Cannot Add !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            Application.ApplicantPersonID = cltrPersonCardWithFilter1.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            Application.ApplicationTypeID = CurrentApplicationType.ApplicationTypeID;
            Application.Status = clsApplication.enStatus.New;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = CurrentApplicationType.ApplicationFees;
            Application.LastStatusDate= DateTime.Now;
         
            if (Application.Save())
            {
                lblDrivingLicensApplicationID.Text = Application.ApplicationID.ToString();
                MessageBox.Show("Saved Successfuly", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
             
                
               if ( !NewLocalDrivingLicense.AddNewApplication(Application.ApplicationID, clsLicenseClass.Find(cbLicensClass.SelectedItem.ToString()).LicenseClassID))

                {
                    MessageBox.Show("Error In Save Local Driving License ", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
              
            }
        }
    }
}

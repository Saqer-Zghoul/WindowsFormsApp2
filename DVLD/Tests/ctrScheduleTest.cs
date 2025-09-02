using DVLD.Global_Class;
using DVLD.Properties;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Tests
{
    public partial class ctrScheduleTest : UserControl
    {
        public enum enMode { AddNew = 0, Update = 1 }
        enMode _Mode = enMode.AddNew;
        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 }
        enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;

        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _TestAppointmentID = -1;
        private clsTestAppointment _TestAppointment;

        public clsTestType.enTestType TestType
        {
            get
            {
                return _TestTypeID;   

            }
            set
            {
                _TestTypeID = value;
                switch (_TestTypeID)
                {

                    case clsTestType.enTestType.VisionTest:
                        gbTitleTest.Text = "Vision Test";
                        pictureBox1.Image = Resources.Vision_512;
                        break;


                    case clsTestType.enTestType.WrittenTest:
                        gbTitleTest.Text = "Written Test";
                        pictureBox1.Image = Resources.Written_Test_512;
                        break;

                    case clsTestType.enTestType.StreetTest:
                        gbTitleTest.Text = "Street Test";
                        pictureBox1.Image = Resources.driving_test_512;
                        break;
                }
            }
            }


        public ctrScheduleTest()
        {
            InitializeComponent();
        }
        bool LoadTestAppointmentData ()
        {
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            if (_TestAppointment == null )
            {
                MessageBox.Show("Error : there is no TestAppointment with Id = " + _TestAppointmentID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
                dateTimePicker1.MinDate = DateTime.Now;
            else
                dateTimePicker1.MinDate = _TestAppointment.AppointmentDate;

            if (_TestAppointment.RetakeTestApplicationID == -1)
            {
                gbRetakeTestInfo.Enabled = false;
                lblR_App_Fees.Text = "0";
                lblR_Test_App_ID.Text = "N/A";

                lblTitle.Text = "Schedule Test "; 
            }
            else
            {
                gbRetakeTestInfo.Enabled= true;
                lblR_Test_App_ID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
                lblR_App_Fees.Text  = _TestAppointment.RetakeTestAppInfo.PaidFees.ToString();
                lblTitle.Text = "Schedule Retake Test ";
                
            }
            
            return true;
        }
        bool _HandelTestAppointmentActiceConstrain()
        {
            if ( _Mode == enMode.AddNew && _LocalDrivingLicenseApplication.IsThereActiveScheduledTest(_TestTypeID))
            {
                MessageBox.Show("Person Already have Active Test Appointment for this test , \n" +
                    " you cannot add new Test Appointment for this person ", "error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                dateTimePicker1.Enabled = false;
                return false;

            }
            return true;
        }

        bool _HandelTestAppointmentLockedConstrain()
        {
            if (_TestAppointment.IsLocked)
            {
                lblUserMessage.Visible = true;

                lblUserMessage.Text = "Person Already sat for this test , Appointment Is Loked ";
                btnSave.Enabled = false;
                dateTimePicker1.Enabled = false;
                return false;

            }
            else
                lblUserMessage.Visible = false;

            
            return true ;
        }
        bool _HandelPreviousTestConstrain()
        {
            switch(_TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    lblUserMessage.Visible = false;
                    return true;

                case clsTestType.enTestType.WrittenTest:
                    if (!_LocalDrivingLicenseApplication.DoesPasseTestType(clsTestType.enTestType.VisionTest))
                    {
                        lblUserMessage.Text = "Cannot Shedule , Vision Test Should be passed first  ";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dateTimePicker1.Enabled = false;
                        return false;    
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        dateTimePicker1.Enabled=true;

                        lblUserMessage.Visible = false;
                        
                    }
                    return true;
                case clsTestType.enTestType.StreetTest:
                    if (!_LocalDrivingLicenseApplication.DoesPasseTestType(clsTestType.enTestType.WrittenTest))
                    {
                        lblUserMessage.Text = "Cannot Shedule , Written Test Should be passed first  ";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dateTimePicker1.Enabled = false;
                        return false;
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        dateTimePicker1.Enabled=true;

                        lblUserMessage.Visible = false;

                    }
                    return true;

            }

            return true;

        }
        public void LoadInfo (int LocalDrivingLicenseApplicationID , int TestAppointmentID =-1)
        {

            if (TestAppointmentID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode= enMode.Update;
           
            
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
           _TestAppointmentID = TestAppointmentID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.Find(LocalDrivingLicenseApplicationID);
            if ( _LocalDrivingLicenseApplication == null )
            {
                MessageBox.Show("Error: No Local Driving License Application with id = " + LocalDrivingLicenseApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }
            
            if (!_LocalDrivingLicenseApplication.DeosAttendTestType(_TestTypeID))
                _CreationMode=enCreationMode.FirstTimeSchedule;
            else
                _CreationMode=enCreationMode.RetakeTestSchedule;

            if(_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                gbRetakeTestInfo.Enabled= true;
                lblR_App_Fees.Text = clsApplicationTypes.Find((int)clsApplicationTypes.enApplicationType.RetakeTest).ApplicationFees.ToString();
                lblTitle.Text ="Schedule Retake Test ";
                lblR_Test_App_ID.Text = "0";
            }
            else
            {
                gbRetakeTestInfo.Enabled= false;
                lblR_Test_App_ID.Text = "N/A";
                lblR_App_Fees.Text = "0";
                lblTitle.Text = "Schedule Test";

            }
            lblDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblPersonName.Text =_LocalDrivingLicenseApplication.ApplicationInfo.PersonInfo.FullName.ToString();
            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrialPerTest(_TestTypeID).ToString();
            lblDrivingClassName.Text = _LocalDrivingLicenseApplication.LicenseClass.ClassName.ToString();
            if (_Mode == enMode.AddNew)
            {
                lblFees.Text = clsTestType.Find((int)_TestTypeID).TestTypeFees.ToString();
                dateTimePicker1.MinDate = DateTime.Now;
                lblR_Test_App_ID.Text = "N/A";
                _TestAppointment = new clsTestAppointment();
            }
            else
            {
                if(!LoadTestAppointmentData())
                return;
            }

            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblR_App_Fees.Text)).ToString();

            if (!_HandelTestAppointmentActiceConstrain())
                return;
            if (!_HandelTestAppointmentLockedConstrain())
                return;
            if (!_HandelRetakeTestApplication())
                return;



        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
         private bool _HandelRetakeTestApplication()
        {
         if (_Mode == enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)
            {
                clsApplication RetakeTestApplication = new clsApplication();
                RetakeTestApplication.ApplicationTypeID = (int)clsApplicationTypes.enApplicationType.RetakeTest;
                RetakeTestApplication.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicationInfo.PersonInfo.PersonID;
                RetakeTestApplication.PaidFees = clsApplicationTypes.Find((int)clsApplicationTypes.enApplicationType.RetakeTest).ApplicationFees;
                RetakeTestApplication.ApplicationDate = DateTime.Now;
                RetakeTestApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                RetakeTestApplication.LastStatusDate = DateTime.Now;
                RetakeTestApplication.Status = clsApplication.enStatus.Completed;
                if (!RetakeTestApplication.Save())
                {
                    MessageBox.Show("Faild to creation Applicatoin ", "Faild ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                _TestAppointment.RetakeTestApplicationID = RetakeTestApplication.ApplicationID;
            }
         return true;   
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandelRetakeTestApplication())
                return;

            _TestAppointment.TestTypeID =_TestTypeID;
            _TestAppointment.LoacalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _TestAppointment.AppointmentDate = dateTimePicker1.Value;
            
            if(_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successufly ", "Saved ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            else
            {
                MessageBox.Show("Error : Data deos not Saved  ", "Saved ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}

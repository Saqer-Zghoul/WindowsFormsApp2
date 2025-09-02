using DVLD.Properties;
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

namespace DVLD.Tests
{
    public partial class ctrScheduledTest : UserControl
    {
        public ctrScheduledTest()
        {
            InitializeComponent();
        }
        int _TestAppointmentID;
        clsTestAppointment _TestAppointment;
        clsTestType.enTestType _TestTypeID;
        int _LocalDrivinigLicenseApplicationID;
        clsLocalDrivingLicenseApplication _LocalDrivinigLicenseApplication;
        public clsTestType.enTestType TestTypeID
        {
            get { return _TestTypeID; }
        
            set
            {
                _TestTypeID = value;
                switch (_TestTypeID)
                {
                    case clsTestType.enTestType.VisionTest:
                        groupBox1.Text = "Vision Test";
                        pictureBox1.Image = Resources.Vision_512;
                        break;
                    case clsTestType.enTestType.WrittenTest:
                        groupBox1.Text = "Written Test";
                        pictureBox1.Image = Resources.Written_Test_512;
                        break;
                    case clsTestType.enTestType.StreetTest:
                        groupBox1.Text = "Street Test";
                        pictureBox1.Image = Resources.driving_test_512;
                        break;


                }
              // TestTypeID = _TestTypeID;
            }

        }


        private void ctrScheduledTest_Load(object sender, EventArgs e)
        {

        }
        public void LoadInfo (int TestAppointmentID)
        {
            _TestAppointmentID = TestAppointmentID;
            _TestAppointment = clsTestAppointment.Find(TestAppointmentID);
            if(_TestAppointment == null)
            {
                MessageBox.Show("Error: there is no test appointment with id = "+TestAppointmentID, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LocalDrivinigLicenseApplicationID = _TestAppointment.LoacalDrivingLicenseApplicationID;
            _LocalDrivinigLicenseApplication = clsLocalDrivingLicenseApplication.Find(_LocalDrivinigLicenseApplicationID);
            lblAppointmentDate.Text = _TestAppointment.AppointmentDate.ToString("dd/MM/yyyy");
            lblDrivingClassName.Text = _LocalDrivinigLicenseApplication.LicenseClass.ClassName.ToString();
            lblDrivingLicenseApplicationID.Text = _LocalDrivinigLicenseApplicationID.ToString();
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            lblPersonName.Text= _LocalDrivinigLicenseApplication.ApplicationInfo.PersonInfo.FullName.ToString();
            if (_TestAppointment.RetakeTestApplicationID !=-1)
            {
                lblTitle.Text = "Retake Scheduled Test";
            }
            else
            {
                lblTitle.Text = "Scheduled Test";
            }
            lblTrial.Text = _LocalDrivinigLicenseApplication.TotalTrialPerTest(_TestTypeID).ToString();
            lblDrivingLicenseApplicationID.Text = _LocalDrivinigLicenseApplicationID.ToString();
            if (_TestAppointment.IsLocked)
            {
                clsTest test = clsTest.FindByTestAppointmentID(_TestAppointment.TestAppointmentID);
                lblTestID.Text = test.TestID.ToString();
            }
            else
            {
                lblTestID.Text = "No Taken Yet! ";
            }
        }
    }
}

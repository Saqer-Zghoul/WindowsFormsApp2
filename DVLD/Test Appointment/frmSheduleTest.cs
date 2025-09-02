using DVLD.Global_Class;
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
using static DVLD_BusinessLayer.clsTestType;

namespace DVLD.Test_Appointment
{
    public partial class frmScheduleTest : Form
    {

        int _DrivigLicenseApplicationID;
        int _TestAppointmentID = -1 ;
        clsTestType.enTestType _TestTypeID;
     
        public frmScheduleTest(int DrivingLicenseApplicationID ,   clsTestType.enTestType TestTypeID, int TestAppointmentID = -1)
        {
            InitializeComponent();
            _DrivigLicenseApplicationID =DrivingLicenseApplicationID;
            _TestAppointmentID = TestAppointmentID;
            _TestTypeID =TestTypeID;

        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrScheduleTest1.TestType = _TestTypeID;
            ctrScheduleTest1.LoadInfo(_DrivigLicenseApplicationID, _TestAppointmentID);
        }

    }
}

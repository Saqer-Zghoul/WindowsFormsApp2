using DVLD.Properties;
using DVLD.Test_Appointment;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Tests
{
    public partial class frmListTestsAppointments : Form
    {
        int _LocalDrivingLicenseApplicationID ;
        
        DataTable _TestAppointment = new DataTable();
        public clsTestType.enTestType TestType;
        public frmListTestsAppointments(int LocalDrivingLicenseApplicationID,clsTestType.enTestType testType)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            TestType =testType;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListTestsAppointments_Load(object sender, EventArgs e)
        {
            cltrD_L_APP_Info1.LoadInfo(_LocalDrivingLicenseApplicationID);
            _TestAppointment = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, TestType);
            dataGridView1.DataSource = _TestAppointment;
        
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "Appointment ID";
                dataGridView1.Columns[0].Width = 100;

                dataGridView1.Columns[1].HeaderText = "Appointment Date";
                dataGridView1.Columns[1].Width = 250;

                dataGridView1.Columns[2].HeaderText = "Paid Fees";
                dataGridView1.Columns[2].Width = 150;

                dataGridView1.Columns[3].HeaderText = "Is Locked";
                dataGridView1.Columns[3].Width = 100;
                lblCountRecords.Text = dataGridView1.Rows.Count.ToString();

            }
            lblCountRecords.Text = "0";
            if (!_LoadImageAndTitleText())
                return;

        }
        private bool _LoadImageAndTitleText()
        {
            switch(TestType)
            {
                case clsTestType.enTestType.VisionTest:
                    lblTitle.Text = "Vision Test Appointments";
                    pictureBox1.Image = Resources.Vision_512;
                    return true;
                case clsTestType.enTestType.WrittenTest:
                    lblTitle.Text = "Writting Test Appointments";
                    pictureBox1.Image = Resources.Written_Test_512;
                    return true;
                case clsTestType.enTestType.StreetTest:
                    lblTitle.Text = "Street Test Appointments";
                    pictureBox1.Image = Resources.driving_test_512;
                    return true;
            }
            return false;
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication LoaclApp = clsLocalDrivingLicenseApplication.Find(_LocalDrivingLicenseApplicationID);
            if(LoaclApp.IsThereActiveScheduledTest(TestType))
            {

                MessageBox.Show("Person Already has Active Test Appointment for this test , \n" +
                    " you cannot add new Test Appointment for this person ", "error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            clsTest LastTest = clsTest.GetLastTestForApplication(LoaclApp.ApplicationInfo.PersonInfo.PersonID,LoaclApp.LicenseClassID,(int)TestType);
            if(LastTest== null)
            {
                frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, TestType);
                frm.ShowDialog();
                frmListTestsAppointments_Load(null, null);
                return;
            }
            if (LastTest.TestResult == true)
            {
                MessageBox.Show("The Person Already Passed for this Test , Cannot Insert New Appointment !! ", "Not Allowed ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmScheduleTest FRM  = new frmScheduleTest(_LocalDrivingLicenseApplicationID, TestType);
            FRM.ShowDialog();
            frmListTestsAppointments_Load(null, null);

        }

  
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, TestType, (int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListTestsAppointments_Load(null, null);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTakeTest frm = new frmTakeTest((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListTestsAppointments_Load(null, null);
        }
    }
}

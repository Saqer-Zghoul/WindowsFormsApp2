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

namespace DVLD.Test_Appointment
{
    public partial class frmTakeTest : Form
    {
        int _TestAppointmentID;
        
        public frmTakeTest(int TestAppointmentID)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
        }
        clsTestAppointment _TestAppointment;
        private void btnSave_Click(object sender, EventArgs e)
        {
          //  if (clsTest.FindByTestAppointmentID(_TestAppointmentID) != null
                clsTest NewTest = new clsTest();
            if (txtNotes.Text.Trim()   != "")
            NewTest.Notes= txtNotes.Text.Trim();
            NewTest.TestResult =Result;
            NewTest.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            NewTest.TestAppointmentID = _TestAppointmentID;
            _TestAppointment.IsLocked = true;
          
            if (NewTest.Save())
            {
                if (MessageBox.Show("Saved Successfuly ", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {

                    this.Close();
                    return;
                }
               
            }
            else
            {
                MessageBox.Show("Error in Save Edit ", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        bool Result = false;
        private void frmTakeTest_Load(object sender, EventArgs e)
        {

            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: there is No TestAppointment with id = "+ _TestAppointmentID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                rbFail.Enabled = false;
                rbPass.Enabled = false;
                return;
            }
            ctrScheduledTest1.TestTypeID = _TestAppointment.TestTypeID;
            ctrScheduledTest1.LoadInfo(_TestAppointmentID);
            txtNotes.Text = "";
            if (_TestAppointment.IsLocked)
            {
                rbFail.Enabled = false;
                rbPass.Enabled = false;
                lblUserMessage.Visible = true; 
                btnSave.Enabled= false;
        
                clsTest test = clsTest.FindByTestAppointmentID(_TestAppointmentID);
                if (test.TestResult == true)
                {
                    rbPass.Checked = true;  
                }
                else
                {
                    rbFail.Checked = true;
               
                }
                txtNotes.Enabled = false;
                txtNotes.Text = test.Notes;
            }

        }

        private void rbPass_CheckedChanged(object sender, EventArgs e)
        {
            Result = true;
        }

        private void rbFail_CheckedChanged(object sender, EventArgs e)
        {
            Result = false;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

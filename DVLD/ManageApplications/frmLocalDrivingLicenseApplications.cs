using DVLD.Issue_Driver_License;
using DVLD.Licenses;
using DVLD.Tests;
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

namespace DVLD.ManageApplications
{
    public partial class frmLocalDrivingLicenseApplications : Form
    {
        public frmLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }
        DataTable _dtListApplications = clsLocalDrivingLicenseApplication.GetListL_D_L_Applications();
        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _dtListApplications = clsLocalDrivingLicenseApplication.GetListL_D_L_Applications();
            dataGridView1.DataSource =_dtListApplications;
          cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Visible = false;

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "L.D.L.APPID";
                dataGridView1.Columns[0].Width = 100;


                dataGridView1.Columns[1].HeaderText = "Driving Class";
                dataGridView1.Columns[1].Width = 200;

                dataGridView1.Columns[2].HeaderText = "National No";
                dataGridView1.Columns[2].Width = 100;

                dataGridView1.Columns[3].HeaderText = "Full Name";
                dataGridView1.Columns[3].Width = 250;

                dataGridView1.Columns[4].HeaderText = "Application Date";
                dataGridView1.Columns[4].Width = 100;

                dataGridView1.Columns[5].HeaderText = "Passed Test";
                dataGridView1.Columns[5].Width = 100;

                dataGridView1.Columns[6].HeaderText = "Status";
                dataGridView1.Columns[6].Width = 75;

            }
            lblCountRecord.Text = dataGridView1.Rows.Count.ToString();
            
        }
        bool IsID = false;
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    IsID = true;
                    break;


                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Status":
                    FilterColumn = "Status";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }
            if (txtFilterValue.Text.Trim() =="" || FilterColumn == "None")
            {
                _dtListApplications.DefaultView.RowFilter = "";
                lblCountRecord.Text = dataGridView1.Rows.Count.ToString();
                return;

            }
            if (FilterColumn == "LocalDrivingLicenseApplicationID")
            {
                _dtListApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());

            }
            else
            {
                _dtListApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            }
            lblCountRecord.Text =dataGridView1.Rows.Count.ToString();

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cbFilterBy.SelectedIndex != 0)
            
            {
                txtFilterValue.Visible = true;
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
         
            
            else
            {
                txtFilterValue.Visible = false;
                _dtListApplications.DefaultView.RowFilter = "";
                lblCountRecord.Text = dataGridView1.Rows.Count.ToString();
                return;
            }

            if (cbFilterBy.SelectedItem == "L.D.L.AppID")
            {
                IsID = true;
            }
            else
            {
                IsID = false;
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (IsID)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // يمنع الإدخال
                }
            }  
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication L_D_L_APP = clsLocalDrivingLicenseApplication.Find((int)dataGridView1.CurrentRow.Cells[0].Value);

          
           

            if (L_D_L_APP.ApplicationInfo.Status == clsApplication.enStatus.Canceled)
            {
                MessageBox.Show("This Application Has Been Canceled Befor ! ", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Are You sure do want to cancel this application ?","Confirm" ,MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
            {

                L_D_L_APP.ApplicationInfo.Status = clsApplication.enStatus.Canceled;
            if (    L_D_L_APP.Update())
                {
                    MessageBox.Show("Done Successfuly  ");
                }
            else
                {
                    MessageBox.Show("Error in Save   ");


                }
            }
            frmLocalDrivingLicenseApplications_Load(null, null);
        }

        private void btnAddApplication_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frm = new frmNewLocalDrivingLicenseApplication();
            frm.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null, null);

        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You sure do want to Delete this application ?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
             if (   clsLocalDrivingLicenseApplication.DeleteLocalDrivingLicenseApplication((int)dataGridView1.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Delete Successfuly " , "Delete",MessageBoxButtons.OK,MessageBoxIcon.Information);

                }
             else
                {
                    MessageBox.Show("Delete Falide  ", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            frmLocalDrivingLicenseApplications_Load(null, null);

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int PassedTest = (int)dataGridView1.CurrentRow.Cells [5].Value;
            clsLocalDrivingLicenseApplication CurrentApplication =  clsLocalDrivingLicenseApplication.Find((int)dataGridView1.CurrentRow.Cells[0].Value);
            switch (CurrentApplication.ApplicationInfo.Status)
            {
                case clsApplication.enStatus.New:
                    sechduleTestToolStripMenuItem.Enabled = true;
                    issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                    editApplicationToolStripMenuItem.Enabled = true;
                    deleteApplicationToolStripMenuItem.Enabled = true;
                    cancelApplicationToolStripMenuItem.Enabled = true;
                    showLicenseToolStripMenuItem.Enabled=false ;

                    if (PassedTest== 0)
                    {
                        scheduleStreetTestToolStripMenuItem.Enabled = false;
                        scheduleWrittenTestToolStripMenuItem.Enabled=false;
                        scheduleVisionTestToolStripMenuItem.Enabled = true;


                    }
                    else if (PassedTest == 1)
                    {
                       
                        scheduleStreetTestToolStripMenuItem.Enabled = false;
                        scheduleWrittenTestToolStripMenuItem.Enabled =true;
                        scheduleVisionTestToolStripMenuItem.Enabled = false;

                    }
                    else if (PassedTest== 2)
                    {
                        scheduleStreetTestToolStripMenuItem.Enabled = true;
                        scheduleWrittenTestToolStripMenuItem.Enabled =false;
                        scheduleVisionTestToolStripMenuItem.Enabled = false;

                    }
                    else if (PassedTest == 3  )
                    {
                        sechduleTestToolStripMenuItem.Enabled = false;
                        issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;
                    }

                    break;

                case clsApplication.enStatus.Completed:

                    issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                    sechduleTestToolStripMenuItem.Enabled = false;
                    editApplicationToolStripMenuItem.Enabled=false;
                    deleteApplicationToolStripMenuItem.Enabled=false;
                    cancelApplicationToolStripMenuItem.Enabled=false;
                    showLicenseToolStripMenuItem.Enabled = true;

                    break;

                case clsApplication.enStatus.Canceled:

                    issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                    sechduleTestToolStripMenuItem.Enabled = false;
                    editApplicationToolStripMenuItem.Enabled=false;
                    deleteApplicationToolStripMenuItem.Enabled=false;
                    cancelApplicationToolStripMenuItem.Enabled=false;

                    showLicenseToolStripMenuItem.Enabled = false;

                    break;
            }



        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestsAppointments frm = new frmListTestsAppointments((int)dataGridView1.CurrentRow.Cells[0].Value,clsTestType.enTestType.VisionTest);
            frm.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null, null);

        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            
            frmListTestsAppointments frm = new frmListTestsAppointments((int)dataGridView1.CurrentRow.Cells[0].Value,clsTestType.enTestType.WrittenTest);
            
            frm.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null, null);

        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestsAppointments frm = new frmListTestsAppointments((int)dataGridView1.CurrentRow.Cells[0].Value,clsTestType.enTestType.StreetTest);
            
            frm.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null, null);
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLicense license = clsLicense.FindLicenseByApplicationID(
                clsLocalDrivingLicenseApplication.Find((int)dataGridView1.CurrentRow.Cells[0].Value).ApplicationID);
            frmShowLicenseInfo frm = new frmShowLicenseInfo(license.LicenseID);
            frm.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null, null);

        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueDriverLicense_FirstTime frm = new frmIssueDriverLicense_FirstTime((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null, null);

        }

        private void showLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmShowLicenseHistory frm = new frmShowLicenseHistory((string)dataGridView1.CurrentRow.Cells[2].Value);
            frm .ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null, null);

        }
    }
}

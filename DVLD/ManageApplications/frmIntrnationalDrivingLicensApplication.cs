using DVLD.Applications.New_Driver_License;
using DVLD.Licenses;
using DVLD.Licenses.InterNational_Licenses;
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
    public partial class frmIntrnationalDrivingLicensesApplication : Form
    {
        public frmIntrnationalDrivingLicensesApplication()
        {
            InitializeComponent();
        }
        DataTable _dtInternationalLicses;
        private void frmIntrnationalDrivingLicensApplication_Load(object sender, EventArgs e)
        {
            _dtInternationalLicses = clsInternationalLicense.ListInternationalLicenses();
            dataGridView1.DataSource = _dtInternationalLicses;
            if (dataGridView1.Rows.Count > 0 )
            {

                dataGridView1.Columns[0].HeaderText = "Int.Licsense ID";
                dataGridView1.Columns[0].Width = 100;

                dataGridView1.Columns[1].HeaderText = "Application ID";
                dataGridView1.Columns[1].Width = 100;

                dataGridView1.Columns[2].HeaderText = "Driver ID";
                dataGridView1.Columns[2].Width = 100;

                dataGridView1.Columns[3].HeaderText = "L.License ID";
                dataGridView1.Columns[3].Width = 100;

                dataGridView1.Columns[4].HeaderText = "Issue Date";
                dataGridView1.Columns[4].Width = 120;

                dataGridView1.Columns[5].HeaderText = "Expiration Date";
                dataGridView1.Columns[5].Width = 120;

                dataGridView1.Columns[6].HeaderText = "Is Active";
                dataGridView1.Columns[6].Width = 75;


            }
            lblCountRecords.Text = dataGridView1.Rows.Count.ToString();
            cbFiterBy.SelectedIndex = 0;
            txtFilterValue.Visible = false;

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // منع الإدخال
            }
        }

        private void cbFiterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFiterBy.Text != "None" && cbFiterBy.Text != "Is Active");
            cbIsActiveValue.Visible = (cbFiterBy.Text == "Is Active");
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            if (cbIsActiveValue.Visible)
            {
                cbIsActiveValue.SelectedIndex = 0;  
            }


        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterBy = "";
            switch (cbFiterBy.Text)
            {
            
                case "International License ID":
                    FilterBy = "InternationalLicenseID";
                    break;
                case "Local License ID":
                    FilterBy = "IssuedUsingLocalLicenseID";

                    break;
                case "Application ID":
                    FilterBy= "ApplicationID";
                    break;
                case "Driver ID":
                    FilterBy= "DriverID";
                    break;
                case "Is Active":
                    FilterBy = "IsActive";
                    break;
                default:
                   FilterBy ="None";

                    break;
            }
            if (txtFilterValue.Text.Trim() == "" || FilterBy == "None")
            {
                _dtInternationalLicses.DefaultView.RowFilter = "";
                lblCountRecords.Text = dataGridView1.Rows.Count.ToString();
                return;

            }

            _dtInternationalLicses.DefaultView.RowFilter = string.Format("[{0}] = {1}",FilterBy,txtFilterValue.Text.Trim());
            lblCountRecords.Text = dataGridView1.Rows.Count.ToString();
        }

        private void cbIsActiveValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbIsActiveValue.Text)
            {
                case "Yes":
                    _dtInternationalLicses.DefaultView.RowFilter = string.Format("IsActive = {0}", 1);
                    break;
                case "No":
                    _dtInternationalLicses.DefaultView.RowFilter = string.Format("IsActive = {0}", 0);
                    break;
                case "All" :
                    _dtInternationalLicses.DefaultView.RowFilter = "";
                    break;
            }
            lblCountRecords.Text = dataGridView1.Rows.Count.ToString();
        }

        private void btnAddNewInternationalApplication_Click(object sender, EventArgs e)
        {
            frmNewInterNationalDriverLicense frm = new frmNewInterNationalDriverLicense();
            frm.ShowDialog();
            frmIntrnationalDrivingLicensApplication_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsDriver Driver = clsDriver.Find((int)dataGridView1.CurrentRow.Cells[2].Value);
            frmPersonInfo frm = new frmPersonInfo(Driver.PersonID);
            frm.ShowDialog();
             
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsDriver Driver = clsDriver.Find((int)dataGridView1.CurrentRow.Cells[2].Value);
            frmShowLicenseHistory frm = new frmShowLicenseHistory(Driver.PersonID);
            frm.ShowDialog();

        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalDriverLicenseInfo frm = new frmInternationalDriverLicenseInfo((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}

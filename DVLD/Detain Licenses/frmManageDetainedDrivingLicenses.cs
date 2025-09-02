using DVLD.Licenses;
using DVLD.Users;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Detain_Licenses
{
    public partial class frmManageDetainedDrivingLicenses : Form
    {
        public frmManageDetainedDrivingLicenses()
        {
            InitializeComponent();
        }
        DataTable _dtListDetain = clsDetainLicense.GetListDetainedLicenses();
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageDetainedDrivingLicenses_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Visible = false;
            _dtListDetain = clsDetainLicense.GetListDetainedLicenses();
            dataGridView1.DataSource = _dtListDetain;
            if (dataGridView1.Rows.Count > 0) 
            {

                dataGridView1.Columns[0].HeaderText = "D.ID";
                dataGridView1.Columns[0].Width = 100;

                dataGridView1.Columns[1].HeaderText = "L.ID";
                dataGridView1.Columns[1].Width = 100;

                dataGridView1.Columns[2].HeaderText = "D.Date";
                dataGridView1.Columns[2].Width = 120;

                dataGridView1.Columns[3].HeaderText = "Is Released";
                dataGridView1.Columns[3].Width = 75;

                dataGridView1.Columns[4].HeaderText = "Fine Fees";
                dataGridView1.Columns[4].Width = 75;

                dataGridView1.Columns[5].HeaderText = "Released Date";
                dataGridView1.Columns[5].Width = 120;

                dataGridView1.Columns[6].HeaderText = "N.No";
                dataGridView1.Columns[6].Width = 100;

                dataGridView1.Columns[7].HeaderText = "Full Name";
                dataGridView1.Columns[7].Width = 200;

                dataGridView1.Columns[8].HeaderText = "Released App.ID";
                dataGridView1.Columns[8].Width = 100;

            }
            lblCountRecord.Text = dataGridView1.Rows.Count.ToString();

        }
        bool isID = false;
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None" &&  cbFilterBy.Text != "Is Released");
            cbIsReleaseValue.Visible = (cbFilterBy.Text == "Is Released");

            txtFilterValue.Text = "";
            cbIsReleaseValue.SelectedIndex = 0;
            if (txtFilterValue.Visible)
            {

                txtFilterValue.Text = "";
                txtFilterValue.Focus();

            }
            if (cbFilterBy.Text == "Detain ID" || cbFilterBy.Text == "License ID" || cbFilterBy.Text == "Release Application ID")
            {
                isID = true;
            }
            else
            {
                isID = false;
            }

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterBy = "";
            switch (cbFilterBy.Text)
            {
                case "Detain ID":
                    FilterBy = "DetainID";
                    break;

                case "License ID":
                    FilterBy = "LicenseID";
                    break;

                case "Release Application ID":
                    FilterBy = "ReleaseApplicationID";
                    break;

                case "National No":
                    FilterBy = "NationalNo";
                    break;

                case "Full Name":
                    FilterBy = "FullName";
                    break;

                default:
                    FilterBy = "None";
                    break;
            }
            if (txtFilterValue.Text.Trim() =="" || FilterBy == "None")
            {
                _dtListDetain.DefaultView.RowFilter = "";
                lblCountRecord.Text = dataGridView1.Rows.Count.ToString();
                return;

            }
            if (FilterBy == "LicenseID" || FilterBy == "ReleaseApplicationID" || FilterBy == "DetainID")
            {
                _dtListDetain.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterBy, txtFilterValue.Text.Trim());

            }
            else
            {
                _dtListDetain.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterBy, txtFilterValue.Text.Trim());

            }
            lblCountRecord.Text =dataGridView1.Rows.Count.ToString();

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (isID)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // يمنع الإدخال
                }
            }
        }

        private void cbIsReleaseValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbIsReleaseValue.Text)
            {
                case "All":
                    _dtListDetain.DefaultView.RowFilter = "";
                    break;
                case "Yes":
                    _dtListDetain.DefaultView.RowFilter = string.Format("IsReleased =  {0}", 1);


                    break;
                case "No":
                    _dtListDetain.DefaultView.RowFilter = string.Format("IsReleased =  {0}", 0);

                    break;


            }

        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
            frmManageDetainedDrivingLicenses_Load(null, null);


        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
            frmManageDetainedDrivingLicenses_Load(null, null);
        }

        private void showPersonDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmPersonInfo frm = new frmPersonInfo((string)dataGridView1.CurrentRow.Cells[6].Value);
            frm.ShowDialog();

          

        }

        private void showLicenseDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo((int)dataGridView1.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
           
        }

        private void showPersonLicenseHistoryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmShowLicenseHistory frm = new frmShowLicenseHistory((string)dataGridView1.CurrentRow.Cells[6].Value);
            frm.ShowDialog();
          
        }

        private void releaseDetainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense((int)dataGridView1.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
            frmManageDetainedDrivingLicenses_Load(null, null);
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            if ((bool)dataGridView1.CurrentRow.Cells[3].Value == false)
            {
                releaseDetainLicenseToolStripMenuItem.Enabled= true;
            }
            else
            {
                releaseDetainLicenseToolStripMenuItem.Enabled=false;
            }
        }
    }
}

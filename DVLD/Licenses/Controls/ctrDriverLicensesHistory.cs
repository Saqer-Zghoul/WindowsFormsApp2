using DVLD.Licenses.InterNational_Licenses;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.Controls
{
    public partial class ctrDriverLicensesHistory : UserControl
    {
        public ctrDriverLicensesHistory()
        {
            InitializeComponent();
        }
        int _DriverID = -1;
   

        public void LoadInfo (int DriverID)
        {
            _DriverID = DriverID;
            TabControl.SelectedTab = tpLocalLicenses;

            LoadLocalLicensesHistory();
        }
        void LoadLocalLicensesHistory()
        {


            dgvLocalLicenses.DataSource = clsLicense.GetLocalLicensesPerDriverID(_DriverID);
            if (dgvLocalLicenses.Rows.Count>0)
            {
                dgvLocalLicenses.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicenses.Columns[0].Width= 100;


                dgvLocalLicenses.Columns[1].HeaderText = "App.ID";
                dgvLocalLicenses.Columns[1].Width= 100;

                dgvLocalLicenses.Columns[2].HeaderText = "Class Name";
                dgvLocalLicenses.Columns[2].Width= 200;

                dgvLocalLicenses.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicenses.Columns[3].Width= 150;

                dgvLocalLicenses.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicenses.Columns[4].Width= 150;

                dgvLocalLicenses.Columns[5].HeaderText = "Is Active";
                dgvLocalLicenses.Columns[5].Width= 75;
            }
            lblLocalLicensesCount.Text = dgvLocalLicenses.Rows.Count.ToString();
        }
        void LoadInternatioanlLicensesHistory  ()
        {


            dgvInternationalLicenses.DataSource = clsInternationalLicense.GetLicensesPerDriverID(_DriverID); 

            if (dgvInternationalLicenses.Rows.Count>0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenses.Columns[0].Width= 100;


                dgvInternationalLicenses.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenses.Columns[1].Width= 100;
              
                dgvInternationalLicenses.Columns[2].HeaderText = "L.License ID";
                dgvInternationalLicenses.Columns[2].Width= 100;
            
                dgvInternationalLicenses.Columns[3].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns[3].Width= 150;
          
                dgvInternationalLicenses.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns[4].Width= 150;
         
                dgvInternationalLicenses.Columns[5].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns[5].Width= 75;
            }
            lblInternationalLicensesCount.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }

        private void tpLocalLicenses_Click(object sender, EventArgs e)
        {
            LoadLocalLicensesHistory();
        }

        private void tpInternationalLicenses_Click(object sender, EventArgs e)
        {
            LoadInternatioanlLicensesHistory();

        }

        private void ctrDriverLicensesHistory_Load(object sender, EventArgs e)
        {

        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo((int)dgvLocalLicenses.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

        }

        private void showLicenseDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmInternationalDriverLicenseInfo frm = new frmInternationalDriverLicenseInfo((int)dgvInternationalLicenses.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

        }
    }
}

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

namespace DVLD.Licenses
{
    public partial class ctrDriverLicenseInfoWithFilter : UserControl
    {
        public event Action<int> OnLicenseSelected;
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            {
                handler(LicenseID);
            }
        }
        private bool _FilterEnable = true;
        public  bool FilterEnable
        {
            get
            {
                return _FilterEnable;

            }
            set
            {
                _FilterEnable= value;
                gbFiltering.Enabled = _FilterEnable;
            }
        }
        private int _LicenseID = -1;

      
        public int LicenseID
        {

            get
            {
                return ctrDriverLicenseInfo1.LicenseID;

            }
        }
        public clsLicense SelectedLicenseInfo
        {
            get { return ctrDriverLicenseInfo1.LicenseInfo; }
        }
        public ctrDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        public void LoadLicenseInfo (int LicenseID)
        {
            txtFilterValue.Text = LicenseID.ToString(); 
            ctrDriverLicenseInfo1.LoadInfo(LicenseID);

            if (OnLicenseSelected != null && FilterEnable)
            {
                OnLicenseSelected(ctrDriverLicenseInfo1.LicenseID);
            }
        }
        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }
        private void DataBackEvent(object sender, int LicenseID)
        {
            
            txtFilterValue.Text = LicenseID.ToString();
            ctrDriverLicenseInfo1.LoadInfo(LicenseID);
        }
        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar ==(char)13)
            {
                btnFindPerson.PerformClick();

            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // منع الإدخال
            }
        }

        private void btnFindPerson_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Filed are not Valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            ctrDriverLicenseInfo1.LoadInfo(int.Parse(txtFilterValue.Text.Trim()));

            if (OnLicenseSelected != null && FilterEnable)
            {
                OnLicenseSelected(ctrDriverLicenseInfo1.LicenseID);
            }

        }

        private void txtFilterValue_Validated(object sender, EventArgs e)
        {

        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterValue.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "Please You are must Enter a value !");


            }
            else
            {
                errorProvider1.SetError(txtFilterValue, null);
            }
        }
    }
}

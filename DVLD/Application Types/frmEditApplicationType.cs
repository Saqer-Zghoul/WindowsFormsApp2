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

namespace DVLD.Application_Types
{
    public partial class frmEditApplicationType : Form
    {
        int _ApplicationTypeID ;
        clsApplicationTypes _Applicatoin;

        public frmEditApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID= ApplicationTypeID;    
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            _Applicatoin = clsApplicationTypes.Find(_ApplicationTypeID);
            if (_Applicatoin == null)
            {
                MessageBox.Show("Application Type With ID " + _ApplicationTypeID +" Cannot Be Found , Please Try Again ", "Searching ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblApplicationID.Text= _Applicatoin.ApplicationTypeID.ToString();    
            txtTitle.Text = _Applicatoin.ApplicationTypeTitle.ToString();
            txtFees.Text = _Applicatoin.ApplicationFees.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Feild are not valid ", "Validation",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;

            }
            _Applicatoin.ApplicationTypeTitle = txtTitle.Text.Trim();
            _Applicatoin.ApplicationFees = Convert.ToSingle (txtFees.Text.Trim());
if (_Applicatoin.Save())
            {
                MessageBox.Show("Saved Successfuly ", "Saved ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
else
            {
                MessageBox.Show("Saved Falid ", "Saved ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // يمنع الإدخال
            }
        }
        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This is required!");

            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}

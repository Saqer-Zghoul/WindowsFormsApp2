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

namespace DVLD.Test_Types
{
    public partial class frmEditTestType : Form
    {
        int _TestTypeID = -1;
        clsTestType _TestType;
        public frmEditTestType(int TestTypeID)
        {
            InitializeComponent();

            _TestTypeID = TestTypeID;
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "This field cannot be empty ! ");
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);
            }
        }

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDescription, "This field cannot be empty ! ");
            }
            else
            {
                errorProvider1.SetError(txtDescription, null);
            }
        }
        public static bool IsNumber(string input)
        {
            return decimal.TryParse(input, out _);
        }
        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "This field cannot be empty ! ");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }

            if (! IsNumber(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number  ! ");
            }
            else
            {
                errorProvider1.SetError(txtFees, null );

            }
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            _TestType = clsTestType.Find(_TestTypeID);
            if (_TestType == null)
            {
                MessageBox.Show("Test Type With ID " + _TestTypeID +" Cannot Be Found , Please Try Again ", "Searching ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                txtDescription.Text = _TestType.TestDescriptionType.ToString();
                lblTestTypeID.Text =_TestTypeID.ToString() ;
                txtFees.Text = _TestType.TestTypeFees.ToString();
                txtTitle.Text = _TestType.TestTypeTitle.ToString();
                    
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("some Field are not Valid!", "Validation ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _TestType.TestTypeTitle = txtTitle.Text.Trim();
            _TestType.TestDescriptionType = txtDescription.Text.Trim();
            _TestType.TestTypeFees = Convert.ToSingle(txtFees.Text.Trim());
            if (_TestType.Save())
            {
                MessageBox.Show("Saved Successfuly ", "Saved ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Saved Falid ", "Saved ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

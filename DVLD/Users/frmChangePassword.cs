using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmChangePassword : Form
    {
        int _UserID;
        clsUser _User;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
        _UserID = UserID;
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _User = clsUser.Find(_UserID);
            if (_User == null)
            {
                MessageBox.Show("The User Cannot be Found ! ", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            cltrUserCardDetails1.LoadInfo(_User.UserID);
            txtNewPassword.Enabled= false;
            txtConfirmNewPassword.Enabled = false;
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtCurrentPassword.Text.Trim() != _User.Password.Trim())
            {
                errorProvider1.SetError(txtCurrentPassword, "Current Password is Wrong !");
                txtNewPassword.Enabled= false;
                txtConfirmNewPassword.Enabled = false;
                e.Cancel = true;
            }
            else {
                txtNewPassword.Enabled = true;
                txtConfirmNewPassword.Enabled = true;

                errorProvider1.SetError(txtCurrentPassword, null); }
         //   e.Cancel = false;
        }
        bool isValid = false;
        private void txtConfirmNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmNewPassword.Text.Trim() != txtNewPassword.Text.Trim())
            {
                errorProvider1.SetError(txtConfirmNewPassword, "Password Confirmation doesn`t match Password !");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(txtConfirmNewPassword, null);
                isValid = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!isValid)
            {
                MessageBox.Show("Some Feild is required !","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return; 
            }
            _User.Password = txtNewPassword.Text.Trim() ;
            if (_User.Save())
            {
                MessageBox.Show("Saved Successuly ! ","Saved",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}

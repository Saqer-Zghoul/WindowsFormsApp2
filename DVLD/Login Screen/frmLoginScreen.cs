using DVLD.Global_Class;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Login_Screen
{
    public partial class frmLoginScreen : Form
    {
        public frmLoginScreen()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser User = clsUser.GetUserInfoByUserNameAndPassword(txtUserName.Text.Trim(),txtPassword.Text.Trim());
            if (User != null)
            {
                if (cbRememberMe.Checked)
                {
                    clsGlobal.RememberUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    clsGlobal.RememberUsernameAndPassword("", "");

                }
                if (!User.IsActive)
                {
                    txtUserName.Focus();
                    MessageBox.Show("Your accountt is not Active , Contact Admin", "In Activation ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
             
                    clsGlobal.CurrentUser = User;
                    this.Hide();
                    frmMain2 frm = new frmMain2(this);
                    frm.ShowDialog();
                
           
            }
            else
            {
                MessageBox.Show("Invalid UserName/Password ! ", "Login Error ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
          
            


        }

        private void frmLoginScreen_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";
            if (clsGlobal.GetStoredCredential(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
            txtPassword.Text = Password;
                cbRememberMe.Checked= true; 

            }
            else
            {
                cbRememberMe.Checked=false;

            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

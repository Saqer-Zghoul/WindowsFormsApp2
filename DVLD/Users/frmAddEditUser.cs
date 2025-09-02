using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmAddEditUser : Form
    {
        public frmAddEditUser()
        {
            InitializeComponent();
            Mode = enMode.AddNew;

        }
        public frmAddEditUser(int UserID)
        {
            InitializeComponent();
            Mode = enMode.Update;
            _UserID = UserID;

        }
        clsUser _User;
        int _UserID = -1;
        int _PersonID = -1;
        clsPerson _Person;
        enum enMode { AddNew, Update }
        enMode Mode;
        private void pbFindPerson_Click(object sender, EventArgs e)
        {
            _Person = clsPerson.Find(txtFilterValue.Text.Trim());
            if (_Person == null)
            {
                MessageBox.Show("The Person With NationalNo = " + txtFilterValue.Text +" Does Not Exist ", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            cltrPersonCardDetails1.linkLabel1.Enabled = true;
            _PersonID = _Person.PersonID;
            cltrPersonCardDetails1.LoadPersonIfo(_Person.PersonID);


        }

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            if (Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New User ";
                cbFilterBy.SelectedIndex = 0;
                cltrPersonCardDetails1.linkLabel1.Enabled = false;
                _User = new clsUser();
            }
            else
            {
                lblTitle.Text ="Update User "; 
                _User = clsUser.Find(_UserID);
                _Person = clsPerson.Find(_User.PersonID);
                cltrPersonCardDetails1.LoadPersonIfo(_User.PersonID);

                gbFiltering.Enabled = false;
                LoadData();
            }
        }
        void LoadData ()
        {

            _User = clsUser.Find(_UserID);
            if (_User == null)
            {
                MessageBox.Show("The User With ID "+ _UserID +" Cannot Found ! ", " Finding  ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblTitle.Text = "Update User";
            lblUserID.Text = _User.UserID.ToString();
            txtConfirmPassword.Text = _User.Password.ToString();
            txtPassword.Text = _User.Password.ToString();
            txtUserName.Text = _User.UserName.ToString();
            if (_User.IsActive)
            {
                cbIsActive.Checked = true;


            }
            else
            {
                cbIsActive.Checked = false ;

            }
        }
        private void pbFindPerson_MouseEnter(object sender, EventArgs e)
        {
            pbFindPerson.BackColor = Color.LightGray;
        }

        private void pbFindPerson_MouseLeave(object sender, EventArgs e)
        {
            pbFindPerson.BackColor = Color.Transparent;
        }

        private void pbAddPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.DataBack +=DataBackFormAddNewPersonForm;
            frm.Show();
        }
        private void DataBackFormAddNewPersonForm(object sender, int PersonID)


        {
            _PersonID = PersonID;
            _Person = clsPerson.Find(_PersonID);
            if (_Person == null)
            {
                MessageBox.Show("The Person with ID = "+ PersonID +" Cannot be Found ! " , "Worring ", MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
                return; 

            }

            cltrPersonCardDetails1.LoadPersonIfo(_PersonID);





        }

        private void pbAddPerson_MouseEnter(object sender, EventArgs e)
        {
            pbAddPerson.BackColor = Color.LightGray;
        }

        private void pbAddPerson_MouseLeave(object sender, EventArgs e)
        {
            pbAddPerson.BackColor = Color.Transparent;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if ( Mode == enMode.Update)
            {
                tabControl1.SelectedTab = tpUserInf;
                return;
            }
            if (_Person == null || _PersonID ==-1)
            {
                MessageBox.Show("Please Select a Person to insert him into Users ", " Attention ", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                return;
            }
            if (clsUser.IsExist(_Person.PersonID))
            {
                MessageBox.Show("The Person with ID = " + _Person.PersonID +" Already Exist ! ", "Attention ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            tabControl1.SelectedTab = tpUserInf;
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

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "The UserName cannot be blank ");
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            }

        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "The Password  cannot be blank ");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
          if (txtPassword.Text.Trim()!= txtConfirmPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Does not the same ");
            }
          else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }

        }
        //Save 
        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren()){
                MessageBox.Show("Some Field are not valid ");
                    return;

            }
            _User.UserName = txtUserName.Text.Trim();
            _User.Password = txtPassword.Text.Trim();
            _User.PersonID = _Person.PersonID;
            _User.IsActive = cbIsActive.Checked;

            if (_User.Save())
            {
                gbFiltering.Enabled=false;

                MessageBox.Show("Saved Successfuly ! ", "Saved ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Mode = enMode.Update;

                _UserID = _User.UserID;

                LoadData();
            }
            else
            {
                MessageBox.Show("Saved Fiald ! ", "Saved ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
         
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

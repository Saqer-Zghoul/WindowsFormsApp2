using DVLD.Properties;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmAddEditPerson : Form
    {

        public delegate void DataBackEvent(object sender, int PersonID);
        public event DataBackEvent DataBack;
        enum enMode { Addnew = 0, Update = 1 }
        enMode Mode = enMode.Addnew;
        enum enGendor { Male, Femal }
        enGendor Gendor = enGendor.Male;
        clsPerson _Person;
        int _PersonID = -1;

        public frmAddEditPerson(int PersonID)
        {
            InitializeComponent();

            _PersonID = PersonID;
           
            Mode = enMode.Update;

        }
        public frmAddEditPerson()
        {
            InitializeComponent();
         
            Mode = enMode.Addnew;

        } 
        private void _LoadData()
        {
            _Person = clsPerson.Find(_PersonID);
            if (_Person != null)
            {
                lblPerson.Text = _Person.PersonID.ToString();
                txtAddress.Text = _Person.Address.ToString();
                txtEmail.Text = _Person.Email.ToString();
                txtFirsName.Text= _Person.FirstName.ToString();
                txtLastName.Text = _Person.LastName.ToString();
                txtThirdName.Text = _Person.ThirdName.ToString();
                txtSecondName.Text = _Person.SecondName.ToString();
                txtPhone.Text = _Person.Phone.ToString();
                rbMale.Checked =  _Person.Gendor == 0 ? true : false;
                rbFemale.Checked =  _Person.Gendor == 0 ? false : true;
                comboBox1.SelectedItem= clsCountry.Find(_Person.NationalityCountryID).CountryName;
                txtNationalNo.Text = _Person.NationalNo.ToString();

                if (_Person.ImageBath !=null)
                    pbPersonImage.ImageLocation = _Person.ImageBath;

                llRemoveImage.Visible = (_Person.ImageBath!="");
            }else
            {
                MessageBox.Show("The Person Cannot found ! ");
            }
          
        }
        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {
            DataTable Countries = clsCountry.GetAllCountries();

            foreach (DataRow country in Countries.Rows)
            {
                comboBox1.Items.Add(country["CountryName"].ToString());
            }

            if (Mode == enMode.Addnew)
            {
                _Person = new clsPerson();
                lblTitle.Text = "Add New Person";
                pbPersonImage.Image = Resources.person_boy;
                rbMale.Checked = true;
                comboBox1.SelectedIndex = 0;
            }

            else
            {
                lblTitle.Text = "Update Person ";

                _LoadData();
            }

        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
            {
                pbPersonImage.Image = Resources.person_boy;
            }
            Gendor = enGendor.Male;

        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {

            if (pbPersonImage.ImageLocation == null)
            {
                pbPersonImage.Image = Resources.person_girl;
            }
            Gendor = enGendor.Femal;

        }
        private bool HandelPersonImage()
        {
            if (pbPersonImage.ImageLocation != _Person.ImageBath)
            {

                if (_Person.ImageBath !="")
                {
                    try
                    {
                        File.Delete(_Person.ImageBath);
                    }
                    catch (IOException)
                    {

                    }
                }

                if (pbPersonImage.ImageLocation !=null) {
                    string SourceImageFile = pbPersonImage.ImageLocation.ToString();
                    if (clsUtil.CopyImageToProkectImageFolder(ref SourceImageFile))
                    {
                        pbPersonImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File ", "Error", MessageBoxButtons.OK);
                        return false;
                    }
                }


            }
            return true;
        }
        bool IsSaved = false ;

        private bool Save()
        {

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Fields are not valid ! ");
                return false; 
            }
            if (!HandelPersonImage())
            {
                MessageBox.Show("The Image Does not Saved ");
                return false;
            }
            _Person.FirstName = txtFirsName.Text.ToString();
            _Person.LastName = txtLastName.Text.ToString();
            _Person.ThirdName = txtThirdName.Text.ToString();
            _Person.SecondName = txtSecondName.Text.ToString();
            _Person.Address = txtAddress.Text.ToString();
            _Person.Email=  txtEmail.Text.ToString();

            _Person.NationalityCountryID = clsCountry.Find(comboBox1.SelectedItem.ToString()).CountryID;

            _Person.DateOfBirth = dateTimePicker1.Value;
            _Person.Phone = txtPhone.Text.ToString();
            _Person.Gendor = (byte)(Gendor == enGendor.Male ? 0 : 1);
            _Person.NationalNo = txtNationalNo.Text.ToString();


            if (pbPersonImage.ImageLocation != null)
            {
                _Person.ImageBath = pbPersonImage.ImageLocation;
            }
            else
            {
                _Person.ImageBath = "";
            }
           if ( _Person.Save())
            {
                _PersonID = _Person.PersonID;
                lblTitle.Text = "Update Person";
               lblPerson.Text = _Person.PersonID.ToString();
                
                return true;
            }
            else 
            {
                MessageBox.Show("Erorr in Save ");
                return  false;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                MessageBox.Show("Saved Successfuly", "Attention", MessageBoxButtons.OK);
                _LoadData();
                IsSaved = true;

            }
            else
            {
                MessageBox.Show("Faild Save ");
            }
            _LoadData();
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files |*.jpg ; *.jpeg; *.png ; *.gif ; *.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)

            {
                string selectedFilePath = openFileDialog1.FileName;
                pbPersonImage.Load(selectedFilePath);
                llRemoveImage.Visible = true;
            }

        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;

            if (Gendor == enGendor.Male)
            {
                pbPersonImage.Image= Resources.person_boy;

            }
            else
            {
                pbPersonImage.Image = Resources.person_girl;

            }
        }
        private void  ValidateEmptyTextBox (object sender , CancelEventArgs e)
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

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text.Trim() == "")
            {
                return;
            }
            if (!clsValidation.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Enter a Valid Email Please ! ");
            }else
            {
                errorProvider1.SetError(txtEmail, null);
            }

        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This Field is Required!");
                return;

            }

            else
            {

                errorProvider1.SetError(txtNationalNo, null);
            }
            if (_Person.NationalNo != txtNationalNo.Text.Trim() && clsPerson.IsPersonExist(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "NationalNo allready exist , please try again with different NationalNo");

            }
            else
                errorProvider1.SetError(txtNationalNo, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (IsSaved)
            DataBack?.Invoke(this, _PersonID);
            this.Close();
        }
    } 
}

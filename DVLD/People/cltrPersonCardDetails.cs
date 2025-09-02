using DVLD.Properties;
using DVLD_BusinessLayer;
using System;
using System.Data.Sql;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Forms;

namespace DVLD
{
    public partial class cltrPersonCardDetails : UserControl
    {
        private int _PersonID = -1;
        public int PersonID { 
            get
            {
                return _PersonID;
            }
        }

        public clsPerson _Person;
        
       
        public cltrPersonCardDetails()
        {
            InitializeComponent();

        }
        public void LoadPersonIfo(int PersonID)
        {
            
            _Person = clsPerson.Find(PersonID);
            _PersonID = PersonID;
            if (_Person == null)
            {

                MessageBox.Show("The Person Cannot Found ! ID = " +PersonID);
                return;
            }
            _FillPersonInfo();
        }
        public void LoadPersonIfo(string  NationalNo)
        {

            _Person = clsPerson.Find(NationalNo);
            if (_Person == null)
            {

                MessageBox.Show("The Person Cannot Found ! NationalNo = " + NationalNo);
                return;
            }    
            _PersonID = _Person.PersonID;

            _FillPersonInfo();
        }
        private void _FillPersonInfo()
        {
            _PersonID = _Person.PersonID;
            lblName.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;
            lblNatonalNo.Text = _Person.NationalNo;
            lblAddress.Text = _Person.Address;


            lblGendor.Text = _Person.Gendor == 0 ? "Male" : "Female";
            if (_Person.Gendor == 0)
            {
                pbImageGengdor.Image = Resources.user;

            }
            else
            {
                pbImageGengdor.Image = Resources.ambassador_female;

            }
            lblPhone.Text = _Person.Phone;
            lblEmail.Text = _Person.Email;
            
           lblPersonID.Text = _Person.PersonID.ToString();
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString();
            lblCountry.Text=_Person.CountryInfo.CountryName;

            _LoadImagePath();

        }

        private void _LoadImagePath()
        {
            if (_Person.Gendor ==0)
            {
                pictureBox1.Image = Resources.person_boy;
            }
            else
            {
                pictureBox1.Image = Resources.person_girl;

            }
            string ImagePath = _Person.ImageBath;
            if (ImagePath !="")

            {
                if (File.Exists(ImagePath))
                {
                    pictureBox1.ImageLocation = ImagePath;
                }

                else
                {
                    MessageBox.Show("The File with path [ " + ImagePath +" ] Does Not Exist ");
                }

            }
        }

        private void cltrPersonCardDetails_Load(object sender, EventArgs e)
        {
        }

        private void lblCountry_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPerson frm  = new frmAddEditPerson(_Person.PersonID);
            frm.ShowDialog();
            LoadPersonIfo(_Person.PersonID);
        }

        private void koko_Click(object sender, EventArgs e)
        {

        }
    }
}

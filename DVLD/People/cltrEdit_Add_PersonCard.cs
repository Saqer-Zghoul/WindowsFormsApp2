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

namespace DVLD
{
    public partial class cltrEdit_Add_PersonCard : UserControl
    {
        public cltrEdit_Add_PersonCard()
        {
            InitializeComponent();
        }
        public void AddNew ()
        {
            clsPerson Person = new clsPerson();

        }


     public void Update (int PersonID)
        {
            DataTable Countries = clsCountry.GetAllCountries();

            foreach (clsCountry country in Countries.Rows)
            {
                comboBox1.Items.Add(country.CountryName.ToString());
            }
            clsPerson Person = clsPerson.Find(PersonID);
            if (PersonID != null)
            {
                txtAddress.Text = Person.Address.ToString();
                txtEmail.Text = Person.Email.ToString();
                txtFirsName.Text= Person.FirstName.ToString();
                txtLastName.Text = Person.LastName.ToString();
                txtThirdName.Text = Person.ThirdName.ToString();    
                txtSecondName.Text = Person.SecondName.ToString();
                txtPhone.Text = Person.Phone.ToString();
                rbMale.Checked =  Person.Gendor == 0 ? true : false;
                rbFemale.Checked =  Person.Gendor == 0 ? false : true;
                comboBox1.SelectedItem= Person.CountryInfo.CountryName;





            }
            else
            {
                MessageBox.Show("The Person Cannot Found !");
                return;
            }
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Images File|*.jpg;*.jpeg;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog1.FileName;
                pbPersonImage.Load(selectedFilePath);
                llRemoveImage.Visible= true;

            }
        
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}

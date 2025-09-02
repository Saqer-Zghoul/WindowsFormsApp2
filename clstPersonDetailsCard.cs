using DVLD_BusinessLayer;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class clstPersonDetailsCard : UserControl
    {
        int PersonID;
        clsPeople Person;
        public clstPersonDetailsCard(int ID)
        {
            InitializeComponent();

            if (ID != -1)
                PersonID = ID;
            else 
                PersonID = -1;

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void clstPersonDetailsCard_Load(object sender, EventArgs e)
        {
            Person = clsPeople.Find(PersonID);
            if (Person != null)
            {
                lblPersonID.Text = Person.PersonID.ToString();
                lblFullName.Text = Person.FirstName + " " + Person.SecondName + " " + Person.ThirdName + " " + Person.LastName;
                lblNationalNo.Text = Person.NationalNo.ToString();
                lblGendor.Text = Person.Gendor.ToString();
                lblEmail.Text = Person.Email.ToString();
                lblAddress.Text = Person.Address.ToString();
                lblDateOfBirth.Text = Person.DateOfBirth.ToString();
                lblCountry.Text = Person.Country.ToString();
                pbPersonImage.Image = Image.FromFile(@"C:\DVLD-People-Images\" + Person.ImageBath.ToString());


            }
            else
                MessageBox.Show("The Person Does Not Found ! ");
        }
    }
}

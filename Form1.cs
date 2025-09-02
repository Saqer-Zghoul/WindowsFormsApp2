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

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        enum enChooseBy { Non, ID , FirstName,SecondName,ThirdName,LastName,Email,Phone,Nationality,NationalNo,Gendor};
        enChooseBy Choice;
        private void Form1_Load(object sender, EventArgs e)
        {
              comboBox1.SelectedIndex = 0;
              textBox1.Visible = false;
            dataGridView1.DataSource = clsPeople.ListPeople();
            lblRecordNumber.Text = dataGridView1.RowCount.ToString();
        }
        private bool _numbersOnly = false;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            if (comboBox1.SelectedItem.ToString() == "Person ID")
            {
                textBox1.Visible = true;
                _numbersOnly = true;
                Choice = enChooseBy.ID;
               
            }
            else if (comboBox1.SelectedItem.ToString() =="Namtional No")
            {
                textBox1.Visible = true;
                _numbersOnly = false;
                Choice = enChooseBy.NationalNo;

            }
            else if (comboBox1.SelectedItem.ToString() == "First Name")
            {

                textBox1.Visible = true;
                _numbersOnly = false;
                Choice = enChooseBy.FirstName;
            }
            else if (comboBox1.SelectedItem.ToString() == "Second Name")
            {

                textBox1.Visible = true;
                _numbersOnly = false;
                Choice = enChooseBy.SecondName;
            }
            else if (comboBox1.SelectedItem.ToString() == "Third Name")
            {

                textBox1.Visible = true;
                _numbersOnly = false;
                Choice = enChooseBy.ThirdName;
            }
            else if (comboBox1.SelectedItem.ToString() == "Last Name")
            {

                textBox1.Visible = true;
                _numbersOnly = false;
                Choice = enChooseBy.LastName;
            }
            else if (comboBox1.SelectedItem.ToString() == "Nationality")
            {

                textBox1.Visible = true;
                _numbersOnly = false;
                Choice = enChooseBy.Nationality;
            }
            else if (comboBox1.SelectedItem.ToString() == "Gendor")
            {

                textBox1.Visible = true;
                _numbersOnly = false;
                Choice = enChooseBy.Gendor;

            }

            else if (comboBox1.SelectedItem.ToString() == "Email")
            {

                textBox1.Visible = true;
                _numbersOnly = false;
                Choice = enChooseBy.Email;

            }

            else if (comboBox1.SelectedItem.ToString() == "Phone")
            {

                textBox1.Visible = true;
                _numbersOnly = false;
                Choice = enChooseBy.Phone;

            }
            else if (comboBox1.SelectedItem.ToString() == "None")
            {
                textBox1.Visible = false;
                Choice = enChooseBy.Non;
              dataGridView1.DataSource=   clsPeople.ListPeople();

            }
        }



        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // يسمح بالأرقام و Backspace فقط
            if (_numbersOnly)
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true; // يمنع الكتابة
                }
            }
            else
                e.Handled = false;


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            switch (Choice)
            {
                case enChooseBy.ID:
                    if (int.TryParse(textBox1.Text, out int personID))
                    {
                        dataGridView1.DataSource = clsPeople.ListPeopleWithID(personID);
                    }
                    lblRecordNumber.Text = dataGridView1.RowCount.ToString();
                    break;
                case enChooseBy.Non:
                    dataGridView1.DataSource = clsPeople.ListPeople();
                    lblRecordNumber.Text = dataGridView1.RowCount.ToString();
                    break;
                case enChooseBy.Email:
                    dataGridView1.DataSource = clsPeople.ListPeopleWithEmail(textBox1.Text.ToString());
                    lblRecordNumber.Text = dataGridView1.RowCount.ToString();
                    break;
                case enChooseBy.Phone:
                    dataGridView1.DataSource = clsPeople.ListPeopleWithPhone(textBox1.Text.ToString());
                    lblRecordNumber.Text = dataGridView1.RowCount.ToString();
                    break;
                case enChooseBy.Gendor:
                    dataGridView1.DataSource = clsPeople.ListPeopleWithGendor(textBox1.Text.ToString());
                    lblRecordNumber.Text = dataGridView1.RowCount.ToString();
                    break;

                case enChooseBy.Nationality:
                    dataGridView1.DataSource = clsPeople.ListPeopleWithNatoinality(textBox1.Text.ToString());
                    lblRecordNumber.Text = dataGridView1.RowCount.ToString();
                    break;
                case enChooseBy.FirstName:
                    dataGridView1.DataSource = clsPeople.ListPeopleWithFirstName(textBox1.Text.ToString());
                    lblRecordNumber.Text = dataGridView1.RowCount.ToString();
                    break;

                case enChooseBy.LastName:
                    dataGridView1.DataSource = clsPeople.ListPeopleWithLastName(textBox1.Text.ToString());
                    lblRecordNumber.Text = dataGridView1.RowCount.ToString();
                    break;
                case enChooseBy.ThirdName:
                    dataGridView1.DataSource = clsPeople.ListPeoplewithThirdName(textBox1.Text.ToString());
                    lblRecordNumber.Text = dataGridView1.RowCount.ToString();
                    break;
                case enChooseBy.SecondName:
                    dataGridView1.DataSource = clsPeople.ListPeopleSecondName(textBox1.Text.ToString());
                    lblRecordNumber.Text = (dataGridView1.RowCount).ToString();
                    break;
                    
                case enChooseBy.NationalNo:
                    dataGridView1.DataSource = clsPeople.ListPeopleWithNationalNo(textBox1.Text.ToString());
                    lblRecordNumber.Text = dataGridView1.RowCount.ToString();
                    break;
            }
        }

        private void cmsAddNewPerson_Click(object sender, EventArgs e)
        {

        }

        private void cmsShowDetails_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

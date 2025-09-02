using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_BusinessLayer;
namespace DVLD
{
    public partial class frmManagePeople : Form
    {


        static private DataTable _tdAllPeople = clsPeople.ListPeople();
        private DataTable _dtPeople = _tdAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName",
            "SecondName", "ThirdName", "LastName", "Gendor", "DateOfBirth", "CountryName"
            , "Phone", "Email");
        public frmManagePeople()
        {
            InitializeComponent();
        }
        
        private void _RefreshPoepleList ()
        {
            _tdAllPeople = clsPeople.ListPeople();

            _dtPeople = _tdAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName",
                "SecondName", "ThirdName", "LastName", "Gendor", "DateOfBirth", "CountryName"
                , "Phone", "Email");

            dataGridView1.DataSource = _dtPeople;
            lblRecordCount.Text = dataGridView1.Rows.Count.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _dtPeople;
            cbFilterBy.SelectedIndex = 0;
            lblRecordCount.Text = dataGridView1.Rows.Count.ToString();
            if (dataGridView1.Rows.Count> 0)
            {

                dataGridView1.Columns[0].HeaderText = "Person ID";
                dataGridView1.Columns[0].Width = 110;

                dataGridView1.Columns[1].HeaderText = "National No.";
                dataGridView1.Columns[1].Width = 120;
                
                dataGridView1.Columns[2].HeaderText = "First Name";
                dataGridView1.Columns[2].Width = 120;
                
                dataGridView1.Columns[3].HeaderText = "Secon Name";
                dataGridView1.Columns[3].Width = 140;
                
                dataGridView1.Columns[4].HeaderText = "Third Name";
                dataGridView1.Columns[4].Width = 120;
                
                dataGridView1.Columns[5].HeaderText = "Last Name";
                dataGridView1.Columns[5].Width = 120;


                dataGridView1.Columns[6].HeaderText = "Gendor";
                dataGridView1.Columns[6].Width = 140;

                dataGridView1.Columns[7].HeaderText = "Date Of Birth";
                dataGridView1.Columns[7].Width = 140;

                dataGridView1.Columns[8].HeaderText = "Nationality";
                dataGridView1.Columns[8].Width = 120;

                dataGridView1.Columns[9].HeaderText = "Phone";
                dataGridView1.Columns[9].Width = 120;


                dataGridView1.Columns[10].HeaderText = "Email";
                dataGridView1.Columns[10].Width = 170;


            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonInfo frm = new frmPersonInfo((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPoepleList();
        }

        private void cmsAddNew_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.ShowDialog();
            _RefreshPoepleList();

        }

        private void cmsDelete_Click(object sender, EventArgs e)
        {
            if (clsPerson.DeletePerson((int)dataGridView1.CurrentRow.Cells[0].Value))
            {
             
                MessageBox.Show("Deleted Successfuly ! ", "Attention ", MessageBoxButtons.OK,MessageBoxIcon.Information);
                _RefreshPoepleList();
            }
        else
            {
                MessageBox.Show(" Faild Deleted  ", "Attention ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
           


        }

        private void comEditPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshPoepleList();

        }

        private void comSendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Does Not Implement ! ");
        }

        private void cmsCallPhone_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Does Not Implement ! ");

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch(cbFilterBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;


                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Gendor":
                    FilterColumn = "Gendor";
                    break;

                case "Date Of Birth":
                    FilterColumn = "DateOfBirth";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;
                case "Country":
                    FilterColumn = "CountryName";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }
            if (txtFilterValue.Text.Trim() =="" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecordCount.Text = dataGridView1.Rows.Count.ToString();
                return;

            }
            if (FilterColumn == "PersonID")
            {
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());

            }
else
            {
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            }
            lblRecordCount.Text =dataGridView1.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            if (txtFilterValue.Visible )
            {
                txtFilterValue.Text="";
                txtFilterValue.Focus();
            }
        }
    }
}

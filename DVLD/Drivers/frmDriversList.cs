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

namespace DVLD.Drivers
{
    public partial class frmDriversList : Form
    {
        public frmDriversList()
        {
            InitializeComponent();
        }
        DataTable _AllDrivers;
        private void frmDriversList_Load(object sender, EventArgs e)
        {
            _AllDrivers = clsDriver.GetListDrivers();
            dataGridView1.DataSource = _AllDrivers;
            if (dataGridView1.Rows.Count > 0 )
            {
                dataGridView1.Columns[0].HeaderText= "Driver ID";
                dataGridView1.Columns[0].Width = 100;

                dataGridView1.Columns[1].HeaderText= "Person ID";
                dataGridView1.Columns[1].Width = 100;


                dataGridView1.Columns[2].HeaderText= "National No";
                dataGridView1.Columns[2].Width = 100;
                dataGridView1.Columns[3].HeaderText= "Full Name";
                dataGridView1.Columns[3].Width = 250;
                dataGridView1.Columns[4].HeaderText= "Date";
                dataGridView1.Columns[4].Width = 150;
                dataGridView1.Columns[5].HeaderText= "Actice Licenses";
                dataGridView1.Columns[5].Width = 100;
            }
            lblCountRecord.Text = dataGridView1.Rows.Count.ToString();
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Visible = false;

        }
        bool IsNumber = false;
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text="";
                txtFilterValue.Focus();
            }

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterBy = "";
            switch (cbFilterBy.Text)
            {
                case "Driver ID":
                    FilterBy = "DriverID";
                    break;

                case "Person ID":
                    FilterBy = "PersonID";
                    break;
                case "Full Name":
                    FilterBy = "FullName";
                    break;
         
                case "Active Licenses":
                    FilterBy = "NumberOfActiveLicenses";
                    break;
                case "National No":
                    FilterBy = "NationalNo";
                    break;

                case "None":
                    FilterBy = "";
                    break;

            }
            if (txtFilterValue.Text.Trim() == "" || FilterBy == "None")
            {
                _AllDrivers.DefaultView.RowFilter = "";
                lblCountRecord.Text = dataGridView1.Rows.Count.ToString();
                return;

            }
            if (FilterBy == "PersonID" || FilterBy == "DriverID" || FilterBy == "NumberOfActiveLicenses")
           _AllDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterBy, txtFilterValue.Text.Trim());
            else 
                _AllDrivers.DefaultView.RowFilter = string.Format("{0} LIKE '{1}%'", FilterBy,txtFilterValue.Text.Trim());

            lblCountRecord.Text = dataGridView1.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "Driver ID" || cbFilterBy.Text == "Active Licenses")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // يمنع الإدخال
                }
            }
        }
    }
}

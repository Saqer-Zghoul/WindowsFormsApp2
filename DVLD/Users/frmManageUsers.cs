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

namespace DVLD.Users
{
    public partial class frmManageUsers : Form
    {
        private int childFormNumber = 0;

        public frmManageUsers()
        {
            InitializeComponent();
        }

        static DataTable _dtUsers = clsUser.ListUsers();
     //   DataTable dtUsers = _dtAllUsers.DefaultView.ToTable(false,"UserID","");
       
        void _RefreshUsersList ()
        {
            _dtUsers = clsUser.ListUsers();
            dataGridView1.DataSource = _dtUsers;

        }

        void ResetDefualtValues()
        {

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None" && cbFilterBy.Text != "Is Active");
            comboBox2.Visible = (cbFilterBy.Text == "Is Active");
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text="";
                txtFilterValue.Focus();
            }
            if ( cbFilterBy.Text == "User ID" || cbFilterBy.Text == "Person ID"  )
            {
                IsID = true;
            }
            else
            {
                IsID = false;

            }
           if (cbFilterBy.Text == "Is Active")
            {
                comboBox2.SelectedIndex = 0;
            }
            

        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _dtUsers;
            cbFilterBy.SelectedIndex = 0;
            if (dataGridView1.Rows.Count>0)
            {
                dataGridView1.Columns[0].HeaderText= "User ID";
                dataGridView1.Columns[0].Width = 120;

                dataGridView1.Columns[1].HeaderText= "Person ID";
                dataGridView1.Columns[1].Width = 120;

                dataGridView1.Columns[2].HeaderText= "Full Name";
                dataGridView1.Columns[2].Width = 250;

                dataGridView1.Columns[3].HeaderText= "User Name";
                dataGridView1.Columns[3].Width = 120;

                dataGridView1.Columns[4].HeaderText= "Is Active ";
                dataGridView1.Columns[4].Width = 120;
                dataGridView1.Columns[4].ValueType = typeof(bool);


            }

            lblCountRecord.Text = dataGridView1.Rows.Count.ToString();
            contextMenuStrip1.Enabled = true;
        
        }
        bool IsID = false;
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {

            string FilterBy = "";
           // bool IsID = false;
            switch (cbFilterBy.Text)
            {
                case "User ID":
                    FilterBy = "UserID";
                    break;
                case "Person ID":
                    FilterBy = "PersonID";

                    break;
                case "Full Name":
                    FilterBy = "fullName";
                    break;
                case "User Name":
                    FilterBy = "UserName";
                    break;
                case "Is Active":
                    FilterBy = "IsActive";
                    break;
                default:
                    FilterBy = "None";
                    break;

            }
            if (txtFilterValue.Text.Trim() =="" || FilterBy == "None")
            {
                _dtUsers.DefaultView.RowFilter = "";
                lblCountRecord.Text = dataGridView1.Rows.Count.ToString();
                return;

            }
            if (FilterBy == "PersonID" || (FilterBy == "UserID"))
            {
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterBy, txtFilterValue.Text.Trim());

            }
            else
            {
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterBy, txtFilterValue.Text.Trim());

            }
            lblCountRecord.Text =dataGridView1.Rows.Count.ToString();

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (IsID)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // يمنع الإدخال
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            short theCase= 0;
            switch(comboBox2.Text)
            {
                case "All":
                    _dtUsers.DefaultView.RowFilter = "";
                    break;
                case "Yes":
                    _dtUsers.DefaultView.RowFilter = string.Format("IsActive =  {0}", 1);


                    break;
                case "No":
                    _dtUsers.DefaultView.RowFilter = string.Format("IsActive =  {0}", 0);

                    break;
            
            
            }
        }

        private void pbAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();
            frm.Show();
            _RefreshUsersList();
        }

        private void showDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowDetails frm = new frmShowDetails((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.Show();

        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser newUser = new frmAddEditUser();  
            newUser.Show(); 
            _RefreshUsersList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser Edit = new frmAddEditUser((int)dataGridView1.CurrentRow.Cells[0].Value);
            Edit.Show();
            _RefreshUsersList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure to Delete This User ? ", "Attention", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation)==DialogResult.OK)
                if (clsUser.DeleteUser((int)dataGridView1.CurrentRow.Cells[0].Value))
                    MessageBox.Show("Deleted Succeffuly ", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Deleted Faild ", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Error);
            _RefreshUsersList();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshUsersList();
        }
    }
}

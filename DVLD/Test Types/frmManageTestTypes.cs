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

namespace DVLD.Test_Types
{
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        DataTable _AllTestType;
        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _AllTestType = clsTestType.ListAllTesTypes();
            dataGridView1.DataSource = _AllTestType;

            lblRecordCount.Text = dataGridView1.Rows.Count.ToString();
            if ( dataGridView1.Rows.Count > 0 )
            {

                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[0].Width = 100;


                dataGridView1.Columns[1].HeaderText = "Title ";
                dataGridView1.Columns[1].Width =100 ;

                dataGridView1.Columns[2].HeaderText = "Description";
                dataGridView1.Columns[2].Width = 400;

                dataGridView1.Columns[3].HeaderText = "Fees";
                dataGridView1.Columns[3].Width = 70;

            }


        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTestType frm = new frmEditTestType((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmManageTestTypes_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

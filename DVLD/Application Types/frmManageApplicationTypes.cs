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

namespace DVLD.Application_Types
{
    public partial class frmManageApplicationTypes : Form
    {
        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }
        DataTable _AllApllicationTypes = clsApplicationTypes.GetAllApplicationTypes();
 private void _RefershData ()

        {
            _AllApllicationTypes = clsApplicationTypes.GetAllApplicationTypes();
                  dataGridView1.DataSource = _AllApllicationTypes;
        }
        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _AllApllicationTypes;
            if (dataGridView1.RowCount > 0 )
            {
                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[0].Width = 100;


                dataGridView1.Columns[1].HeaderText = "Application Types Title ";
                dataGridView1.Columns[1].Width = 350;

                dataGridView1.Columns[2].HeaderText = "Applicatin Fees ";
                dataGridView1.Columns[2].Width = 100;
            }
            lblRecordCount.Text = dataGridView1.Rows.Count.ToString();
        }

        private void changeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditApplicationType frm = new frmEditApplicationType((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefershData();
        }
    }
}

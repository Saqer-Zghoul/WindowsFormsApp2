using DVLD.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmMain : Form
    {
        private int childFormNumber = 0;

        public frmMain()
        {
            InitializeComponent();
        }
        Form frm1 = new frmManagePeople();
        private void cmsManagePeople_Click(object sender, EventArgs e)
        {
           
            frm1.MdiParent = this;
            frm1.Show();

        }
        Form frm = new frmManageUsers();
        private void cmsUsers_Click(object sender, EventArgs e)
        {
           
            //frm.MdiParent = this ;
            frm.MdiParent = this;
            frm.Show();
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}

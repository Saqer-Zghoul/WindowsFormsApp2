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
    public partial class frmShowDetails : Form
    {
        int _UserID=-1;
        public frmShowDetails(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowDetails_Load(object sender, EventArgs e)
        {
            cltrUserCardDetails1.LoadInfo(_UserID);
        }
    }
}

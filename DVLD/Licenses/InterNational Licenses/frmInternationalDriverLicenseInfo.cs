using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.InterNational_Licenses
{
    public partial class frmInternationalDriverLicenseInfo : Form
    {
        int _InternationalLicenseID = -1;
        public frmInternationalDriverLicenseInfo(int internationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID=internationalLicenseID; 
        }

        private void frmInternationalDriverLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrInternationalDriverLicense1.LoadInternationalLicense(_InternationalLicenseID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}

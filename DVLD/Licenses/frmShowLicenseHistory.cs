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

namespace DVLD.Licenses
{
    public partial class frmShowLicenseHistory : Form
    {
        public frmShowLicenseHistory(int personID)
        {
            InitializeComponent();
       
            _PersonID=personID;
            _FilterEnable = false;

        }
        public frmShowLicenseHistory(string  NationalNo)
        {
            InitializeComponent();

            _NationalNo=NationalNo;
            _FilterEnable = false;

        }
        
        public frmShowLicenseHistory() { 
            
            InitializeComponent();
            _FilterEnable  = true;
        }
        string _NationalNo = "";
        bool _FilterEnable = false;
        int _DriverID = -1;
        int _PersonID = -1;
        private void cltrPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            clsDriver Driver = clsDriver.FindByPersonID(cltrPersonCardWithFilter1.PersonID);
            if (Driver == null )
            {
                MessageBox.Show("There is no Driver with Person id = "+cltrPersonCardWithFilter1.PersonID +" , \n Please Choose another Person  ", "not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            _DriverID = Driver.DriverID;
            ctrDriverLicensesHistory1.LoadInfo(_DriverID);
        
        }

        private void frmShowLicenseHistory_Load(object sender, EventArgs e)
        {
            if (!_FilterEnable)
            {
                cltrPersonCardWithFilter1.FilterEnable = false;
              
                if (_NationalNo != "")
                {
                    _PersonID = clsPerson.Find(_NationalNo).PersonID;
                }
         
                 cltrPersonCardWithFilter1.LoadPersonInfo(_PersonID);
     
               clsDriver Driver = clsDriver.FindByPersonID(_PersonID);
             if (Driver == null)
                {
                    MessageBox.Show("There is no Driver with Person id = "+cltrPersonCardWithFilter1.PersonID +" , \n Please Choose another Person  ", "not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;

                }
                _DriverID= Driver.DriverID;
                ctrDriverLicensesHistory1.LoadInfo(_DriverID);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

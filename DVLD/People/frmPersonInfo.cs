using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_BusinessLayer;
namespace DVLD
{
    public partial class frmPersonInfo : Form
    {
        int _PersonID =-1 ;
        public frmPersonInfo(int ID)
        {
            InitializeComponent();
            _PersonID = ID;
            //_LoadData();
        }
        string _NationalNo;
        public frmPersonInfo(string  NationalNo)
        {
            InitializeComponent();
            _NationalNo = NationalNo;
            //_LoadData();
        }
        private void frmPersonInfo_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
                cltrPersonCardDetails1.LoadPersonIfo(_PersonID);
            else
                cltrPersonCardDetails1.LoadPersonIfo(_NationalNo);

           
         

            
        }
    
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

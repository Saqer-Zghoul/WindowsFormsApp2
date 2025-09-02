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

namespace DVLD
{
    public partial class cltrUserCardDetails : UserControl
    {
        public cltrUserCardDetails()
        {
            InitializeComponent();
        }

        private void cltrUserInfo1_Load(object sender, EventArgs e)
        {

        }
        public void LoadInfo (int UserID)
        {
            clsUser User = clsUser.Find(UserID);
            if (User != null)
            {

                cltrPersonCardDetails1.LoadPersonIfo(User.PersonID);
                cltrUserInfo1.LoadUserInfo(User);
            }
            else
            {
                MessageBox.Show("The User Cannot be Found ! UserID = "+UserID, "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

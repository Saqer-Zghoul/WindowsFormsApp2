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
    public partial class cltrUserInfo : UserControl
    {
        public cltrUserInfo()
        {
            InitializeComponent();
        }
        public void LoadUserInfo(clsUser User )

        {
            if (User != null)
            {
                lblIsActive.Text = User.IsActive ? "Yes" : "No";
                lblUserID.Text = User.UserID.ToString();
                lblUserName.Text = User.UserName;
            }
            
            } 
    }
}

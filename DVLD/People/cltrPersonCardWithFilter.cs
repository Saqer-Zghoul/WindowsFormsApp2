﻿using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class cltrPersonCardWithFilter : UserControl
    {
        public event Action<int> OnPersonSelected;
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID); 
            }
        }
        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson= value;
                btnAddNewPerson.Visible = _ShowAddPerson;

            }

        }
        private bool _FilterEnable = true;   
        public  bool FilterEnable
        {
            get
            {
                return _FilterEnable;

            }
            set
            {
                _FilterEnable= value;   
                gbFiltering.Enabled = _FilterEnable;    
            }
        }
        private int _PersonID = -1;
        public int PersonID
        {
         
            get
            {
                return cltrPersonCardDetails1.PersonID;

            }
        }
        public clsPerson SelectedPersonInfo
        {
            get { return cltrPersonCardDetails1._Person; }
        }

        public cltrPersonCardWithFilter()
        {
            InitializeComponent();
        }
        private void FindNow()
        {
            switch(cbFilterBy.Text)
            {
                case "Person ID":
                    cltrPersonCardDetails1.LoadPersonIfo(int.Parse(txtFilterValue.Text.Trim()));
                    break;
                case "National No":
                    cltrPersonCardDetails1.LoadPersonIfo(txtFilterValue.Text.Trim());
                    break;
                default:
                    break;
            }
            if (OnPersonSelected != null && FilterEnable)
            {
                OnPersonSelected(cltrPersonCardDetails1.PersonID);
            }

        }
public void LoadPersonInfo (int PersonID)
        {
            cbFilterBy.SelectedIndex =1;
            txtFilterValue.Text = PersonID.ToString();
            FindNow();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text= "";
            txtFilterValue.Focus();
        }

        private void btnFindPerson_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Filed are not Valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            FindNow();
        }

        private void cltrPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex =0;
            txtFilterValue.Focus();
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterValue.Text.Trim()))
                {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "this field is required !");


            }
            else
            {
                errorProvider1.SetError(txtFilterValue, null);
            }
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();
        }
    private void DataBackEvent(object sender, int PersonID)
        {
            cbFilterBy.SelectedIndex= 1;
            txtFilterValue.Text = PersonID.ToString();  
            cltrPersonCardDetails1.LoadPersonIfo(PersonID);
        }
    public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ( e.KeyChar ==(char)13)
            {
                btnFindPerson.PerformClick();   

            }
            if (cbFilterBy.Text == "Person ID")
            {
                e.Handled=!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);   
            }
        }
    }

}

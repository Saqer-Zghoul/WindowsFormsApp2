using System;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class cltrCalculater : UserControl
    {
        public cltrCalculater()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            lblResult.Text = (Convert.ToInt32(int.Parse(textBox1.Text) + int.Parse(textBox2.Text))).ToString();

        }
    }
}

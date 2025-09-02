namespace DVLD
{
    partial class cltrUserCardDetails
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cltrPersonCardDetails1 = new DVLD.cltrPersonCardDetails();
            this.cltrUserInfo1 = new DVLD.cltrUserInfo();
            this.SuspendLayout();
            // 
            // cltrPersonCardDetails1
            // 
            this.cltrPersonCardDetails1.Location = new System.Drawing.Point(6, 7);
            this.cltrPersonCardDetails1.Name = "cltrPersonCardDetails1";
            this.cltrPersonCardDetails1.Size = new System.Drawing.Size(756, 309);
            this.cltrPersonCardDetails1.TabIndex = 0;
            // 
            // cltrUserInfo1
            // 
            this.cltrUserInfo1.Location = new System.Drawing.Point(17, 322);
            this.cltrUserInfo1.Name = "cltrUserInfo1";
            this.cltrUserInfo1.Size = new System.Drawing.Size(743, 74);
            this.cltrUserInfo1.TabIndex = 1;
            // 
            // cltrUserCardDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cltrUserInfo1);
            this.Controls.Add(this.cltrPersonCardDetails1);
            this.Name = "cltrUserCardDetails";
            this.Size = new System.Drawing.Size(763, 399);
            this.ResumeLayout(false);

        }

        #endregion

        private cltrPersonCardDetails cltrPersonCardDetails1;
        private cltrUserInfo cltrUserInfo1;
    }
}

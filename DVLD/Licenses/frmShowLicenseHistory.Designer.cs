namespace DVLD.Licenses
{
    partial class frmShowLicenseHistory
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cltrPersonCardWithFilter1 = new DVLD.People.cltrPersonCardWithFilter();
            this.ctrDriverLicensesHistory1 = new DVLD.Licenses.Controls.ctrDriverLicensesHistory();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.PersonLicenseHistory_512;
            this.pictureBox1.Location = new System.Drawing.Point(12, 90);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(267, 335);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(474, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 38);
            this.label1.TabIndex = 2;
            this.label1.Text = "License History";
            // 
            // cltrPersonCardWithFilter1
            // 
            this.cltrPersonCardWithFilter1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cltrPersonCardWithFilter1.FilterEnable = true;
            this.cltrPersonCardWithFilter1.Location = new System.Drawing.Point(264, 44);
            this.cltrPersonCardWithFilter1.Name = "cltrPersonCardWithFilter1";
            this.cltrPersonCardWithFilter1.ShowAddPerson = true;
            this.cltrPersonCardWithFilter1.Size = new System.Drawing.Size(895, 428);
            this.cltrPersonCardWithFilter1.TabIndex = 0;
            this.cltrPersonCardWithFilter1.OnPersonSelected += new System.Action<int>(this.cltrPersonCardWithFilter1_OnPersonSelected);
            // 
            // ctrDriverLicensesHistory1
            // 
            this.ctrDriverLicensesHistory1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ctrDriverLicensesHistory1.Location = new System.Drawing.Point(21, 467);
            this.ctrDriverLicensesHistory1.Name = "ctrDriverLicensesHistory1";
            this.ctrDriverLicensesHistory1.Size = new System.Drawing.Size(1117, 350);
            this.ctrDriverLicensesHistory1.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(971, 807);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(137, 54);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmShowLicenseHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1171, 865);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrDriverLicensesHistory1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cltrPersonCardWithFilter1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmShowLicenseHistory";
            this.Text = "Show License History";
            this.Load += new System.EventHandler(this.frmShowLicenseHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private People.cltrPersonCardWithFilter cltrPersonCardWithFilter1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private Controls.ctrDriverLicensesHistory ctrDriverLicensesHistory1;
        private System.Windows.Forms.Button btnClose;
    }
}
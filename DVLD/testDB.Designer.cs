namespace DVLD
{
    partial class testDB
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
            this.ctrlApplicationBasicInfo1 = new DVLD.UserControls.ctrlApplicationBasicInfo();
            this.SuspendLayout();
            // 
            // ctrlApplicationBasicInfo1
            // 
            this.ctrlApplicationBasicInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlApplicationBasicInfo1.Location = new System.Drawing.Point(55, 62);
            this.ctrlApplicationBasicInfo1.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlApplicationBasicInfo1.Name = "ctrlApplicationBasicInfo1";
            this.ctrlApplicationBasicInfo1.Size = new System.Drawing.Size(896, 217);
            this.ctrlApplicationBasicInfo1.TabIndex = 0;
            // 
            // testDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 450);
            this.Controls.Add(this.ctrlApplicationBasicInfo1);
            this.Name = "testDB";
            this.Text = "testDB";
            this.Load += new System.EventHandler(this.testDB_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.ctrlApplicationBasicInfo ctrlApplicationBasicInfo1;
    }
}
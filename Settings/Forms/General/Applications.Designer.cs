namespace Settings.Forms
{
    partial class Applications
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbCodeBeautifulApplication = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tbNewApplication = new System.Windows.Forms.TextBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.cbListOfApplications = new System.Windows.Forms.ComboBox();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.bClone = new System.Windows.Forms.Button();
            this.gbCodeBeautifulApplication.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbCodeBeautifulApplication
            // 
            this.gbCodeBeautifulApplication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCodeBeautifulApplication.Controls.Add(this.bClone);
            this.gbCodeBeautifulApplication.Controls.Add(this.btnAdd);
            this.gbCodeBeautifulApplication.Controls.Add(this.tbNewApplication);
            this.gbCodeBeautifulApplication.Controls.Add(this.btnRemove);
            this.gbCodeBeautifulApplication.Controls.Add(this.cbListOfApplications);
            this.gbCodeBeautifulApplication.Location = new System.Drawing.Point(4, 4);
            this.gbCodeBeautifulApplication.Name = "gbCodeBeautifulApplication";
            this.gbCodeBeautifulApplication.Size = new System.Drawing.Size(332, 79);
            this.gbCodeBeautifulApplication.TabIndex = 0;
            this.gbCodeBeautifulApplication.TabStop = false;
            this.gbCodeBeautifulApplication.Text = "Code Beautiful Applications";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(170, 45);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tbNewApplication
            // 
            this.tbNewApplication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNewApplication.Location = new System.Drawing.Point(6, 47);
            this.tbNewApplication.Name = "tbNewApplication";
            this.tbNewApplication.Size = new System.Drawing.Size(158, 20);
            this.tbNewApplication.TabIndex = 2;
            this.tbNewApplication.TextChanged += new System.EventHandler(this.tbNewApplication_TextChanged);
            this.tbNewApplication.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbNewApplication_KeyDown);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Enabled = false;
            this.btnRemove.Location = new System.Drawing.Point(170, 17);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // cbListOfApplications
            // 
            this.cbListOfApplications.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbListOfApplications.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbListOfApplications.FormattingEnabled = true;
            this.cbListOfApplications.Location = new System.Drawing.Point(6, 19);
            this.cbListOfApplications.Name = "cbListOfApplications";
            this.cbListOfApplications.Size = new System.Drawing.Size(158, 21);
            this.cbListOfApplications.Sorted = true;
            this.cbListOfApplications.TabIndex = 0;
            this.cbListOfApplications.DropDown += new System.EventHandler(this.cbListOfApplications_DropDown);
            this.cbListOfApplications.DropDownClosed += new System.EventHandler(this.cbListOfApplications_DropDownClosed);
            this.cbListOfApplications.SelectedValueChanged += new System.EventHandler(this.cbListOfApplications_SelectedValueChanged);
            // 
            // panelInfo
            // 
            this.panelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelInfo.Location = new System.Drawing.Point(4, 90);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(326, 26);
            this.panelInfo.TabIndex = 1;
            // 
            // bClone
            // 
            this.bClone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bClone.Location = new System.Drawing.Point(251, 17);
            this.bClone.Name = "bClone";
            this.bClone.Size = new System.Drawing.Size(75, 50);
            this.bClone.TabIndex = 4;
            this.bClone.Text = "Clone";
            this.bClone.UseVisualStyleBackColor = true;
            this.bClone.Click += new System.EventHandler(this.bClone_Click);
            // 
            // Applications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.gbCodeBeautifulApplication);
            this.Name = "Applications";
            this.Size = new System.Drawing.Size(339, 119);
            this.gbCodeBeautifulApplication.ResumeLayout(false);
            this.gbCodeBeautifulApplication.PerformLayout();
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.GroupBox gbCodeBeautifulApplication;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox tbNewApplication;
        private System.Windows.Forms.ComboBox cbListOfApplications;
        private System.Windows.Forms.Button btnRemove;
        #endregion
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Button bClone;
    }
}

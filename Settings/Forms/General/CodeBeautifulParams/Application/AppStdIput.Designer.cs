namespace Settings.Forms
{
    partial class AppStdIput
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbArguments = new System.Windows.Forms.TextBox();
            this.lApplication = new System.Windows.Forms.Label();
            this.tbApplication = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnMenu = new System.Windows.Forms.Button();
            this.cbUseEncoding = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Arguments:";
            // 
            // tbArguments
            // 
            this.tbArguments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbArguments.Location = new System.Drawing.Point(6, 61);
            this.tbArguments.Name = "tbArguments";
            this.tbArguments.Size = new System.Drawing.Size(213, 20);
            this.tbArguments.TabIndex = 4;
            this.tbArguments.TextChanged += new System.EventHandler(this.tbArguments_TextChanged);
            // 
            // lApplication
            // 
            this.lApplication.AutoSize = true;
            this.lApplication.Location = new System.Drawing.Point(3, 6);
            this.lApplication.Name = "lApplication";
            this.lApplication.Size = new System.Drawing.Size(62, 13);
            this.lApplication.TabIndex = 2;
            this.lApplication.Text = "Application:";
            // 
            // tbApplication
            // 
            this.tbApplication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbApplication.Location = new System.Drawing.Point(6, 22);
            this.tbApplication.Name = "tbApplication";
            this.tbApplication.Size = new System.Drawing.Size(213, 20);
            this.tbApplication.TabIndex = 0;
            this.tbApplication.TextChanged += new System.EventHandler(this.tbApplication_TextChanged);
            this.tbApplication.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbApplication_KeyPress);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(225, 20);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(30, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = ",,,";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnMenu
            // 
            this.btnMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMenu.AutoSize = true;
            this.btnMenu.Location = new System.Drawing.Point(225, 59);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(30, 23);
            this.btnMenu.TabIndex = 6;
            this.btnMenu.Text = ">";
            this.btnMenu.UseVisualStyleBackColor = true;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // cbUseEncoding
            // 
            this.cbUseEncoding.AutoSize = true;
            this.cbUseEncoding.Location = new System.Drawing.Point(6, 91);
            this.cbUseEncoding.Name = "cbUseEncoding";
            this.cbUseEncoding.Size = new System.Drawing.Size(143, 17);
            this.cbUseEncoding.TabIndex = 7;
            this.cbUseEncoding.Text = "Use source file encoding";
            this.cbUseEncoding.UseVisualStyleBackColor = true;
            this.cbUseEncoding.CheckedChanged += new System.EventHandler(this.cbEncoding_CheckedChanged);
            // 
            // AppStdIput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.cbUseEncoding);
            this.Controls.Add(this.btnMenu);
            this.Controls.Add(this.tbArguments);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lApplication);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbApplication);
            this.Name = "AppStdIput";
            this.Size = new System.Drawing.Size(263, 125);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbArguments;
        private System.Windows.Forms.Label lApplication;
        private System.Windows.Forms.TextBox tbApplication;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.CheckBox cbUseEncoding;
    }
}

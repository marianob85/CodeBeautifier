namespace Settings.Forms
{
    partial class Application
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
            this.gbExecutable = new System.Windows.Forms.GroupBox();
            this.gpDescription = new System.Windows.Forms.GroupBox();
            this.panelApplication = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbAppType = new System.Windows.Forms.ComboBox();
            this.gbExecutable.SuspendLayout();
            this.gpDescription.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbExecutable
            // 
            this.gbExecutable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbExecutable.Controls.Add(this.gpDescription);
            this.gbExecutable.Controls.Add(this.label1);
            this.gbExecutable.Controls.Add(this.cbAppType);
            this.gbExecutable.Location = new System.Drawing.Point(4, 4);
            this.gbExecutable.Name = "gbExecutable";
            this.gbExecutable.Size = new System.Drawing.Size(372, 282);
            this.gbExecutable.TabIndex = 0;
            this.gbExecutable.TabStop = false;
            this.gbExecutable.Text = "Executable";
            // 
            // gpDescription
            // 
            this.gpDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpDescription.Controls.Add(this.panelApplication);
            this.gpDescription.Location = new System.Drawing.Point(7, 46);
            this.gpDescription.Name = "gpDescription";
            this.gpDescription.Size = new System.Drawing.Size(359, 230);
            this.gpDescription.TabIndex = 9;
            this.gpDescription.TabStop = false;
            // 
            // panelApplication
            // 
            this.panelApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelApplication.Location = new System.Drawing.Point(6, 19);
            this.panelApplication.Name = "panelApplication";
            this.panelApplication.Size = new System.Drawing.Size(347, 205);
            this.panelApplication.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Type:";
            // 
            // cbAppType
            // 
            this.cbAppType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAppType.FormattingEnabled = true;
            this.cbAppType.Location = new System.Drawing.Point(41, 19);
            this.cbAppType.Name = "cbAppType";
            this.cbAppType.Size = new System.Drawing.Size(135, 21);
            this.cbAppType.TabIndex = 6;
            this.cbAppType.SelectionChangeCommitted += new System.EventHandler(this.cbAppType_SelectionChangeCommitted);
            // 
            // Application
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.gbExecutable);
            this.Name = "Application";
            this.Size = new System.Drawing.Size(379, 289);
            this.gbExecutable.ResumeLayout(false);
            this.gbExecutable.PerformLayout();
            this.gpDescription.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.GroupBox gbExecutable;

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbAppType;
        private System.Windows.Forms.Panel panelApplication;
        private System.Windows.Forms.GroupBox gpDescription;
    }
}

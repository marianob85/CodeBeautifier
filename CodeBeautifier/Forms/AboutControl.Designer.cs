namespace Manobit.CodeBeautifier.Forms
{
    partial class AboutControl
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkLabelCopyright = new System.Windows.Forms.LinkLabel();
            this.linkLabelMail = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.lvModules = new System.Windows.Forms.ListView();
            this.Module = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Version = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox1.Location = new System.Drawing.Point(6, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(89, 108);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(109, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Copyright:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.linkLabelCopyright);
            this.groupBox1.Controls.Add(this.linkLabelMail);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lvModules);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(361, 134);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "About";
            // 
            // linkLabelCopyright
            // 
            this.linkLabelCopyright.AutoSize = true;
            this.linkLabelCopyright.Location = new System.Drawing.Point(169, 16);
            this.linkLabelCopyright.Name = "linkLabelCopyright";
            this.linkLabelCopyright.Size = new System.Drawing.Size(80, 13);
            this.linkLabelCopyright.TabIndex = 16;
            this.linkLabelCopyright.TabStop = true;
            this.linkLabelCopyright.Text = "Mariusz Brzeski";
            this.linkLabelCopyright.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelCopyright_LinkClicked);
            // 
            // linkLabelMail
            // 
            this.linkLabelMail.AutoSize = true;
            this.linkLabelMail.Location = new System.Drawing.Point(169, 32);
            this.linkLabelMail.Name = "linkLabelMail";
            this.linkLabelMail.Size = new System.Drawing.Size(149, 13);
            this.linkLabelMail.TabIndex = 15;
            this.linkLabelMail.TabStop = true;
            this.linkLabelMail.Text = "mariusz.brzeski@manobit.com";
            this.linkLabelMail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelMail_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(126, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "e-mail:";
            // 
            // lvModules
            // 
            this.lvModules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvModules.BackColor = System.Drawing.SystemColors.Control;
            this.lvModules.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvModules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Module,
            this.Version});
            this.lvModules.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvModules.Location = new System.Drawing.Point(104, 48);
            this.lvModules.MultiSelect = false;
            this.lvModules.Name = "lvModules";
            this.lvModules.Size = new System.Drawing.Size(251, 83);
            this.lvModules.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvModules.TabIndex = 13;
            this.lvModules.UseCompatibleStateImageBehavior = false;
            this.lvModules.View = System.Windows.Forms.View.Details;
            // 
            // Module
            // 
            this.Module.Width = 120;
            // 
            // AboutControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.groupBox1);
            this.Name = "AboutControl";
            this.Size = new System.Drawing.Size(368, 138);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lvModules;
        private System.Windows.Forms.ColumnHeader Module;
        private System.Windows.Forms.ColumnHeader Version;
        private System.Windows.Forms.LinkLabel linkLabelCopyright;
        private System.Windows.Forms.LinkLabel linkLabelMail;
        private System.Windows.Forms.Label label2;
    }
}

namespace Manobit.CodeBeautifier
{
    partial class Progress
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.m_ssStatus = new System.Windows.Forms.StatusStrip();
            this.m_tsslVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_lProject = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lProject = new System.Windows.Forms.Label();
            this.lFile = new System.Windows.Forms.Label();
            this.m_bCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_lFailed = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_lSuccess = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_ssStatus.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(0, 102);
            this.progressBar1.MarqueeAnimationSpeed = 20;
            this.progressBar1.Maximum = 30;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.progressBar1.Size = new System.Drawing.Size(323, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 0;
            // 
            // m_ssStatus
            // 
            this.m_ssStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_tsslVersion,
            this.tsslStatus});
            this.m_ssStatus.Location = new System.Drawing.Point(0, 152);
            this.m_ssStatus.Name = "m_ssStatus";
            this.m_ssStatus.Size = new System.Drawing.Size(323, 22);
            this.m_ssStatus.SizingGrip = false;
            this.m_ssStatus.TabIndex = 1;
            this.m_ssStatus.Text = "statusStrip1";
            // 
            // m_tsslVersion
            // 
            this.m_tsslVersion.AutoSize = false;
            this.m_tsslVersion.Name = "m_tsslVersion";
            this.m_tsslVersion.Size = new System.Drawing.Size(100, 17);
            this.m_tsslVersion.Text = "Version: ";
            this.m_tsslVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslStatus
            // 
            this.tsslStatus.AutoSize = false;
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.tsslStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tsslStatus.Size = new System.Drawing.Size(150, 17);
            this.tsslStatus.Text = "Status";
            this.tsslStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lProject
            // 
            this.m_lProject.AutoSize = true;
            this.m_lProject.Location = new System.Drawing.Point(29, 21);
            this.m_lProject.Name = "m_lProject";
            this.m_lProject.Size = new System.Drawing.Size(46, 13);
            this.m_lProject.TabIndex = 2;
            this.m_lProject.Text = "Project: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "File: ";
            // 
            // lProject
            // 
            this.lProject.AutoSize = true;
            this.lProject.Location = new System.Drawing.Point(81, 21);
            this.lProject.Name = "lProject";
            this.lProject.Size = new System.Drawing.Size(16, 13);
            this.lProject.TabIndex = 4;
            this.lProject.Text = "...";
            this.lProject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lFile
            // 
            this.lFile.AutoSize = true;
            this.lFile.Location = new System.Drawing.Point(81, 40);
            this.lFile.Name = "lFile";
            this.lFile.Size = new System.Drawing.Size(16, 13);
            this.lFile.TabIndex = 5;
            this.lFile.Text = "...";
            this.lFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_bCancel
            // 
            this.m_bCancel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_bCancel.Location = new System.Drawing.Point(0, 129);
            this.m_bCancel.Name = "m_bCancel";
            this.m_bCancel.Size = new System.Drawing.Size(323, 23);
            this.m_bCancel.TabIndex = 6;
            this.m_bCancel.Text = "Cancel";
            this.m_bCancel.UseVisualStyleBackColor = true;
            this.m_bCancel.Click += new System.EventHandler(this.m_bCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.m_lFailed);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.m_lSuccess);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.m_lProject);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lFile);
            this.groupBox1.Controls.Add(this.lProject);
            this.groupBox1.Location = new System.Drawing.Point(12, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 96);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CodeBeautifier status";
            // 
            // m_lFailed
            // 
            this.m_lFailed.AutoSize = true;
            this.m_lFailed.Location = new System.Drawing.Point(81, 76);
            this.m_lFailed.Name = "m_lFailed";
            this.m_lFailed.Size = new System.Drawing.Size(16, 13);
            this.m_lFailed.TabIndex = 9;
            this.m_lFailed.Text = "...";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Failed:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lSuccess
            // 
            this.m_lSuccess.AutoSize = true;
            this.m_lSuccess.Location = new System.Drawing.Point(81, 58);
            this.m_lSuccess.Name = "m_lSuccess";
            this.m_lSuccess.Size = new System.Drawing.Size(16, 13);
            this.m_lSuccess.TabIndex = 7;
            this.m_lSuccess.Text = "...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Successed:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Progress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 174);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_bCancel);
            this.Controls.Add(this.m_ssStatus);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Progress";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CodeBeautifier progress";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.LightPink;
            this.m_ssStatus.ResumeLayout(false);
            this.m_ssStatus.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        System.Windows.Forms.ProgressBar progressBar1;
        System.Windows.Forms.StatusStrip m_ssStatus;
        System.Windows.Forms.ToolStripStatusLabel m_tsslVersion;
        System.Windows.Forms.Label m_lProject;
        System.Windows.Forms.Label label1;
        System.Windows.Forms.Label lProject;
        System.Windows.Forms.Label lFile;
        System.Windows.Forms.Button m_bCancel;
        System.Windows.Forms.GroupBox groupBox1;
        System.Windows.Forms.Label m_lFailed;
        System.Windows.Forms.Label label6;
        System.Windows.Forms.Label m_lSuccess;
        System.Windows.Forms.Label label4;
        System.Windows.Forms.ToolStripStatusLabel tsslStatus;

    }
}
namespace Manobit.CodeBeautifier
{
    partial class About
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
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_hCLose = new System.Windows.Forms.Button();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // m_hCLose
            // 
            this.m_hCLose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_hCLose.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_hCLose.Location = new System.Drawing.Point(0, 142);
            this.m_hCLose.Name = "m_hCLose";
            this.m_hCLose.Size = new System.Drawing.Size(371, 27);
            this.m_hCLose.TabIndex = 0;
            this.m_hCLose.Text = "Close";
            this.m_hCLose.UseVisualStyleBackColor = true;
            // 
            // panelInfo
            // 
            this.panelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelInfo.Location = new System.Drawing.Point(0, 1);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(370, 142);
            this.panelInfo.TabIndex = 1;
            // 
            // About
            // 
            this.AcceptButton = this.m_hCLose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.m_hCLose;
            this.ClientSize = new System.Drawing.Size(371, 169);
            this.ControlBox = false;
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.m_hCLose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_hCLose;
        private System.Windows.Forms.Panel panelInfo;

    }
}
namespace Settings.Forms
{
    partial class Language
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
            this.gbLanguageIdent = new System.Windows.Forms.GroupBox();
            this.rbVSLanguage = new System.Windows.Forms.RadioButton();
            this.rbExtension = new System.Windows.Forms.RadioButton();
            this.pLangExt = new System.Windows.Forms.Panel();
            this.gbLanguageIdent.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbLanguageIdent
            // 
            this.gbLanguageIdent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLanguageIdent.Controls.Add(this.rbVSLanguage);
            this.gbLanguageIdent.Controls.Add(this.rbExtension);
            this.gbLanguageIdent.Location = new System.Drawing.Point(4, 4);
            this.gbLanguageIdent.Name = "gbLanguageIdent";
            this.gbLanguageIdent.Size = new System.Drawing.Size(298, 51);
            this.gbLanguageIdent.TabIndex = 0;
            this.gbLanguageIdent.TabStop = false;
            this.gbLanguageIdent.Text = "Language identification";
            // 
            // rbVSLanguage
            // 
            this.rbVSLanguage.AutoSize = true;
            this.rbVSLanguage.Enabled = false;
            this.rbVSLanguage.Location = new System.Drawing.Point(189, 23);
            this.rbVSLanguage.Name = "rbVSLanguage";
            this.rbVSLanguage.Size = new System.Drawing.Size(101, 17);
            this.rbVSLanguage.TabIndex = 1;
            this.rbVSLanguage.Text = "By Visual Studio";
            this.rbVSLanguage.UseVisualStyleBackColor = true;
            // 
            // rbExtension
            // 
            this.rbExtension.AutoSize = true;
            this.rbExtension.Checked = true;
            this.rbExtension.Location = new System.Drawing.Point(43, 23);
            this.rbExtension.Name = "rbExtension";
            this.rbExtension.Size = new System.Drawing.Size(85, 17);
            this.rbExtension.TabIndex = 0;
            this.rbExtension.TabStop = true;
            this.rbExtension.Text = "By extension";
            this.rbExtension.UseVisualStyleBackColor = true;
            // 
            // pLangExt
            // 
            this.pLangExt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pLangExt.Location = new System.Drawing.Point(4, 61);
            this.pLangExt.Name = "pLangExt";
            this.pLangExt.Size = new System.Drawing.Size(298, 90);
            this.pLangExt.TabIndex = 1;
            // 
            // Language
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.pLangExt);
            this.Controls.Add(this.gbLanguageIdent);
            this.Name = "Language";
            this.Size = new System.Drawing.Size(305, 154);
            this.gbLanguageIdent.ResumeLayout(false);
            this.gbLanguageIdent.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.GroupBox gbLanguageIdent;
        private System.Windows.Forms.RadioButton rbVSLanguage;
        private System.Windows.Forms.RadioButton rbExtension;
        private System.Windows.Forms.Panel pLangExt;

        #endregion
    }
}

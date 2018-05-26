namespace Settings.Forms
{
    partial class General
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
            this.pgGeneral = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // pgGeneral
            // 
            this.pgGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgGeneral.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.pgGeneral.Location = new System.Drawing.Point(4, 4);
            this.pgGeneral.Name = "pgGeneral";
            this.pgGeneral.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.pgGeneral.Size = new System.Drawing.Size(289, 258);
            this.pgGeneral.TabIndex = 0;
            // 
            // General
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.pgGeneral);
            this.Name = "General";
            this.Size = new System.Drawing.Size(296, 265);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pgGeneral;

    }
}

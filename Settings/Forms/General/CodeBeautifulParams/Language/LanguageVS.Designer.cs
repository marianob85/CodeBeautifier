namespace Settings.Forms
{
    partial class LanguageVS
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
            this.lwOrigin = new System.Windows.Forms.ListView();
            this.lwUsed = new System.Windows.Forms.ListView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnremove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lwOrigin
            // 
            this.lwOrigin.Location = new System.Drawing.Point(3, 30);
            this.lwOrigin.Name = "lwOrigin";
            this.lwOrigin.Size = new System.Drawing.Size(143, 189);
            this.lwOrigin.TabIndex = 0;
            this.lwOrigin.UseCompatibleStateImageBehavior = false;
            this.lwOrigin.View = System.Windows.Forms.View.Details;
            // 
            // lwUsed
            // 
            this.lwUsed.Location = new System.Drawing.Point(233, 30);
            this.lwUsed.Name = "lwUsed";
            this.lwUsed.Size = new System.Drawing.Size(143, 189);
            this.lwUsed.TabIndex = 1;
            this.lwUsed.UseCompatibleStateImageBehavior = false;
            this.lwUsed.View = System.Windows.Forms.View.Details;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(152, 30);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnremove
            // 
            this.btnremove.Location = new System.Drawing.Point(152, 59);
            this.btnremove.Name = "btnremove";
            this.btnremove.Size = new System.Drawing.Size(75, 23);
            this.btnremove.TabIndex = 3;
            this.btnremove.Text = "Remove";
            this.btnremove.UseVisualStyleBackColor = true;
            // 
            // LanguageVS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.btnremove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lwUsed);
            this.Controls.Add(this.lwOrigin);
            this.Name = "LanguageVS";
            this.Size = new System.Drawing.Size(379, 222);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.ListView lwOrigin;
        private System.Windows.Forms.ListView lwUsed;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnremove;

        #endregion
    }
}

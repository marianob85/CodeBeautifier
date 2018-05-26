namespace Settings.Forms
{
    partial class LanguageExt
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
            this.tbNew = new System.Windows.Forms.TextBox();
            this.lNew = new System.Windows.Forms.Label();
            this.lv_List = new System.Windows.Forms.ListView();
            this.bAdd = new System.Windows.Forms.Button();
            this.bRemove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbNew
            // 
            this.tbNew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNew.Location = new System.Drawing.Point(6, 23);
            this.tbNew.Name = "tbNew";
            this.tbNew.Size = new System.Drawing.Size(71, 20);
            this.tbNew.TabIndex = 0;
            this.tbNew.TextChanged += new System.EventHandler(this.tbNew_TextChanged);
            this.tbNew.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbNew_KeyDown);
            // 
            // lNew
            // 
            this.lNew.AutoSize = true;
            this.lNew.Location = new System.Drawing.Point(3, 6);
            this.lNew.Name = "lNew";
            this.lNew.Size = new System.Drawing.Size(80, 13);
            this.lNew.TabIndex = 1;
            this.lNew.Text = "New extension:";
            // 
            // lv_List
            // 
            this.lv_List.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_List.Location = new System.Drawing.Point(6, 49);
            this.lv_List.Name = "lv_List";
            this.lv_List.Size = new System.Drawing.Size(233, 131);
            this.lv_List.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lv_List.TabIndex = 2;
            this.lv_List.UseCompatibleStateImageBehavior = false;
            this.lv_List.View = System.Windows.Forms.View.List;
            this.lv_List.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lv_List_ItemSelectionChanged);
            // 
            // bAdd
            // 
            this.bAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bAdd.Location = new System.Drawing.Point(83, 22);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(75, 22);
            this.bAdd.TabIndex = 3;
            this.bAdd.Text = "Add";
            this.bAdd.UseVisualStyleBackColor = true;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // bRemove
            // 
            this.bRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bRemove.Location = new System.Drawing.Point(164, 22);
            this.bRemove.Name = "bRemove";
            this.bRemove.Size = new System.Drawing.Size(75, 22);
            this.bRemove.TabIndex = 4;
            this.bRemove.Text = "Remove";
            this.bRemove.UseVisualStyleBackColor = true;
            this.bRemove.Click += new System.EventHandler(this.bRemove_Click);
            // 
            // LanguageExt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.bRemove);
            this.Controls.Add(this.bAdd);
            this.Controls.Add(this.lv_List);
            this.Controls.Add(this.lNew);
            this.Controls.Add(this.tbNew);
            this.Name = "LanguageExt";
            this.Size = new System.Drawing.Size(242, 183);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TextBox tbNew;
        private System.Windows.Forms.Label lNew;
        private System.Windows.Forms.ListView lv_List;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.Button bRemove;

        #endregion
    }
}

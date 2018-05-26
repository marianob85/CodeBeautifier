namespace Settings.Forms
{
    partial class ExcludeFiles
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
            this.lwFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAdd = new System.Windows.Forms.Button();
            this.tbAdd = new System.Windows.Forms.TextBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnFilesForm = new System.Windows.Forms.Button();
            this.cbUseIgnoreFiles = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lwFiles
            // 
            this.lwFiles.AllowColumnReorder = true;
            this.lwFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lwFiles.CheckBoxes = true;
            this.lwFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lwFiles.FullRowSelect = true;
            this.lwFiles.GridLines = true;
            this.lwFiles.Location = new System.Drawing.Point(6, 46);
            this.lwFiles.Name = "lwFiles";
            this.lwFiles.ShowItemToolTips = true;
            this.lwFiles.Size = new System.Drawing.Size(360, 176);
            this.lwFiles.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lwFiles.TabIndex = 0;
            this.lwFiles.UseCompatibleStateImageBehavior = false;
            this.lwFiles.View = System.Windows.Forms.View.Details;
            this.lwFiles.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lwFiles_ItemChecked);
            this.lwFiles.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lwFiles_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File path";
            this.columnHeader1.Width = 332;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.AutoSize = true;
            this.btnAdd.Location = new System.Drawing.Point(225, 17);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(57, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tbAdd
            // 
            this.tbAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAdd.Location = new System.Drawing.Point(6, 19);
            this.tbAdd.Name = "tbAdd";
            this.tbAdd.Size = new System.Drawing.Size(213, 20);
            this.tbAdd.TabIndex = 2;
            this.tbAdd.TextChanged += new System.EventHandler(this.tbAdd_TextChanged);
            this.tbAdd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbAdd_KeyDown);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.AutoSize = true;
            this.btnRemove.Location = new System.Drawing.Point(288, 17);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(57, 23);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnFilesForm
            // 
            this.btnFilesForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilesForm.Location = new System.Drawing.Point(351, 17);
            this.btnFilesForm.Name = "btnFilesForm";
            this.btnFilesForm.Size = new System.Drawing.Size(15, 23);
            this.btnFilesForm.TabIndex = 5;
            this.btnFilesForm.Text = ".";
            this.btnFilesForm.UseVisualStyleBackColor = true;
            this.btnFilesForm.Click += new System.EventHandler(this.btnFilesForm_Click);
            // 
            // cbUseIgnoreFiles
            // 
            this.cbUseIgnoreFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbUseIgnoreFiles.AutoSize = true;
            this.cbUseIgnoreFiles.Location = new System.Drawing.Point(6, 19);
            this.cbUseIgnoreFiles.Name = "cbUseIgnoreFiles";
            this.cbUseIgnoreFiles.Size = new System.Drawing.Size(117, 17);
            this.cbUseIgnoreFiles.TabIndex = 6;
            this.cbUseIgnoreFiles.Text = "Use \'.cbignore\' files";
            this.cbUseIgnoreFiles.UseVisualStyleBackColor = true;
            this.cbUseIgnoreFiles.CheckedChanged += new System.EventHandler(this.cbUseIgnoreFiles_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tbAdd);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.lwFiles);
            this.groupBox1.Controls.Add(this.btnFilesForm);
            this.groupBox1.Controls.Add(this.btnRemove);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(372, 228);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Global ignore";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cbUseIgnoreFiles);
            this.groupBox2.Location = new System.Drawing.Point(4, 238);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(372, 48);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Additional ignore";
            // 
            // ExcludeFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ExcludeFiles";
            this.Size = new System.Drawing.Size(379, 289);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.ListView lwFiles;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox tbAdd;
        private System.Windows.Forms.Button btnRemove;

        #endregion
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnFilesForm;
        private System.Windows.Forms.CheckBox cbUseIgnoreFiles;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

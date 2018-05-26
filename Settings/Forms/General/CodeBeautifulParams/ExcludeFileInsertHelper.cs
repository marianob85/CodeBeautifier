using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Settings.Sources;

namespace Settings.Forms
{
    public partial class ExcludeFileInserrtHelper : Form
    {
        private String[] m_excludeFiles = null;

        public ExcludeFileInserrtHelper( String[] excludeFiles )
        {
            m_excludeFiles = excludeFiles;
            InitializeComponent();
            this.tbFileList.Clear();
            foreach( var excludeFile in m_excludeFiles)
            {
                this.tbFileList.AppendText( excludeFile.ToString() + Environment.NewLine );
            }
        }

        public String[] excludeFiles
        {
            get { return m_excludeFiles; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click( object sender, EventArgs e )
        {
            m_excludeFiles = tbFileList.Text.Split( System.Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries );
            Close();
        }
    }
}

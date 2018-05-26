using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Settings.Sources;
using Settings.Sources.Validator;

namespace Settings.Forms
{
    public partial class Main : UserControl, IValidator
    {
        public event EventHandler Action = delegate { };
        private AppOptions m_options = null;
        System.Windows.Forms.TreeNode m_treeView = null;
        public Main( AppOptions options, System.Windows.Forms.TreeNode treeView )
        {
            m_options = options;
            m_treeView = treeView;
            InitializeComponent();

            this.Anchor = (System.Windows.Forms.AnchorStyles)( System.Windows.Forms.AnchorStyles.Bottom
                                                                    | System.Windows.Forms.AnchorStyles.Top
                                                                    | System.Windows.Forms.AnchorStyles.Left
                                                                    | System.Windows.Forms.AnchorStyles.Right );

            this.cbEnable.Checked  = options.enable;
            this.tbName.Text = m_options.name;
            onVisibleChanged( this, null );
            this.VisibleChanged += new EventHandler( onVisibleChanged );
        }

        private void onVisibleChanged( Object sender, EventArgs e )
        {
            UserControl control = sender as UserControl;
            if( control.Visible )
            {
                switch( m_options.applicationMode )
                {
                    case ApplicationMode.File:
                        this.lInfo.Text = FileInformation( m_options.appModeFile.executablePath );
                        break;
                    case ApplicationMode.StdInput:
                        this.lInfo.Text = FileInformation( m_options.appModeStdInput.executablePath );
                        break;
                };
            }
        }

        private void cbEnable_CheckStateChanged( System.Object sender, System.EventArgs e )
        {
            CheckBox cb = sender as CheckBox;
            m_options.enable = cb.Checked;

            Action( this, null );
        }

        public IValidatorData validate()
        {
            if( !m_options.enable )
            {
                return new ValidatorData { result = ValidatorDataResult.Disable, message = "Current code beautiful disabled" };
            }

            return new ValidatorData();
        }
        
        private String FileInformation( String FileName )
        {
            String fileInfo = String.Empty;

            System.Diagnostics.FileVersionInfo FileInfo;
            try
            {
                FileInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo( FileName );
            }
            catch( Exception )
            {
                return "No file selected";
            }

            if( !String.IsNullOrEmpty( FileInfo.CompanyName ) )
            {
                fileInfo += "CompanyName: " + FileInfo.CompanyName + Environment.NewLine;
            }
            if( !String.IsNullOrEmpty( FileInfo.ProductName ) )
            {
                fileInfo += "ProductName: " + FileInfo.ProductName + Environment.NewLine;
            }
            if( !String.IsNullOrEmpty( FileInfo.ProductVersion ) )
            {
                fileInfo += "ProductVersion: " + FileInfo.ProductVersion + Environment.NewLine;
            }
            if( !String.IsNullOrEmpty( FileInfo.FileDescription ) )
            {
                fileInfo += "FileDescription: " + FileInfo.FileDescription + Environment.NewLine;
            }
            if( !String.IsNullOrEmpty( FileInfo.FileVersion ) )
            {
                fileInfo += "FileVersion: " + FileInfo.FileVersion + Environment.NewLine;
            }
            if( !String.IsNullOrEmpty( FileInfo.LegalCopyright ) )
            {
                fileInfo += "LegalCopyright: " + FileInfo.LegalCopyright + Environment.NewLine;
            }

            if( String.IsNullOrEmpty( fileInfo ) )
            {
                fileInfo = "No information";
            }

            return fileInfo;
        }

        private void tbName_Leave( object sender, EventArgs e )
        {
            m_options.name = this.tbName.Text;
            m_treeView.Name = m_options.name;
            m_treeView.Text = m_options.name;
        }

        private void tbName_KeyDown( object sender, KeyEventArgs e )
        {
            if( e.KeyCode == Keys.Return )
            {
                m_options.name = this.tbName.Text;
                m_treeView.Name = m_options.name;
                m_treeView.Text = m_options.name;
            }
        }
    }
}

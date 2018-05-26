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
    public partial class ExcludeFiles : UserControl, IValidator
    {
        public event EventHandler Action = delegate { };
        private AppOptions m_options = null;

        public ExcludeFiles( AppOptions options )
        {
            m_options = options;
            InitializeComponent();

            this.Anchor = (System.Windows.Forms.AnchorStyles)( System.Windows.Forms.AnchorStyles.Bottom
                                                                    | System.Windows.Forms.AnchorStyles.Top
                                                                    | System.Windows.Forms.AnchorStyles.Left
                                                                    | System.Windows.Forms.AnchorStyles.Right );
            this.btnAdd.Enabled = false;
            this.btnRemove.Enabled = false;
            this.cbUseIgnoreFiles.Checked = m_options.useIgnoreFiles;
            refreshList();
        }

        public IValidatorData validate()
        {
            return new ValidatorData();
        }

        private void btnAdd_Click( System.Object sender, System.EventArgs e )
        {
            m_options.excludeFiles.Add(  this.tbAdd.Text );
            this.tbAdd.Clear();

            refreshList();
        }

        private void btnRemove_Click( System.Object sender, System.EventArgs e )
        {
            // Remove selected
            if( lwFiles.CheckedItems.Count == 0 )
            {
                foreach( ListViewItem item in lwFiles.SelectedItems )
                {
                    m_options.excludeFiles.Remove( item.Text );
                }
            }
            foreach( ListViewItem item in lwFiles.CheckedItems )
            {
                m_options.excludeFiles.Remove( item.Text );
            }
            this.btnRemove.Enabled = lwFiles.SelectedItems.Count == 0;
            refreshList();
        }

        private void tbAdd_TextChanged( System.Object sender, System.EventArgs e )
        {
            this.btnAdd.Enabled = validateFile( this.tbAdd.Text );
        }

        private void refreshList()
        {
            this.lwFiles.Items.Clear();

            foreach( var excludeFile in m_options.excludeFiles )
            {
                this.lwFiles.Items.Add( excludeFile );
            }
        }

        private void lwFiles_ItemSelectionChanged( System.Object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e )
        {
            ListView lw = sender as ListView;

            this.btnRemove.Enabled = ( lw.CheckedItems.Count + lw.SelectedItems.Count ) != 0;
        }

        private void tbAdd_KeyDown( System.Object sender, System.Windows.Forms.KeyEventArgs e )
        {
            if( e.KeyCode == Keys.Enter
                && validateFile( this.tbAdd.Text ) )
            {
                e.Handled = true;
                btnAdd_Click( null, null );
            }
        }

        private void lwFiles_ItemChecked( object sender, ItemCheckedEventArgs e )
        {
            ListView lw = sender as ListView;

            this.btnRemove.Enabled = ( lw.CheckedItems.Count + lw.SelectedItems.Count ) != 0;
        }

        private void btnFilesForm_Click( object sender, EventArgs e )
        {
            ExcludeFileInserrtHelper excludeFilesHelper = new ExcludeFileInserrtHelper( m_options.excludeFiles.ToArray() );
            excludeFilesHelper.ShowDialog();

            m_options.excludeFiles = new List< String >( excludeFilesHelper.excludeFiles );
            refreshList();
        }

        private bool validateFile( String fileName )
        {
            if( String.IsNullOrEmpty( fileName ) )
            {
                return false;
            }

            if( m_options.excludeFiles.Contains( fileName ) )
            {
                return false;
            }
            foreach( Char c in System.IO.Path.GetInvalidPathChars() )
            {
                if( fileName.Contains( c.ToString() ) )
                {
                    return false;
                }
            }
            return true;
        }

        private void cbUseIgnoreFiles_CheckedChanged( object sender, EventArgs e )
        {
            m_options.useIgnoreFiles = ( sender as CheckBox ).Checked;
        }
    }
}

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
    public partial class Applications : UserControl, IValidator
    {
        public event EventHandler Action = delegate { };
        public event EventHandler Remove = delegate { };
        public event EventHandler Add = delegate { };
        private Options m_options = null;

        public Applications( Options options )
        {
            m_options = options;
            InitializeComponent();

            this.Anchor = (System.Windows.Forms.AnchorStyles)( System.Windows.Forms.AnchorStyles.Bottom
                                                                    | System.Windows.Forms.AnchorStyles.Top
                                                                    | System.Windows.Forms.AnchorStyles.Left
                                                                    | System.Windows.Forms.AnchorStyles.Right );
            refreshList();
        }


        private void refreshList()
        {
            this.cbListOfApplications.Items.Clear();
            if (m_options.AppList.Count != 0)
            {
                this.cbListOfApplications.Items.AddRange( m_options.AppList.ToArray () );
                this.cbListOfApplications.SelectedItem = this.cbListOfApplications.Items[ 0 ];
            }
        }

        private void tbNewApplication_TextChanged( System.Object sender, System.EventArgs e )
        {
            TextBox tb = sender as TextBox;
            this.btnAdd.Enabled = validateNewParam(tb.Text );
            RefreshCloneButton();
        }

        private bool validateNewParam( string text )
        {
            bool bEnable = true;

            foreach( AppOptions app in m_options.AppList )
            {
                bEnable &= app.ToString().CompareTo( text ) != 0;
            }

            bEnable &= !( String.IsNullOrEmpty( text ) );

            return bEnable;
        }

        private void cbListOfApplications_DropDown( System.Object sender, System.EventArgs e )
        {
            ComboBox AppList = sender as ComboBox;

            AppList.Items.Clear();
            AppList.Items.AddRange( m_options.AppList.ToArray () );
        }

        private void btnAdd_Click( System.Object sender, System.EventArgs args )
        {
            try
            {
                AppOptions appOptions = new AppOptions( this.tbNewApplication.Text );
                m_options.AppList.Add( appOptions );
                Add( appOptions, null );
                this.tbNewApplication.Clear();

            }
            catch( System.Exception e )
            {
                MessageBox.Show( "Error", e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error );
            }

            Action( this, null );
        }

        private void cbListOfApplications_SelectedValueChanged( System.Object sender, System.EventArgs e )
        {
            ComboBox AppList = sender as ComboBox;

            this.btnRemove.Enabled = AppList.SelectedItem != null;
            RefreshCloneButton();
        }

        private void cbListOfApplications_DropDownClosed( System.Object sender, System.EventArgs e )
        {
            ComboBox AppList = sender as ComboBox;

            this.btnRemove.Enabled = AppList.SelectedItem != null;
            RefreshCloneButton();
        }

        private void btnRemove_Click( System.Object sender, System.EventArgs e )
        {
            m_options.AppList.Remove( this.cbListOfApplications.SelectedItem as AppOptions );
            Remove( this.cbListOfApplications.SelectedItem, null );

            this.cbListOfApplications.SelectedItem = null;

            Action( this, null );
        }

        public IValidatorData validate()
        {
            if( m_options.AppList.Count == 0 )
            {
                return new ValidatorData { result = ValidatorDataResult.Error, message = "No code beautiful application declared" };
            }

            return new ValidatorData();
        }

        private void tbNewApplication_KeyDown( System.Object sender, System.Windows.Forms.KeyEventArgs e )
        {
            if( e.KeyCode == Keys.Enter )
            {
                e.Handled = true;
                btnAdd_Click( null, null );
            }
        }

        private void bClone_Click( object sender, EventArgs e )
        {
            try
            {
                var options = this.cbListOfApplications.SelectedItem as AppOptions;
                AppOptions newOptions = new AppOptions( this.tbNewApplication.Text, options );
                m_options.AppList.Add( newOptions );
                Add( newOptions, null );
                this.tbNewApplication.Clear();

            }
            catch( System.Exception ex )
            {
                MessageBox.Show( "Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error );
            }

            Action( this, null );
        }

        private void RefreshCloneButton()
        {
            this.bClone.Enabled = this.cbListOfApplications.SelectedItem != null && validateNewParam( this.tbNewApplication.Text );
        }
    }
}

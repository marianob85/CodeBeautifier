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
    public partial class AppFile : UserControl, IValidator
    {
        public event EventHandler Action = delegate { };
        private AppOptions m_options = null;

        public AppFile( AppOptions options )
        {
            m_options = options;
            InitializeComponent();

            this.tbApplication.Text = m_options.appModeFile.executable;
            this.tbArguments.Text = m_options.appModeFile.parameters;
        }

        public IValidatorData validate()
        {
            String ExePath = m_options.appModeFile.executablePath;

            if( String.IsNullOrEmpty( ExePath ) )
            {
                return new ValidatorData { result = ValidatorDataResult.Error, message = "No Application execute set" };
            }

            if( !System.IO.File.Exists( ExePath ) )
            {
                return new ValidatorData { result = ValidatorDataResult.Error, message = "Application execute does not exist" };
            }
            return new ValidatorData();
        }

        private void btnBrowse_Click( object sender, EventArgs e )
        {
            System.Windows.Forms.OpenFileDialog AppFileDialog = new System.Windows.Forms.OpenFileDialog();

            AppFileDialog.Filter += "Executable|*.exe";
            AppFileDialog.Multiselect = false;

            if( AppFileDialog.ShowDialog() == DialogResult.OK )
            {
                this.tbApplication.Text = AppFileDialog.FileName;
            }
        }

        private void tbApplication_TextChanged( object sender, EventArgs e )
        {
            TextBox tbExecutable = sender as TextBox;

            m_options.appModeFile.executable = tbExecutable.Text;

            Action( this, null );
        }

        private void btnMenu_Click( object sender, EventArgs e )
        {
            Button btn = sender as Button;
            System.Windows.Forms.ContextMenu cms = new System.Windows.Forms.ContextMenu();
            foreach( String varName in ApplicationModeFile.variableList )
            {
                MenuItem mi = new MenuItem();
                mi.Name = varName;
                mi.Text = varName;
                mi.Click += new EventHandler( onVariableMenuSelect );

                cms.MenuItems.Add( mi );
            }

            cms.Show( btn, new System.Drawing.Point( btn.Size.Width, btn.Size.Height ) );
        }

        private void onVariableMenuSelect( object sender, EventArgs e )
        {
            MenuItem mi = sender as MenuItem;
            this.tbArguments.Text = this.tbArguments.Text.Insert( this.tbArguments.SelectionStart, mi.Text );
        }

        private void tbArguments_TextChanged( object sender, EventArgs e )
        {
            TextBox textBox = sender as TextBox;

            m_options.appModeFile.parameters = textBox.Text;

            Action( this, null );
        }

        private void tbApplication_KeyPress( object sender, KeyPressEventArgs e )
        {
            if( Array.IndexOf( System.IO.Path.GetInvalidPathChars(), e.KeyChar ) > -1 && !char.IsControl( e.KeyChar ) )
            {
                e.Handled = true;
            }
        }
    }
}

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
    public partial class Configuration : UserControl, IValidator
    {
        public event EventHandler Action = delegate { };
        private AppOptions m_options = null;

        public Configuration( AppOptions options )
        {
            m_options = options;

            InitializeComponent();

            this.Anchor = (System.Windows.Forms.AnchorStyles)( System.Windows.Forms.AnchorStyles.Bottom
                                                                    | System.Windows.Forms.AnchorStyles.Top
                                                                    | System.Windows.Forms.AnchorStyles.Left
                                                                    | System.Windows.Forms.AnchorStyles.Right );

            this.tbConfigFile.Text = m_options.applicationConfig.fileName;

            Object[] appMode = new Object[ System.Enum.GetValues( typeof( SettingsOptionsConfigFile.SearchType ) ).Length ];
            System.Enum.GetValues( typeof( SettingsOptionsConfigFile.SearchType ) ).CopyTo( appMode, 0 );
            this.cbSearchType.Items.AddRange( appMode );
            this.cbSearchType.SelectedItem = m_options.applicationConfig.searchType;
            cbSearchType_SelectionChangeCommitted( this.cbSearchType, null );

            Action( this, null );
        }

        public IValidatorData validate()
        {
            if ( m_options.appModeFile.parameters.Contains("$(ConfigFile)")
                || m_options.appModeStdInput.parameters.Contains("$(ConfigFile)" ))
            {
                if( m_options.applicationConfig.fileName.Length == 0)
                    return new ValidatorData { message = "Config file not set", result = ValidatorDataResult.Error };
            }
                
            return new ValidatorData { message = "", result = ValidatorDataResult.Ok };
        }

        private void tbConfigFile_TextChanged( object sender, EventArgs e )
        {
            TextBox tbExecutable = sender as TextBox;

            m_options.applicationConfig.fileName = tbExecutable.Text;

            Action( this, null );
        }

        private void cbSearchType_SelectionChangeCommitted( object sender, EventArgs e )
        {
            ComboBox cb = sender as ComboBox;
            SettingsOptionsConfigFile.SearchType ap = (SettingsOptionsConfigFile.SearchType)cb.SelectedItem;
            m_options.applicationConfig.searchType = ap;

            this.description.Text = SettingsOptionsConfigFile.searchTypeDescription[ ap ];
        }
    }
}

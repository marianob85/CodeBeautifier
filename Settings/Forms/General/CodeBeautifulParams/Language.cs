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
    public partial class Language : UserControl, IValidator
    {
        public event EventHandler Action = delegate { };
        private AppOptions m_options = null;

        public Language( AppOptions options )
        {
            m_options = options;

            InitializeComponent();

            this.Anchor = (System.Windows.Forms.AnchorStyles)( System.Windows.Forms.AnchorStyles.Bottom
                                                                    | System.Windows.Forms.AnchorStyles.Top
                                                                    | System.Windows.Forms.AnchorStyles.Left
                                                                    | System.Windows.Forms.AnchorStyles.Right );

            Name = "PanelCodeBeautifulLanguageSpecified";

            rbVSLanguage.Name = LanguageIdent.VisualStudio.ToString();
            rbVSLanguage.Tag = LanguageIdent.VisualStudio;
            rbExtension.Name = LanguageIdent.Extension.ToString();
            rbExtension.Tag = LanguageIdent.Extension;

            rbExtension.CheckedChanged += new EventHandler( LangIdent_CheckedChange );
            rbVSLanguage.CheckedChanged += new EventHandler( LangIdent_CheckedChange );

            ( gbLanguageIdent.Controls[ LanguageIdent.Extension.ToString() ] as RadioButton ).Checked = true;

            LangIdent_CheckedChange( gbLanguageIdent.Controls[ LanguageIdent.Extension.ToString() ], null );
        }

        public IValidatorData validate()
        {
            System.Windows.Forms.Control pPanel = this.Controls[ "pLangExt" ];
            IValidator validator = pPanel.Controls[ 0 ] as IValidator;

            return validator.validate();
        }
        private void LangIdent_CheckedChange( Object sender, EventArgs args )
        {
            RadioButton rb = sender as RadioButton;

            if( !rb.Checked )
            {
                return;
            }

            m_options.languageIdent = (LanguageIdent)rb.Tag ;

            System.Windows.Forms.Control pPanel = this.Controls[ "pLangExt" ];
           
            UserControl userPanel = null;

            switch( m_options.languageIdent )
            {
                case LanguageIdent.Extension:
                    {
                        userPanel = new LanguageExt( m_options.languageIdentExt );
                    }
                    break;
                case LanguageIdent.VisualStudio:
                    {
                        userPanel = new LanguageVS();
                    }
                    break;
            }
            userPanel.ClientSize = pPanel.ClientSize;
            userPanel.Anchor = (System.Windows.Forms.AnchorStyles)( System.Windows.Forms.AnchorStyles.Bottom
                                                        | System.Windows.Forms.AnchorStyles.Top
                                                        | System.Windows.Forms.AnchorStyles.Left
                                                        | System.Windows.Forms.AnchorStyles.Right );
            pPanel.Controls.Clear();
            pPanel.Controls.Add( userPanel );

            (userPanel as IValidator).Action += new EventHandler( onAction );

            Action( this, null );
        }

        private void onAction( Object sender, EventArgs args )
        {
            Action( this, null );
        }
    }
}

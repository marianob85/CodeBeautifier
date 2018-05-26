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
    public partial class Application : UserControl, IValidator
    {
        public event EventHandler Action = delegate { };
        private AppOptions m_options = null;

        public Application( AppOptions options )
        {
            m_options = options;

            InitializeComponent();

            this.Anchor = (System.Windows.Forms.AnchorStyles)( System.Windows.Forms.AnchorStyles.Bottom
                                                                    | System.Windows.Forms.AnchorStyles.Top
                                                                    | System.Windows.Forms.AnchorStyles.Left
                                                                    | System.Windows.Forms.AnchorStyles.Right );

            this.cbAppType.Items.Clear();

            Object[] appMode = new Object[ System.Enum.GetValues( typeof( ApplicationMode )).Length ];
            System.Enum.GetValues( typeof( ApplicationMode ) ).CopyTo( appMode, 0 );
            this.cbAppType.Items.AddRange(appMode);
            this.cbAppType.SelectedItem = m_options.applicationMode;
            cbAppType_SelectionChangeCommitted( this.cbAppType, null );
        }

        private void cbAppType_SelectionChangeCommitted( object sender, EventArgs e )
        {
            this.panelApplication.Controls.Clear();

            ComboBox cb = sender as ComboBox;
            ApplicationMode ap = (ApplicationMode)cb.SelectedItem;
            m_options.applicationMode = ap;
            this.gpDescription.Text = ap.ToString();
            switch( ap )
            {
                case ApplicationMode.File:
                    {
                        this.gpDescription.Text = ap.ToString();
                        AppFile af = new AppFile( m_options );
                        af.ClientSize = this.panelApplication.ClientSize;
                        af.Anchor = (System.Windows.Forms.AnchorStyles)( System.Windows.Forms.AnchorStyles.Bottom
                                                        | System.Windows.Forms.AnchorStyles.Top
                                                        | System.Windows.Forms.AnchorStyles.Left
                                                        | System.Windows.Forms.AnchorStyles.Right );
                        af.Action += new EventHandler( onAppAciton );
                        this.panelApplication.Controls.Add( af );
                    }
                    break;
                case ApplicationMode.StdInput:
                    {
                        this.gpDescription.Text = ap.ToString();
                        AppStdIput af = new AppStdIput( m_options );
                        af.ClientSize = this.panelApplication.ClientSize;
                        af.Anchor = (System.Windows.Forms.AnchorStyles)( System.Windows.Forms.AnchorStyles.Bottom
                                                        | System.Windows.Forms.AnchorStyles.Top
                                                        | System.Windows.Forms.AnchorStyles.Left
                                                        | System.Windows.Forms.AnchorStyles.Right );
                        af.Action += new EventHandler( onAppAciton );
                        this.panelApplication.Controls.Add( af );
                    }
                    break;
            };

            Action( this, null );
        }

        public IValidatorData validate()
        {
            if( this.panelApplication.Controls.Count != 0 )
            {
                Control c = this.panelApplication.Controls[0];

                return ( c as IValidator ).validate();
            }
            return new ValidatorData{ message="Not implemented", result = ValidatorDataResult.Error };

        }

        private void onAppAciton( Object sender, EventArgs e )
        {
            Action( this, null );
        }
    }
}

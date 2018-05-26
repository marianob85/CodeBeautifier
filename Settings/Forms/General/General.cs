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
    public partial class General : UserControl, IValidator
    {
        public event EventHandler Action = delegate { };
        private Options m_options = null;

        public General( Options options )
        {
            m_options = options;
            InitializeComponent();

            this.Anchor = (System.Windows.Forms.AnchorStyles)( System.Windows.Forms.AnchorStyles.Bottom
                                                                    | System.Windows.Forms.AnchorStyles.Top
                                                                    | System.Windows.Forms.AnchorStyles.Left
                                                                    | System.Windows.Forms.AnchorStyles.Right );
            this.Paint += new PaintEventHandler( onPaint );
            this.pgGeneral.PropertyValueChanged += pgGeneral_PropertyValueChanged;
        }

        void pgGeneral_PropertyValueChanged( object s, PropertyValueChangedEventArgs e )
        {
            refreshProperty(s as PropertyGrid);
        }

        private void refreshProperty( PropertyGrid pg )
        {
            setPropertyReadOnly( TypeDescriptor.GetProperties( pg.SelectedObject.GetType() ), m_options.general.autoFormatWhenKeyPressed, "whenType" );
            //setPropertyReadOnly( TypeDescriptor.GetProperties( pg.SelectedObject.GetType() ), m_options.general.createDumpFileOnError, "dumpFile" );
            pg.Refresh();
        }

        private void setPropertyReadOnly( PropertyDescriptorCollection collection, bool value, String startWith )
        {
            foreach( PropertyDescriptor desc in collection )
            {
                if( desc.Name.StartsWith( startWith ) )
                {
                    PropertyDescriptor descriptor = TypeDescriptor.GetProperties( desc.ComponentType )[ desc.Name ];

                    if( descriptor != null )
                    {
                        ReadOnlyAttribute attrib = (ReadOnlyAttribute)descriptor.Attributes[ typeof( ReadOnlyAttribute ) ];
                        System.Reflection.FieldInfo isBrow = attrib.GetType().GetField( "isReadOnly", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance );
                        isBrow.SetValue( attrib, !value );
                    }
                }
                setPropertyReadOnly( desc.GetChildProperties(), value, startWith );
            }
        }

        private void onPaint( System.Object obj, PaintEventArgs args )
        {
            this.pgGeneral.SelectedObject = m_options.general;
            refreshProperty( this.pgGeneral );
        }

        public IValidatorData validate()
        {
            return new ValidatorData();
        }
    }
}

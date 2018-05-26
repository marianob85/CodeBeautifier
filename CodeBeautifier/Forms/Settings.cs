using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Settings.Sources;
using Settings.Forms;

namespace Manobit.CodeBeautifier
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();

            Forms.AboutControl about = new Forms.AboutControl( AppDomain.CurrentDomain.GetAssemblies() );
            Base options = new Base( about );
            
            options.Close += new Base.EventAction( onClose );
            options.Save += new Base.EventAction( onSave );
            options.Cancel += new Base.EventAction( onCancel );

            this.Controls.Add( options );
            this.ClientSize = options.ClientSize;
            this.MinimumSize = this.Size;
            options.Anchor = (System.Windows.Forms.AnchorStyles)( System.Windows.Forms.AnchorStyles.Bottom
																		   | System.Windows.Forms.AnchorStyles.Top
																		   | System.Windows.Forms.AnchorStyles.Left
																		   | System.Windows.Forms.AnchorStyles.Right );
        }

        private void onClose( Object sender, Options args )
        {
            Close();
        }

        private void onSave( Object sender, Options args )
        {}

        private void onCancel( Object sender, Options args )
        {
            Close();
        }
    }
}

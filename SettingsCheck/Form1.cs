using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Settings.Sources;
using Manobit.CodeBeautifier.Forms;

namespace SettingsTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            Settings.Forms.Base form = new Settings.Forms.Base( new AboutControl( AppDomain.CurrentDomain.GetAssemblies() ) );
            this.Controls.Add( form );
            this.ClientSize = form.ClientSize;
            this.MinimumSize = this.Size;
            form.Anchor = (System.Windows.Forms.AnchorStyles)( System.Windows.Forms.AnchorStyles.Bottom
                                                                           | System.Windows.Forms.AnchorStyles.Top
                                                                           | System.Windows.Forms.AnchorStyles.Left
                                                                           | System.Windows.Forms.AnchorStyles.Right );

            form.Close += new Settings.Forms.Base.EventAction( onClose );
            form.Save += new Settings.Forms.Base.EventAction( onSave );
            form.Cancel += new Settings.Forms.Base.EventAction( onCancel );
        }

        private void onClose( Object sender, Options args )
        {
            Close();
        }

        private void onSave( Object sender, Options args )
        {
        }

        private void onCancel( Object sender, Options args )
        {
            Close();
        }
    }
}

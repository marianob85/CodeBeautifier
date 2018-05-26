using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;


namespace Manobit.CodeBeautifier
{
    public partial class About : Form
    {
        public About( Assembly[] assems )
        {
            InitializeComponent();
            this.panelInfo.Controls.Add( new Forms.AboutControl( assems ) );
        }

        private void m_hCLose_Click( object sender, EventArgs e )
        {
            Close();
        }
    }
}

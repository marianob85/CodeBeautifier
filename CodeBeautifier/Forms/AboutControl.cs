using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Manobit.CodeBeautifier.Forms
{
    public partial class AboutControl : UserControl
    {
        public AboutControl( Assembly[] assems )
        {
            InitializeComponent();

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            DateTime fileCreationDateTime = System.IO.File.GetLastWriteTimeUtc( assembly.Location );

            //label5.Text = assembly.GetName().Version.ToString() + " @ " + fileCreationDateTime.ToShortDateString();

            AssemblyCopyrightAttribute copyright = Assembly.GetExecutingAssembly().GetCustomAttributes( typeof( AssemblyCopyrightAttribute ), false )[ 0 ] as AssemblyCopyrightAttribute;
            AssemblyCompanyAttribute company = Assembly.GetExecutingAssembly().GetCustomAttributes( typeof( AssemblyCompanyAttribute ), false )[ 0 ] as AssemblyCompanyAttribute;
            AssemblyDescriptionAttribute description = Assembly.GetExecutingAssembly().GetCustomAttributes( typeof( AssemblyDescriptionAttribute ), false )[ 0 ] as AssemblyDescriptionAttribute;

            this.Text = "About " + description.Description;

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = CodeBeautifierBase.Resources.avatar1;

            var link = "http://www.manobit.com/code-beautifier-for-visual-studio/";
            this.linkLabelCopyright.Links.Add( 0, link.Length, link );

            var email = "mariusz.brzeski@manobit.com";
            this.linkLabelMail.Links.Add( 0, email.Length, email );

            foreach( var assemblyObj in assems )
            {
                try
                {
                    var customAttrs = assemblyObj.GetCustomAttributes( typeof( AssemblyCompanyAttribute ), false );
                    if ( customAttrs.Length == 0 )
                    {
                        continue;
                    }
                    AssemblyCompanyAttribute assemblyCompany = customAttrs[ 0 ] as AssemblyCompanyAttribute;
                    if( assemblyCompany.Company == null )
                    {
                        continue;
                    }
                    if( assemblyCompany.Company.CompareTo( "Manobit" ) == 0 )
                    {
                        ListViewItem lwi = new ListViewItem( assemblyObj.GetName().Name );
                        lwi.SubItems.Add( assemblyObj.GetName().Version.ToString() );
                        this.lvModules.Items.Add( lwi );
                    }
                }
                catch( Exception )
                { }
            }
            //this.lvModules.AutoResizeColumns( ColumnHeaderAutoResizeStyle.ColumnContent );
            this.lvModules.Enabled = false;
        }

        private void linkLabelCopyright_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            // Display the appropriate link based on the value of the 
            // LinkData property of the Link object.
            string target = e.Link.LinkData as string;

            // If the value looks like a URL, navigate to it.
            // Otherwise, display it in a message box.
            System.Diagnostics.Process.Start( target );
        }

        private void linkLabelMail_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            // Display the appropriate link based on the value of the 
            // LinkData property of the Link object.
            string target = e.Link.LinkData as string;

            // If the value looks like a URL, navigate to it.
            // Otherwise, display it in a message box.
            System.Diagnostics.Process.Start( "mailto:"+target );
        }
    }
}

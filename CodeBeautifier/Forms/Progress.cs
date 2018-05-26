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
    public partial class Progress : Form
    {
        delegate void EventHandlerFinish();
        private delegate void EventHandlerProgress( Object sender, EventArgs e, EnvDTE.ProjectItem projectItem );
        private delegate void EventHandlerProgress1( Object sender, EventArgs e, EnvDTE.ProjectItem projectItem, UInt32 currentElement, UInt32 maxElements, UInt32 succesCount, UInt32 failedCount );
        private delegate void EventHandlerProgressA( Object sender, EventArgs e, String name );
        private delegate void EventHandlerProgressB( Object sender, EventArgs e, EnvDTE.Document document );
        private delegate void EventHandlerProgress1A( Object sender, EventArgs e, String filePath, UInt32 currentElement, UInt32 maxElements, UInt32 succesCount, UInt32 failedCount );
        private delegate void EventHandlerProgress1B( Object sender, EventArgs e, EnvDTE.Document document, UInt32 currentElement, UInt32 maxElements, UInt32 succesCount, UInt32 failedCount );
        private delegate void EventHandlerStart( Object sender, EventArgs e, String name );

        public delegate void EventHandlerCancel();
        public event EventHandlerCancel Cancel = delegate { };

        public Progress()
        {
            InitializeComponent();

            m_tsslVersion.Text = "Version: " + System.Diagnostics.FileVersionInfo.GetVersionInfo( Assembly.GetExecutingAssembly().Location ).FileVersion.Replace( ',', '.' );
        }

        public void onStarted( Object sender,EventArgs e, String status )
        {
            if( InvokeRequired )
            {
                this.BeginInvoke( new EventHandlerStart( onStarted ), new object[]{ sender, e, status });
            }
            else
            {
                this.tsslStatus.Text = "Status: " + status;
            }
        }

        public void onProgress( Object sender, EventArgs e, EnvDTE.ProjectItem projectItem )
        {
            if( !this.IsDisposed )
            {
                if( InvokeRequired )
                {
                    this.BeginInvoke( new EventHandlerProgress( onProgress ), new object[] { sender, e, projectItem } );
                }
                else
                {
                    this.progressBar1.Style = ProgressBarStyle.Marquee;
                    this.progressBar1.Step = 1;
                    this.progressBar1.Minimum = 0;
                    this.progressBar1.Maximum = 30;
                    this.progressBar1.MarqueeAnimationSpeed = 20;

                    // DEFAULT
                    this.lProject.Text = projectItem.ContainingProject.Name;
                    this.lFile.Text = projectItem.Name;
                    this.m_lSuccess.Text = "---------";
                    this.m_lFailed.Text = "---------";
                }
            }
        }

        public void onProgressA( Object sender, EventArgs e, String name )
        {
            if( !this.IsDisposed )
            {
                if( InvokeRequired )
                {
                    this.BeginInvoke( new EventHandlerProgressA( onProgressA ), new object[] { sender, e, name } );
                }
                else
                {
                    this.progressBar1.Style = ProgressBarStyle.Marquee;
                    this.progressBar1.Step = 1;
                    this.progressBar1.Minimum = 0;
                    this.progressBar1.Maximum = 30;
                    this.progressBar1.MarqueeAnimationSpeed = 20;

                    this.lProject.Text = "---------";
                    this.lFile.Text = System.IO.Path.GetFileName( name );
                    this.m_lSuccess.Text = "---------";
                    this.m_lFailed.Text = "---------";
                }
            }
        }

        public void onProgressB( Object sender, EventArgs e, EnvDTE.Document document, UInt32 currentElement, UInt32 maxElements, UInt32 succesCount, UInt32 failedCount )
        {
            if( InvokeRequired )
            {
                this.BeginInvoke( new EventHandlerProgress1B( onProgressB ), new object[] { sender, e, document, currentElement, maxElements, succesCount, failedCount } );
            }
            else
            {
                this.progressBar1.Style = ProgressBarStyle.Blocks;
                this.progressBar1.Value = (int)currentElement;
                this.progressBar1.Minimum = 0;
                this.progressBar1.Maximum = (int)maxElements;

                this.lProject.Text = "---------";
                this.lFile.Text = System.IO.Path.GetFileName( document.FullName );
                this.m_lSuccess.Text = succesCount.ToString();
                this.m_lFailed.Text = failedCount.ToString();
            }
        }

        public void onProgressA( Object sender, EventArgs e, String filePath, UInt32 currentElement, UInt32 maxElements, UInt32 succesCount, UInt32 failedCount )
        {
            if( InvokeRequired )
            {
                this.BeginInvoke( new EventHandlerProgress1A( onProgressA ), new object[] { sender, e, filePath, currentElement, maxElements, succesCount, failedCount } );
            }
            else
            {
                this.progressBar1.Style = ProgressBarStyle.Blocks;
                this.progressBar1.Value = (int)currentElement;
                this.progressBar1.Minimum = 0;
                this.progressBar1.Maximum = (int)maxElements;

                this.lProject.Text = "---------";
                this.lFile.Text = System.IO.Path.GetFileName( filePath );
                this.m_lSuccess.Text = succesCount.ToString();
                this.m_lFailed.Text = failedCount.ToString();
            }
        }


        public void onProgress( Object sender, EventArgs e, EnvDTE.ProjectItem projectItem, UInt32 currentElement, UInt32 maxElements, UInt32 succesCount, UInt32 failedCount )
        {
            if( InvokeRequired )
            {
                this.BeginInvoke( new EventHandlerProgress1( onProgress ), new object[] { sender, e, projectItem, currentElement, maxElements, succesCount, failedCount } );
            }
            else
            {
                this.progressBar1.Style = ProgressBarStyle.Blocks;
                this.progressBar1.Value = (int)currentElement;
                this.progressBar1.Minimum = 0;
                this.progressBar1.Maximum = (int)maxElements;

                this.lProject.Text = projectItem.ContainingProject.Name;
                this.lFile.Text = projectItem.Name;
                this.m_lSuccess.Text = succesCount.ToString();
                this.m_lFailed.Text  = failedCount.ToString();
            }
        }

        public void onFinished( Object sender, EventArgs e )
        {
            if( !this.IsDisposed )
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                if( InvokeRequired )
                {
                    this.BeginInvoke( new EventHandlerFinish( Close ), new object[] { sender, e } );
                }
                else
                {
                    Close();
                }
            }
        }

        private void m_bCancel_Click( object sender, EventArgs e )
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Cancel();
            //Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Settings.Sources;
using EnvDTE;

namespace Manobit.CodeBeautifier.Sources
{
    public class DocumentsMakerGui : DocumentsMakerSingle
    {
        private System.Threading.Thread m_worker;
        private List<Object> m_objects = null;

        public DocumentsMakerGui(EnvDTE80.DTE2 dte2, Options settings )
            : base( dte2, settings ) {}

        public override bool make( List<Object> objects )
        {
            m_objects = objects;
            Progress progressForm = new Progress();
            progressForm.Cancel += new Progress.EventHandlerCancel(onFormCancel);

            Started += new EventHandlerStart( progressForm.onStarted );
            Progress += new EventHandlerProgress( progressForm.onProgress );
            ProgressA += new EventHandlerProgressA( progressForm.onProgressA );
            ProgressB += new EventHandlerProgressB( progressForm.onProgressB );
            Finished += new EventHandlerFinish( progressForm.onFinished );
            m_worker = new System.Threading.Thread( new System.Threading.ThreadStart( worker ) );

            progressForm.Shown += new EventHandler( onDialogShown );
            progressForm.ShowDialog();

            m_worker.Join();
            progressForm.Close();
            return true;
        }

        private void worker()
        {
            base.make( m_objects );
        }

        private void onDialogShown( Object obj, EventArgs args)
        {
            m_worker.Start();
        }

        private void onFormCancel()
        {
            m_cancelPending = true;
        }
    }
}

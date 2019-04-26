using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Settings.Sources;

namespace Manobit.CodeBeautifier.Sources
{
    public class DocumentFinderGUI : DocumentFinder
    {
        private Thread m_worker;
        private List<Object> m_returnObjects = new List<object>();

        public DocumentFinderGUI(  EnvDTE80.DTE2 dte2, Options settings )
            : base(dte2, settings ) { }

        public override List<Object> opened( bool noSavedOnly )
        {
            m_returnObjects = new List<object>(); ;
            Progress progressForm = new Progress();
            progressForm.Cancel += new Progress.EventHandlerCancel(onFormCancel);
            Started += new EventHandlerStart( progressForm.onStarted );
            Progress += new EventHandlerProgress( progressForm.onProgress );
            ProgressA += new EventHandlerProgressA( progressForm.onProgressA );
            Finished += new EventHandlerFinish( progressForm.onFinished );

            m_worker = new Thread( new ParameterizedThreadStart( openedThread ) );

            progressForm.Tag = noSavedOnly;
            progressForm.Shown += new EventHandler( onFormShow );
            progressForm.ShowDialog();

            m_worker.Join();

            return m_returnObjects;
        }
        public override List<Object> activeProject()
        {
            m_returnObjects = new List<object>(); ;
            Progress progressForm = new Progress();
            progressForm.Cancel += new Progress.EventHandlerCancel(onFormCancel);
            Started += new EventHandlerStart( progressForm.onStarted );
            Progress += new EventHandlerProgress( progressForm.onProgress );
            Finished += new EventHandlerFinish( progressForm.onFinished );

            m_worker = new Thread( new ThreadStart( activeProjectThread ) );

            progressForm.Shown += new EventHandler( onFormShow );
            progressForm.ShowDialog();

            m_worker.Join();

            return m_returnObjects;
        }

        public override List<Object> solution()
        {
            m_returnObjects = new List<object>(); ;
            Progress progressForm = new Progress();
            progressForm.Cancel += new Progress.EventHandlerCancel(onFormCancel);
            Started += new EventHandlerStart( progressForm.onStarted );
            Progress += new EventHandlerProgress( progressForm.onProgress );
            Finished += new EventHandlerFinish( progressForm.onFinished );

            m_worker = new Thread( new ThreadStart( solutionThread ) );

            progressForm.Shown += new EventHandler( onFormShow );
            progressForm.ShowDialog();

            m_worker.Join();

            return m_returnObjects;
        }

        private void onFormShow( Object sender, EventArgs args )
        {
            Object threadArgs = ( sender as System.Windows.Forms.Form ).Tag;

            if( threadArgs != null )
            {
                m_worker.Start( threadArgs );
            }
            else
            {
                m_worker.Start();
            }
        }

        private void openedThread( Object obj )
        {
            m_returnObjects = base.opened( (bool)obj );
        }

        private void activeProjectThread()
        {
            m_returnObjects = base.activeProject();
        }

        private void solutionThread()
        {
            m_returnObjects = base.solution();
        }

        private void onFormCancel()
        {
            m_worker.Abort();
        }
    };
}

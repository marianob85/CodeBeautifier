using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE;
using Settings.Sources;

namespace Manobit.CodeBeautifier.Sources
{
    public class DocumentFinder
    {
        private IServiceProvider m_serviceProvider;
        private Logger m_logger;

        protected delegate void EventHandlerProgress( Object sender, EventArgs e, ProjectItem projectItem );
        protected delegate void EventHandlerProgressA( Object sender, EventArgs e, String name );
        protected delegate void EventHandlerFinish(Object sender,EventArgs e);
        protected delegate void EventHandlerStart( Object sender, EventArgs e, String status );

        protected event EventHandlerProgress Progress = delegate { };
        protected event EventHandlerProgressA ProgressA = delegate { };
        protected event EventHandlerFinish Finished = delegate { };
        protected event EventHandlerStart Started = delegate { };

        public DocumentFinder( IServiceProvider serviceProvider, Options settings )
        {
            this.m_logger = new Logger( serviceProvider, settings );
            this.m_serviceProvider = serviceProvider;
        }

        private void dumpObjects( List<Object> objects )
        {
            foreach( Object obj in objects )
            {
                if( obj is String )
                    m_logger.Log( String.Format( "DocumentFinder: Element: {0}.", obj ), OptionsGeneral.LoggerPriority.High );
                else if( obj is ProjectItem )
                    m_logger.Log( String.Format( "DocumentFinder: Element: {0}.", ( obj as ProjectItem ).Properties.Item( "FullPath" ).Value ), OptionsGeneral.LoggerPriority.High );
                else if( obj is Document )
                    m_logger.Log( String.Format( "DocumentFinder: Element: {0}.", ( obj as Document ).FullName ), OptionsGeneral.LoggerPriority.High );
            }
        }

        virtual public List<Object> opened( bool noSavedOnly )
        {
            EnvDTE80.DTE2 dte2 = m_serviceProvider.GetService( typeof( EnvDTE.DTE ) ) as EnvDTE80.DTE2;

            setStart();

            m_logger.Log( "DocumentFinder: Searching for opened.", OptionsGeneral.LoggerPriority.High );

            List<Object> docContainer = new List<Object>();
            foreach( Window window in dte2.Windows )
            {
                if( window.Type == vsWindowType.vsWindowTypeDocument )
                {
                    try
                    {
                        Document document = window.Document;
                        if( !noSavedOnly
                            || ( !document.Saved && noSavedOnly ) )
                        {
                            // Opened only must be as TextDocument
                            ProjectItem prjItem = ( ( document.Object( "TextDocument" ) as TextDocument ).Parent as Document ).ProjectItem;
                            if( prjItem.Document != null )
                            {
                                docContainer.AddRange( getProjectItem( prjItem ) );
                            }
                            else
                            {
                                // Only for progress window
                                docContainer.AddRange( getProjectItem( window.Document.FullName ) );
                            }
                        }
                    }
                    catch( Exception )
                    {
                        // If cast will not success we will catch exception
                        // Nothing to do
                    }
                }
            }
            m_logger.Log( String.Format( "DocumentFinder: Found {0} items.", docContainer.Count ), OptionsGeneral.LoggerPriority.High );

            setFinish();

            return docContainer;
        }

        virtual public List<Object> activeProject()
        {
            EnvDTE80.DTE2 dte2 = m_serviceProvider.GetService( typeof( EnvDTE.DTE ) ) as EnvDTE80.DTE2;
            setStart();

            List<Object> docContainer = new List<Object>();

            if( ( dte2.ActiveSolutionProjects as Array ).Length == 0 )
            {
                return docContainer;
            }

            Array projects = dte2.ActiveSolutionProjects as Array;
            foreach( EnvDTE.Project project in projects )
            {
                docContainer.AddRange( getProjectItems( project.ProjectItems ) );
            }

            setFinish();
            return docContainer;
        }

        virtual public List<Object> solution()
        {
            EnvDTE80.DTE2 dte2 = m_serviceProvider.GetService( typeof( EnvDTE.DTE ) ) as EnvDTE80.DTE2;
            setStart();

            List<Object> docContainer = new List<Object>();

            foreach( EnvDTE.Project project in dte2.Solution.Projects )
            {
                if( project.ProjectItems != null )
                {
                    docContainer.AddRange( getProjectItems( project.ProjectItems ) );
                }
            }

            setFinish();
            return docContainer;
        }

        protected List<Object> getUIHierarchyItems( UIHierarchyItems uiHItems )
        {
            EnvDTE80.DTE2 dte2 = m_serviceProvider.GetService( typeof( EnvDTE.DTE ) ) as EnvDTE80.DTE2;
            List<Object> docContainer = new List<Object>();

            foreach( UIHierarchyItem hItem in uiHItems )
            {
                ProjectItem prjItem = null;
                try
                {
                    prjItem = hItem.Object as ProjectItem;
                    if( prjItem.FileCodeModel != null )
                    {
                        docContainer.AddRange( getProjectItem( prjItem ) );
                    }
                    else
                    {
                        docContainer.AddRange( getUIHierarchyItems( hItem.UIHierarchyItems ) );
                    }
                }
                catch( InvalidCastException  /*e*/ )
                {
                    docContainer.AddRange( getUIHierarchyItems( hItem.UIHierarchyItems ) );
                }
                catch( Exception e )
                {
                    m_logger.Log( "Can't determine project item", OptionsGeneral.LoggerPriority.Medium );
                    m_logger.Log( e );
                }
            }
            return docContainer;
        }
        protected List<Object> getProjectItems( ProjectItems hPrjItems )
        {
            if( hPrjItems == null)
            {
                return new List<Object>();
            }
            EnvDTE80.DTE2 dte2 = m_serviceProvider.GetService( typeof( EnvDTE.DTE ) ) as EnvDTE80.DTE2;
            List<Object> docContainer = new List<Object>();

            foreach( ProjectItem prjItem in hPrjItems )
            {
                var test = prjItem.Name;
                if( prjItem.FileCodeModel != null )
                {
                    try
                    {
                        docContainer.AddRange( getProjectItem( prjItem ) );
                    }
                    catch( Exception e )
                    {
                        m_logger.Log("Cant determine project item", OptionsGeneral.LoggerPriority.Medium);
                        m_logger.Log( e );
                    }
                }
                docContainer.AddRange( getProjectItems( prjItem.ProjectItems ) );
                if( prjItem.SubProject != null )
                {
                    docContainer.AddRange( getProjectItems( prjItem.SubProject.ProjectItems ) );
                }
            }

            return docContainer;
        }
        protected List<Object> getProjectItem( ProjectItem prjItem )
        {
            List<Object> docContainer = new List<Object>();

            docContainer.Add( prjItem );
            Progress( this,null, prjItem );

            return docContainer;
        }
        protected List<Object> getProjectItem( String filePath )
        {
            List<Object> docContainer = new List<Object>();

            docContainer.Add( filePath );
            ProgressA( this, null, filePath );

            return docContainer;
        }

        protected void setStart()
        {
            Started( this, null, "Acquiring project files..." );
        }

        protected void setFinish()
        {
            Finished(this,null);
        }
    };

}

using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using Manobit.CodeBeautifier.Sources;
using Settings.Sources;

namespace CodeBeautifier.Sources
{
    public class CodeBeautifierEventHandler
    {
        protected DTE2 m_hApplicationObject;

        protected CommandEvents m_hEventOnBuildSolution;
        protected CommandEvents m_hEventOnRebuildSolution;
        protected CommandEvents m_hEventOnBuildOnlyProject;
        protected CommandEvents m_hEventOnRebuildOnlyProject;
        protected CommandEvents m_hEventOnBuildSelected;
        protected CommandEvents m_hEventOnRebuildSelected;
        protected IServiceProvider m_serviceProvider;
        private bool m_savePending = false;
        private EnvDTE80.TextDocumentKeyPressEvents m_textDocumentEvents = null;
        private EnvDTE.DocumentEvents m_documentEvents = null;
        protected SettingsContainer m_settings = new SettingsContainer();
        protected Logger m_logger;

        public CodeBeautifierEventHandler( IServiceProvider serviceProvider )
        {
            m_serviceProvider = serviceProvider;
            m_hApplicationObject = serviceProvider.GetService( typeof( EnvDTE.DTE ) ) as EnvDTE80.DTE2;

            m_settings.onOptionsChanged += new EventHandler( onConfigurationChanged );
            onConfigurationChanged( this, null );

            Command hCmdBuildSolution = m_hApplicationObject.Commands.Item( "Build.BuildSolution", 0 );
            Command hCmdRebuildSolution = m_hApplicationObject.Commands.Item( "Build.RebuildSolution", 0 );
            Command hCmdBuildOnlyProject = m_hApplicationObject.Commands.Item( "Build.BuildOnlyProject", 0 );
            Command hCmdRebuildOnlyProject = m_hApplicationObject.Commands.Item( "Build.RebuildOnlyProject", 0 );

            Command hCmdBuildSelection = m_hApplicationObject.Commands.Item( "Build.BuildSelection", 886 );
            Command hCmdRebuildSelection = m_hApplicationObject.Commands.Item( "Build.RebuildSelection", 887 );

            // BuildSolution
            m_hEventOnBuildSolution = m_hApplicationObject.Events.CommandEvents[ hCmdBuildSolution.Guid, hCmdBuildSolution.ID ] as CommandEvents;
            m_hEventOnBuildSolution.BeforeExecute += new _dispCommandEvents_BeforeExecuteEventHandler( OnBuildSolution );

            m_hEventOnRebuildSolution = m_hApplicationObject.Events.CommandEvents[ hCmdRebuildSolution.Guid, hCmdRebuildSolution.ID ] as CommandEvents;
            m_hEventOnRebuildSolution.BeforeExecute += new _dispCommandEvents_BeforeExecuteEventHandler( OnReBuildSolution );

            m_hEventOnBuildOnlyProject = m_hApplicationObject.Events.CommandEvents[ hCmdBuildOnlyProject.Guid, hCmdBuildOnlyProject.ID ] as CommandEvents;
            m_hEventOnBuildOnlyProject.BeforeExecute += new _dispCommandEvents_BeforeExecuteEventHandler( OnBuildProject );

            m_hEventOnRebuildOnlyProject = m_hApplicationObject.Events.CommandEvents[ hCmdRebuildOnlyProject.Guid, hCmdRebuildOnlyProject.ID ] as CommandEvents;
            m_hEventOnRebuildOnlyProject.BeforeExecute += new _dispCommandEvents_BeforeExecuteEventHandler( OnReBuildProject );


            m_hEventOnBuildSelected = m_hApplicationObject.Events.CommandEvents[ hCmdBuildSelection.Guid, hCmdBuildSelection.ID ] as CommandEvents;
            m_hEventOnBuildSelected.BeforeExecute += new _dispCommandEvents_BeforeExecuteEventHandler( OnBuildProject );

            m_hEventOnRebuildSelected = m_hApplicationObject.Events.CommandEvents[ hCmdRebuildSelection.Guid, hCmdRebuildSelection.ID ] as CommandEvents;
            m_hEventOnRebuildSelected.BeforeExecute += new _dispCommandEvents_BeforeExecuteEventHandler( OnReBuildProject );

            EnvDTE80.Events2 dteEvents2 = m_hApplicationObject.Events as EnvDTE80.Events2;
            m_textDocumentEvents = dteEvents2.get_TextDocumentKeyPressEvents();
            m_textDocumentEvents.AfterKeyPress += onTextDocumentKeyPressed;

            m_documentEvents = dteEvents2.get_DocumentEvents();
            m_documentEvents.DocumentSaved += onDocumentSaved;
        }

        private void onDocumentSaved(Document document)
        {
            if (m_settings.options.general.onDocumentSave && document != null && m_savePending == false)
            {
                m_logger.Log("onDocumentSaved:");
                makeDocument(document);
                m_savePending = true;
                document.Save();
                m_savePending = false;
            }
        }

        private void onTextDocumentKeyPressed( string Keypress, TextSelection Selection, bool InStatementCompletion )
        {
            if( m_settings.options.general.autoFormatWhenKeyPressed )
            {
                if( m_settings.options.general.handleNoneProjectFile == false
                    && Selection.Parent.Parent.ProjectItem.Document == null )
                {
                    return;
                }

                DocumentsMakerSingle documentsMaker = new DocumentsMakerSingle( m_serviceProvider, m_settings.options );
                documentsMaker.make( new DocumentsMakerSingle.KeyPressedData( Selection, Keypress ) );
            }
        }

        private void onConfigurationChanged( Object sender, EventArgs e )
        {
            m_settings.read();
            m_logger = new Logger( m_serviceProvider, m_settings.options );
        }

        private void OnBuildSolution( string Guid, int ID, object CustomIn, object CustomOut, ref bool CancelDefault )
        {
            if( !m_settings.options.general.onBuildSolution )
            {
                return;
            }

            m_logger.Log( "onBuildSolution: " + Guid + " - " + ID.ToString() );

            makeAllOpened( true );
        }
        private void OnReBuildSolution( string Guid, int ID, object CustomIn, object CustomOut, ref bool CancelDefault )
        {
            if( !m_settings.options.general.onRebuildSolution )
            {
                return;
            }

            m_logger.Log( "onReBuildSolution: " + Guid + " - " + ID.ToString() );

            makeAllOpened( true );
        }
        private void OnBuildProject( string Guid, int ID, object CustomIn, object CustomOut, ref bool CancelDefault )
        {
            if( !m_settings.options.general.onBuildActiveProject )
            {
                return;
            }

            m_logger.Log( "onBuildSelected: " + Guid + " - " + ID.ToString() );

            makeAllOpened( true );
        }
        private void OnReBuildProject( string Guid, int ID, object CustomIn, object CustomOut, ref bool CancelDefault )
        {
            if( !m_settings.options.general.onRebuildActiveProject )
            {
                return;
            }

            m_logger.Log( "onReBuildSelected: " + Guid + " - " + ID.ToString() );

            makeAllOpened( true );
        }

        private void makeAllOpened( bool noSavedOnly )
        {
            DocumentFinderGUI finderGui = new DocumentFinderGUI( m_serviceProvider, m_settings.options );

            List<Object> documents = finderGui.opened( noSavedOnly );

            DocumentsMakerGui documentsMaker = new DocumentsMakerGui( m_serviceProvider, m_settings.options );
            documentsMaker.make( documents );
        }

        private void makeCurrent()
        {
            EnvDTE80.DTE2 dte2 = m_serviceProvider.GetService( typeof( EnvDTE.DTE ) ) as EnvDTE80.DTE2;

            switch( dte2.ActiveWindow.Type )
            {
                case vsWindowType.vsWindowTypeDocument:
                    {
                        Document activeDocument = dte2.ActiveDocument as Document;
                        if( !activeDocument.Saved )
                        {
                            makeDocument(activeDocument);
                        }
                    }
                    break;
            } // switch
        }

        private void makeDocument(Document document)
        {
            List<Object> objects = new List<Object>();

            if (document.ProjectItem.Document != null)
                objects.Add(document.ProjectItem.Document);
            else if (m_settings.options.general.handleNoneProjectFile)
                objects.Add(document);

            if (objects.Count > 0)
            {
                IDocumentsMaker docMaker = new DocumentsMakerSingle(m_serviceProvider, m_settings.options);
                docMaker.make(objects);
            }
        }

    };


    public abstract class CodeBeautifierCommands
    {
        protected DTE2 m_hApplicationObject;
        protected AddIn m_hAddInInstance;
        protected IServiceProvider m_serviceProvider;
        protected int m_iIconIdx;
        protected String m_strCommandName;
        protected String m_strCommandCaption;
        protected String m_hShortcut;
        protected Command m_hCommand;
        protected bool m_group;
        protected SettingsContainer m_settings = new SettingsContainer();
        protected Logger m_logger;

        public CodeBeautifierCommands( IServiceProvider serviceProvider,
            String strCommandName,
            String strCommandCaption,
            String hShortcut,
            int iIconIdx,
            bool group = false )
        {
            m_serviceProvider = serviceProvider;
            m_hApplicationObject = serviceProvider.GetService( typeof( EnvDTE.DTE ) ) as EnvDTE80.DTE2;
            m_hAddInInstance = serviceProvider.GetService( typeof( AddIn ) ) as AddIn;
            m_strCommandName = strCommandName;
            m_strCommandCaption = strCommandCaption;
            m_iIconIdx = iIconIdx;
            m_group = group;

            m_settings.onOptionsChanged += new EventHandler( onConfigurationChanged );
            onConfigurationChanged( this, null );
        }

        private void onConfigurationChanged( Object sender, EventArgs e )
        {
            m_settings.read();
            m_logger = new Logger( m_serviceProvider, m_settings.options );
        }

        public bool Exec( String strCommandName )
        {
            if( strCommandName.CompareTo( "CodeBeautifier.Connect." + m_strCommandName ) == 0 )
            {
                m_logger.Log( m_strCommandName + " " + m_strCommandCaption );
                Execute();
                return true;
            }
            return false;
        }
        public bool QueryStatus( String strCommandName, ref vsCommandStatus StatusOption )
        {
            if( strCommandName.CompareTo( "CodeBeautifier.Connect." + m_strCommandName ) == 0 )
            {
                StatusOption = (vsCommandStatus)( vsCommandStatus.vsCommandStatusSupported );
                if( Status() )
                {
                    StatusOption = StatusOption | (vsCommandStatus)vsCommandStatus.vsCommandStatusEnabled;
                }
                return true;
            }
            return false;
        }
        public void BuildUI( CommandBar MenuCommandBar, String strPath, int Pos )
        {
            MenuBuilder hMenuBuilder = new MenuBuilder( m_hApplicationObject, m_hAddInInstance );

            // Build menu
            CommandBar hCurrentBar = hMenuBuilder.BuildUI( MenuCommandBar, strPath, m_hCommand, m_group, Pos );

            if( CreateCommand() )
            {
                hMenuBuilder.BuildCommand( hCurrentBar, m_hCommand, m_group );
            }
        }

        public abstract void Execute();

        protected abstract bool Status();

        private bool CreateCommand()
        {
            try
            {
                Commands2 hCommands = m_hApplicationObject.Commands as Commands2;
                m_hCommand = hCommands.Item( "CodeBeautifier.Connect." + m_strCommandName, 0 );

                return false;
            }
            catch( ArgumentException )
            {
                // Build command
                object[] contextGUIDS = new object[] { };
                Commands2 hCommands = m_hApplicationObject.Commands as Commands2;
                m_hCommand = hCommands.AddNamedCommand2( m_hAddInInstance,
                    (String)m_strCommandName,
                    (String)m_strCommandCaption,
                    (String)m_strCommandName,
                    true,
                    m_iIconIdx,
                    contextGUIDS,
                    (int)vsCommandStatus.vsCommandStatusSupported,
                    (int)vsCommandStyle.vsCommandStylePictAndText, vsCommandControlType.vsCommandControlTypeButton );
                return true;
            }
        }
    }

    public class CodeBeautifierActiveDocument : CodeBeautifierCommands
    {
        public CodeBeautifierActiveDocument( IServiceProvider serviceProvider,
            String strCommandName,
            String strCommandCaption,
            String hShortcut,
            int iIconIdx )
            : base( serviceProvider, strCommandName, strCommandCaption, hShortcut, iIconIdx )
        { }

        public override void Execute()
        {
            EnvDTE80.DTE2 dte2 = m_serviceProvider.GetService( typeof( EnvDTE.DTE ) ) as EnvDTE80.DTE2;
            List<Object> objects = new List<Object>();
            if( dte2.ActiveDocument.ProjectItem.Document != null )
            {
                objects.Add( dte2.ActiveDocument.ProjectItem );
            }
            else if( m_settings.options.general.handleNoneProjectFile )
            {
                objects.Add( dte2.ActiveDocument.FullName );
            }

            IDocumentsMaker docMaker = new DocumentsMakerSingle( m_serviceProvider, m_settings.options );
            docMaker.make( objects );
        }

        protected override bool Status()
        {
            EnvDTE.DTE dte = m_serviceProvider.GetService( typeof( EnvDTE.DTE ) ) as EnvDTE.DTE;

            return dte.ActiveDocument != null;
        }
    }

    public class CodeBeautifierAllOpenedDocuments : CodeBeautifierCommands
    {
        public CodeBeautifierAllOpenedDocuments( IServiceProvider serviceProvider,
        String strCommandName,
        String strCommandCaption,
        String hShortcut,
        int iIconIdx )
            : base( serviceProvider, strCommandName, strCommandCaption, hShortcut, iIconIdx )
        { }

        public override void Execute()
        {
            DocumentFinderGUI finderGui = new DocumentFinderGUI( m_serviceProvider, m_settings.options );

            List<Object> documents = finderGui.opened( false );

            DocumentsMakerGui documentsMaker = new DocumentsMakerGui( m_serviceProvider, m_settings.options );
            documentsMaker.make( documents );
        }

        protected override bool Status()
        {
            EnvDTE.DTE dte = m_serviceProvider.GetService( typeof( EnvDTE.DTE ) ) as EnvDTE.DTE;

            bool enable = false;
            foreach( EnvDTE.Window window in dte.Windows )
            {
                enable |= window.Type == EnvDTE.vsWindowType.vsWindowTypeDocument;
            }
            return enable;
        }
    }
    public class CodeBeautifierActiveProject : CodeBeautifierCommands
    {
        public CodeBeautifierActiveProject( IServiceProvider serviceProvider,
            String strCommandName,
            String strCommandCaption,
            String hShortcut,
            int iIconIdx )
            : base( serviceProvider, strCommandName, strCommandCaption, hShortcut, iIconIdx )
        { }

        public override void Execute()
        {
            DocumentFinderGUI finderGui = new DocumentFinderGUI( m_serviceProvider, m_settings.options );

            List<Object> documents = finderGui.activeProject();

            DocumentsMakerGui documentsMaker = new DocumentsMakerGui( m_serviceProvider, m_settings.options );
            documentsMaker.make( documents );
        }

        protected override bool Status()
        {

            EnvDTE.DTE dte = m_serviceProvider.GetService( typeof( EnvDTE.DTE ) ) as EnvDTE.DTE;

            try
            {
                System.Array activeProjects = dte.ActiveSolutionProjects as System.Array;
                return activeProjects.Length > 0;
            }
            catch( Exception )
            {
                return false;
            }
        }
    }

    public class CodeBeautifierSolution : CodeBeautifierCommands
    {
        public CodeBeautifierSolution( IServiceProvider serviceProvider,
            String strCommandName,
            String strCommandCaption,
            String hShortcut,
            int iIconIdx )
            : base( serviceProvider, strCommandName, strCommandCaption, hShortcut, iIconIdx )
        { }

        public override void Execute()
        {
            DocumentFinderGUI finderGui = new DocumentFinderGUI( m_serviceProvider, m_settings.options );

            List<Object> documents = finderGui.solution();

            DocumentsMakerGui documentsMaker = new DocumentsMakerGui( m_serviceProvider, m_settings.options );
            documentsMaker.make( documents );
        }

        protected override bool Status()
        {
            EnvDTE.DTE dte = m_serviceProvider.GetService( typeof( EnvDTE.DTE ) ) as EnvDTE.DTE;

            try
            {
                return dte.Solution.Projects.Count > 0;
            }
            catch( Exception )
            {
                return false;
            }
        }
    }

    public class CodeBeautifierOptions : CodeBeautifierCommands
    {
        public CodeBeautifierOptions( IServiceProvider serviceProvider,
        String strCommandName,
        String strCommandCaption,
        String hShortcut,
        int iIconIdx,
            bool bGroup )
            : base( serviceProvider, strCommandName, strCommandCaption, hShortcut, iIconIdx, bGroup )
        { }

        public override void Execute()
        {
            Manobit.CodeBeautifier.Settings settings = new Manobit.CodeBeautifier.Settings();
            settings.ShowDialog();
        }

        protected override bool Status()
        {
            return true;
        }
    }

    public class CodeBeautifierAbout : CodeBeautifierCommands
    {
        public CodeBeautifierAbout( IServiceProvider serviceProvider,
        String strCommandName,
        String strCommandCaption,
        String hShortcut,
        int iIconIdx,
            bool bGroup )
            : base( serviceProvider, strCommandName, strCommandCaption, hShortcut, iIconIdx, bGroup )
        { }

        public override void Execute()
        {
            Manobit.CodeBeautifier.About settings = new Manobit.CodeBeautifier.About( AppDomain.CurrentDomain.GetAssemblies() );
            settings.ShowDialog();
        }

        protected override bool Status()
        {
            return true;
        }
    }
}

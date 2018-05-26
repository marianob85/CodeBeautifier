using System;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using EnvDTE;
using Manobit.CodeBeautifier;
using Manobit.CodeBeautifier.Sources;
using Settings.Sources;
using Settings.Forms;
using System.Windows.Forms;

namespace Manobit.CodeBeautifier
{

    [Guid( "F9421A4E-1F44-47B5-807D-E4CA88088891" )]
    public class OptionPageCustom : DialogPage
    {
        private string m_optionValue = "CodeBeautifier";
        private Base m_baseForm = null;
        private bool m_bActive = false;

        public string OptionString
        {
            get { return m_optionValue; }
            set { m_optionValue = value; }
        }

        protected override IWin32Window Window
        {
            get
            {
                Forms.AboutControl about = new Forms.AboutControl( AppDomain.CurrentDomain.GetAssemblies() );
                m_baseForm = new Base( about );
                m_baseForm.disableButtons = true;
                m_baseForm.Anchor = (System.Windows.Forms.AnchorStyles)( System.Windows.Forms.AnchorStyles.Bottom
                                                | System.Windows.Forms.AnchorStyles.Top
                                                | System.Windows.Forms.AnchorStyles.Left
                                                | System.Windows.Forms.AnchorStyles.Right );
                return m_baseForm;
            }
        }
        protected override void OnApply( DialogPage.PageApplyEventArgs e )
        {
            if( e.ApplyBehavior == ApplyKind.Apply )
            {
                m_baseForm.settings.save();
            }
            else
            {
                m_baseForm.settings.read();
                m_baseForm.refresh();
            }
        }
        protected override void OnClosed( EventArgs e )
        {
            m_bActive = false;
        }

        protected override void OnDeactivate( System.ComponentModel.CancelEventArgs e )
        { }

        protected override void OnActivate( System.ComponentModel.CancelEventArgs e )
        {
            if( m_bActive == false )
            {
                m_baseForm.settings.read();
                m_baseForm.refresh();
            }
            m_bActive = true;
        }
    }

    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration( UseManagedResourcesOnly = true )]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration( "#110", "#112", "1.0", IconResourceID = 400 )]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource( "Menus.ctmenu", 1 )]
    [Guid( GuidList.guidCodeBeautifierPkgString )]
    [ProvideAutoLoad( UIContextGuids.SolutionExists )]
    [ProvideAutoLoad( UIContextGuids.NoSolution )]
    [ProvideOptionPage( typeof( OptionPageCustom ), "Manobit", "CodeBeautifier", 0, 0, true )]
    public sealed class CodeBeautifierPackage : Package
    {
        private List<CommandEvents> m_commandEvents = new List<CommandEvents>(); // Command need to be saved
        private SettingsContainer m_settings = new SettingsContainer();
        private DTEEvents m_dteEvents = null;
        private EnvDTE80.TextDocumentKeyPressEvents m_textDocumentEvents = null;
        private EnvDTE.DocumentEvents m_documentEvents = null;
        private bool m_savePending = false;
        private Logger m_logger;

        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require
        /// any Visual Studio service because at this point the package object is created but
        /// not sited yet inside Visual Studio environment. The place to do all the other
        /// initialization is the Initialize method.
        /// </summary>
        public CodeBeautifierPackage() { }

        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler( MyHandler );

            m_settings.onOptionsChanged += new EventHandler( onConfigurationChanged );

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = GetService( typeof( IMenuCommandService ) ) as OleMenuCommandService;
            if( null != mcs )
            {
                // Register new command
                CommandID cmdOptions = new CommandID( GuidList.guidCodeBeautifierCmdSet, (int)PkgCmdIDList.cmdOptions );
                CommandID cmdAbout = new CommandID( GuidList.guidCodeBeautifierCmdSet, (int)PkgCmdIDList.cmdAbout );
                CommandID cmdCurrentDocument = new CommandID( GuidList.guidCodeBeautifierCmdSet, (int)PkgCmdIDList.cmdCurrentDocument );
                CommandID cmdAllOpenDocuments = new CommandID( GuidList.guidCodeBeautifierCmdSet, (int)PkgCmdIDList.cmdAllOpenDocuments );
                CommandID cmdSelectedProject = new CommandID( GuidList.guidCodeBeautifierCmdSet, (int)PkgCmdIDList.cmdSelectedProject );
                CommandID cmdSelectedSolution = new CommandID( GuidList.guidCodeBeautifierCmdSet, (int)PkgCmdIDList.cmdSelectedSolution );

                MenuCommand menuOptions = new MenuCommand( menuOptionsCallback, cmdOptions );
                MenuCommand menuAbout = new MenuCommand( menuAboutCallback, cmdAbout );
                OleMenuCommand menuCurrentDocument = new OleMenuCommand( callbackCurrentCallback, cmdCurrentDocument );
                OleMenuCommand menuAllOpenDocuments = new OleMenuCommand( callbackAllOpenDocuments, cmdAllOpenDocuments );
                OleMenuCommand menuSelectedProject = new OleMenuCommand( callbackSelectedProject, cmdSelectedProject );
                OleMenuCommand menuSolution = new OleMenuCommand( callbackSolution, cmdSelectedSolution );

                menuCurrentDocument.BeforeQueryStatus += new EventHandler( onBeforeQueryStatusCurrentDocument );
                menuAllOpenDocuments.BeforeQueryStatus += new EventHandler( onBeforeQueryStatusAllOpenDocuments );
                menuSelectedProject.BeforeQueryStatus += new EventHandler( onBeforeQueryStatusSelectedProject );
                menuSolution.BeforeQueryStatus += new EventHandler( onBeforeQueryStatusSolution );

                mcs.AddCommand( menuOptions );
                mcs.AddCommand( menuAbout );
                mcs.AddCommand( menuCurrentDocument );
                mcs.AddCommand( menuAllOpenDocuments );
                mcs.AddCommand( menuSelectedProject );
                mcs.AddCommand( menuSolution );

                beforeExecute( "{5EFC7975-14BC-11CF-9B2B-00AA00573819}", 882 ).BeforeExecute += onBuildSolution;
                beforeExecute( "{5EFC7975-14BC-11CF-9B2B-00AA00573819}", 883 ).BeforeExecute += onReBuildSolution;
                beforeExecute( "{5EFC7975-14BC-11CF-9B2B-00AA00573819}", 886 ).BeforeExecute += onBuildSelected;
                beforeExecute( "{5EFC7975-14BC-11CF-9B2B-00AA00573819}", 887 ).BeforeExecute += onReBuildSelected;
                beforeExecute( "{1496A755-94DE-11D0-8C3F-00C04FC2AAE2}", 1603 ).BeforeExecute += onBuildSelected;
                beforeExecute( "{1496A755-94DE-11D0-8C3F-00C04FC2AAE2}", 1604 ).BeforeExecute += onReBuildSelected;

                EnvDTE80.DTE2 dte2 = GetService( typeof( EnvDTE.DTE ) ) as EnvDTE80.DTE2;
                m_dteEvents = dte2.Events.DTEEvents;
                m_dteEvents.OnStartupComplete += onStartUpComplete;

                EnvDTE80.Events2 dteEvents2 = dte2.Events as EnvDTE80.Events2;

                m_documentEvents = dteEvents2.get_DocumentEvents();
                m_documentEvents.DocumentSaved += onDocumentSaved;

                m_textDocumentEvents = dteEvents2.get_TextDocumentKeyPressEvents();
                m_textDocumentEvents.AfterKeyPress += onTextDocumentKeyPressed;
            }
        }

        #endregion

        private void onDocumentSaved (Document document )
        {
            if (m_settings.options.general.onDocumentSave && document != null && m_savePending == false )
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
                if ( m_settings.options.general.handleNoneProjectFile == false 
                    && Selection.Parent.Parent.ProjectItem.Document == null )
                {
                    return;
                }

                DocumentsMakerSingle documentsMaker = new DocumentsMakerSingle( this, m_settings.options );
                documentsMaker.make( new DocumentsMakerSingle.KeyPressedData( Selection, Keypress ) );
            }
        }

        private CommandEvents beforeExecute( String guid, int cmdID )
        {
            EnvDTE80.DTE2 dte2 = GetService( typeof( EnvDTE.DTE ) ) as EnvDTE80.DTE2;

            var commandEvent = dte2.Events.CommandEvents[ guid, cmdID ];
            m_commandEvents.Add( commandEvent );
            return commandEvent;
        }

        private void onStartUpComplete()
        {
            onConfigurationChanged( null, null );
        }

        private void onBuildSolution( string guid, int id, object customIn, object customOut, ref bool cancelDefault )
        {
            if( !m_settings.options.general.onBuildSolution )
            {
                return;
            }

            m_logger.Log( "onBuildSolution: " + guid + " - " + id.ToString() );

            makeAllOpened( true );
        }

        private void onReBuildSolution( string guid, int id, object customIn, object customOut, ref bool cancelDefault )
        {
            if( !m_settings.options.general.onRebuildSolution )
            {
                return;
            }

            m_logger.Log( "onReBuildSolution: " + guid + " - " + id.ToString() );

            makeAllOpened( true );
        }

        private void onBuildSelected( string guid, int id, object customIn, object customOut, ref bool cancelDefault )
        {
            if( !m_settings.options.general.onBuildActiveProject )
            {
                return;
            }

            m_logger.Log( "onBuildSelected: " + guid + " - " + id.ToString() );

            makeAllOpened( true );
        }

        private void onReBuildSelected( string guid, int id, object customIn, object customOut, ref bool cancelDefault )
        {
            if( !m_settings.options.general.onRebuildActiveProject )
            {
                return;
            }

            m_logger.Log( "onReBuildSelected: " + guid + " - " + id.ToString() );

            makeAllOpened( true );
        }

        private void menuOptionsCallback( object sender, EventArgs e )
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void menuAboutCallback( object sender, EventArgs e )
        {
            About options = new About( AppDomain.CurrentDomain.GetAssemblies() );

            options.ShowDialog();
        }

        private void callbackCurrentCallback( object sender, EventArgs e )
        {
            if( statusCurrentDocument() )
            {
                EnvDTE80.DTE2 dte2 = GetService( typeof( EnvDTE.DTE ) ) as EnvDTE80.DTE2;

                makeDocument( dte2.ActiveDocument );
            }
        }

        private void onBeforeQueryStatusCurrentDocument( object sender, EventArgs e )
        {
            var myCommand = sender as OleMenuCommand;
            myCommand.Enabled = statusCurrentDocument();
        }

        private bool statusCurrentDocument()
        {
            EnvDTE.DTE dte = GetService( typeof( EnvDTE.DTE ) ) as EnvDTE.DTE;

            return dte.ActiveDocument != null;
        }

        private void callbackAllOpenDocuments( object sender, EventArgs e )
        {
            makeAllOpened( false );
        }

        private void onBeforeQueryStatusAllOpenDocuments( object sender, EventArgs e )
        {
            var myCommand = sender as OleMenuCommand;

            EnvDTE.DTE dte = GetService( typeof( EnvDTE.DTE ) ) as EnvDTE.DTE;

            myCommand.Enabled = false;
            foreach( EnvDTE.Window window in dte.Windows )
            {
                myCommand.Enabled |= window.Type == EnvDTE.vsWindowType.vsWindowTypeDocument;
            }
        }

        private void callbackSelectedProject( object sender, EventArgs e )
        {
            DocumentFinderGUI finderGui = new DocumentFinderGUI( this, m_settings.options );

            List<Object> documents = finderGui.activeProject();

            DocumentsMakerGui documentsMaker = new DocumentsMakerGui( this, m_settings.options );
            documentsMaker.make( documents );
        }

        private void onBeforeQueryStatusSelectedProject( object sender, EventArgs e )
        {
            var myCommand = sender as OleMenuCommand;

            EnvDTE.DTE dte = GetService( typeof( EnvDTE.DTE ) ) as EnvDTE.DTE;

            try
            {
                System.Array activeProjects = dte.ActiveSolutionProjects as System.Array;
                myCommand.Enabled = activeProjects.Length > 0;
            }
            catch( Exception )
            {
                myCommand.Enabled = false;
            }
        }

        private void callbackSolution( object sender, EventArgs e )
        {
            EnvDTE.DTE dte = GetService( typeof( EnvDTE.DTE ) ) as EnvDTE.DTE;

            DocumentFinderGUI finderGui = new DocumentFinderGUI( this, m_settings.options );

            List<Object> documents = finderGui.solution();

            DocumentsMakerGui documentsMaker = new DocumentsMakerGui( this, m_settings.options );
            documentsMaker.make( documents );
        }

        private void onBeforeQueryStatusSolution( object sender, EventArgs e )
        {
            var myCommand = sender as OleMenuCommand;

            EnvDTE.DTE dte = GetService( typeof( EnvDTE.DTE ) ) as EnvDTE.DTE;

            try
            {
                myCommand.Enabled = dte.Solution.Projects.Count > 0;
            }
            catch( Exception )
            {
                myCommand.Enabled = false;
            }
        }

        private void makeAllOpened( bool noSavedOnly )
        {
            DocumentFinderGUI finderGui = new DocumentFinderGUI( this, m_settings.options );

            List<Object> documents = finderGui.opened( noSavedOnly );

            DocumentsMakerGui documentsMaker = new DocumentsMakerGui( this, m_settings.options );
            documentsMaker.make( documents );
        }

        private void makeCurrent()
        {
            EnvDTE80.DTE2 dte2 = GetService( typeof( EnvDTE.DTE ) ) as EnvDTE80.DTE2;

            switch( dte2.ActiveWindow.Type )
            {
                case vsWindowType.vsWindowTypeDocument:
                    {
                        Document activeDocument = dte2.ActiveDocument as Document;
                        if( !activeDocument.Saved )
                        {
                            makeDocument( activeDocument );
                        }
                    }
                    break;
            } // switch
        }

        private void makeDocument( Document document )
        {
            List<Object> objects = new List<Object>();

            if (document.ProjectItem.Document != null)
                objects.Add( document.ProjectItem.Document );
            else if (m_settings.options.general.handleNoneProjectFile)
                objects.Add( document );

            if ( objects.Count > 0 )
            {
                IDocumentsMaker docMaker = new DocumentsMakerSingle(this, m_settings.options);
                docMaker.make(objects);
            }
        }

        private void onConfigurationChanged( Object sender, EventArgs e )
        {
            m_settings.read();
            m_logger = new Logger( this, m_settings.options );
        }
        private void MyHandler( object sender, UnhandledExceptionEventArgs args )
        {
            if( m_settings.options.general.createDumpFileOnError
                && String.IsNullOrEmpty( m_settings.options.general.dumpFilepath ) == false )
            {
                System.Diagnostics.MiniDump.CreateDump( m_settings.options.general.dumpFilepath );
            }
        }
    }
}

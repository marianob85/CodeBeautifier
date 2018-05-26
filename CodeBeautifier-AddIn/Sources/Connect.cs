using System;
using Extensibility;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using System.Resources;
using System.Reflection;
using System.Globalization;
using CodeBeautifier.Sources;
using Settings.Forms;
using Settings.Sources;
using Manobit.CodeBeautifier.Forms;
using System.Diagnostics;
using Manobit.CodeBeautifier.Sources;

namespace CodeBeautifier
{
    public class OptionPage : Base, EnvDTE.IDTToolsOptionsPage
    {
        public OptionPage() : base()
        {
            disableButtons = true;
            this.Size = new System.Drawing.Size( 470, 324 );
            Anchor = (System.Windows.Forms.AnchorStyles)( System.Windows.Forms.AnchorStyles.Bottom
                                            | System.Windows.Forms.AnchorStyles.Top
                                            | System.Windows.Forms.AnchorStyles.Left
                                            | System.Windows.Forms.AnchorStyles.Right );
        }

	    //------------------ start - IDTToolsOptionsPage --------------------------
        public void GetProperties( ref object PropertiesObject )
        {}
        public void OnAfterCreated(  DTE DTEObject )
        {
            settings.read();
            refresh();
        }
        public void OnCancel()
        {
            settings.read();
            refresh();
        }
        public void OnHelp()
        {}
        public void OnOK()
        {
            settings.save();
        }
	    //-------------------- end - IDTToolsOptionsPage --------------------------
    };

    /// <summary>The object for implementing an Add-in.</summary>
    /// <seealso class='IDTExtensibility2' />
    public class Connect : IDTExtensibility2, IDTCommandTarget, IServiceProvider
    {
        private DTE2 _applicationObject;
        private AddIn _addInInstance;
        private CodeBeautifierCommands[] m_hCodeBeautifierObjects;
        private CodeBeautifierEventHandler m_codeBeautifierEventHandler;
        private SettingsContainer m_settings = new SettingsContainer();
        private static readonly String s_strVS_MenuBar = "MenuBar";
        private static readonly String s_strVS_Tools = "Tools";
        private static readonly String s_strAPP_CodeBeautifier_Caption = "&CodeBeautifier";
        private static readonly String s_strCMD_CodeBeautifier_Current_Name = "CurrentDocument";
        private static readonly String s_strCMD_CodeBeautifier_Current_Caption = "&Current Document";
        private static readonly String s_strCMD_CodeBeautifier_AllOpenDoc_Name = "AllOpenDocument";
        private static readonly String s_strCMD_CodeBeautifier_AllOpenDoc_Caption = "&All Open Document";
        private static readonly String s_strCMD_CodeBeautifier_ActiveProject_Name = "SelectedProject";
        private static readonly String s_strCMD_CodeBeautifier_ActiveProject_Caption = "Selected &Project";
        private static readonly String s_strCMD_CodeBeautifier_Solution_Name = "Solution";
        private static readonly String s_strCMD_CodeBeautifier_Solution_Caption = "S&olution";
        private static readonly String s_strCMD_CodeBeautifier_About_Name = "About";
        private static readonly String s_strCMD_CodeBeautifier_About_Caption = "A&bout";
        private static readonly String s_strCMD_CodeBeautifier_Options_Name = "Options";
        private static readonly String s_strCMD_CodeBeautifier_Options_Caption = "&Options";


        public Object GetService( Type serviceType )
        {
            if( serviceType == typeof( EnvDTE.DTE ) )
            {
                return _applicationObject;
            }
            if( serviceType == typeof( AddIn ) )
            {
                return _addInInstance;
            }

            return null;
        }

        /// <summary>Implements the constructor for the Add-in object. Place your initialization code within this method.</summary>
        public Connect()
        {
        }

        /// <summary>Implements the OnConnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being loaded.</summary>
        /// <param term='application'>Root object of the host application.</param>
        /// <param term='connectMode'>Describes how the Add-in is being loaded.</param>
        /// <param term='addInInst'>Object representing this Add-in.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnConnection( object application, ext_ConnectMode connectMode, object addInInst, ref Array custom )
        {
            _applicationObject = (DTE2)application;
            _addInInstance = (AddIn)addInInst;

            m_settings.onOptionsChanged += new EventHandler( onConfigurationChanged );
            onConfigurationChanged( this, null );

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler( MyHandler );

            if( connectMode == ext_ConnectMode.ext_cm_AfterStartup )
            {
                CreateUI();
            }
        }
        private void MyHandler( object sender, UnhandledExceptionEventArgs args )
        {
            if ( m_settings.options.general.createDumpFileOnError
                && String.IsNullOrEmpty( m_settings.options.general.dumpFilepath) == false )
            {
                System.Diagnostics.MiniDump.CreateDump( m_settings.options.general.dumpFilepath );
            }
        }

        private void onConfigurationChanged( Object sender, EventArgs e )
        {
            m_settings.read();
        }

        /// <summary>Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being unloaded.</summary>
        /// <param term='disconnectMode'>Describes how the Add-in is being unloaded.</param>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnDisconnection( ext_DisconnectMode disconnectMode, ref Array custom )
        {
        }

        /// <summary>Implements the OnAddInsUpdate method of the IDTExtensibility2 interface. Receives notification when the collection of Add-ins has changed.</summary>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />		
        public void OnAddInsUpdate( ref Array custom )
        {
        }

        /// <summary>Implements the OnStartupComplete method of the IDTExtensibility2 interface. Receives notification that the host application has completed loading.</summary>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnStartupComplete( ref Array custom )
        {
            CreateUI();
        }

        /// <summary>Implements the OnBeginShutdown method of the IDTExtensibility2 interface. Receives notification that the host application is being unloaded.</summary>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnBeginShutdown( ref Array custom )
        {
        }

        /// <summary>Implements the QueryStatus method of the IDTCommandTarget interface. This is called when the command's availability is updated</summary>
        /// <param term='commandName'>The name of the command to determine state for.</param>
        /// <param term='neededText'>Text that is needed for the command.</param>
        /// <param term='status'>The state of the command in the user interface.</param>
        /// <param term='commandText'>Text requested by the neededText parameter.</param>
        /// <seealso class='Exec' />
        public void QueryStatus( string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText )
        {
            if( neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone )
            {
                if( commandName.CompareTo( "CodeBeautifier.Connect.About" ) == 0 )
                {
                    status = (vsCommandStatus)( vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled );
                    return;
                }

                if( commandName.CompareTo( "CodeBeautifier.Connect.Options" ) == 0 )
                {
                    status = (vsCommandStatus)( vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled );
                    return;
                }


                if( m_hCodeBeautifierObjects != null )
                {
                    foreach( CodeBeautifierCommands hCodeBeautifierCmd in m_hCodeBeautifierObjects )
                    {
                        try
                        {
                            if( hCodeBeautifierCmd.QueryStatus( commandName, ref status ) )
                            {
                                return;
                            }
                        }
                        catch( Exception )
                        { }
                    }
                }
                else
                {
                    status = (vsCommandStatus)( vsCommandStatus.vsCommandStatusUnsupported );
                }
            }
        }

        /// <summary>Implements the Exec method of the IDTCommandTarget interface. This is called when the command is invoked.</summary>
        /// <param term='commandName'>The name of the command to execute.</param>
        /// <param term='executeOption'>Describes how the command should be run.</param>
        /// <param term='varIn'>Parameters passed from the caller to the command handler.</param>
        /// <param term='varOut'>Parameters passed from the command handler to the caller.</param>
        /// <param term='handled'>Informs the caller if the command was handled or not.</param>
        /// <seealso class='Exec' />
        public void Exec( string commandName, vsCommandExecOption executeOption, ref object varIn, ref object varOut, ref bool handled )
        {
            handled = false;
            if( executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault )
            {
                foreach( CodeBeautifierCommands hCodeBeautifierCmd in m_hCodeBeautifierObjects )
                {
                    try
                    {
                        if( hCodeBeautifierCmd.Exec( commandName ) )
                        {
                            handled = true;
                            return;
                        }
                    }
                    catch( Exception )
                    { }
                }
            }
        }

        private void CreateObjects()
        {
            m_hCodeBeautifierObjects = new CodeBeautifierCommands[] { 
                    new CodeBeautifierActiveDocument(   this,
													    s_strCMD_CodeBeautifier_Current_Name,
													    s_strCMD_CodeBeautifier_Current_Caption,
													    null,
													    230 ),

	                new CodeBeautifierAllOpenedDocuments(   this,
														    s_strCMD_CodeBeautifier_AllOpenDoc_Name,
														    s_strCMD_CodeBeautifier_AllOpenDoc_Caption,
														    null,
														    2045 ),
	                new CodeBeautifierActiveProject(    this,
														s_strCMD_CodeBeautifier_ActiveProject_Name,
														s_strCMD_CodeBeautifier_ActiveProject_Caption,
														null,
														1773 ),
                    new CodeBeautifierSolution(    this,
														s_strCMD_CodeBeautifier_Solution_Name,
														s_strCMD_CodeBeautifier_Solution_Caption,
														null,
														1773 ),
            	    new CodeBeautifierOptions(    this,
											s_strCMD_CodeBeautifier_Options_Name,
											s_strCMD_CodeBeautifier_Options_Caption,
											null,
											2773, 
                                            true ),
                    new CodeBeautifierAbout(    this,
								s_strCMD_CodeBeautifier_About_Name,
								s_strCMD_CodeBeautifier_About_Caption,
								null,
								-1, 
                                false )
            };

            m_codeBeautifierEventHandler = new CodeBeautifierEventHandler( this );
        }

        private void CreateUI()
        {
            CreateObjects();

            String strPath = "&Manobit" + "\\" + s_strAPP_CodeBeautifier_Caption;

            CommandBars commandBars = _applicationObject.CommandBars as CommandBars;
            CommandBar commandBarMenu = commandBars[ (String)s_strVS_MenuBar ];
            CommandBar commandBarTools = commandBars[ (String)s_strVS_Tools ];

            int iPos = ( commandBarTools.Parent as CommandBarControl ).Index + 1;

            foreach( CodeBeautifierCommands hCodeBeautifierCmd in m_hCodeBeautifierObjects )
            {
                hCodeBeautifierCmd.BuildUI( commandBarMenu, strPath, iPos );
                ++iPos;
            }
        }
    }
}
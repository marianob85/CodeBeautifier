using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio;
using Settings.Sources;
using System.Diagnostics;

namespace Manobit.CodeBeautifier.Sources
{
    public class Logger
    {
        private String m_windowPaneName = "CodeBeautiful";
        private Options m_settings;
        private EnvDTE.OutputWindowPane m_outputWindowPane;

        public Logger( IServiceProvider serviceProvider, Options settings )
        {
            m_settings = settings;
            EnvDTE80.DTE2 dte2 = serviceProvider.GetService( typeof( EnvDTE.DTE ) ) as EnvDTE80.DTE2;

            try
            {
                m_outputWindowPane = dte2.ToolWindows.OutputWindow.OutputWindowPanes.Item( m_windowPaneName );
            }
            catch
            {
                m_outputWindowPane = dte2.ToolWindows.OutputWindow.OutputWindowPanes.Add( m_windowPaneName );
            }
        }

        public void Log( String message, OptionsGeneral.LoggerPriority priority = OptionsGeneral.LoggerPriority.Low )
        {
            if( priority <= m_settings.general.logType )
            {
                m_outputWindowPane.OutputString( message + Environment.NewLine );
            }
        }

        public void Log( System.Exception e )
        {
            Log( e.Message, OptionsGeneral.LoggerPriority.High );
            Log( e.StackTrace, OptionsGeneral.LoggerPriority.High );

            if( m_settings.general.createDumpFileOnError
                && String.IsNullOrEmpty( m_settings.general.dumpFilepath ) == false
                && m_settings.general.logType == OptionsGeneral.LoggerPriority.HighWithDump )
            {
                System.Diagnostics.MiniDump.CreateDump( m_settings.general.dumpFilepath);
            }
        }
    }
}

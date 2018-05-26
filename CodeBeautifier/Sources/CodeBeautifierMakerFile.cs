using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using Settings.Sources;
using EnvDTE;

namespace Manobit.CodeBeautifier.Sources
{
    public class MakerUsingFile : Maker
    {
        private ApplicationModeFile m_appModeOptions;

        public MakerUsingFile( IServiceProvider serviceProvider, Options optons, AppOptions appOptions )
            : base( serviceProvider, optons, appOptions )
        {
            this.m_appModeOptions = appOptions.appModeFile;
        }

        protected override bool makeFile( String fileName, String orgFileName )
        {
            System.Diagnostics.Process processCodeBeautifier = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfoCodeBeautifier = new System.Diagnostics.ProcessStartInfo();

            String hCodeBeautifierFullPath = m_appModeOptions.executablePath;

            String hCodeBeautifierPath = Path.GetDirectoryName( hCodeBeautifierFullPath );
            String hCodeBeautifierFileName = Path.GetFileName( hCodeBeautifierFullPath );

            m_logger.Log( String.Format( "Maker: {0}: Source file: {1}", m_appOptions.name, fileName, OptionsGeneral.LoggerPriority.High ) );
            if( !File.Exists( hCodeBeautifierFullPath ) )
            {
                m_logger.Log( String.Format( "Maker: {0}: {1} not found!", m_appOptions.name, hCodeBeautifierFullPath ) );
                return false;
            }

            String parameters = m_appModeOptions.parameters.Replace( "$(FileName)", fileName );
            if( parameters.Contains( "$(ConfigFile)" ) )
                parameters = parameters.Replace( "$(ConfigFile)", configFile( orgFileName ) );

            startInfoCodeBeautifier.Arguments = parameters;
            startInfoCodeBeautifier.WorkingDirectory = hCodeBeautifierPath;
            startInfoCodeBeautifier.FileName = hCodeBeautifierFileName;
            startInfoCodeBeautifier.UseShellExecute = true;
            startInfoCodeBeautifier.CreateNoWindow = true;
            startInfoCodeBeautifier.RedirectStandardOutput = false;
            startInfoCodeBeautifier.WindowStyle = ProcessWindowStyle.Hidden;

            processCodeBeautifier.StartInfo = startInfoCodeBeautifier;

            m_logger.Log( String.Format( "{0}: Starting process: FileName: {1}, Arguments: {2}, WorkingDirectory: {3}",
                m_appOptions.name,
                processCodeBeautifier.StartInfo.FileName,
                processCodeBeautifier.StartInfo.Arguments,
                processCodeBeautifier.StartInfo.WorkingDirectory, OptionsGeneral.LoggerPriority.High ) );

            try
            {
                processCodeBeautifier.Start();
                processCodeBeautifier.WaitForExit();
            }
            catch( System.ComponentModel.Win32Exception e )
            {
                m_logger.Log( String.Format( "Maker: {0}: CodeBeautifier execute error!", m_appOptions.name ) );
                m_logger.Log( e );
                return false;
            }
            catch( Exception e )
            {
                m_logger.Log( String.Format( "Maker: {0}: CodeBeautifier execute error!", m_appOptions.name ) );
                m_logger.Log( e );
                return false;
            }
            return processCodeBeautifier.ExitCode == 0 ? true : false;
        }

        protected override System.String makeText( System.String text, System.String filePath )
        {
            String strTempName;
            StreamWriter streamTempFile;
            StreamReader streamTempReader;

            do
            {
                strTempName = Path.GetTempFileName();
            }
            while( !File.Exists( strTempName ) );

            streamTempFile = new StreamWriter( strTempName, false );
            streamTempFile.Write( text );
            streamTempFile.Close();

            String NewText = null;

            // If returned 0 then success
            if( makeFile( strTempName, filePath ) )
            {
                streamTempReader = new StreamReader( strTempName );
                NewText = streamTempReader.ReadToEnd();
                streamTempReader.Close();
            }

            // Delete temp file
            System.IO.File.Delete( strTempName );

            return NewText;
        }
    }
}
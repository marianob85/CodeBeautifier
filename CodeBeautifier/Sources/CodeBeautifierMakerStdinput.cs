using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using Settings.Sources;
using EnvDTE;

namespace Manobit.CodeBeautifier.Sources
{
    public class MakerUsingStdInput : Maker
    {
        private ApplicationModeStdInput m_appModeOptions;

        public MakerUsingStdInput( IServiceProvider serviceProvider, Options optons, AppOptions appOptions )
            : base( serviceProvider, optons, appOptions )
        {
            this.m_appModeOptions = appOptions.appModeStdInput;
        }

        protected override bool makeFile( String fileName, String orgFilename )
        {
            m_logger.Log( String.Format( "Maker: {0}: Source file: {1}", m_appOptions.name, fileName, OptionsGeneral.LoggerPriority.High ) );
            StreamReader reader = new StreamReader( fileName );
            String parsedText = makeText( reader.ReadToEnd(), fileName );
            reader.Close();
            StreamWriter writer = new StreamWriter( fileName, false );
            writer.Write( parsedText );
            writer.Close();
            return true;
        }

        protected override System.String makeText( System.String text, System.String filePath )
        {
            String parameters = m_appModeOptions.parametersWithEnv.Replace( "$(FileName)", filePath );
            if( parameters.Contains("$(ConfigFile)") )
                parameters = parameters.Replace( "$(ConfigFile)", configFile( filePath ) );

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = m_appModeOptions.executablePath;
            // Poor man's escaping - this will not work when quotes are already escaped
            // in the input (but we don't need more).
            process.StartInfo.Arguments = parameters;
            var path = Directory.GetParent( filePath ).ToString();
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.WorkingDirectory = path;

            if( m_appModeOptions.useEncoding )
            {
                using( var reader = new StreamReader( filePath ) )
                {
                    var f1 = reader.ReadToEnd().ToLower();
                    process.StartInfo.StandardOutputEncoding = reader.CurrentEncoding;
                }
                if( process.StartInfo.StandardOutputEncoding == null )
                {
                    m_logger.Log( String.Format( "Maker: {0}: Unknown source file encoding", m_appOptions.name ), OptionsGeneral.LoggerPriority.Medium );
                }
                else
                {
                    m_logger.Log( String.Format( "Maker: {0}: Using source file encoding: {1}", m_appOptions.name, process.StartInfo.StandardOutputEncoding.EncodingName ), OptionsGeneral.LoggerPriority.Medium );
                }
            }

            m_logger.Log( String.Format( "Maker: {0}: Starting process: {1}\n           Arguments: {2}\n           WorkingDirectory: {3}",
                            m_appOptions.name,
                            process.StartInfo.FileName,
                            process.StartInfo.Arguments,
                            process.StartInfo.WorkingDirectory ), OptionsGeneral.LoggerPriority.High );
            try
            {
                process.Start();
            }
            catch( Exception e )
            {
                m_logger.Log( String.Format( "Maker: {0}: Cannot execute {1}", m_appOptions.name, process.StartInfo.FileName ) );
                m_logger.Log( e );
                return null;
            }
            if( process.StartInfo.StandardOutputEncoding != null )
            {
                var bytes = process.StartInfo.StandardOutputEncoding.GetBytes( text );
                process.StandardInput.BaseStream.Write( bytes, 0, bytes.Length );
            }
            else
            {
                process.StandardInput.Write( text );
            }
            // 3. We notify clang-format that the input is done - after this point clang-format
            //    will start analyzing the input and eventually write the output.
            process.StandardInput.Close();
            // 4. We must read clang-format's output before waiting for it to exit; clang-format
            //    will close the channel by exiting.
            string output = process.StandardOutput.ReadToEnd();
            // 5. clang-format is done, wait until it is fully shut down.
            process.WaitForExit();
            if( process.ExitCode != 0 )
            {
                // FIXME: If clang-format writes enough to the standard error stream to block,
                // we will never reach this point; instead, read the standard error asynchronously.
                //throw new Exception( process.StandardError.ReadToEnd() );
                m_logger.Log( String.Format( "{0}: Cannot execute {1}", m_appOptions.name, process.StartInfo.FileName ) );
                m_logger.Log( process.StandardError.ReadToEnd() );
                return null;
            }

            if( shouldApplyEOL() )
            {
                output = EOLConverter.convert( output, applayEOLType() );
            }

            return output;
        }
    }
}
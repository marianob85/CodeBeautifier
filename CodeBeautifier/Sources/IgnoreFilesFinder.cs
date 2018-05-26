using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Settings.Sources;

namespace Manobit.CodeBeautifier.Sources
{
    class IgnoreFilesFinder
    {
        protected Logger m_logger = null;
        protected AppOptions m_appOptions;
        private string m_ingoreFileName = null;
        private string m_ignoreEntry = null;
        private readonly string m_cbIgnore = ".cbignore";

        public IgnoreFilesFinder( IServiceProvider serviceProvider, Options options,AppOptions appOptions, string fileNamePath )
        {
            m_appOptions = appOptions;
            this.m_logger = new Logger( serviceProvider, options );

            m_logger.Log( String.Format( "Ignore file: Searching for file: {0}", m_cbIgnore ), OptionsGeneral.LoggerPriority.High );

            // Create collection of files
            var dirInfo = System.IO.Directory.GetParent( fileNamePath) ;
            do
            {
                var cbignore = Path.Combine( dirInfo.FullName, m_cbIgnore );
                if( File.Exists( cbignore ) )
                {
                    m_ingoreFileName = cbignore;
                    break;
                }

                dirInfo = dirInfo.Parent;

            } while( dirInfo != null );

            if( m_ingoreFileName != null )
            {
                var parentIgnoreFileName = System.IO.Directory.GetParent( m_ingoreFileName );

                foreach( var itIgnoreLine in File.ReadAllLines( m_ingoreFileName ) )
                {
                    var fileName = System.IO.Path.GetFileName( fileNamePath );
                    string ignoreLine;
                    if( itIgnoreLine.StartsWith("/") )
                    {
                        ignoreLine = parentIgnoreFileName.FullName.Replace( '\\', '/' ) + itIgnoreLine;
                    }
                    else
                    {
                        ignoreLine = parentIgnoreFileName.FullName.Replace( '\\', '/' ) + "/" + itIgnoreLine;
                    }
                    fileName = fileNamePath.Replace( '\\', '/' );

                    m_logger.Log( String.Format( "Ignore file: {0}: preprocess - entry:     {1}", m_appOptions.name, ignoreLine ), OptionsGeneral.LoggerPriority.High );
                    m_logger.Log( String.Format( "Ignore file: {0}: preprocess - file name: {1}", m_appOptions.name, fileName ), OptionsGeneral.LoggerPriority.High );

                    System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex( ignoreLine );
                    
                    if( re.Match( fileName ).Success )
                    {
                        m_ignoreEntry = itIgnoreLine;
                        return;
                    }
                }
            }
        }
         
        public bool matched()
        {
            return m_ignoreEntry != null
                && m_ingoreFileName != null;
        }

        public string matchedEntry()
        {
            return m_ignoreEntry;
        }

        public string matchedFile()
        {
            return m_ingoreFileName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Settings.Sources;

namespace Manobit.CodeBeautifier.Sources
{
    public abstract partial class Maker
    {
        public bool validate( System.String fileName )
        {
            if( !m_appOptions.enable )
            {
                m_logger.Log( String.Format( "{0}: Disabled", m_appOptions.name ), OptionsGeneral.LoggerPriority.Medium );
                return false;
            }
            else
            {
                m_logger.Log( String.Format( "{0}: Enabled", m_appOptions.name ), OptionsGeneral.LoggerPriority.Medium );
            }

            // Check extension
            String ext = System.IO.Path.GetExtension( fileName ).TrimStart( '.' );
            if( !m_appOptions.languageIdentExt.Contains( ext ) )
            {
                m_logger.Log( String.Format( "{0}: File extension {1} not registered", m_appOptions.name, ext ) );
                return false;
            }

            m_logger.Log( String.Format( "{0}: Searching through global exclude files", m_appOptions.name, ext ), OptionsGeneral.LoggerPriority.Medium );
            foreach( var excludeFile in m_appOptions.excludeFiles )
            {
                try
                {
                    System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex( excludeFile );

                    if( re.Match( fileName ).Success )
                    {
                        m_logger.Log( String.Format( "{0}: File name {1} using regular extension excluded from maker", m_appOptions.name, fileName ), OptionsGeneral.LoggerPriority.Medium );
                        return false;
                    }
                }
                catch( Exception e )
                {
                    m_logger.Log( String.Format( "{0}: File name {1} using regular extension excluded from maker: internal error", m_appOptions.name, fileName ), OptionsGeneral.LoggerPriority.Medium );
                    m_logger.Log( e );
                    return false;
                }
            }

            if( m_appOptions.useIgnoreFiles )
            {
                m_logger.Log( String.Format( "{0}: Searching through ignore files", m_appOptions.name, ext ), OptionsGeneral.LoggerPriority.Medium );
                IgnoreFilesFinder ignoreFileFinder = new IgnoreFilesFinder(m_dte2, m_options, m_appOptions, fileName);

                if( ignoreFileFinder.matchedFile() != null  )
                {
                    m_logger.Log( String.Format( "{0}: Found ignore file: {1}", m_appOptions.name, ignoreFileFinder.matchedFile() ), OptionsGeneral.LoggerPriority.High );
                }

                if( ignoreFileFinder.matched() )
                {
                    m_logger.Log( String.Format( "{0}: File name {1} excluded from maker by ignore file {2} and entry '{3}'",
                                                m_appOptions.name,
                                                fileName,
                                                ignoreFileFinder.matchedFile(),
                                                ignoreFileFinder.matchedEntry() ),
                                                OptionsGeneral.LoggerPriority.Medium );
                    return false;
                }
            }
            return true;
        }
    }
}

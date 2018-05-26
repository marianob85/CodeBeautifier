using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manobit.CodeBeautifier
{
    partial class Maker
    {
        public bool validate( System.String fileName )
        {
            if( !m_params.Enable )
            {
                //m_logger.Log( "File not make because of disable configuration", LoggerPriority.Medium );
                return false;
            }
            // Check extension
            if( m_params.LangIdentExtension.Contains( System.IO.Path.GetExtension( fileName ).TrimStart( '.' ) ) )
            {
                m_logger.Log( "File extension was not register", LoggerPriority.Medium );
                return false;
            }

            foreach( String excludeFile in m_params.FileExcludeList )
            {
                if( m_params.FileExcludeListRegEx )
                {
                    try
                    {
                        System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex( excludeFile );

                        if( re.Match( fileName ).Success )
                        {
                            m_logger.Log( "File not make because exclusion", LoggerPriority.Medium );
                            return false;
                        }
                    }
                    catch( Exception e )
                    {
                        m_logger.Log( "File not make because exclusion", LoggerPriority.Medium );
                        m_logger.Log( e );
                        return false;
                    }
                }
                else
                {
                    if( System.IO.Path.GetFileName( fileName ).CompareTo( excludeFile ) == 0 )
                    {
                        m_logger.Log( "File not make because exclusion", LoggerPriority.High );
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

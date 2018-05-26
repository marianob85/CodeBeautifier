using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SettingsControl;
using EnvDTE;

namespace Manobit.CodeBeautifier
{
    public partial class Maker
    {
        private SCodeBeautifulParam m_params;
        private Logger m_logger;

        public Maker( IServiceProvider serviceProvider, CFileContainer settings, SCodeBeautifulParam parameters )
        {
            this.m_logger = new Logger( serviceProvider, settings );
            this.m_params = parameters;
        }

        public bool make( EnvDTE.ProjectItem projectItem )
        {
            String  fullFilePath = projectItem.Properties.Item( "FullPath" ).Value as String;
            // ---------------------- start: Validate  ----------------------

            if( !validate( fullFilePath ) )
            {
                return false;
            }

            //// ------------------------ end: Validate  ----------------------
            if( projectItem.Document != null )
            {
                try
                {
                    return makeDocument( projectItem.Document.Object( "TextDocument" ) as TextDocument  );
                }
                catch( Exception e)
                {
                    m_logger.Log(e);
                    return false;
                }
            }
            else
            {
                return makeFile( fullFilePath );
            }
        }

        public bool make( System.String fileName )
        {
            if( !validate( fileName ) )
            {
                return false;
            }

            return makeFile( fileName );
        }

        private bool makeFile( String fileName )
        {
            return true;
        }

        bool makeDocument( TextDocument textDocument )
        {
            return true;
        }

    }
}

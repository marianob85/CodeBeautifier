using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using Settings.Sources;
using EnvDTE;

namespace Manobit.CodeBeautifier.Sources
{
    public abstract partial class Maker
    {
        public static Maker CreateInstance( IServiceProvider serviceProvider, Options options, AppOptions appOptions )
        {
            switch( appOptions.applicationMode )
            {
                case ApplicationMode.File:
                    return new MakerUsingFile( serviceProvider, options, appOptions );
                case ApplicationMode.StdInput:
                    return new MakerUsingStdInput( serviceProvider, options, appOptions );
                default:
                    return null;
            }
        }

        protected Logger m_logger;
        protected IServiceProvider m_serviceProvider;
        protected AppOptions m_appOptions;
        Options m_options;

        public Maker( IServiceProvider serviceProvider, Options options, AppOptions appOptions )
        {
            m_options = options;
            m_appOptions = appOptions;
            this.m_logger = new Logger( serviceProvider, options );
            this.m_serviceProvider = serviceProvider;
        }

        protected String configFile( String fileName )
        {

            m_logger.Log( String.Format( "ConfigFile: Searching for file: {0}", m_appOptions.applicationConfig.fileName ), OptionsGeneral.LoggerPriority.High );
            // Create collection of files
            var dirInfo = System.IO.Directory.GetParent( fileName );
            do
            {
                var configFile = Path.Combine( dirInfo.FullName, m_appOptions.applicationConfig.fileName );
                if( File.Exists( configFile ) )
                {
                    m_logger.Log( String.Format( "ConfigFile: Found: {0}", configFile ), OptionsGeneral.LoggerPriority.High );
                    return configFile;
                }
                    

                dirInfo = dirInfo.Parent;

            } while( dirInfo != null );

            m_logger.Log( String.Format( "ConfigFile: Not found starting from: {0}", System.IO.Directory.GetParent( fileName ) ), OptionsGeneral.LoggerPriority.High );
            return String.Empty;
        }

        public bool make( EnvDTE.ProjectItem projectItem )
        {
            String fullFilePath = projectItem.Properties.Item( "FullPath" ).Value as String;

            if( !validate( fullFilePath ) )
            {
                return false;
            }

            if( projectItem.Document !=null )
            {
                return makeDocument( projectItem.Document.Object( "TextDocument" ) as TextDocument );
            }
            else
            {
                return makeFile( fullFilePath, fullFilePath );
            }

        }
        public bool make( System.String fileName )
        {
            if( !validate( fileName ) )
            {
                return false;
            }

            return makeFile( fileName, fileName );
        }

        public bool make( Document document )
        {
            if( !validate( document.FullName ) )
            {
                return false;
            }
            return makeDocument( document.Object( "TextDocument" ) as TextDocument );
        }

        public bool make( DocumentsMakerSingle.KeyPressedData keyPressedData )
        {
            if( !validate( keyPressedData.textSelection.Parent.Parent.FullName ) )
            {
                return false;
            }

            if ( m_options.general.formatOptionsForC.isValid( keyPressedData.textSelection.Parent.Language, keyPressedData.key ) )
            {
                return make( keyPressedData, m_options.general.formatOptionsForC );
            }
            if( m_options.general.formatOptionsForCSharp.isValid( keyPressedData.textSelection.Parent.Language, keyPressedData.key ) )
            {
                return make( keyPressedData, m_options.general.formatOptionsForCSharp );
            }

            return true;
        }

        protected bool makeDocument( TextDocument textDocument )
        {
            try
            {
                m_logger.Log( String.Format( "Maker: {0}: Document language: {1}", m_appOptions.name, textDocument.Parent.Language ), OptionsGeneral.LoggerPriority.High );

                Document hDocument = textDocument.Parent as Document;
                m_logger.Log( String.Format( "Maker: {0}: Source file: {1}", m_appOptions.name, hDocument.FullName ) );
                String strOrgText = textDocument.StartPoint.CreateEditPoint().GetText(textDocument.EndPoint);
                String strNewText = makeText(strOrgText, hDocument.FullName);

                if (String.IsNullOrEmpty(strOrgText) || String.IsNullOrEmpty(strNewText))
                {
                    m_logger.Log( String.Format( "Maker: {0}: CodeBeautifier encounter error while parsing document.", m_appOptions.name ) );
                    return false;
                }

                if( strNewText.Equals (strOrgText) )
                {
                    m_logger.Log( String.Format( "Maker: {0}: No changes after document format.", m_appOptions.name ), OptionsGeneral.LoggerPriority.Medium );
                    return true;
                }

                ICodeBeautifierDocument documentMaker = null;
                
                if ( m_options.general.trackChanges)
                {
                    documentMaker = new CodeBeautifierDocumentWithtrackChanges(m_serviceProvider);
                }
                else
                {
                    documentMaker = new CodeBeautifierDocument(m_serviceProvider);
                }
                

                return documentMaker.make(textDocument, strOrgText, strNewText);
            }
            catch (Exception e)
            {
                m_logger.Log( String.Format( "Maker: {0}: CodeBeautifier encounter error while make document.", m_appOptions.name ) );
                m_logger.Log(e);

                return false;
            }
        }

        protected abstract bool makeFile( String fileName, String orgFileName );

        protected abstract System.String makeText( System.String text, System.String filePath );
    }
}

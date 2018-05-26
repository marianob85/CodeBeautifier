using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using Settings.Sources;
using EnvDTE;
using EnvDTE80;


namespace Manobit.CodeBeautifier.Sources
{
    public abstract partial class Maker
    {
        protected bool makeSemicolon( TextSelection textSelection )
        {
            EditPoint2 epend = textSelection.ActivePoint.CreateEditPoint() as EditPoint2;
            EditPoint2 epbegin = textSelection.ActivePoint.CreateEditPoint() as EditPoint2;

            epbegin.StartOfLine();

            String strOrgText = epbegin.GetText( epend );

            // Check for bracket pair
            int bOpen = 0;
            int bClose = 0;
            foreach( char c in strOrgText.Trim() )
            {
                if( c == '}' ) ++bClose;
                if( c == '{' ) ++bOpen;
            }
            
            if( bClose != bOpen )
            {
                m_logger.Log( String.Format( "{0}: CodeBeautifier encounter error while parsing line ( bracket not equal ).", m_appOptions.name ) );
                return false;
            }

            String strNewText = getLeadingWhiteSpace(strOrgText) + makeText( strOrgText, textSelection.Parent.Parent.FullName );

            if( String.IsNullOrEmpty( strOrgText ) || String.IsNullOrEmpty( strNewText ) )
            {
                m_logger.Log( String.Format( "{0}: CodeBeautifier encounter error while parsing document.", m_appOptions.name ) );
                return false;
            }

            if( strNewText.Equals( strOrgText ) )
            {
                m_logger.Log( String.Format( "{0}: No changes after document format.", m_appOptions.name ), OptionsGeneral.LoggerPriority.Medium );
                return true;
            }

            epbegin.ReplaceText( epend, strNewText, (int)( vsEPReplaceTextOptions.vsEPReplaceTextKeepMarkers | vsEPReplaceTextOptions.vsEPReplaceTextNormalizeNewlines ) );

            return false;
        }
    }
}   
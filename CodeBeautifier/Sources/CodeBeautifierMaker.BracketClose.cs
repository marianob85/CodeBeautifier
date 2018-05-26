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
        protected bool makeBracketClose( TextSelection textSelection )
        {
            EditPoint2 epend = textSelection.ActivePoint.CreateEditPoint() as EditPoint2;
            EditPoint2 epbegin = textSelection.ActivePoint.CreateEditPoint() as EditPoint2;

            String bracketOpen;
            int otherbracket = 0;
            do
            {
                EditPoint2 last = epbegin.CreateEditPoint() as EditPoint2;
                epbegin.CharLeft();
                bracketOpen = epbegin.GetText( last );

                if( bracketOpen.CompareTo( "}" ) == 0 )
                {
                    ++otherbracket;
                }
                if( bracketOpen.CompareTo( "{" ) == 0 )
                {
                    --otherbracket;
                }

                if( epbegin.AtStartOfDocument
                    && ( bracketOpen.CompareTo( "{" ) != 0 || otherbracket != 0 ) )
                {
                    return false;
                }

            } while( bracketOpen.CompareTo( "{" ) != 0 || otherbracket != 0 );

            epbegin.StartOfLine();

            String strOrgText = epbegin.GetText( epend );
            String strNewText = makeText( strOrgText, textSelection.Parent.Parent.FullName );

            if( String.IsNullOrEmpty( strOrgText ) || String.IsNullOrEmpty( strNewText ) )
            {
                m_logger.Log( String.Format( "{0}: CodeBeautifier encounter error while parsing document.", m_appOptions.name ) );
                return false;
            }

            // Insert leading white space to each line
            String strNewIdentText = String.Empty;
            String leadingWhiteSpace = getLeadingWhiteSpace( strOrgText );
            using( StringReader reader = new StringReader( strNewText ) )
            {
                string line = reader.ReadLine();
                do
                {
                    strNewIdentText += leadingWhiteSpace + line;

                    line = reader.ReadLine();
                    if( line != null )
                    {
                        strNewIdentText += Environment.NewLine;
                    }
                } while( line != null );
            }

            if( strNewIdentText.Equals( strOrgText ) )
            {
                m_logger.Log( String.Format( "{0}: No changes after document format.", m_appOptions.name ), OptionsGeneral.LoggerPriority.Medium );
                return true;
            }

            epbegin.ReplaceText( epend, strNewIdentText, (int)( vsEPReplaceTextOptions.vsEPReplaceTextKeepMarkers | vsEPReplaceTextOptions.vsEPReplaceTextNormalizeNewlines ) );

            return true;
        }
    }
}
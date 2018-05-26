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
        protected bool make( DocumentsMakerSingle.KeyPressedData keyPressedData, FormatOptionForLanguageC formatOptions )
        {
            if( formatOptions.whenTypeSemicolon
                && keyPressedData.key.CompareTo(";") == 0 )
            {
               return makeSemicolon( keyPressedData.textSelection );
            }
            if( formatOptions.whenTypeBracketClose
                && keyPressedData.key.CompareTo( "}" ) == 0 )
            {
                return makeBracketClose( keyPressedData.textSelection );
            }
            return true;
        }

        protected bool make( DocumentsMakerSingle.KeyPressedData keyPressedData, FormatOptionForLanguageCSharp formatOptions )
        {
            if( formatOptions.whenTypeSemicolon
                && keyPressedData.key.CompareTo( ";" ) == 0 )
            {
                return makeSemicolon( keyPressedData.textSelection );
            }
            if( formatOptions.whenTypeBracketClose
                && keyPressedData.key.CompareTo( "}" ) == 0 )
            {
                return makeBracketClose( keyPressedData.textSelection );
            }
            return true;
        }
    }
}
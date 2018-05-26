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
        private String getLeadingWhiteSpace( String text )
        {
            String leadingWhiteSpaceToAdd = String.Empty;
            foreach( char c in text )
            {
                if( char.IsWhiteSpace( c ) )
                {
                    leadingWhiteSpaceToAdd += c;
                }
                else break;
            }

            return leadingWhiteSpaceToAdd;
        }
    }
}
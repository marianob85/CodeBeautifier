using System.Text;

namespace Manobit.CodeBeautifier.Sources
{
    public class EOLConverter
    {
        public enum EOLType
        {
            Windows,
            Unix,
            Mac
        }

        public static System.String convert( System.String text, EOLType type )
        {
            if( string.IsNullOrEmpty( text ) )
                return "";

            var anyEol = eolChars( EOLType.Windows );
            var newEol = eolChars( type );
            StringBuilder builder = new StringBuilder();
            for( int searchIdx = 0; searchIdx < text.Length; )
            {
                var i = text.IndexOfAny( anyEol, searchIdx );
                // last line without eol
                if( i == -1 )
                {
                    builder.Append( text.Substring( searchIdx ) );
                    searchIdx = text.Length;
                }
                else
                {
                    var shift = i - searchIdx;
                    builder.Append( text.Substring( searchIdx, shift ) );
                    var ommit = skip( text, i);
                    if( ommit > 0 )
                    {
                        builder.Append( newEol );
                        searchIdx += ommit;
                    }
                    searchIdx += shift;
                }
            }

            return builder.ToString();
        }

        private static int skip( string text, int i )
        {
            if( text.Length - i == 1 )
            {
                return 0;
            }

            if( text[ i ] == '\r' )
            {
                if( text.Length - i > 1 && text[ i + 1 ] == '\n' )
                {
                    return 2;
                }
                return 1;
            }
            else if( text[ i ] == '\n' )
            {
                return 1;
            }
            return 0;
        }

        public static char[] eolChars( EOLType type )
        {
            switch( type )
            {
                case EOLType.Windows:
                    return new char[] { '\r', '\n' };
                case EOLType.Unix:
                    return new char[] { '\n' };
                case EOLType.Mac:
                    return new char[] { '\r' };
            }
            return new char[] { };
        }
    }
}

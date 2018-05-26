using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.ComponentModel;

namespace Settings.Sources
{
    [TypeConverter( typeof( FormatOptionForLanguageCSharpConverter ) )]
    public class FormatOptionForLanguageCSharp
    {
        private bool m_autoFormatwhenTypeEnter = false;
        private bool m_autoFormatWhenTypeSemicolon = false;
        private bool m_autoFormatWhenTypeBracketClose = false;

        public bool isValid( String lang, String key )
        {
            if( lang.CompareTo( "CSharp" ) != 0 )
            {
                return false;
            }

            if( m_autoFormatwhenTypeEnter
                && Environment.NewLine.Contains( key ) )
            {
                return true;
            }
            if( m_autoFormatWhenTypeSemicolon
                && key.CompareTo( ";" ) == 0 )
            {
                return true;
            }
            if( m_autoFormatWhenTypeBracketClose
                && key.CompareTo( "}" ) == 0 )
            {
                return true;
            }
            
            return false;
        }

        public FormatOptionForLanguageCSharp()
        { }

        public FormatOptionForLanguageCSharp( FormatOptionForLanguageCSharp opt )
        {
            m_autoFormatwhenTypeEnter = opt.m_autoFormatwhenTypeEnter;
            m_autoFormatWhenTypeSemicolon = opt.m_autoFormatWhenTypeSemicolon;
            m_autoFormatWhenTypeBracketClose = opt.m_autoFormatWhenTypeBracketClose;
        }

        public class FormatOptionForLanguageCSharpConverter : ExpandableObjectConverter
        {

            public override object ConvertTo( ITypeDescriptorContext context,
                                   System.Globalization.CultureInfo culture,
                                   object value, Type destType )
            {
                if( destType == typeof( string ) && value is FormatOptionForLanguageCSharp )
                {
                    return "...";
                }
                return base.ConvertTo( context, culture, value, destType );

            }
        }

        [DescriptionAttribute( "Automatically format when enter is pressed" ),
        ReadOnlyAttribute( false ),
        BrowsableAttribute(false),
        DisplayName( "Format on enter" )]
        public bool whenTypeEnter
        {
            get { return m_autoFormatwhenTypeEnter; }
            set { m_autoFormatwhenTypeEnter = value; }
        }
        [DescriptionAttribute( "Automatically format when ';' is pressed" ),
        ReadOnlyAttribute( false ),
        DisplayName( "Format on semicolon" )]
        public bool whenTypeSemicolon
        {
            get { return m_autoFormatWhenTypeSemicolon; }
            set { m_autoFormatWhenTypeSemicolon = value; }
        }
        [DescriptionAttribute( "Automatically format when '}' is pressed" ),
        ReadOnlyAttribute( false ),
        DisplayName( "Format on bracket close" )]
        public bool whenTypeBracketClose
        {
            get { return m_autoFormatWhenTypeBracketClose; }
            set { m_autoFormatWhenTypeBracketClose = value; }
        }

        public override int GetHashCode()
        {
            return m_autoFormatwhenTypeEnter.GetHashCode()
                    * m_autoFormatWhenTypeSemicolon.GetHashCode() ^ 2
                    * m_autoFormatWhenTypeBracketClose.GetHashCode() ^3;
        }

        public override bool Equals( Object other )
        {
            if( ReferenceEquals( null, other ) ) return false;
            if( ReferenceEquals( this, other ) ) return true;
            if( this.GetType() != other.GetType() )
                return false;

            return Equals( (FormatOptionForLanguageCSharp)other );
        }

        private bool Equals( FormatOptionForLanguageCSharp options )
        {
            return m_autoFormatwhenTypeEnter.Equals( options.m_autoFormatwhenTypeEnter )
                && m_autoFormatWhenTypeSemicolon.Equals( options.m_autoFormatWhenTypeSemicolon )
                && m_autoFormatWhenTypeBracketClose.Equals( options.m_autoFormatWhenTypeBracketClose );
        }
    }
}
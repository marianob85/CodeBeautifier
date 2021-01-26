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

    [XmlRootAttribute( "CodeBeautifulOptions", Namespace = "http://www.manobit.com" )]
    public class Options
    {
        private List<AppOptions> m_applications = new List<AppOptions>();
        private OptionsGeneral m_optionsGeneral = new OptionsGeneral();

        public Options()
        { }

        public List<AppOptions> AppList
        {
            get { return m_applications; }
            set
            {
                m_applications = value;
            }
        }

        public OptionsGeneral general
        {
            get { return m_optionsGeneral; }
            set
            {
                m_optionsGeneral = value;
            }
        }

        internal void save()
        {
            MemoryStream stream = serialize();

            RegistryKey reg_key = Registry.CurrentUser.OpenSubKey( "Software", true );
            RegistryKey sub_key = reg_key.CreateSubKey( "Manobit" );
            sub_key.SetValue( "Options", stream.ToArray() );
        }

        static internal Options read()
        {
            RegistryKey reg_key = Registry.CurrentUser.OpenSubKey( "Software", true );
            RegistryKey sub_key = reg_key.CreateSubKey( "Manobit" );

            byte[] settings = (byte[])sub_key.GetValue( "Options" );
            if (settings == null)
                return new Options();
            return deserialize( new MemoryStream( settings ) );

        }

        private MemoryStream serialize()
        {
            XmlSerializer serializer = new XmlSerializer( this.GetType() );
            MemoryStream stream = new MemoryStream();
            serializer.Serialize( stream, this );
            return stream;
        }

        static private Options deserialize( MemoryStream stream )
        {
            XmlSerializer deserializer = new XmlSerializer( typeof( Options ) );
            return (Options)deserializer.Deserialize( stream );
        }

        internal MemoryStream export()
        {
            return serialize();
        }

        static internal Options import( MemoryStream stream )
        {
            return deserialize( stream );
        }

        public override int GetHashCode()
        {
            return m_applications.GetHashCode() ^ 2
                    * m_optionsGeneral.GetHashCode() ^ 4;
        }

        public override bool Equals( Object other )
        {
            if( ReferenceEquals( null, other ) ) return false;
            if( ReferenceEquals( this, other ) ) return true;
            if( this.GetType() != other.GetType() )
                return false;

            return Equals( (Options)other );
        }

        private bool Equals( Options options )
        {
            return m_applications.TrueForAll( options.m_applications.Contains ) && options.m_applications.TrueForAll( m_applications.Contains )
                && m_optionsGeneral.Equals( options.m_optionsGeneral );
        }
    }
}
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

    public class ApplicationModeStdInput
    {
        private String m_executable = String.Empty;
        private String m_parameters = String.Empty;
        private bool m_useEncoding = false;
        private string m_encoding = String.Empty;

        public ApplicationModeStdInput()
        { }

        public ApplicationModeStdInput( ApplicationModeStdInput options )
        {
            m_executable = options.m_executable;
            m_parameters = options.m_parameters;
            m_useEncoding = options.m_useEncoding;
            m_encoding = options.m_encoding;
        }

        [XmlIgnore]
        static public Encoding[] encodingCollection
        {
            get
            {
                return new Encoding[] 
                {
                    Encoding.Unicode,
                    Encoding.UTF8,
                    Encoding.UTF7,
                    Encoding.UTF32,
                    Encoding.ASCII
                    };
            }
        }

        [XmlIgnore]
        static internal String[] variableList
        {
            get
            {
                return new String[] { "$(FileName)", "$(ConfigFile)" };
            }
        }

        public String executable
        {
            get { return m_executable; }
            set
            {
                m_executable = value;
            }
        }

        public String parameters
        {
            get { return m_parameters; }
            set
            {
                m_parameters = value;
            }
        }

        public bool useEncoding
        {
            get { return m_useEncoding; }
            set
            {
                m_useEncoding = value;
            }
        }

        [XmlIgnore]
        public String parametersWithEnv
        {
            get
            {
                if( String.IsNullOrEmpty( m_parameters ) )
                {
                    return m_parameters;
                }

                try
                {
                    return Environment.ExpandEnvironmentVariables( parameters );
                }
                catch( Exception )
                {
                    return parameters;
                }
            }
        }

        [XmlIgnore]
        public String executablePath
        {
            get
            {
                if( String.IsNullOrEmpty( executable ) )
                {
                    return null;
                }

                String hCodeBeautifierFullPath;
                try
                {
                    hCodeBeautifierFullPath = Environment.ExpandEnvironmentVariables( executable );
                }
                catch( Exception )
                {
                    hCodeBeautifierFullPath = executable;
                }

                if( !System.IO.Path.IsPathRooted( hCodeBeautifierFullPath ) )
                {
                    hCodeBeautifierFullPath = Path.Combine( Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly().Location ), hCodeBeautifierFullPath );
                }
                return hCodeBeautifierFullPath;
            }
        }

        public override int GetHashCode()
        {
            return m_executable.GetHashCode() ^ 2
                    * m_parameters.GetHashCode() ^ 3;
        }

        public override bool Equals( Object other )
        {
            if( ReferenceEquals( null, other ) ) return false;
            if( ReferenceEquals( this, other ) ) return true;
            if( this.GetType() != other.GetType() )
                return false;

            return Equals( (ApplicationModeStdInput)other );
        }

        private bool Equals( ApplicationModeStdInput options )
        {
            return m_executable.Equals( options.m_executable )
                && m_parameters.Equals( options.m_parameters );
        }
    }
}

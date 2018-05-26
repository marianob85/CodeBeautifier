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
    public class ApplicationModeFile
    {
        private String m_executable = String.Empty;
        private String m_parameters = String.Empty;

        public ApplicationModeFile()
        { }

        public ApplicationModeFile( ApplicationModeFile options )
        {
            m_executable = options.m_executable;
            m_parameters = options.m_parameters;
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

            return Equals( (ApplicationModeFile)other );
        }

        private bool Equals( ApplicationModeFile options )
        {
            return m_executable.Equals( options.m_executable )
                && m_parameters.Equals( options.m_parameters );
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Settings.Sources
{
    public class SettingsOptionsConfigFile
    {
        private String m_configFile = String.Empty;
        private SearchType m_searchType = SearchType.ClosestParentDirectory;


        [XmlIgnore]
        static internal Dictionary<SearchType, String> searchTypeDescription
        {
            get
            {
                return new Dictionary<SearchType, String> { { SearchType.ClosestParentDirectory, "Search for config file located in the closest parent directory of the input file ( like in clang-format build in funcion" }, };
            }
        }

        public enum SearchType
        {
            [XmlEnum( Name = "ClosestParentDirectory" )]
            ClosestParentDirectory,
        };

        public String fileName
        {
            get { return m_configFile; }
            set
            {
                m_configFile = value;
            }
        }

        public SearchType searchType
        {
            get { return m_searchType; }
            set
            {
                m_searchType = value;
            }
        }

        public override int GetHashCode()
        {
            return m_configFile.GetHashCode()
                    * m_searchType.GetHashCode() ^ 2;
        }

        public override bool Equals( Object other )
        {
            if( ReferenceEquals( null, other ) ) return false;
            if( ReferenceEquals( this, other ) ) return true;
            if( this.GetType() != other.GetType() )
                return false;

            return Equals( (SettingsOptionsConfigFile)other );
        }

        private bool Equals( SettingsOptionsConfigFile options )
        {
            return m_configFile.Equals( options.m_configFile )
                    && m_searchType.Equals( options.m_searchType );
        }

    }
}
